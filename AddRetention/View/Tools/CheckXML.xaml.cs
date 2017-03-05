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
using Salary.Helpers;
using System.ComponentModel;
using System.IO;
//using VSAX3;

namespace Salary.View
{
    /// <summary>
    /// Interaction logic for CheckXML.xaml
    /// </summary>
    public partial class CheckXML : UserControl
    {
        public CheckXML()
        {
            _model = new CheckXMLModel();
            InitializeComponent();
            DataContext = Model;
        }
        CheckXMLModel _model;
        public CheckXMLModel Model
        {
            get
            {
                return _model;
            }
        }

        private void Start_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Model.IsRunning && string.IsNullOrWhiteSpace(Model.Error);
        }

        private void Start_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           // Model.StartCheckFile();
        }
    }

    /// <summary>
    /// Класс - тип файла для проверки
    /// </summary>
    public class TypeFileCheck
    {
        /// <summary>
        /// Наименование типа
        /// </summary>
        public string TypeName
        {
            get;set;
        }

        List<TypeVersion> _typeVersionSource;

        /// <summary>
        /// Источник списка версий
        /// </summary>
        public List<TypeVersion> TypeVersionSource
        {
            get
            {
                return _typeVersionSource;
            }
            set
            {
                _typeVersionSource = value;
                
            }
        }
    }

    /// <summary>
    /// Класс - версия файла проверки
    /// </summary>
    public class TypeVersion
    {
        /// <summary>
        /// Код типа версии
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// Наименование версии файла
        /// </summary>
        public string VersionName
        {
            get;
            set;
        }

        /// <summary>
        /// Имя файлы валидации XSD схемы
        /// </summary>
        public string FileXSDPath
        {
            get;
            set;
        }
    }

    public partial class CheckXMLModel : NotificationObject, IDataErrorInfo
    {
        static CheckXMLModel()
        {
            StartCheckXML = new RoutedUICommand("Начать проверку файла", "CheckXML", typeof(CheckXMLModel));
        }
        public CheckXMLModel()
        {
           /* bw = new BackgroundWorker();
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.DoWork += StartCheck;*/
        }

       /* void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Ошибка проверки файла");
            else
                if (e.Cancelled) return;
                else
                {
                    MessageBox.Show("Проверка файла завершена. Список ошибок смотрите в таблице", "Зарплата предприятия");
                    List<ErrorsStruct> res = (List<ErrorsStruct>)e.Result;
                    ErrorsSource = res;
                }

        }
        
        List<ErrorsStruct> _listErrors;
        /// <summary>
        /// Источник данных ошибок
        /// </summary>
        public List<ErrorsStruct> ErrorsSource
        {
            get
            {
                return _listErrors;
            }
            set {
                _listErrors = value;
                RaisePropertyChanged(() => ErrorsSource);
            }
        }
        

        List<TypeFileCheck> _listTypeFile;
        /// <summary>
        /// Источник данных для типов файлов
        /// </summary>
        public List<TypeFileCheck> TypeFileSource
        {
            get
            {
                if (_listTypeFile == null)
                    _listTypeFile = AppXmlHelper.GetElements("TypeFileCheckXML").Select(r => new TypeFileCheck() 
                            { 
                                TypeName = r.Attribute("Name").Value, 
                                TypeVersionSource = r.Descendants().Select(e=>new TypeVersion()
                                                        { 
                                                            Code = e.Attribute("Code").Value, 
                                                            VersionName = e.Attribute("VersionName").Value, 
                                                            FileXSDPath = e.Attribute("FileXSDPath").Value
                                                        }).ToList()
                            }).ToList();
                return _listTypeFile;
            }
        }
        */
        TypeFileCheck _selectedTypeFile;

        /// <summary>
        /// Выбранный тип файла для обработки
        /// </summary>
        public TypeFileCheck SelectedTypeFile
        {
            get
            {
                return _selectedTypeFile;
            }
            set
            {
                _selectedTypeFile = value;
                RaisePropertyChanged(() => SelectedTypeFile);
            }
        }

        TypeVersion _selectedTypeVersion;

        /// <summary>
        /// Выбранная версия проверки файла
        /// </summary>
        public TypeVersion SelectedTypeVersion
        {
            get
            {
                return _selectedTypeVersion;
            }
            set
            {
                _selectedTypeVersion = value;
                RaisePropertyChanged(() => SelectedTypeVersion);
            }
        }

        string  _filePathSource=string.Empty;
        /// <summary>
        /// Файл-источник данных для валидации
        /// </summary>
        public string FilePathSource
        {
            get
            {
                return _filePathSource;
            }
            set
            {
                _filePathSource = value;
                RaisePropertyChanged(() => FilePathSource);
            }
        }

        public static RoutedUICommand StartCheckXML;

        /// <summary>
        /// Ошибка в модели проверки
        /// </summary>
        public string Error
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(FilePathSource) || !File.Exists(FilePathSource))
                    return "Заданный файл проверки не существует";
                if (SelectedTypeFile == null) return "Не выбран тип файла для проверки";
                if (SelectedTypeVersion == null) return "Не выбрана версия файла для проверки";
                return string.Empty;
            }
        }

        /// <summary>
        /// Ошибка поля модели
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get 
            { 
                if (columnName=="FilePathSource")
                    if (string.IsNullOrWhiteSpace(FilePathSource) || !File.Exists(FilePathSource))
                        return "Заданный файл проверки не существует";
                if (columnName=="SelectedTypeFile")
                    if (SelectedTypeFile == null) 
                        return "Не выбран тип файла для проверки";
                if (columnName=="SelectedTypeVersion")
                    if (SelectedTypeVersion==null)
                        return "Не выбрана версия файла для проверки";
                return string.Empty;
            }
        }

        private BackgroundWorker bw;
/*
        /// <summary>
        /// Запуск проверки файла
        /// </summary>
        public void StartCheckFile()
        {
            if (bw != null && bw.IsBusy)
                return;
            
            bw.RunWorkerAsync(new object[]{FilePathSource, 
                Connect.CurrentAppPath+@"\XmlData\"+SelectedTypeVersion.FileXSDPath, 
                EVsaxValidateType.SaxSchematronUsch});
        }

        ushort _defaulErrorCount = 100;

        /// <summary>
        /// Кол-во ошибок которые требуется показывать
        /// </summary>
        public ushort DefaultErrorCount
        {
            get
            {
                return _defaulErrorCount;
            }
            set
            {
                _defaulErrorCount = value;
                RaisePropertyChanged(() => DefaultErrorCount);
            }

        }

        /// <summary>
        /// #1: Валидация Xml по Xsd без схематрона, но с формированием XPath путей до тегов с ошибками
        /// и с выводом их как на экран в консоль, так и в файл:
        /// в Аргумент требуется положить Object[] XmlFile, XSDFile, ValidationType = EVsaxValidateType.SaxSchematronUsch
        /// </summary>
        private void StartCheck(object sender, DoWorkEventArgs e)
        {
            //XML:
            //Имя XML файла входного
            var pXml = new FileStream(((object[])e.Argument)[0].ToString(), FileMode.Open, FileAccess.Read);

            //XSD:
            // Имя файла валидации XSD схемы
            var pXsd = new System.Xml.Schema.XmlSchemaSet();
            pXsd.Add("", ((object[])e.Argument)[1].ToString());

            /// <summary>
            /// #1: Валидация Xml по Xsd без схематрона, но с формированием XPath путей до тегов с ошибками
            /// ValidatingType = EVsaxValidateType.SnpSax
            /// и с выводом их как на экран в консоль, так и в файл:
            /// Версия: (3.3.145.301)+
            /// 
            /// #2: Валидация Xml по Xsd с схематроном и с формированием XPath путей до тегов с ошибками
            /// ValidatingType = EVsaxValidateType.SaxSchematronUsc
            /// и с выводом их как на экран в консоль, так и в файл, но с ограничением кол-ва ошибок по схематрону в 10 вместо 100 по умолчанию:
            /// Версия: (3.3.147.101)+

            EVsaxValidateType validationType = (EVsaxValidateType)((object[])e.Argument)[2]; // default value is EVsaxValidateType.SaxSchematronUsch
            var reader = new SnpXmlValidatingReader { ValidatingType = validationType, ErrorCounter = DefaultErrorCount };
            bool validate_result = reader.Validate(pXml, String.Empty, pXsd);
            ErrorsStruct[] l = new ErrorsStruct[reader.ErrorHandler.Errors.Count];
            reader.ErrorHandler.Errors.CopyTo(l);
            e.Result = l.ToList();
            reader.Close();
        }*/

        /// <summary>
        /// Запущена ли проверка
        /// </summary>
        public bool IsRunning 
        { 
            get
            {
                return bw != null && bw.IsBusy;
            }
        }

    }
}
