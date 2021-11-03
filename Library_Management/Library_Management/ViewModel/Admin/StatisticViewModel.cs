using Library_Management.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Documents;
using System.Data;

namespace Library_Management.ViewModel.Admin
{
    public class StatisticViewModel : BaseViewModel
    {
        #region Init List Data
        private ObservableCollection<Model.Human> _LvHuman;
        public ObservableCollection<Model.Human> LvHuman { get => _LvHuman; set { _LvHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.UserStaff> _LvUserStaff;
        public ObservableCollection<Model.UserStaff> LvUserStaff { get => _LvUserStaff; set { _LvUserStaff = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BookSubject> _LvBookSubject;
        public ObservableCollection<Model.BookSubject> LvBookSubject { get => _LvBookSubject; set { _LvBookSubject = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BorrowBook> _LvBorrowBook;
        public ObservableCollection<Model.BorrowBook> LvBorrowBook { get => _LvBorrowBook; set { _LvBorrowBook = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BorrowBook> _LvBorrowBookDistinct;
        public ObservableCollection<Model.BorrowBook> LvBorrowBookDistinct { get => _LvBorrowBookDistinct; set { _LvBorrowBookDistinct = value; OnPropertyChanged(); } }

        private ObservableCollection<DayWork> _LvDayWork;
        public ObservableCollection<DayWork> LvDayWork { get => _LvDayWork; set { _LvDayWork = value; OnPropertyChanged(); } }

        private ObservableCollection<Age> _LvAge;
        public ObservableCollection<Age> LvAge { get => _LvAge; set { _LvAge = value; OnPropertyChanged(); } }

        private ObservableCollection<Age> _LvAgeDistinct;
        public ObservableCollection<Age> LvAgeDistinct { get => _LvAgeDistinct; set { _LvAgeDistinct = value; OnPropertyChanged(); } }

        private ObservableCollection<CountBookBorrow> _LvCountBookBorrow;
        public ObservableCollection<CountBookBorrow> LvCountBookBorrow { get => _LvCountBookBorrow; set { _LvCountBookBorrow = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Human
        private SeriesCollection _SeriesCollectionHuman;
        public SeriesCollection SeriesCollectionHuman { get => _SeriesCollectionHuman; set { _SeriesCollectionHuman = value; OnPropertyChanged(); } }

        private string[] _LabelsHuman;
        public string[] LabelsHuman { get => _LabelsHuman; set { _LabelsHuman = value; OnPropertyChanged(); } }

        private Func<double, string> _FormatterHuman;
        public Func<double, string> FormatterHuman { get => _FormatterHuman; set { _FormatterHuman = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Staff
        private SeriesCollection _SeriesCollectionStaff;
        public SeriesCollection SeriesCollectionStaff { get => _SeriesCollectionStaff; set { _SeriesCollectionStaff = value; OnPropertyChanged(); } }

        private string[] _LabelsStaff;
        public string[] LabelsStaff { get => _LabelsStaff; set { _LabelsStaff = value; OnPropertyChanged(); } }

        private Func<double, string> _FormatterStaff;
        public Func<double, string> FormatterStaff { get => _FormatterStaff; set { _FormatterStaff = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Subject
        private SeriesCollection _SeriesCollectionSubject;
        public SeriesCollection SeriesCollectionSubject { get => _SeriesCollectionSubject; set { _SeriesCollectionSubject = value; OnPropertyChanged(); } }

        private string[] _LabelsSubject;
        public string[] LabelsSubject { get => _LabelsSubject; set { _LabelsSubject = value; OnPropertyChanged(); } }

        private Func<double, string> _FormatterSubject;
        public Func<double, string> FormatterSubject { get => _FormatterSubject; set { _FormatterSubject = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Date
        private DateTime _StartDay;
        public DateTime StartDay { get => _StartDay; set { _StartDay = value; OnPropertyChanged(); } }

        private DateTime _EndDate;
        public DateTime EndDate { get => _EndDate; set { _EndDate = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollectionDay;
        public SeriesCollection SeriesCollectionDay { get => _SeriesCollectionDay; set { _SeriesCollectionDay = value; OnPropertyChanged(); } }

        private string[] _LabelsDay;
        public string[] LabelsDay { get => _LabelsDay; set { _LabelsDay = value; OnPropertyChanged(); } }

        private Func<double, string> _FormatterDay;
        public Func<double, string> FormatterDay { get => _FormatterDay; set { _FormatterDay = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Age
        private SeriesCollection _SeriesCollectionAge;
        public SeriesCollection SeriesCollectionAge { get => _SeriesCollectionAge; set { _SeriesCollectionAge = value; OnPropertyChanged(); } }

        private string[] _LabelsAge;
        public string[] LabelsAge { get => _LabelsAge; set { _LabelsAge = value; OnPropertyChanged(); } }

        private Func<double, string> _FormatterAge;
        public Func<double, string> FormatterAge { get => _FormatterAge; set { _FormatterAge = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Book
        private SeriesCollection _SeriesCollectionBook;
        public SeriesCollection SeriesCollectionBook { get => _SeriesCollectionBook; set { _SeriesCollectionBook = value; OnPropertyChanged(); } }

        private string[] _LabelsBook;
        public string[] LabelsBook { get => _LabelsBook; set { _LabelsBook = value; OnPropertyChanged(); } }

        private Func<double, string> _FormatterBook;
        public Func<double, string> FormatterBook { get => _FormatterBook; set { _FormatterBook = value; OnPropertyChanged(); } }
        #endregion

        #region Test
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> Formatter { get; set; }

        public List<Brushes> ListColor;
        #endregion

        #region ICommand Human
        public ICommand RefreshHumanCommand { get; set; }
        public ICommand ResetHumanScoreCommand { get; set; }
        public ICommand HumanExcelCommand { get; set; }
        public ICommand HumanWordCommand { get; set; }
        public ICommand HumanPrintCommand { get; set; }
        #endregion

        #region ICommand Staff
        public ICommand RefreshStaffCommand { get; set; }
        public ICommand StaffExcelCommand { get; set; }
        public ICommand StaffWordCommand { get; set; }
        public ICommand StaffPrintCommand { get; set; }
        #endregion

        #region ICommand Subject
        public ICommand RefreshSubjectCommand { get; set; }
        public ICommand SubjectExcelCommand { get; set; }
        public ICommand SubjectWordCommand { get; set; }
        public ICommand SubjectPrintCommand { get; set; }
        #endregion

        #region ICommand Age and Date 
        public ICommand ShowDateCommand { get; set; }
        public ICommand ChartDataClick { get; set; }
        public ICommand StatisticAgeCountBookCommand { get; set; }
        public ICommand StatisticAgeCommand { get; set; }
        #endregion

        #region ICommand Book
        public ICommand RefreshBookCommand { get; set; }
        public ICommand ResetBookScoreCommand { get; set; }
        public ICommand BookExcelCommand { get; set; }
        public ICommand BookWordCommand { get; set; }
        public ICommand BookPrintCommand { get; set; }
        #endregion

        public StatisticViewModel()
        {
            #region Get List Data
            LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0).OrderByDescending(x => x.Score));
            LvUserStaff = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0));
            LvBookSubject = new ObservableCollection<Model.BookSubject>(DataProvider.Ins.DB.BookSubjects.Where(x => x.CountDelete == 0));
            LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.CountDelete == 0));
            LvDayWork = new ObservableCollection<DayWork>();
            LvAge = new ObservableCollection<Age>();
            LvAgeDistinct = new ObservableCollection<Age>();
            LvCountBookBorrow = new ObservableCollection<CountBookBorrow>();
            #endregion

            #region var
            StartDay = DateTime.Now;
            EndDate = DateTime.Now;
            #endregion

            #region human
            LiveChartHuman();

            RefreshHumanCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                //LiveChartHuman();
            });

            ResetHumanScoreCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                foreach (Model.Human item in LvHuman)
                {
                    var Human = DataProvider.Ins.DB.Humen.Where(x => x.Id == item.Id && x.CountDelete == 0).SingleOrDefault();
                    Human.Score = 0;

                    DataProvider.Ins.DB.SaveChanges();
                }
                LiveChartHuman();
                MessageBox.Show("Successful");
            });

            HumanExcelCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {
                string filePath = GetFilePathExcel();
                string[] arrColumnHeader = { "Id", "Full Name", "Date Of Birth", "Authority", "Gender", "Address", "Phone", "Email", "Note", "Score" };
                OuputExcel(filePath, 1, arrColumnHeader, 10, "LIST HUMAN SCORE", "LIST HUMAN");
            });

            HumanWordCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {
                string filePath = GetFilePathWord();

                int RowCount = LvHuman.Count();
                int ColumnCount = 10;
                string[] arrColumnHeader = { "Id", "Full Name", "Date Of Birth", "Authority", "Gender", "Address", "Phone", "Email", "Note", "Score" };
                string nameList = "LIST HUMAN SCORE";
                string headerWork = "LIST HUMAN";
                OuputWord(RowCount, ColumnCount, arrColumnHeader, nameList, headerWork, 1, filePath);
            });

            HumanPrintCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                string[] arrColumnHeader = { "Id", "Full Name", "Date Of Birth", "Authority", "Gender", "Address", "Phone", "Email", "Note", "Score" };
                OuputPrint(1, arrColumnHeader, "LIST HUMAN SCORE");
            });

            #endregion

            #region staff
            LiveChartStaff();

            RefreshStaffCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                LiveChartStaff();
            });

            StaffExcelCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {
                string filePath = GetFilePathExcel();
                string[] arrColumnHeader = { "Id", "Full Name", "Authority", "Score Input", "Score Ouput" };
                OuputExcel(filePath, 2, arrColumnHeader, 5, "LIST STAFF SCORE", "LIST STAFF");
            });

            StaffWordCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {
                string filePath = GetFilePathWord();

                int RowCount = LvUserStaff.Count();
                int ColumnCount = 5;
                string[] arrColumnHeader = { "Id", "Full Name", "Authority", "Score Input", "Score Ouput" };
                string nameList = "LIST STAFF SCORE";
                string headerWork = "LIST STAFF";
                OuputWord(RowCount, ColumnCount, arrColumnHeader, nameList, headerWork, 2, filePath);

            });

            StaffPrintCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                string[] arrColumnHeader = { "Id", "Full Name", "Authority", "Score Input", "Score Ouput" };
                OuputPrint(2, arrColumnHeader, "LIST STAFF SCORE");
            });

            #endregion

            #region subject
            LiveChartSubject();

            RefreshSubjectCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                LiveChartSubject();
            });

            SubjectExcelCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {
                string filePath = GetFilePathExcel();
                string[] arrColumnHeader = { "Id", "Full Name", "Score Input", "Score Ouput", "Score" };
                OuputExcel(filePath, 3, arrColumnHeader, 5, "LIST SUBJECT SCORE", "LIST SUBJECT");
            });

            SubjectWordCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {

                string filePath = GetFilePathWord();

                int RowCount = LvBookSubject.Count();
                int ColumnCount = 5;
                string[] arrColumnHeader = { "Id", "Full Name", "Score Input", "Score Ouput", "Score" };
                string nameList = "LIST SUBJECT SCORE";
                string headerWork = "LIST SUBJECT";
                OuputWord(RowCount, ColumnCount, arrColumnHeader, nameList, headerWork, 3, filePath);

            });

            SubjectPrintCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                string[] arrColumnHeader = { "Id", "Full Name", "Score Input", "Score Ouput", "Score" };
                OuputPrint(3, arrColumnHeader, "LIST SUBJECT SCORE");
            });
            #endregion

            #region date
            ShowDateCommand = new RelayCommand<Object>((p) => {
                if (StartDay.Date <= EndDate.Date)
                    return true;
                return false;
            }, (p) => {
                LiveChartDay();
            });

            ChartDataClick = new RelayCommand<ChartPoint>((p) => {
                return true;
            }, (p) => {


                DateTime getDate = DateTime.Parse(p.SeriesView.Title);

                int size = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (LabelsDay[i] == getDate.DayOfWeek.ToString())
                    {
                        size = i;
                        break;
                    }
                }

                if (getDate.DayOfWeek.ToString() != "Sunday")
                {
                    MessageBox.Show("Date: " + getDate.AddDays(p.X - size).ToString("dd/MM/yyyy" + "\n" + "Score: " + p.Y.ToString()));
                }
                else
                {
                    MessageBox.Show("Date: " + getDate.AddDays(p.X).Date.ToString("dd/MM/yyyy" + "\n" + "Score: " + p.Y.ToString()));
                };

            });
            #endregion

            #region age
            LiveChartAge(1);

            StatisticAgeCommand = new RelayCommand<ChartPoint>((p) => {
                return true;
            }, (p) => {
                LiveChartAge(1);
            });

            StatisticAgeCountBookCommand = new RelayCommand<ChartPoint>((p) => {
                return true;
            }, (p) => {
                LiveChartAge(2);
            });
            #endregion

            #region book
            LiveChartBook();

            RefreshBookCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                LiveChartBook();
            });

            ResetBookScoreCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
            });

            BookExcelCommand = new RelayCommand<Button>((a) => { return true; }, (a) =>
            {
                string filePath = GetFilePathExcel();
                string[] arrColumnHeader = { "Id", "Book Title", "Score" };
                OuputExcel(filePath, 4, arrColumnHeader, 3, "LIST BOOK SCORE", "LIST BOOK");
            });

            BookWordCommand = new RelayCommand<Button>((a) => { return true; }, (a) =>
            {
                string filePath = GetFilePathWord();
                int RowCount = LvCountBookBorrow.Count();
                int ColumnCount = 3;
                string[] arrColumnHeader = { "Id", "Book Title", "Score" };
                string nameList = "LIST BOOK SCORE";
                string headerWork = "LIST BOOK";
                OuputWord(RowCount, ColumnCount, arrColumnHeader, nameList, headerWork, 4, filePath);

            });

            BookPrintCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                string[] arrColumnHeader = { "Id", "Book Title", "Score" };
                OuputPrint(4, arrColumnHeader, "LIST BOOK SCORE");
            });

            #endregion

        }


        public string GetFilePathExcel()
        {
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel |*.xlsx";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Invalid file path");
                return null;
            }
            return filePath;
        }

        public void OuputExcel(string FilePath, int option, string[] arrHeaderColumn, int Column, string Title, string Name)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook worKbooK;
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            Microsoft.Office.Interop.Excel.Range celLrangE;

            excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;
            worKbooK = excel.Workbooks.Add(Type.Missing);

            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = Name;

            worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, Column]].Merge();
            worKsheeT.Cells[1, 1] = Title;
            worKsheeT.get_Range("A1", "A1").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            worKsheeT.get_Range("A1", "A1").Cells.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbSkyBlue;
            worKsheeT.get_Range("A1", "A1").Cells.Font.Bold = true;

            worKsheeT.Cells.Font.Size = 12;
            worKsheeT.get_Range("A1", "A1").Cells.Font.Size = 15;
            int indexCellStart = 3;

            for (int i = 0; i < Column; i++)
            {
                worKsheeT.Cells[indexCellStart, i + 1] = arrHeaderColumn[i];
            }

            int indexRow = 4;

            if (option == 1)
            {
                foreach (Model.Human item in LvHuman)
                {
                    worKsheeT.Cells[indexRow, 1] = item.MS;
                    worKsheeT.Cells[indexRow, 2] = item.DisplayName;
                    worKsheeT.Cells[indexRow, 3] = item.DateOfBirth.ToShortDateString();
                    worKsheeT.Cells[indexRow, 4] = item.AuthorityHuman.DisplayName;
                    worKsheeT.Cells[indexRow, 5] = item.Gender.DisplayName;
                    worKsheeT.Cells[indexRow, 6] = item.Address;
                    worKsheeT.Cells[indexRow, 7] = item.Phone;
                    worKsheeT.Cells[indexRow, 8] = item.Email;
                    worKsheeT.Cells[indexRow, 9] = item.Note;
                    worKsheeT.Cells[indexRow, 10] = item.Score;
                    worKsheeT.get_Range("A" + indexRow.ToString(), "A" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    worKsheeT.get_Range("C" + indexRow.ToString(), "G" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    worKsheeT.get_Range("I" + indexRow.ToString(), "J" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    indexRow++;
                }

                worKsheeT.get_Range("A3", "J3").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                worKsheeT.get_Range("A3", "J3").Cells.Font.Bold = true;
                worKsheeT.get_Range("A3", "J3").Cells.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGreen;
            }
            else if (option == 2)
            {
                foreach (UserStaff item in LvUserStaff)
                {
                    worKsheeT.Cells[indexRow, 1] = item.Id;
                    worKsheeT.Cells[indexRow, 2] = item.Human.DisplayName;
                    worKsheeT.Cells[indexRow, 3] = item.AuthorityStaff.DisplayName;
                    worKsheeT.Cells[indexRow, 4] = item.ScoreInputBook;
                    worKsheeT.Cells[indexRow, 5] = item.ScoreOuputBook;
                    worKsheeT.get_Range("A" + indexRow.ToString(), "A" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    worKsheeT.get_Range("C" + indexRow.ToString(), "E" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    indexRow++;
                }

                worKsheeT.get_Range("A3", "E3").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                worKsheeT.get_Range("A3", "E3").Cells.Font.Bold = true;
                worKsheeT.get_Range("A3", "E3").Cells.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGreen;
            }
            else if (option == 3)
            {
                foreach (BookSubject item in LvBookSubject)
                {
                    worKsheeT.Cells[indexRow, 1] = item.Id;
                    worKsheeT.Cells[indexRow, 2] = item.DisplayName;
                    worKsheeT.Cells[indexRow, 3] = item.ScoreInputSubject;
                    worKsheeT.Cells[indexRow, 4] = item.ScoreOuputSubject;
                    worKsheeT.Cells[indexRow, 5] = item.Note;
                    worKsheeT.get_Range("A" + indexRow.ToString(), "A" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    worKsheeT.get_Range("C" + indexRow.ToString(), "E" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    indexRow++;
                }

                worKsheeT.get_Range("A3", "E3").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                worKsheeT.get_Range("A3", "E3").Cells.Font.Bold = true;
                worKsheeT.get_Range("A3", "E3").Cells.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGreen;
            }
            else if (option == 4)
            {
                foreach (CountBookBorrow item in LvCountBookBorrow)
                {
                    worKsheeT.Cells[indexRow, 1] = item.Id;
                    worKsheeT.Cells[indexRow, 2] = item.DisplayName;
                    worKsheeT.Cells[indexRow, 3] = item.Score;
                    worKsheeT.get_Range("A" + indexRow.ToString(), "A" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    worKsheeT.get_Range("C" + indexRow.ToString(), "C" + indexRow.ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    indexRow++;
                }

                worKsheeT.get_Range("A3", "C3").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                worKsheeT.get_Range("A3", "C3").Cells.Font.Bold = true;
                worKsheeT.get_Range("A3", "C3").Cells.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbLightGreen;
            }
            else
            {

            }

            celLrangE = worKsheeT.Range[worKsheeT.Cells[indexRow, 1], worKsheeT.Cells[indexCellStart, Column]];
            celLrangE.EntireColumn.AutoFit();
            Microsoft.Office.Interop.Excel.Borders border = celLrangE.Borders;
            border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;

            worKbooK.SaveAs(FilePath); ;
            worKbooK.Close();
            excel.Quit();

            MessageBox.Show("Successful");
        }

        public string GetFilePathWord()
        {
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "World | *.docx";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Invalid file path");
                return null;
            }
            return filePath;
        }

        public void OuputWord(int RowCount, int ColumnCount, string[] arrColumnHeader, string nameList, string headerWord, int option, string filePath)
        {

            Word.Document oDoc = new Word.Document();
            oDoc.Application.Visible = true;

            foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
            {
                Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                headerRange.Text = headerWord;
                headerRange.Font.Name = "Times New Roman";
                headerRange.Font.Size = 16;
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            }

            object oMissing = System.Reflection.Missing.Value;
            var para = oDoc.Content.Paragraphs.Add(ref oMissing);
            para.Range.Text = "LIBRARY MANAGER";
            para.Range.Font.Name = "Times New Roman";
            para.Range.InsertParagraphAfter();
            para.Range.Text = ("HIKI, " + DateTime.Now);
            para.Range.Underline = (Word.WdUnderline)1;
            //para.CharacterUnitFirstLineIndent = 2;
            para.Range.Font.Name = "Times New Roman";
            para.Range.InsertParagraphAfter();
            para.Range.Underline = 0;
            para.Range.Text = nameList;
            para.Range.Bold = 1;
            para.Range.Font.Size = 15;
            para.Range.Font.Name = "Times New Roman";
            para.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            para.Range.InsertParagraphAfter();
            para.Range.Bold = 0;

            para.Range.InsertParagraphAfter();
            oDoc.Application.Selection.MoveDown();
            oDoc.Application.Selection.MoveDown();
            oDoc.Application.Selection.MoveDown();
            oDoc.Application.Selection.MoveDown();

            oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            dynamic oRange = oDoc.Content.Application.Selection.Range;
            string oTemp = "";


            if (option == 1) // list human
            {
                foreach (Model.Human item in LvHuman)
                {
                    oTemp = oTemp + item.MS + "\t" + item.DisplayName + "\t" + item.DateOfBirth.ToShortDateString() + "\t" + item.AuthorityHuman.DisplayName + "\t" + item.Gender.DisplayName + "\t"
                        + item.Address + "\t" + item.Phone + "\t" + item.Email + "\t" + item.Note + "\t" + item.Score + "\t"; ;
                }
            }
            else if (option == 2) // list staff
            {
                foreach (UserStaff item in LvUserStaff)
                {
                    oTemp = oTemp + item.Id + "\t" + item.Human.DisplayName + "\t" + item.AuthorityStaff.DisplayName + "\t" + item.ScoreInputBook + "\t" + item.ScoreOuputBook + "\t";
                }
            }
            else if (option == 3) //list subject
            {
                foreach (BookSubject item in LvBookSubject)
                {
                    oTemp = oTemp + item.Id + "\t" + item.DisplayName + "\t" + item.ScoreInputSubject + "\t" + item.ScoreOuputSubject + "\t" + item.Note + "\t";
                }
            }
            else if (option == 4) //list count borrow book
            {
                foreach (CountBookBorrow item in LvCountBookBorrow)
                {
                    oTemp = oTemp + item.Id + "\t" + item.DisplayName + "\t" + item.Score + "\t";
                }
            }
            else
            {

            }

            oRange.Text = oTemp;
            object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
            object ApplyBorders = true;
            object AutoFit = true;
            object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;
            oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount, Type.Missing, Type.Missing, ref ApplyBorders, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);
            oRange.Select();

            oDoc.Application.Selection.Tables[1].Rows[1].Select();
            oDoc.Application.Selection.InsertRowsAbove(1);

            for (int c = 0; c <= ColumnCount - 1; c++)
            {
                oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = arrColumnHeader[c];
            }


            oDoc.Application.Selection.Tables[1].Select();
            oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
            oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
            oDoc.Application.Selection.Tables[1].Range.Font.Name = "Times New Roman";
            oDoc.Application.Selection.Tables[1].Range.Font.Size = 12;
            oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;
            oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
            oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            oDoc.Application.Selection.Tables[1].Rows[RowCount + 2].Delete();
            oDoc.SaveAs2(filePath);

            MessageBox.Show("Successful");
        }

        public void OuputPrint(int Option, string[] arrColumnHeader, string Title)
        {
            FlowDocument fd = new FlowDocument();

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            fd.PageHeight = pd.PrintableAreaHeight;
            fd.PageWidth = 816;
            fd.ColumnWidth = 816;


            Paragraph p = new Paragraph(new Run(Title));
            p.FontSize = 18;
            p.FontWeight = FontWeights.Bold;
            p.TextAlignment = TextAlignment.Center;

            fd.Blocks.Add(p);

            Table table = new Table();
            TableRowGroup tableRowGroup = new TableRowGroup();
            TableRow r = new TableRow();

            if (Option == 1)
            {
                for (int j = 0; j < arrColumnHeader.Length; j++)
                {

                    r.Cells.Add(new TableCell(new Paragraph(new Run(arrColumnHeader[j]))));
                    r.Cells[j].ColumnSpan = 9;
                    r.Cells[j].Padding = new Thickness(4);
                    r.Cells[j].BorderBrush = Brushes.Black;
                    r.Cells[j].FontWeight = FontWeights.Bold;
                    r.Cells[j].Background = Brushes.DarkGray;
                    r.Cells[j].Foreground = Brushes.White;
                    r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
                    r.Cells[j].TextAlignment = TextAlignment.Center;
                }

                tableRowGroup.Rows.Add(r);
                table.RowGroups.Add(tableRowGroup);

                foreach (Model.Human item in LvHuman)
                {
                    table.BorderBrush = Brushes.Gray;
                    table.BorderThickness = new Thickness(1, 1, 0, 0);
                    table.FontSize = 12;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();

                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Id.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.DisplayName.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.DateOfBirth.ToShortDateString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.AuthorityHuman.DisplayName.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Gender.DisplayName.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Address.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Phone.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Email.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Note))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Score.ToString()))));


                    for (int i = 0; i < 10; i++)
                    {
                        r.Cells[i].ColumnSpan = 9;
                        r.Cells[i].Padding = new Thickness(4);
                        r.Cells[i].BorderBrush = Brushes.DarkGray;
                        r.Cells[i].BorderThickness = new Thickness(0, 0, 1, 1);
                        if (i != 1)
                        {
                            r.Cells[i].TextAlignment = TextAlignment.Center;
                        }
                    }


                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);

                }
            }
            else if (Option == 2)
            {
                for (int j = 0; j < arrColumnHeader.Length; j++)
                {

                    r.Cells.Add(new TableCell(new Paragraph(new Run(arrColumnHeader[j]))));
                    r.Cells[j].ColumnSpan = 4;
                    r.Cells[j].Padding = new Thickness(4);
                    r.Cells[j].BorderBrush = Brushes.Black;
                    r.Cells[j].FontWeight = FontWeights.Bold;
                    r.Cells[j].Background = Brushes.DarkGray;
                    r.Cells[j].Foreground = Brushes.White;
                    r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
                    r.Cells[j].TextAlignment = TextAlignment.Center;
                }

                tableRowGroup.Rows.Add(r);
                table.RowGroups.Add(tableRowGroup);


                foreach (UserStaff item in LvUserStaff)
                {
                    table.BorderBrush = Brushes.Gray;
                    table.BorderThickness = new Thickness(1, 1, 0, 0);
                    table.FontSize = 12;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();

                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Id.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Human.DisplayName.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.AuthorityStaff.DisplayName.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.ScoreInputBook.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.ScoreOuputBook.ToString()))));

                    for (int i = 0; i < 5; i++)
                    {
                        r.Cells[i].ColumnSpan = 4;
                        r.Cells[i].Padding = new Thickness(4);
                        r.Cells[i].BorderBrush = Brushes.DarkGray;
                        r.Cells[i].BorderThickness = new Thickness(0, 0, 1, 1);
                        if (i != 1)
                        {
                            r.Cells[i].TextAlignment = TextAlignment.Center;
                        }
                    }

                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);

                }
            }
            else if (Option == 3)
            {
                for (int j = 0; j < arrColumnHeader.Length; j++)
                {

                    r.Cells.Add(new TableCell(new Paragraph(new Run(arrColumnHeader[j]))));
                    r.Cells[j].ColumnSpan = 4;
                    r.Cells[j].Padding = new Thickness(4);
                    r.Cells[j].BorderBrush = Brushes.Black;
                    r.Cells[j].FontWeight = FontWeights.Bold;
                    r.Cells[j].Background = Brushes.DarkGray;
                    r.Cells[j].Foreground = Brushes.White;
                    r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
                    r.Cells[j].TextAlignment = TextAlignment.Center;
                }

                tableRowGroup.Rows.Add(r);
                table.RowGroups.Add(tableRowGroup);


                foreach (BookSubject item in LvBookSubject)
                {
                    table.BorderBrush = Brushes.Gray;
                    table.BorderThickness = new Thickness(1, 1, 0, 0);
                    table.FontSize = 12;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();

                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Id.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.DisplayName.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.ScoreInputSubject.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.ScoreOuputSubject.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Note.ToString()))));

                    for (int i = 0; i < 5; i++)
                    {
                        r.Cells[i].ColumnSpan = 4;
                        r.Cells[i].Padding = new Thickness(4);
                        r.Cells[i].BorderBrush = Brushes.DarkGray;
                        r.Cells[i].BorderThickness = new Thickness(0, 0, 1, 1);
                        if (i != 1)
                        {
                            r.Cells[i].TextAlignment = TextAlignment.Center;
                        }
                    }

                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);
                }
            }
            else if (Option == 4)
            {
                for (int j = 0; j < arrColumnHeader.Length; j++)
                {

                    r.Cells.Add(new TableCell(new Paragraph(new Run(arrColumnHeader[j]))));
                    r.Cells[j].ColumnSpan = 2;
                    r.Cells[j].Padding = new Thickness(4);
                    r.Cells[j].BorderBrush = Brushes.Black;
                    r.Cells[j].FontWeight = FontWeights.Bold;
                    r.Cells[j].Background = Brushes.DarkGray;
                    r.Cells[j].Foreground = Brushes.White;
                    r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
                    r.Cells[j].TextAlignment = TextAlignment.Center;
                }

                tableRowGroup.Rows.Add(r);
                table.RowGroups.Add(tableRowGroup);


                foreach (CountBookBorrow item in LvCountBookBorrow)
                {
                    table.BorderBrush = Brushes.Gray;
                    table.BorderThickness = new Thickness(1, 1, 0, 0);
                    table.FontSize = 12;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();

                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Id.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.DisplayName.ToString()))));
                    r.Cells.Add(new TableCell(new Paragraph(new Run(item.Score.ToString()))));

                    for (int i = 0; i < 3; i++)
                    {
                        r.Cells[i].ColumnSpan = 2;
                        r.Cells[i].Padding = new Thickness(4);
                        r.Cells[i].BorderBrush = Brushes.DarkGray;
                        r.Cells[i].BorderThickness = new Thickness(0, 0, 1, 1);
                        if (i != 1)
                        {
                            r.Cells[i].TextAlignment = TextAlignment.Center;
                        }
                    }

                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);
                }
            }
            else
            {

            }
            fd.Blocks.Add(table);

            IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");


            MessageBox.Show("Successful");
        }

        public void LiveChartHuman()
        {
            ChartValues<int> scoreHuman = new ChartValues<int>();

            LabelsHuman = new string[LvHuman.Count];
            int indexHuman = 0;

            foreach (Model.Human item in LvHuman)
            {
                if (indexHuman > 10) break;
                scoreHuman.Add((int)item.Score);
                LabelsHuman[indexHuman] = item.DisplayName;
                indexHuman++;
            }

            SeriesCollectionHuman = new SeriesCollection();

            try
            {
                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[0],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.DeepPink,
                });
                SeriesCollectionHuman[0].Values = new ChartValues<int> { scoreHuman[0] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[1],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.GreenYellow,
                });
                SeriesCollectionHuman[1].Values = new ChartValues<int> { scoreHuman[1] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[2],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.DeepSkyBlue,
                });
                SeriesCollectionHuman[2].Values = new ChartValues<int> { scoreHuman[2] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[3],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.BurlyWood,
                });
                SeriesCollectionHuman[3].Values = new ChartValues<int> { scoreHuman[3] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[4],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.Chocolate,
                });
                SeriesCollectionHuman[4].Values = new ChartValues<int> { scoreHuman[4] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[5],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.CornflowerBlue,
                });
                SeriesCollectionHuman[5].Values = new ChartValues<int> { scoreHuman[5] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[6],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.Orchid,
                });
                SeriesCollectionHuman[6].Values = new ChartValues<int> { scoreHuman[6] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[7],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.LightGreen,
                });
                SeriesCollectionHuman[7].Values = new ChartValues<int> { scoreHuman[7] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[8],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.LightGreen,
                });
                SeriesCollectionHuman[8].Values = new ChartValues<int> { scoreHuman[8] };

                SeriesCollectionHuman.Add(new ColumnSeries
                {
                    Title = LabelsHuman[9],
                    MaxColumnWidth = 120,
                    ColumnPadding = 90,
                    Fill = System.Windows.Media.Brushes.LightGreen,
                });
                SeriesCollectionHuman[9].Values = new ChartValues<int> { scoreHuman[9] };
            }
            catch
            {

            }

        }

        public void LiveChartStaff()
        {
            ChartValues<int> scoreInputStaff = new ChartValues<int>();
            ChartValues<int> scoreOuputStaff = new ChartValues<int>();

            LabelsStaff = new string[LvUserStaff.Count];

            int indexStaff = 0;
            foreach (UserStaff item in LvUserStaff)
            {
                scoreInputStaff.Add((int)item.ScoreInputBook);
                scoreOuputStaff.Add((int)item.ScoreOuputBook);
                var getDisplayName = DataProvider.Ins.DB.Humen.Where(x => x.Id == item.IdHuman && x.CountDelete == 0).SingleOrDefault();
                LabelsStaff[indexStaff] = getDisplayName.DisplayName;
                indexStaff++;
            }

            SeriesCollectionStaff = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Input",
                    Values = new ChartValues<int>(scoreInputStaff)
                }
            };

            SeriesCollectionStaff.Add(new ColumnSeries
            {
                Title = "Ouput",
                Values = new ChartValues<int>(scoreOuputStaff)
            });

            FormatterStaff = value => value.ToString();
        }

        public void LiveChartSubject()
        {
            ChartValues<int> scoreInputSubject = new ChartValues<int>();
            ChartValues<int> scoreOuputSubject = new ChartValues<int>();

            LabelsSubject = new string[LvBookSubject.Count];

            int indexSubject = 0;
            foreach (BookSubject item in LvBookSubject)
            {
                scoreInputSubject.Add((int)item.ScoreInputSubject);
                scoreOuputSubject.Add((int)item.ScoreOuputSubject);
                LabelsSubject[indexSubject] = item.DisplayName;
                indexSubject++;
            }

            SeriesCollectionSubject = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Input",
                    Values = new ChartValues<int>(scoreInputSubject)
                }
            };

            SeriesCollectionSubject.Add(new ColumnSeries
            {
                Title = "Ouput",
                Values = new ChartValues<int>(scoreOuputSubject)
            });

            FormatterSubject = value => value.ToString();
        }

        public void LiveChartDay()
        {
            int stt = 0;
            LvDayWork.Clear();

            SeriesCollectionDay = new SeriesCollection { };

            LabelsDay = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            int countDay = 0;
            int countDayRepeat = 0;

            int size = 0;
            for (int i = 0; i < 7; i++)
            {
                if (LabelsDay[i] == StartDay.DayOfWeek.ToString())
                {
                    size = i + 1;
                    break;
                }
            }

            ChartValues<int> countListBorrowBookDay = new ChartValues<int>();

            for (int i = 1; i < size; i++)
            {
                countListBorrowBookDay.Add(0);
            }

            int countBorrowBook = 0;
            DateTime copyDateStartDay = StartDay.Date;

            int countWeek = 0;
            countDay = size - 1;


            while (copyDateStartDay.AddDays(countDayRepeat).Date <= EndDate.Date)
            {
                stt++;
                DayWork dayWork = new DayWork();
                dayWork.STT = stt;

                if (countDay < 7)
                {
                    for (int i = 0; i < LvBorrowBook.Count; i++)
                    {
                        if (LvBorrowBook[i].DateBorrowed.Date == copyDateStartDay.AddDays(countDayRepeat).Date)
                        {
                            countBorrowBook++;
                        }
                    }
                    dayWork.DisplayName = copyDateStartDay.AddDays(countDayRepeat).Date.ToString("dd/MM/yyyy");
                    dayWork.Score = countBorrowBook;
                    LvDayWork.Add(dayWork);
                    countDay++;
                    countListBorrowBookDay.Add(countBorrowBook);
                    countDayRepeat++;
                    countBorrowBook = 0;
                }
                else
                {
                    countDay = 0;
                    countWeek++;
                    stt--;
                    string title = "";
                    if (countWeek == 1)
                    {
                        title = StartDay.Date.ToString("dd/MM");
                    }
                    else
                    {
                        title = StartDay.AddDays(8 - size + 7 * (countWeek - 2)).Date.ToString("dd/MM");
                    }

                    SeriesCollectionDay.Add(new LineSeries
                    {
                        Title = title,
                        Values = new ChartValues<int>(countListBorrowBookDay)
                    });
                    countListBorrowBookDay.Clear();
                }

            }

            if (countDay != 1 || StartDay.AddDays(1).Date == EndDate.Date)
            {
                if (StartDay.Date == EndDate.Date)
                {
                    SeriesCollectionDay.Add(new LineSeries
                    {
                        Title = StartDay.AddDays(9 - size + 7 * (countWeek - 1)).Date.ToString("dd/MM"),
                        Values = new ChartValues<int>(countListBorrowBookDay)
                    });
                }
                else
                {
                    SeriesCollectionDay.Add(new LineSeries
                    {
                        Title = StartDay.AddDays(8 - size + 7 * (countWeek - 1)).Date.ToString("dd/MM"),
                        Values = new ChartValues<int>(countListBorrowBookDay)
                    });
                }

            }


            //YFormatter = value => value.ToString("C");
        }

        public void LiveChartAge(int option)
        {
            Func<ChartPoint, string> labelPoint = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            string[] listTitle = { "16-17", "18-29", "30-49", "50-64", "65+" };

            List<int> listAge = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                listAge.Add(0);
            }

            if (option == 1)
            {
                LvAgeDistinct.Clear();
                var results = (from ta in DataProvider.Ins.DB.BorrowBooks select ta.IdHuman).Distinct();
                results.ToList();
                foreach (var item in results)
                {
                    var getDateOfBirthHuman = DataProvider.Ins.DB.Humen.Where(x => x.Id == item && x.CountDelete == 0).SingleOrDefault();
                    int age = DateTime.Now.Year - getDateOfBirthHuman.DateOfBirth.Year;

                    if (age >= 16 && age <= 17)
                    {
                        listAge[0]++;
                    }
                    else if (age >= 18 && age <= 29)
                    {
                        listAge[1]++;
                    }
                    else if (age >= 30 && age <= 49)
                    {
                        listAge[2]++;
                    }
                    else if (age >= 50 && age <= 64)
                    {
                        listAge[3]++;
                    }
                    else if (age >= 65)
                    {
                        listAge[4]++;
                    }
                    else
                    {
                        continue;
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    Age age = new Age();
                    age.STT = i + 1; ;
                    age.DisplayName = listTitle[i];
                    age.Score = listAge[i];
                    age.Percent = 0;
                    LvAgeDistinct.Add(age);
                }

                int sumStaff = 0;

                for (int i = 0; i < 5; i++)
                {
                    sumStaff += listAge[i];
                }

                double persent = 0;
                for (int i = 0; i < 5; i++)
                {
                    persent = (listAge[i] / (1.0 * sumStaff)) * 100;
                    LvAgeDistinct[i].Percent = Math.Round(persent, 2);
                }
            }
            else
            {
                LvAge.Clear();
                foreach (var item in LvBorrowBook)
                {
                    var getDateOfBirthHuman = DataProvider.Ins.DB.Humen.Where(x => x.Id == item.IdHuman && x.CountDelete == 0).SingleOrDefault();
                    int age = DateTime.Now.Year - getDateOfBirthHuman.DateOfBirth.Year;

                    if (age >= 16 && age <= 17)
                    {
                        listAge[0]++;
                    }
                    else if (age >= 18 && age <= 29)
                    {
                        listAge[1]++;
                    }
                    else if (age >= 30 && age <= 49)
                    {
                        listAge[2]++;
                    }
                    else if (age >= 50 && age <= 64)
                    {
                        listAge[3]++;
                    }
                    else if (age >= 65)
                    {
                        listAge[4]++;
                    }
                    else
                    {
                        continue;
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    Age age = new Age();
                    age.STT = i + 1; ;
                    age.DisplayName = listTitle[i];
                    age.Score = listAge[i];
                    age.Percent = 0;
                    LvAge.Add(age);
                }
                int sumStaff = 0;

                for (int i = 0; i < 5; i++)
                {
                    sumStaff += listAge[i];
                }

                double persent = 0;
                for (int i = 0; i < 5; i++)
                {
                    persent = (listAge[i] / (1.0 * sumStaff)) * 100;
                    LvAge[i].Percent = Math.Round(persent, 2);
                }
            }


            SeriesCollectionAge = new SeriesCollection
            {
                new PieSeries
                {
                    Title = listTitle[0],
                    Values = new ChartValues<double> {listAge[0]},
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill = System.Windows.Media.Brushes.Green
                },
                new PieSeries
                {
                    Title = listTitle[1],
                    Values = new ChartValues<double> {listAge[1]},
                    DataLabels = true,
                    LabelPoint = labelPoint

                },
                new PieSeries
                {
                    Title = listTitle[2],
                    Values = new ChartValues<double> {listAge[2]},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = listTitle[3],
                    Values = new ChartValues<double> {listAge[3]},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = listTitle[4],
                    Values = new ChartValues<double> {listAge[4]},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };


        }

        public void LiveChartBook()
        {
            LvCountBookBorrow.Clear();
            ChartValues<int> scoreInputBook = new ChartValues<int>();

            var results = (from book in DataProvider.Ins.DB.BorrowBooks group 1 by book.Book.DisplayName into g select new { DisplayName = g.Key, count = g.Count() }).OrderByDescending(x => x.count);
            results.ToList();

            int idBook = 0;
            foreach (var item in results)
            {
                idBook++;
                CountBookBorrow bookBorrow = new CountBookBorrow();
                bookBorrow.Id = idBook;
                bookBorrow.DisplayName = item.DisplayName;
                bookBorrow.Score = item.count;
                LvCountBookBorrow.Add(bookBorrow);
            }

            if (idBook > 10)
            {
                LabelsBook = new string[10];
            }
            else
            {
                LabelsBook = new string[results.Count()];
            }

            int indexBook = 0;
            foreach (var item in results)
            {
                scoreInputBook.Add(item.count);
                LabelsBook[indexBook] = item.DisplayName;
                indexBook++;
                if (indexBook == 10) break;
            }

            SeriesCollectionBook = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Borrow",
                    Fill = Brushes.DarkCyan,
                    Values = new ChartValues<int>(scoreInputBook)
                }
            };


            FormatterBook = value => value.ToString();
        }
    }

    public class DayWork : BaseViewModel
    {
        private int _STT;
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private int _Score;
        public int Score { get => _Score; set { _Score = value; OnPropertyChanged(); } }
    }

    public class Age : DayWork
    {
        private double _Percent;
        public double Percent { get => _Percent; set { _Percent = value; OnPropertyChanged(); } }
    }

    public class CountBookBorrow : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private int _Score;
        public int Score { get => _Score; set { _Score = value; OnPropertyChanged(); } }
    }
}
