using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace Salary.ViewModel
{
    public class TaskNotify<TResult>: INotifyPropertyChanged
    {
        bool isBusy = false;
        public Task<TResult> Task
        {
            get;
            private set;
        }
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        public TaskNotify(Task<TResult> task)
        {
            Task = task;
            if (!Task.IsCompleted)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerSupportsCancellation=true;
                bw.DoWork+=new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.RunWorkerAsync(Task);
            }

        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnPropertyChanged("Result");
        }

        public void ReRunTask(Task<TResult> task)
        {
            //if (Task.Status== TaskStatus.Running)
            Task = task;
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync(Task);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            WatchTaskAsync(e.Argument as Task<TResult>);
        }

        private void WatchTaskAsync(Task<TResult> task)
        {
            IsBusy = true;
            try
            {
                if (task.Status == TaskStatus.Created || task.Status== TaskStatus.WaitingToRun)
                    task.Start();
                task.Wait();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            OnPropertyChanged("IsCompleted");
            if (task.IsCanceled)
                OnPropertyChanged("IsCancelled");
            else
                if (task.IsFaulted)
                {
                    OnPropertyChanged("IsFailed");
                    OnPropertyChanged("TaskException");
                }
                else
                {
                    OnPropertyChanged("IsSuccessfullyCompleted");
                    OnPropertyChanged("Result");
                }
            IsBusy = false;
        }
        public bool IsCompleted
        {
            get
            {
                return Task.IsCompleted;
            }
        }
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        public bool IsSuccessfullyCompleted
        {
            get
            {
                return Task.Status == TaskStatus.RanToCompletion;
            }
        }

        public bool IsCancelled
        {
            get
            {
                return Task.IsCanceled;
            }
        }

        public bool IsFailed
        {
            get
            {
                return Task.IsFaulted;
            }
        }

        public Exception TaskException
        {
            get
            {
                return Task.Exception;
            }
        }
        public TResult Result
        {
            get
            {
                return (Task.Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
