using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Oracle.DataAccess.Client;
using System.ComponentModel;
using Salary;
using Salary.Helpers;
using LibrarySalary.Helpers;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        
        public MessageControl()
        {
            InitializeComponent();
        }
    }
    public class MessageModel: NotificationObject
    {
        public MessageModel()
        {
            try
            {
                _ds = new DataSet();
                _ds.Tables.Add("MESSAGE");
                _daMessage = new OracleDataAdapter(string.Format(Queries.GetQuery("SelectMessage.sql"), Connect.SchemaApstaff), Connect.CurConnect);
                _daMessage.SelectCommand.BindByName = true;
                _daMessage.SelectCommand.Parameters.Add("p_APP_NAME", OracleDbType.Varchar2);
                _daMessage.SelectCommand.Parameters.Add("p_MESSAGE_ID", OracleDbType.Decimal);
                _daMessage.SelectCommand.Parameters.Add("p_begin_date", OracleDbType.Date);
                _daMessage.SelectCommand.Parameters.Add("p_end_date", OracleDbType.Date);
                // Read data from a simple setting            
            }
            catch (Exception ex)
            { }
        }

        private bool isLastLoaded = false;
        private DateTime _selectedDateBegin = DateTime.Today;
        public DateTime SelectedDateBegin
        {
            get { return this._selectedDateBegin; }
            set
            {
                if (value != this._selectedDateBegin)
                {
                    this._selectedDateBegin = value;
                    RaisePropertyChanged(() => SelectedDateBegin);
                    LoadMessage(_selectedDateBegin, _selectedDateEnd);
                    RaisePropertyChanged(() => Messages);
                }
            }
        }
        
        private DateTime _selectedDateEnd = DateTime.Today;
        public DateTime SelectedDateEnd
        {
            get { return this._selectedDateEnd; }
            set
            {
                if (value != this._selectedDateEnd)
                {
                    this._selectedDateEnd = value;
                    RaisePropertyChanged(() => SelectedDateEnd);
                    LoadMessage(_selectedDateBegin, _selectedDateEnd);
                    RaisePropertyChanged(() => Messages);
                }
            }
        }
        
        private string _app_name = "";
        public string AppName
        {
            get {return _app_name;}
            set 
            { 
                _app_name = value;
                RaisePropertyChanged(() => AppName);
            }
        }
        private DataSet _ds;
        private OracleDataAdapter _daMessage;

        Decimal? _lastMessageID = null;
        public decimal? LastMessageID
        {
            get
            {
                return _lastMessageID;
            }
            set
            {
                _lastMessageID = value;
            }
        }

        public DataView Messages
        {
            get
            {
                if (_ds != null && _ds.Tables.Contains("MESSAGE"))
                    return _ds.Tables["MESSAGE"].DefaultView;
                else return null;
            }
        }


        private void LoadMessage(DateTime? begin_date, DateTime? end_date)
        {
            var localSettings = Salary.Properties.Settings.Default;
            if (!isLastLoaded)
                LastMessageID = (decimal)localSettings[AppName + "Message_ID"];
            _ds.Tables["MESSAGE"].Rows.Clear();
            _daMessage.SelectCommand.Parameters["p_APP_NAME"].Value = AppName;
            _daMessage.SelectCommand.Parameters["p_MESSAGE_ID"].Value = LastMessageID;
            _daMessage.SelectCommand.Parameters["p_begin_date"].Value = begin_date;
            _daMessage.SelectCommand.Parameters["p_end_date"].Value = end_date;
            _daMessage.Fill(_ds.Tables["MESSAGE"]);
            object last_value = _ds.Tables["MESSAGE"].Compute("MAX(MESSAGE_ID)", "");
            if (last_value!= null && last_value!=DBNull.Value && Convert.ToDecimal(last_value)>LastMessageID)
            {
                localSettings[AppName + "Message_ID"] = last_value;
                localSettings.Save();
                LastMessageID = Convert.ToDecimal(last_value);
                _isHidded = false;// значит есть новые сообщения - при первом запуске у нас покажется список
            }
            isLastLoaded = true;
        }

        bool _isHidded = true;
        public bool IsHidden
        {
            get
            {
                if (!_lastMessageID.HasValue)
                    LoadMessage(_selectedDateBegin, _selectedDateEnd);
                return _isHidded;
            }
        }
    }
}
