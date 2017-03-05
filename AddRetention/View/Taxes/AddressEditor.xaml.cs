using EntityGenerator;
using Oracle.DataAccess.Client;
using Salary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace Salary.View.Taxes
{
    /// <summary>
    /// Interaction logic for AddressEditor.xaml
    /// </summary>
    public partial class AddressEditor : UserControl
    {
        private RegistrViewModel _model;

        public AddressEditor()
        {
            _model = new RegistrViewModel();
            InitializeComponent();
            DataContext = Model;
        }

        public RegistrViewModel Model
        {
            get
            {
                return _model;
            }
        }
    }

    /// <summary>
    /// Представление для адреса
    /// </summary>
    public class RegistrViewModel : Registr, IDataErrorInfo
    {
        OracleDataAdapter odaRegion, odaDistrict, odaCity, odaLocality, odaStreet;
        DataSet ds;

        public RegistrViewModel(string perNum = null)
        {
            ds = new DataSet();
            odaRegion = new OracleDataAdapter(@"select * from apstaff.region order by name_region", CurConnect);
            odaRegion.SelectCommand.BindByName = true;
            odaRegion.SelectCommand.Parameters.Add("p_code_region", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaRegion.TableMappings.Add("Table", "REGION");

            odaDistrict = new OracleDataAdapter(@"select * from apstaff.district where substr(code_district,1,2)=:p_code_region order by name_district", CurConnect);
            odaDistrict.SelectCommand.BindByName = true;
            odaDistrict.SelectCommand.Parameters.Add("p_code_region", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaDistrict.TableMappings.Add("Table", "DISTRICT");

            odaCity = new OracleDataAdapter(@"select * from apstaff.city where substr(code_city,1,2)=:p_code_region and (:p_code_district is null or substr(code_city,1,5)=:p_code_district) order by name_city", CurConnect);
            odaCity.SelectCommand.BindByName = true;
            odaCity.SelectCommand.Parameters.Add("p_code_region", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaCity.SelectCommand.Parameters.Add("p_code_district", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaCity.TableMappings.Add("Table", "CITY");

            odaLocality = new OracleDataAdapter(@"select * from apstaff.locality where substr(locality_code,1,2)=:p_code_region
                and (:p_code_district is null or substr(code_city,1,5)=:p_code_district) order by locality_name", CurConnect);
            odaLocality.SelectCommand.BindByName = true;
            odaLocality.SelectCommand.Parameters.Add("p_code_locality", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaLocality.TableMappings.Add("Table", "LOCALITY");

            odaStreet = new OracleDataAdapter(@"select * from apstaff.street where code_street=:p_code_street order by name_street", CurConnect);
            odaStreet.SelectCommand.BindByName = true;
            odaStreet.SelectCommand.Parameters.Add("p_code_street", OracleDbType.Varchar2, null, ParameterDirection.Input);
            odaStreet.TableMappings.Add("Table", "STREET");
        }
        string _codeRegion, _codeDistrict, _codeCity, _codeLocality;

        [OracleParameterMapping(ParameterName ="p_code_region")]
        public string CodeRegion
        {
            get
            {
                return _codeRegion;
            }
            set
            {
                _codeRegion = value;
                UpdateDistrictSource();
                UpdateCitySource();
            }
        }

        public List<Region> RegionSource
        {
            get
            {
                return ds.Tables["REGION"].ConvertToEntityList<Region>();
            }
        }

        #region Район
        [OracleParameterMapping(ParameterName = "p_code_district")]
        public string CodeDistrict
        {
            get
            {
                return _codeDistrict;
            }
        }

        public List<District> DistrictSource
        {
            get
            {
                return ds.Tables["DISTRICT"].ConvertToEntityList<District>();
            }
        }

        private void UpdateDistrictSource()
        {
            odaDistrict.TryFillWithClear(ds, this);
            RaisePropertyChanged(() => DistrictSource);
        }
        #endregion

        #region Город
        [OracleParameterMapping(ParameterName = "p_code_city")]
        public string CodeCity
        {
            get
            {
                return _codeCity;
            }
        }
        public List<City> CitySource
        {
            get
            {
                return ds.Tables["CITY"].ConvertToEntityList<City>();
            }
        }
        private void UpdateCitySource()
        {
            odaCity.TryFillWithClear(ds, this);
            RaisePropertyChanged(() => CitySource);
        }
        #endregion

        #region Населенный пункт
        [OracleParameterMapping(ParameterName = "p_code_locality")]
        public string CodeLocality
        {
            get
            {
                return _codeLocality;
            }
        }
        
        public List<Locality> LocalitySource
        {
            get
            {
                return ds.Tables["LOCALITY"].ConvertToEntityList<Locality>();
            }
        }
        #endregion

        public List<Street> StreetSource
        {
            get
            {
                return ds.Tables["STREET"].ConvertToEntityList<Street>();
            }
        }
    }
}
