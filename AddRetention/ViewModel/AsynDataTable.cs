using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Threading;

namespace Salary.ViewModel
{
    public class AsyncDataTable : DataTable, INotifyPropertyChanged
    {
        bool _isLoading = false;
        IDataProvider _itemsProvider;
        /// <summary>
        /// Класс загрузки используется для загрузки только одной таблицы асинхронно
        /// </summary>
        /// <param name="_odaLoader"> Адаптер содержаний команду выбора с параметрами</param>
        public AsyncDataTable(IDataProvider _provider):base()
        {
            _itemsProvider = _provider;
        }
        public void LoadDataAsync()
        {
            IsLoading = true;
            ThreadPool.QueueUserWorkItem(LoadDataWork);
        }

        private void LoadDataWork(object e)
        {
            this.BeginLoadData();
            _itemsProvider.FetchData(this);
            _itemsProvider.GetSyncContext().Send(LoadCompleted, null);
            
        }

        private void LoadCompleted(object args)
        {
            this.EndLoadData();
            IsLoading = false;
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
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
