using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using Salary.View;
using System.Windows;
using System.Data;

namespace Salary.Helpers
{
    public class AbortableBackgroundWorker : BackgroundWorker
    {

        private Thread workerThread;

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                e.Cancel = true; //We must set Cancel property to true!
                Thread.ResetAbort(); //Prevents ThreadAbortException propagation
            }
            catch (OracleException ex)
            {
                if (ex.Number == 1013)
                {
                    e.Cancel = true; //We must set Cancel property to true!
                }
                else
                    throw ex;
            }
        }


        public void Abort()
        {
            if (ExecutingCommand != null)
                ExecutingCommand.Cancel();
            else
                if (workerThread != null)
                {
                    workerThread.Abort();
                    workerThread = null;
                }
        }

        public OracleCommand ExecutingCommand
        {
            get;
            set;
        }

        private WaitWindow waitDialog
        {
            get;
            set;
        }

        public static void RunAsyncWithWaitDialog(DependencyObject sender, string caption, DoWorkEventHandler dowork,
            object argument, OracleCommand executingCommand, RunWorkerCompletedEventHandler workComplete)
        {
            AbortableBackgroundWorker bw = new AbortableBackgroundWorker();
            bw.DoWork += dowork;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerCompleted += workComplete;
            WaitWindow f = new WaitWindow(caption, bw);
            f.Owner = Window.GetWindow(sender);
            bw.waitDialog = f;
            f.Show();
            bw.ExecutingCommand = executingCommand;
            bw.RunWorkerAsync(argument);
        }

        static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            (sender as AbortableBackgroundWorker).waitDialog.Close();
        }
        /// <summary>
        /// Асинхронно выполняет загрузку данных и при ошибках показывает сообщение, иначе если все успешно прошло, выполняет делегат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="caption"></param>
        /// <param name="argument"></param>
        /// <param name="execCommand"></param>
        /// <param name="workSuccessEnded"></param>
        public static void RunAsyncWithWaitDialog(DependencyObject sender, string caption, object argument, OracleCommand execCommand, RunWorkerCompletedEventHandler workSuccessEnded)
        { 
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, caption, 
                (p, pw)=>
                    {
                        OracleDataAdapter a = pw.Argument as OracleDataAdapter;
                        DataSet ds = new DataSet();
                        a.Fill(ds);
                        pw.Result = ds;
                    }, argument, execCommand, 
                (p, pw)=>
                    {
                        if (pw.Cancelled) return;
                        else if (pw.Error!=null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                        else 
                            workSuccessEnded(p, pw);
                    });
        }


        /// <summary>
        /// Асинхронно выполняет команду и при ошибках показывает сообщение, иначе если все успешно прошло, выполняет делегат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="caption"></param>
        /// <param name="argument"></param>
        /// <param name="execCommand"></param>
        /// <param name="workSuccessEnded"></param>
        public static void RunAsyncWithWaitDialog(DependencyObject sender, string caption, OracleCommand execCommand, RunWorkerCompletedEventHandler workSuccessEnded)
        {
            AbortableBackgroundWorker.RunAsyncWithWaitDialog(sender, caption,
                (p, pw) =>
                {
                    execCommand.ExecuteNonQuery();
                }, null, execCommand,
                (p, pw) =>
                {
                    if (pw.Cancelled) return;
                    else if (pw.Error != null) MessageBox.Show(pw.Error.GetFormattedException(), "Ошибка получения данных");
                    else
                        workSuccessEnded(p, pw);
                });
        }
    }
}