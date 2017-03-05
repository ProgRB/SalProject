using Oracle.DataAccess.Client;
using Salary;
using Salary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySalary.Helpers
{
    public class ConnectionQueue: List<AbortableBackgroundWorker>
    {
        private OracleConnection _connection;
        private bool _isBusy = false;

        public ConnectionQueue(): base()
        {
        }

        public OracleConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        public OracleConnection NewConnection()
        {
            return Salary.Connect.CreateNewUserConnection();
        }

        public void Enqueue(AbortableBackgroundWorker item)
        {
            item.RunWorkerCompleted += Item_RunWorkerCompleted;
            item.TempStatus = "Ожидание в очереди операций...";
            base.Add(item);
            
            if (!IsBusy)
            {
                CheckConnection();
                RunAsyncNext();
            }
        }

        private void CheckConnection()
        {
            if (Connection == null || !Connection.Ping())
            {
                Connection = NewConnection();
            }
        }

        private void Item_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            this.Dequeue();
            RunAsyncNext();
        }

        public AbortableBackgroundWorker Dequeue()
        {
            if (this.Count > 0)
            {
                var item = this[0];
                this.Remove(this[0]);
                return item;
            }
            else
                return null;
        }

        /// <summary>
        /// Запускает следующую операцию в очереди
        /// </summary>
        public void RunAsyncNext()
        {
            if (this.Count > 0)
            {
                IsBusy = true;
                AbortableBackgroundWorker bw = this.Peek();
                if (bw.ExecutingCommand!=null)
                    bw.ExecutingCommand.Connection = Connection;
                bw.TempStatus = null;
                bw.RunWorkerAsync(bw.Argument);
            }
        }

        public AbortableBackgroundWorker Peek()
        {
            return this.Count > 0 ? this[0] : null; 
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
            }
        }

        protected new void Remove(AbortableBackgroundWorker item)
        {
            base.Remove(item);
            ListChanged?.Invoke(this, EventArgs.Empty);
        }

        protected new void Add(AbortableBackgroundWorker item)
        {
            base.Add(item);
            ListChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ListChanged;
    }
}
