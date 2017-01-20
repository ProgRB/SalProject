using EntityGenerator;
using LibrarySalary.Helpers;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace Salary.ViewModel.Salary
{
    public class TaxEmpDocumViewModel : NotificationObject
    {
        DataSet ds;
        OracleDataAdapter odaTaxCompany, odaTaxDocums;
        private TaxCompany _currentCompany;
        private string _errorMessage;
        private DateTime _selectedDate = DateTime.Today.Trunc("Month").AddDays(-1);
        private DataRowView _currentDocument;
        private string _perNumFilter = string.Empty;
        private string _percent;

        public TaxEmpDocumViewModel()
        {
            ds = new DataSet();
            odaTaxCompany = new OracleDataAdapter("Select * from salary.tax_company", Connect.CurConnect);
            odaTaxCompany.TableMappings.Add("Table", "TAX_COMPANY");

            odaTaxDocums = new OracleDataAdapter(Queries.GetQueryWithSchema(@"Taxes\SelectEmpTaxDocums.sql"), Connect.CurConnect);
            odaTaxDocums.SelectCommand.BindByName = true;
            odaTaxDocums.SelectCommand.Parameters.Add("p_date", OracleDbType.Date, null, ParameterDirection.Input);
            odaTaxDocums.SelectCommand.Parameters.Add("p_tax_company_id", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaTaxDocums.SelectCommand.Parameters.Add("p_percent", OracleDbType.Decimal, null, ParameterDirection.Input);
            odaTaxDocums.SelectCommand.Parameters.Add("c", OracleDbType.RefCursor, ParameterDirection.Output);
            odaTaxDocums.TableMappings.Add("Table", "TAX_EMP_DOCUM");

            RefreshTaxCompanies();
        }

        /// <summary>
        /// Выбранная отчетная дата месяц год
        /// </summary>
        [OracleParameterMapping(ParameterName="p_date")]
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
                UpdateTaxDocums();
            }
        }

        /// <summary>
        /// Фильтр по табельному номеру
        /// </summary>
        public string PerNumFilter
        {
            get
            {
                return _perNumFilter;
            }
            set
            {
                _perNumFilter = value;
                RaisePropertyChanged(() => PerNumFilter);
                RaisePropertyChanged(() => EmpTaxDocumSource);
            }
        }

        [OracleParameterMapping(ParameterName = "p_percent")]
        public string PercentFilter
        {
            get
            {
                return _percent;
            }
            set
            {
                _percent = value;
                RaisePropertyChanged(() => PercentFilter);
            }
        }


        #region участок источники данных для списков

        /// <summary>
        /// Список организаций
        /// </summary>
        public List<TaxCompany> TaxCompanySource
        {
            get
            {
                if (ds != null && ds.Tables.Contains("TAX_COMPANY"))
                {
                    return ds.Tables["TAX_COMPANY"].ConvertToEntityList<TaxCompany>();
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Обновление списка организаций
        /// </summary>
        public void RefreshTaxCompanies()
        {
            Exception ex = odaTaxCompany.TryFillWithClear(ds, this);
            if (ex != null)
                MessageBox.Show(ex.GetFormattedException(), "Ошибка обновления списка организаций");
            RaisePropertyChanged(() => TaxCompanySource);
        }

        /// <summary>
        /// Источник данных список документов
        /// </summary>
        public List<DataRowView> EmpTaxDocumSource
        {
            get
            {
                if (ds != null && ds.Tables.Contains("TAX_EMP_DOCUM"))
                {
                    return ds.Tables["TAX_EMP_DOCUM"].DefaultView.OfType<DataRowView>().Where(r=>r["PER_NUM"].ToString().Contains(PerNumFilter)).ToList();
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Обновление списка документов налогов
        /// </summary>
        public void UpdateTaxDocums()
        {
            Exception ex = odaTaxDocums.TryFillWithClear(ds, this);
            if (ex != null)
                ErrorMessage = ex.GetFormattedException();
            else
                ErrorMessage = string.Empty;
            RaisePropertyChanged(() => EmpTaxDocumSource);
        }

        #endregion

        

        /// <summary>
        /// Текущая выбранная организация 
        /// </summary>
        public TaxCompany CurrentCompany
        {
            get
            {
                return _currentCompany;
            }

            set
            {
                if (_currentCompany != value)
                {
                    _currentCompany = value;
                    RaisePropertyChanged(() => CurrentCompany);
                    UpdateTaxDocums();
                }
            }
        }

        /// <summary>
        /// Текущая выбранная организация айдишник
        /// </summary>
        [OracleParameterMapping(ParameterName="p_tax_company_id")]
        public decimal? CurrentTaxCompanyID
        {
            get
            {
                return _currentCompany != null ? _currentCompany.TaxCompanyID : null;
            }
        }

        

        /// <summary>
        /// Текст ошибки какой-нибудь
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
            }
        }

        /// <summary>
        /// Текущий выбранный документ
        /// </summary>
        public DataRowView CurrentDocument 
        {
            get
            {
                return _currentDocument;
            }
            set
            {
                _currentDocument = value;
                RaisePropertyChanged(() => CurrentDocument);
            }
        }

        /// <summary>
        /// Айдишник текущего выбанного документа
        /// </summary>
        public decimal? CurrentDocumentID
        {
            get
            {
                return _currentDocument != null ? _currentDocument.Row.Field2<Decimal?>("TAX_EMP_DOCUM_ID") : null;
            }
        }
    }
}
