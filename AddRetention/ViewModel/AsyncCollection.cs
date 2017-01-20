using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;
using Oracle.DataAccess.Client;
using System.Collections;
using System.Reflection;
using System.Windows.Data;

namespace Salary.ViewModel
{
    /// <summary>
    /// Интерфейс содержит загрузку данных
    /// </summary>
    /// <returns></returns>
    public interface IItemsProvider<T>
    {
        /// <summary>
        /// Процедура интерфейса загрузки данных
        /// </summary>
        /// <returns>Возвращает список данных</returns>
        IList<T> FetchData();
        /// <summary>
        /// Сама процедура омены загрузки
        /// </summary>
        void CancelFetch();
        SynchronizationContext GetSyncContext();
    }

    /// <summary>
    /// Интерфейс обертка для получения данных
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Интерфейс содержит загрузку данных
        /// </summary>
        /// <returns></returns>
        void FetchData(DataTable t);
        void CancelFetch();
        SynchronizationContext GetSyncContext();
    }

    public class AsyncCollectionList<T>: IList<T>, IList, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private List<T> list = new List<T>();
        string primaryKey;
        bool _isLoading = false;
        IItemsProvider<T> _itemsProvider;
        /// <summary>
        /// Класс загрузки используется для загрузки только одной таблицы асинхронно
        /// </summary>
        public AsyncCollectionList(IItemsProvider<T> _provider, string PrimaryKeyField = "")
        {
            _itemsProvider = _provider;
            primaryKey = PrimaryKeyField;
            IsAsync = true;
        }

        /// <summary>
        /// Класс загрузки используется для загрузки только одной таблицы асинхронно
        /// </summary>
        public AsyncCollectionList(IItemsProvider<T> _provider, bool is_async, string PrimaryKeyField = "")
        {
            _itemsProvider = _provider;
            primaryKey = PrimaryKeyField;
            IsAsync = is_async;
        }

        public string PrimaryKey
        {
            get
            {
                return primaryKey;
            }
        }

        PropertyDescriptor _pinfo;
        public PropertyDescriptor PrimaryProperty
        {
            get
            {
                return _pinfo;
            }
            private
            set
            {
                _pinfo = value;
            }            
        }
        object _temp_index = null;
        /// <summary>
        /// Асинхронная загрузка данных для листа
        /// </summary>
        public void LoadDataAsync()
        {
            if (this.SelectedItem == null)
                _temp_index = null;
            else
            {
                if (string.IsNullOrEmpty(primaryKey))
                    _temp_index = this.IndexOf(SelectedItem);
                else
                {
                    PrimaryProperty =  TypeDescriptor.GetProperties(_selectedItem)[primaryKey];
                    if (PrimaryProperty != null)
                    {
                        _temp_index = PrimaryProperty.GetValue(SelectedItem);
                    }
                    else
                        throw new Exception(string.Format("Ошибка AsynCollection. Невозможно получить указанное ключевое поле <{0}>", primaryKey));
                }
            }
            IsLoading = true;
            if (IsAsync)
                ThreadPool.QueueUserWorkItem(LoadDataWork);
            else
                LoadDataWork(null);
        }

        private ICollectionView _collectionView;
        public ICollectionView CollectionView
        {
            get
            {
                if (_collectionView == null)
                    _collectionView = CollectionViewSource.GetDefaultView(this);
                return _collectionView;
            }
        }
                
        private void LoadDataWork(object e)
        {
            _isSuccess = true;
            _itemsProvider.GetSyncContext().Send(RaiseEvent, "IsNotSuccessfully");
            try
            {                
                _itemsProvider.CancelFetch();
                IList<T> l = _itemsProvider.FetchData();
                _itemsProvider.GetSyncContext().Send(LoadCompleted, l);
            }
            catch (Exception ex)
            {
                if (ex is OracleException && (ex as OracleException).Number == 1013)
                    return;
                else
                    _itemsProvider.GetSyncContext().Send(LoadFailed, ex);
            }
        }

        private void LoadFailed(object args)
        {
            LoadException = (Exception)args;
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
            IsNotSuccessfully = true;
            IsLoading = false;
        }

        private void LoadCompleted(object args)
        {
            list = (List<T>)args;
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
            T current_value;
            if (this.Count>0)
            {
                if (this.Count == 1)
                    current_value = this[0];
                else
                    if (_temp_index != null)
                        if (string.IsNullOrEmpty(PrimaryKey))
                            if (this.Count > (int)_temp_index)
                                current_value = this[(int)_temp_index];
                            else
                                current_value = default(T);
                        else
                        {
                            PrimaryProperty = TypeDescriptor.GetProperties(list[0])[primaryKey];
                            current_value = list.FirstOrDefault(t => PrimaryProperty.GetValue(t).Equals(_temp_index));
                        }
                    else
                        current_value = default(T);
            }
            else
                current_value = default(T);
            IsLoading = false;
            IsNotSuccessfully = false;
            SelectedItem = current_value;
            OnLoadFinished();
        }

        Exception ld_ex;

        /// <summary>
        /// Ошибка, возникшая при загрузке данных, либо налл
        /// </summary>
        public Exception LoadException
        {
            get
            {
                return ld_ex;
            }

            set
            {
                ld_ex = value;
                OnPropertyChanged("LoadException");
            }
        }
        bool _isSuccess = false;

        /// <summary>
        /// Возвращает значение равное неуспешности выполнения асинхронной загрузки
        /// </summary>
        public bool IsNotSuccessfully
        {
            get
            {
                return _isSuccess;
            }
            set
            {
                _isSuccess = value;
                OnPropertyChanged("IsNotSuccessfully");
            }
        }

        /// <summary>
        /// событие вызываемое по окончании загрузки
        /// </summary>
        public event EventHandler LoadFinished;

        private void OnLoadFinished()
        {

            if (LoadFinished != null)
                LoadFinished(this, EventArgs.Empty);
        }

        private void OnCollectionChanging(object args)
        {
            OnCollectionChanged((NotifyCollectionChangedAction)args);
        }

        /// <summary>
        /// Возвращает значение, означающее что в данный момент идет загрузка данных
        /// </summary>
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
        
        private T _selectedItem = default(T);
        public T SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
                OnCurrentItemChanged();
            }
        }

        public void RaiseEvent(object name)
        {
            OnPropertyChanged((string)name);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// событие вызваемое в момент изменения коллекции
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public bool IsAsync
        {
            get;
            set;
        }
#region IList interface implementation
        public event EventHandler CurrentItemChanged;

        private void OnCurrentItemChanged()
        {
            if (CurrentItemChanged != null)
                CurrentItemChanged(this, EventArgs.Empty);
        }

        private void OnCollectionChanged()
        {
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }
        private void OnCollectionChanged(NotifyCollectionChangedAction t)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(t));
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list==null? 0: list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int Add(object value)
        {
            list.Add((T)value);
            return list.Count-1;
        }

        public bool Contains(object value)
        {
            return list.Contains((T)value);
        }

        public int IndexOf(object value)
        {
            return list.IndexOf((T)value);
        }

        public void Insert(int index, object value)
        {
            list.Insert(index, (T)value);
        }

        public bool IsFixedSize
        {
            get { return true; }
        }

        public void Remove(object value)
        {
            list.Remove((T)value);
        }

        object IList.this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = (T)value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            int j = index;
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(list[i], j);
                j++;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

#endregion
        public object SyncRoot
        {
            get { return this; }
        }
    }
   
}
