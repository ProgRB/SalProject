using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace EntityGenerator
{
    public class EntityRelationList<T> : SortableBindingList<T> where T : RowEntityBase, new()
    {
        public EntityRelationList()
            : base()
        {
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "EntityState" && e.NewIndex < this.Count && this[e.NewIndex].EntityState == System.Data.DataRowState.Deleted)
                    this.Remove(this[e.NewIndex]);
            }
        }

        public EntityRelationList(IList<T> e)
            : base(e)
        {
            //RelatedEntity = null;
        }

        /// <summary>
        /// Объект, являющийся родителем зависимых связей
        /// </summary>
        public EntityGenerator.RowEntityBase RelatedEntity
        {
            get;
            set;
        }

        /// <summary>
        /// Имя столбца являщийся родительски и связующим
        /// </summary>
        public string RelationColumn
        {
            get;
            set;
        }
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            if (e.NewObject == null)
            {
                try
                {
                    T obj = new T();
                    obj.DataRow = RelatedEntity.DataSet.Tables[obj.SchemaTableName].Rows.Add();
                    e.NewObject = obj;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                
            }
            base.OnAddingNew(e);
        }

        /// <summary>
        /// Перегруженное удаление записи- удаляется не только запись но и связанный объект устанавливает как удаленный
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            T val = this.Items[index];
            base.RemoveItem(index);
            if (val!=null && val.DataRow!=null)
                val.DataRow.Delete();
        }

        protected override void InsertItem(int index, T item)
        {
            if (item!=null && item.DataRow != null && item.DataRow.RowState== System.Data.DataRowState.Detached)
            {
                item.DataTable.Rows.Add(item.DataRow);
            }
            base.InsertItem(index, item);
        }

        /// <summary>
        /// перегрузка очистки списка
        /// </summary>
        protected override void ClearItems()
        {
            for (int i = this.Count - 1; i > -1; --i)
                this.RemoveItem(i);
        }
    }

    /// <summary>
    /// Provides a generic collection that supports data binding and additionally supports sorting.
    /// See http://msdn.microsoft.com/en-us/library/ms993236.aspx
    /// If the elements are IComparable it uses that; otherwise compares the ToString()
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class SortableBindingList<T> : BindingList<T> where T : class
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor _sortProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        public SortableBindingList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        /// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
        public SortableBindingList(IList<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the list supports sorting.
        /// </summary>
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the list is sorted.
        /// </summary>
        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }

        /// <summary>
        /// Gets the direction the list is sorted.
        /// </summary>
        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirection; }
        }

        /// <summary>
        /// Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortProperty; }
        }

        /// <summary>
        /// Removes any sort applied with ApplySortCore if sorting is implemented
        /// </summary>
        protected override void RemoveSortCore()
        {
            _sortDirection = ListSortDirection.Ascending;
            _sortProperty = null;
            _isSorted = false; //thanks Luca
        }

        /// <summary>
        /// Sorts the items if overridden in a derived class
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="direction"></param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            List<T> list = Items as List<T>;
            if (list == null) return;

            list.Sort(Compare);

            _isSorted = true;
            //fire an event that the list has been changed.
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }


        private int Compare(T lhs, T rhs)
        {
            var result = OnComparison(lhs, rhs);
            //invert if descending
            if (_sortDirection == ListSortDirection.Descending)
                result = -result;
            return result;
        }

        private int OnComparison(T lhs, T rhs)
        {
            object lhsValue = lhs == null ? null : _sortProperty.GetValue(lhs);
            object rhsValue = rhs == null ? null : _sortProperty.GetValue(rhs);
            if (lhsValue == null)
            {
                return (rhsValue == null) ? 0 : -1; //nulls are equal
            }
            if (rhsValue == null)
            {
                return 1; //first has value, second doesn't
            }
            if (lhsValue is IComparable)
            {
                return ((IComparable)lhsValue).CompareTo(rhsValue);
            }
            if (lhsValue.Equals(rhsValue))
            {
                return 0; //both are the same
            }
            //not comparable, compare ToString
            return lhsValue.ToString().CompareTo(rhsValue.ToString());
        }
    }
}
