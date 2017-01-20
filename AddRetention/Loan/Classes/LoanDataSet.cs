using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using LibrarySalary.Helpers;

namespace Salary.Loan.Classes
{
    public static class LoanDataSet
    {
        private static DataSet _dsDictionary = new DataSet();

        public static DataSet DsDictionary
        {
            get { return _dsDictionary; }
            set { _dsDictionary = value; }
        }

        public static DataTable Type_Loan
        {
            get { return _dsDictionary.Tables["TYPE_LOAN"]; }
        }

        public static DataTable Purpose_Loan
        {
            get { return _dsDictionary.Tables["PURPOSE_LOAN"]; }
        }

        public static DataTable Refinancing_Rate
        {
            get { return _dsDictionary.Tables["REFINANCING_RATE"]; }
        }

        public static DataTable Loan_Cost_Item
        {
            get { return _dsDictionary.Tables["LOAN_COST_ITEM"]; }
        }

        public static DataTable Item_Fin_Plan
        {
            get { return _dsDictionary.Tables["ITEM_FIN_PLAN"]; }
        }

        static LoanDataSet()
        {
            // TYPE_LOAN
            _dsDictionary.Tables.Add("TYPE_LOAN");
            new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectTypeLoan.sql"), Connect.SchemaSalary), Connect.CurConnect).
                Fill(_dsDictionary.Tables["TYPE_LOAN"]);
            // REFINANCING_RATE
            _dsDictionary.Tables.Add("REFINANCING_RATE");
            new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectRefinancing_Rate.sql"), Connect.SchemaSalary), Connect.CurConnect).
                Fill(_dsDictionary.Tables["REFINANCING_RATE"]);
            // LOAN_COST_ITEM
            _dsDictionary.Tables.Add("LOAN_COST_ITEM");
            new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectLoan_Cost_Item.sql"), Connect.SchemaSalary), Connect.CurConnect).
                Fill(_dsDictionary.Tables["LOAN_COST_ITEM"]);
            // ITEM_FIN_PLAN
            _dsDictionary.Tables.Add("ITEM_FIN_PLAN");
            new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectItem_Fin_Plan.sql"), Connect.SchemaSalary), Connect.CurConnect).
                Fill(_dsDictionary.Tables["ITEM_FIN_PLAN"]);

            _dsDictionary.Relations.Add("LOAN_COST_ITEM_id_fk", _dsDictionary.Tables["LOAN_COST_ITEM"].Columns["LOAN_COST_ITEM_ID"],
                _dsDictionary.Tables["ITEM_FIN_PLAN"].Columns["LOAN_COST_ITEM_ID"], true);
            ForeignKeyConstraint _fk = _dsDictionary.Tables["ITEM_FIN_PLAN"].Constraints["LOAN_COST_ITEM_id_fk"] as ForeignKeyConstraint;
            _fk.AcceptRejectRule = AcceptRejectRule.None;
            _fk.DeleteRule = Rule.Cascade;
            _fk.UpdateRule = Rule.Cascade;
            _dsDictionary.EnforceConstraints = true;

            // PURPOSE_LOAN
            _dsDictionary.Tables.Add("PURPOSE_LOAN");
            new OracleDataAdapter(string.Format(Queries.GetQuery("Loan/SelectPurposeLoan.sql"), Connect.SchemaSalary), Connect.CurConnect).
                Fill(_dsDictionary.Tables["PURPOSE_LOAN"]);
            _dsDictionary.Tables["PURPOSE_LOAN"].Columns.Add("PURPOSE_LOAN_DISP").Expression = "PURPOSE_LOAN_CODE+' ('+PURPOSE_LOAN_NAME+')'";
        }
    }
}
