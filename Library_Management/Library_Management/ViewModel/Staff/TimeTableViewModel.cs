using Library_Management.Model;
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
using Word = Microsoft.Office.Interop.Word;


namespace Library_Management.ViewModel.Staff
{
    public class TimeTableViewModel : LoginViewModel
    {
        #region Init List Data
        private ObservableCollection<Model.TimeTable> _CheckIdTimeTableManager;
        public ObservableCollection<Model.TimeTable> CheckIdTimeTableManager { get => _CheckIdTimeTableManager; set { _CheckIdTimeTableManager = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.TimeTable> _LvTimeTableManager;
        public ObservableCollection<Model.TimeTable> LvTimeTableManager { get => _LvTimeTableManager; set { _LvTimeTableManager = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.TimeTable> _CheckIdTimeTableLibrarian;
        public ObservableCollection<Model.TimeTable> CheckIdTimeTableLibrarian { get => _CheckIdTimeTableLibrarian; set { _CheckIdTimeTableLibrarian = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.TimeTable> _LvTimeTableLibrarian;
        public ObservableCollection<Model.TimeTable> LvTimeTableLibrarian { get => _LvTimeTableLibrarian; set { _LvTimeTableLibrarian = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.TimeTable> _CheckIdTimeTableArranger;
        public ObservableCollection<Model.TimeTable> CheckIdTimeTableArranger { get => _CheckIdTimeTableArranger; set { _CheckIdTimeTableArranger = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.TimeTable> _LvTimeTableArranger;
        public ObservableCollection<Model.TimeTable> LvTimeTableArranger { get => _LvTimeTableArranger; set { _LvTimeTableArranger = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.UserStaff> _LvUserManager;
        public ObservableCollection<Model.UserStaff> LvUserManager { get => _LvUserManager; set { _LvUserManager = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.UserStaff> _LvUserLibrarian;
        public ObservableCollection<Model.UserStaff> LvUserLibrarian { get => _LvUserLibrarian; set { _LvUserLibrarian = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.UserStaff> _LvUserArranger;
        public ObservableCollection<Model.UserStaff> LvUserArranger { get => _LvUserArranger; set { _LvUserArranger = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.HistoryCreateTimeTable> _LvHistory;
        public ObservableCollection<Model.HistoryCreateTimeTable> LvHistory { get => _LvHistory; set { _LvHistory = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.TimeLine> _LvTimeLine;
        public ObservableCollection<Model.TimeLine> LvTimeLine { get => _LvTimeLine; set { _LvTimeLine = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.TimeTable> _LvTimeTableManagerStaffLogin;
        public ObservableCollection<Model.TimeTable> LvTimeTableManagerStaffLogin { get => _LvTimeTableManagerStaffLogin; set { _LvTimeTableManagerStaffLogin = value; OnPropertyChanged(); } }

        #endregion

        #region Init Variable Manager
        private int _IdListManager;
        public int IdListManager { get => _IdListManager; set { _IdListManager = value; OnPropertyChanged(); } }

        private string _DisplayNameStaffWorkManager;
        public string DisplayNameStaffWorkManager { get => _DisplayNameStaffWorkManager; set { _DisplayNameStaffWorkManager = value; OnPropertyChanged(); } }

        private bool _CheckWork;
        public bool CheckWork { get => _CheckWork; set { _CheckWork = value; OnPropertyChanged(); } }

        private bool _CheckNoWork;
        public bool CheckNoWork { get => _CheckNoWork; set { _CheckNoWork = value; OnPropertyChanged(); } }

        private bool _CheckGoLate;
        public bool CheckGoLate { get => _CheckGoLate; set { _CheckGoLate = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Librarian
        private int _IdListLibrarian;
        public int IdListLibrarian { get => _IdListLibrarian; set { _IdListLibrarian = value; OnPropertyChanged(); } }

        private string _DisplayNameStaffWorkLibrarian;
        public string DisplayNameStaffWorkLibrarian { get => _DisplayNameStaffWorkLibrarian; set { _DisplayNameStaffWorkLibrarian = value; OnPropertyChanged(); } }

        private bool _CheckWorkLibrarian;
        public bool CheckWorkLibrarian { get => _CheckWorkLibrarian; set { _CheckWorkLibrarian = value; OnPropertyChanged(); } }

        private bool _CheckNoWorkLibrarian;
        public bool CheckNoWorkLibrarian { get => _CheckNoWorkLibrarian; set { _CheckNoWorkLibrarian = value; OnPropertyChanged(); } }

        private bool _CheckGoLateLibrarian;
        public bool CheckGoLateLibrarian { get => _CheckGoLateLibrarian; set { _CheckGoLateLibrarian = value; OnPropertyChanged(); } }
        #endregion
        
        #region Init Variable Arranger
        private int _IdListArranger;
        public int IdListArranger { get => _IdListArranger; set { _IdListArranger = value; OnPropertyChanged(); } }

        private string _DisplayNameStaffWorkArranger;
        public string DisplayNameStaffWorkArranger { get => _DisplayNameStaffWorkArranger; set { _DisplayNameStaffWorkArranger = value; OnPropertyChanged(); } }

        private bool _CheckWorkArranger;
        public bool CheckWorkArranger { get => _CheckWorkArranger; set { _CheckWorkArranger = value; OnPropertyChanged(); } }

        private bool _CheckNoWorkArranger;
        public bool CheckNoWorkArranger { get => _CheckNoWorkArranger; set { _CheckNoWorkArranger = value; OnPropertyChanged(); } }

        private bool _CheckGoLateArranger;
        public bool CheckGoLateArranger { get => _CheckGoLateArranger; set { _CheckGoLateArranger = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable
        private int _IdTimeLine;
        public int IdTimeLine { get => _IdTimeLine; set { _IdTimeLine = value; OnPropertyChanged(); } }

        private string _TimeWork;
        public string TimeWork { get => _TimeWork; set { _TimeWork = value; OnPropertyChanged(); } }

        private string _DateStartTimeTable;
        public string DateStartTimeTable { get => _DateStartTimeTable; set { _DateStartTimeTable = value; OnPropertyChanged(); } }

        private string _EndDateTimeTable;
        public string EndDateTimeTable { get => _EndDateTimeTable; set { _EndDateTimeTable = value; OnPropertyChanged(); } }
        #endregion

        private Visibility _OptionVisibilityCheckIn;
        public Visibility OptionVisibilityCheckIn { get => _OptionVisibilityCheckIn; set { _OptionVisibilityCheckIn = value; OnPropertyChanged(); } }

        #region Command Manager
        public ICommand AddTimeTableManager { get; set; }
        public ICommand OuputManagerWordCommand { get; set; }
        public ICommand CheckWorkCommand { get; set; }
        public ICommand CheckNoWorkCommand { get; set; }
        public ICommand ChecGoLateCommand { get; set; }
        public ICommand CheckInManagerCommand { get; set; }
        #endregion

        #region Command Librarian
        public ICommand AddTimeTableLibrarian { get; set; }
        public ICommand CheckWorkLibrarianCommand { get; set; }
        public ICommand CheckNoWorkLibrarianCommand { get; set; }
        public ICommand ChecGoLateLibrarianCommand { get; set; }
        public ICommand CheckInLibrarianCommand { get; set; }
        public ICommand OuputLibrarianWordCommand { get; set; }

        #endregion

        #region Command Arranger
        public ICommand AddTimeTableArranger { get; set; }
        public ICommand CheckWorkArrangerCommand { get; set; }
        public ICommand CheckNoWorkArrangerCommand { get; set; }
        public ICommand ChecGoLateArrangerCommand { get; set; }
        public ICommand CheckInArrangerCommand { get; set; }
        public ICommand OuputArrangerWordCommand { get; set; }
        #endregion


        public TimeTableViewModel()
        {
            //Arranger
            #region
            try
            {
                CheckIdTimeTableManager = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 1));
                var checkIdManager = CheckIdTimeTableManager.LastOrDefault();
                if(checkIdManager != null)
                {
                    IdListManager = checkIdManager.Id;
                    LvTimeTableManager = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 1 && x.Id > checkIdManager.Id - 3));
                    
                    if(RuleLogin == 1)
                    {
                        //LvTimeTableManagerStaffLogin = new ObservableCollection<Model.TimeTable>();

                        //for (int i = 0; i < LvTimeTableManager.Count; i++)
                        //{
                        //    Model.TimeTable newTimeTableManager = new Model.TimeTable();

                        //    newTimeTableManager = LvTimeTableManager[i];

                        //    if (LvTimeTableManager[i].IdHumanSunday != IdStaff)
                        //    {
                        //        newTimeTableManager.IdHumanSunday = 0;
                        //        newTimeTableManager.Human3 = null;
                        //    }
                        //    if (LvTimeTableManager[i].IdHumanMonday != IdStaff)
                        //    {
                        //        newTimeTableManager.IdHumanMonday = 0;
                        //        newTimeTableManager.Human1 = null;
                        //    }
                        //    if (LvTimeTableManager[i].IdHumanTuesday != IdStaff)
                        //    {
                        //        newTimeTableManager.IdHumanTuesday = 0;
                        //        newTimeTableManager.Human5 = null;
                        //    }
                        //    if (LvTimeTableManager[i].IdHumanWednesday != IdStaff)
                        //    {
                        //        newTimeTableManager.IdHumanWednesday = 0;
                        //        newTimeTableManager.Human6 = null;
                        //    }
                        //    if (LvTimeTableManager[i].IdHumanThursday != IdStaff)
                        //    {
                        //        newTimeTableManager.IdHumanThursday = 0;
                        //        newTimeTableManager.Human4 = null;
                        //    }
                        //    if (LvTimeTableManager[i].IdHumanFriday != IdStaff)
                        //    {
                        //        newTimeTableManager.IdHumanFriday = 0;
                        //        newTimeTableManager.Human2 = null;
                        //    }
                        //    if (LvTimeTableManager[i].IdHumanSaturday != IdStaff)
                        //    {
                        //        newTimeTableManager.IdHumanSaturday = 0;
                        //        newTimeTableManager.Human = null;
                        //    }
                        //    LvTimeTableManagerStaffLogin.Add(newTimeTableManager);
                        //}
                    }
                }

                if(LvTimeTableManager != null)
                {
                    DateTime? getDayStartWeek = LvTimeTableManager[0].EndDate;
                    DateStartTimeTable = getDayStartWeek.Value.AddDays(-7).AddSeconds(1).ToString();
                    EndDateTimeTable = LvTimeTableManager[0].EndDate.ToString();
                }
                
                CheckIdTimeTableLibrarian = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 2));
                var checkIdLibrarian = CheckIdTimeTableLibrarian.LastOrDefault();
                if(checkIdLibrarian != null)
                {
                    IdListLibrarian = checkIdLibrarian.Id;
                    LvTimeTableLibrarian = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 2 && x.Id > checkIdLibrarian.Id - 3));

                }

                CheckIdTimeTableArranger = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 3));
                var checkIdArranger = CheckIdTimeTableArranger.LastOrDefault();
                if(checkIdArranger != null)
                {
                    IdListArranger = checkIdArranger.Id;
                    LvTimeTableArranger = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 3 && x.Id > checkIdArranger.Id - 3));
                }
                
            }
            catch
            {
                //MessageBox.Show("Time Table Not Enough Initialization");
            }

            LvUserManager = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 1));
            LvUserLibrarian = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 2));
            LvUserArranger = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 3));

            LvHistory = new ObservableCollection<Model.HistoryCreateTimeTable>(DataProvider.Ins.DB.HistoryCreateTimeTables);
            LvHistory = new ObservableCollection<HistoryCreateTimeTable>(LvHistory.Reverse());

            LvTimeLine = new ObservableCollection<Model.TimeLine>(DataProvider.Ins.DB.TimeLines.Where(x => x.CountDelete == 0));

            if (CheckTime(LvTimeLine) == true)
            {
                try
                {
                    DisplayNameStaffWorkManager = CheckInStaffWork(LvTimeTableManager, DisplayNameStaffWorkManager);
                    DisplayNameStaffWorkLibrarian = CheckInStaffWork(LvTimeTableLibrarian, DisplayNameStaffWorkLibrarian);
                    DisplayNameStaffWorkArranger = CheckInStaffWork(LvTimeTableArranger, DisplayNameStaffWorkArranger);
                }
                catch
                {

                }
                OptionVisibilityCheckIn = Visibility.Visible;
            }
            else
            {
                OptionVisibilityCheckIn = Visibility.Collapsed;
            }

            AddTimeTableManager = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                CreateTimeTable(LvUserManager, 1);

                try
                {
                    CheckIdTimeTableManager = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 1));
                    var checkIdManager = CheckIdTimeTableManager.LastOrDefault();
                    LvTimeTableManager = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 1 && x.Id > checkIdManager.Id - 3));

                    var getDayStartWeek = LvTimeTableManager[0].EndDate;
                    DateStartTimeTable = getDayStartWeek.AddDays(-7).AddSeconds(1).ToString();
                    EndDateTimeTable = LvTimeTableManager[0].EndDate.ToString();
                }
                catch
                {

                }

                MessageBox.Show("Successful");
            });

            AddTimeTableLibrarian = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                CreateTimeTable(LvUserLibrarian, 2);
                try
                {
                    CheckIdTimeTableLibrarian = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 2));
                    var checkIdLibrarian = CheckIdTimeTableLibrarian.LastOrDefault();
                    LvTimeTableLibrarian = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 2 && x.Id > checkIdLibrarian.Id - 3));

                    var getDayStartWeek = LvTimeTableLibrarian[0].EndDate;
                    DateStartTimeTable = getDayStartWeek.AddDays(-7).AddSeconds(1).ToString();
                    EndDateTimeTable = LvTimeTableLibrarian[0].EndDate.ToString();
                }
                catch
                {

                }
                MessageBox.Show("Successful");
            });

            AddTimeTableArranger = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                CreateTimeTable(LvUserArranger, 3);
                try
                {
                    CheckIdTimeTableArranger = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 3));
                    var checkIdArranger = CheckIdTimeTableArranger.LastOrDefault();
                    LvTimeTableArranger = new ObservableCollection<Model.TimeTable>(DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == 3 && x.Id > checkIdArranger.Id - 3));

                    var getDayStartWeek = LvTimeTableArranger[0].EndDate;
                    DateStartTimeTable = getDayStartWeek.AddDays(-7).AddSeconds(1).ToString();
                    EndDateTimeTable = LvTimeTableArranger[0].EndDate.ToString();
                }
                catch
                {

                }

                MessageBox.Show("Successful");
            });
            #endregion

            #region Manager
            CheckWorkCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckWork = (bool)p.IsChecked;
                if (CheckWork == true)
                {
                    CheckNoWork = !CheckWork;
                    CheckGoLate = !CheckWork;
                }
            });

            CheckNoWorkCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckNoWork = (bool)p.IsChecked;
                if (CheckNoWork == true)
                {
                    CheckWork = !CheckNoWork;
                    CheckGoLate = !CheckNoWork;
                }
            });

            ChecGoLateCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckGoLate = (bool)p.IsChecked;
                if (CheckGoLate == true)
                {
                    CheckNoWork = !CheckGoLate;
                    CheckWork = !CheckGoLate;
                }
            });

            CheckInManagerCommand = new RelayCommand<Object>((p) => {
                if (CheckWork == false && CheckNoWork == false && CheckGoLate == false)
                    return false;

                if (CheckStaffWork(LvTimeTableManager, IdTimeLine) == false)
                    return false;

                return true;
            }, (p) =>
            {
                string optionWork = "";
                if (CheckWork == true)
                {
                    optionWork = "Đi làm";
                }
                if (CheckNoWork == true)
                {
                    optionWork = "Đi trễ";
                }
                if (CheckGoLate == true)
                {
                    optionWork = "Không đi làm";
                }
                CheckIn(1, IdTimeLine, optionWork, IdListManager);


            });

            OuputManagerWordCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {

                string filePath = GetFilePathWord();

                int RowCount = LvTimeTableManager.Count();
                int ColumnCount = 11;
                string[] arrColumnHeader = { "Time Start", "Time End", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Date Init", "End Date" };
                string nameList = "TIME TABLE MANAGER";
                string headerWork = "MANAGER";
                OuputWord(RowCount, ColumnCount, arrColumnHeader, nameList, headerWork, 1, filePath, LvTimeTableManager);

            });
            #endregion

            #region Librarian
            CheckWorkLibrarianCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckWorkLibrarian = (bool)p.IsChecked;
                if (CheckWorkLibrarian == true)
                {
                    CheckNoWorkLibrarian = !CheckWorkLibrarian;
                    CheckGoLateLibrarian = !CheckWorkLibrarian;
                }
            });

            CheckNoWorkLibrarianCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckNoWorkLibrarian = (bool)p.IsChecked;
                if (CheckNoWorkLibrarian == true)
                {
                    CheckWorkLibrarian = !CheckNoWorkLibrarian;
                    CheckGoLateLibrarian = !CheckNoWorkLibrarian;
                }
            });

            ChecGoLateLibrarianCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckGoLateLibrarian = (bool)p.IsChecked;
                if (CheckGoLateLibrarian == true)
                {
                    CheckNoWorkLibrarian = !CheckGoLateLibrarian;
                    CheckWorkLibrarian = !CheckGoLateLibrarian;
                }
            });

            CheckInLibrarianCommand = new RelayCommand<Object>((p) => {
                if (CheckWorkLibrarian == false && CheckNoWorkLibrarian == false && CheckGoLateLibrarian == false)
                    return false;

                if (CheckStaffWork(LvTimeTableManager, IdTimeLine) == false)
                    return false;

                return true;
            }, (p) =>
            {
                string optionWork = "";
                if (CheckWorkLibrarian == true)
                {
                    optionWork = "Đi làm";
                }
                if (CheckNoWorkLibrarian == true)
                {
                    optionWork = "Đi trễ";
                }
                if (CheckGoLateLibrarian == true)
                {
                    optionWork = "Không đi làm";
                }
                CheckIn(2, IdTimeLine, optionWork, IdListLibrarian);


            });

            OuputLibrarianWordCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {

                string filePath = GetFilePathWord();

                int RowCount = LvTimeTableManager.Count();
                int ColumnCount = 11;
                string[] arrColumnHeader = { "Time Start", "Time End", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Date Init", "End Date" };
                string nameList = "TIME TABLE LIBRARIAN";
                string headerWork = "LIBRARIAN";
                OuputWord(RowCount, ColumnCount, arrColumnHeader, nameList, headerWork, 1, filePath, LvTimeTableLibrarian);

            });
            #endregion

            #region Arranger
            CheckWorkArrangerCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckWorkArranger = (bool)p.IsChecked;
                if (CheckWorkArranger == true)
                {
                    CheckNoWorkArranger = !CheckWorkArranger;
                    CheckGoLateArranger = !CheckWorkArranger;
                }
            });

            CheckNoWorkArrangerCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckNoWorkArranger = (bool)p.IsChecked;
                if (CheckNoWorkArranger == true)
                {
                    CheckWorkArranger = !CheckNoWorkArranger;
                    CheckGoLateArranger = !CheckNoWorkArranger;
                }
            });

            ChecGoLateArrangerCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckGoLateArranger = (bool)p.IsChecked;
                if (CheckGoLateArranger == true)
                {
                    CheckNoWorkArranger = !CheckGoLateArranger;
                    CheckWorkArranger = !CheckGoLateArranger;
                }
            });

            CheckInArrangerCommand = new RelayCommand<Object>((p) => {
                if (CheckWorkArranger == false && CheckNoWorkArranger == false && CheckGoLateArranger == false)
                    return false;

                if (CheckStaffWork(LvTimeTableManager, IdTimeLine) == false)
                    return false;

                return true;
            }, (p) =>
            {
                string optionWork = "";
                if (CheckWorkArranger == true)
                {
                    optionWork = "Đi làm";
                }
                if (CheckNoWorkArranger == true)
                {
                    optionWork = "Đi trễ";
                }
                if (CheckGoLateArranger == true)
                {
                    optionWork = "Không đi làm";
                }
                CheckIn(3, IdTimeLine, optionWork, IdListArranger);


            });

            OuputArrangerWordCommand = new RelayCommand<Button>((a) => { return true; }, (a) => {

                string filePath = GetFilePathWord();

                int RowCount = LvTimeTableManager.Count();
                int ColumnCount = 11;
                string[] arrColumnHeader = { "Time Start", "Time End", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Date Init", "End Date" };
                string nameList = "TIME TABLE ARRANGER";
                string headerWork = "ARRANGER";
                OuputWord(RowCount, ColumnCount, arrColumnHeader, nameList, headerWork, 1, filePath, LvTimeTableArranger);

            });
            #endregion

            
        }

        
        public void CreateTimeTable(ObservableCollection<Model.UserStaff> LvUser, int IdAuthority)
        {
            DateTime getDateTime = DateTime.Now;
            int countManager = LvUser.Count();

            List<HumanManager> ListHumanManagers = new List<HumanManager>();

            int countWork = 21 / countManager;

            if (countManager * countWork != 21)
            {
                int countRandom = 21 - countManager * countWork;

                int id = 1;
                foreach (UserStaff item in LvUser)
                {
                    HumanManager humanManager = new HumanManager();
                    humanManager.Id = id;
                    humanManager.IdHuman = item.IdHuman;
                    humanManager.CountWork = countWork;
                    humanManager.CountShiftsWorked = 0;
                    id++;

                    ListHumanManagers.Add(humanManager);
                }

                for (int i = 0; i < countRandom; i++)
                {
                    Random idHumanManager = new Random();
                    int idHumanRandom = idHumanManager.Next(0, countManager);

                    if (countWork == ListHumanManagers[idHumanRandom].CountWork)
                    {
                        ListHumanManagers[idHumanRandom].CountWork++;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            else
            {
                int id = 1;
                foreach (UserStaff item in LvUser)
                {
                    HumanManager humanManager = new HumanManager();
                    humanManager.Id = id;
                    humanManager.IdHuman = item.IdHuman;
                    humanManager.CountWork = countWork;
                    humanManager.CountShiftsWorked = 0;
                    id++;

                    ListHumanManagers.Add(humanManager);
                }

            }

            HumanManager[,] createHumanManager = new HumanManager[3, 7];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Random idHumanManager = new Random();
                    int idHumanRandom = idHumanManager.Next(0, countManager);
                    MessageBox.Show("IdHumanRandom: " + idHumanRandom.ToString());
                    if (i == 0)
                    {
                        if (ListHumanManagers[idHumanRandom].CountWork > ListHumanManagers[idHumanRandom].CountShiftsWorked)
                        {
                            createHumanManager[i, j] = ListHumanManagers[idHumanRandom];
                            ListHumanManagers[idHumanRandom].CountShiftsWorked++;
                        }
                        else
                        {
                            i = 0; j--;
                        }
                    }
                    else if (i == 1)
                    {
                        if (ListHumanManagers[idHumanRandom].CountWork > ListHumanManagers[idHumanRandom].CountShiftsWorked)
                        {
                            createHumanManager[i, j] = ListHumanManagers[idHumanRandom];
                            ListHumanManagers[idHumanRandom].CountShiftsWorked++;
                        }
                        else
                        {
                            i = 1; j--;
                        }
                    }
                    else
                    {
                        if (ListHumanManagers[idHumanRandom].CountWork > ListHumanManagers[idHumanRandom].CountShiftsWorked)
                        {
                            if (createHumanManager[0, j].Id == ListHumanManagers[idHumanRandom].Id && createHumanManager[1, j].Id == ListHumanManagers[idHumanRandom].Id)
                            {
                                i = 2; j = j - 1;
                            }
                            else
                            {
                                createHumanManager[i, j] = ListHumanManagers[idHumanRandom];
                                ListHumanManagers[idHumanRandom].CountShiftsWorked++;
                            }
                        }
                        else
                        {
                            i = 2; j--;
                        }
                    }
                }
            }

            DateTime getTimeInit = DateTime.Now;

            int addDay = 0;
            for (int i = 0; i < 7; i++)
            {
                if (getTimeInit.AddDays(i).DayOfWeek.ToString() == "Saturday")
                {
                    addDay = i;
                    break;
                }
            }

            DateTime endTime = getTimeInit.AddDays(addDay).AddHours(23 - getTimeInit.Hour).AddMinutes(59 - getTimeInit.Minute).AddSeconds(59 - getTimeInit.Second);

            for (int i = 0; i < 3; i++)
            {
                var TimeTableManager = new Model.TimeTable()
                {
                    IdAuthorityStaff = IdAuthority,
                    IdTimeLine = i + 1,
                    IdHumanSunday = createHumanManager[i, 0].IdHuman,
                    IdHumanMonday = createHumanManager[i, 1].IdHuman,
                    IdHumanTuesday = createHumanManager[i, 2].IdHuman,
                    IdHumanWednesday = createHumanManager[i, 3].IdHuman,
                    IdHumanThursday = createHumanManager[i, 4].IdHuman,
                    IdHumanFriday = createHumanManager[i, 5].IdHuman,
                    IdHumanSaturday = createHumanManager[i, 6].IdHuman,
                    InnitiatedDate = getTimeInit,
                    EndDate = endTime,
                    CountDelete = 0
                };

                DataProvider.Ins.DB.TimeTables.Add(TimeTableManager);
                DataProvider.Ins.DB.SaveChanges();

            }

            var getDisplayNameStaff = DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0 && x.Id == GetIdHuman).SingleOrDefault();
            string history = getDateTime.ToString() + " " + getDisplayNameStaff.DisplayName + " (" + IdStaff.ToString() + ") " + "đã khởi tạo lịch làm việc ";

            if (IdAuthority == 1)
            {
                history += "Manager";
            }
            else if (IdAuthority == 2)
            {
                history += "Librarian";
            }
            else
            {
                history += "Arrange";
            }

            var HistoryCreateTimeTable = new Model.HistoryCreateTimeTable() { IdUserStaff = IdStaff, DateCreate = getDateTime, DisplayName = history };
            DataProvider.Ins.DB.HistoryCreateTimeTables.Add(HistoryCreateTimeTable);
            DataProvider.Ins.DB.SaveChanges();

            LvHistory = new ObservableCollection<Model.HistoryCreateTimeTable>(DataProvider.Ins.DB.HistoryCreateTimeTables);
            LvHistory = new ObservableCollection<HistoryCreateTimeTable>(LvHistory.Reverse());
        }

        public string CheckInStaffWork(ObservableCollection<Model.TimeTable> LvTimeTable, string DisplayName)
        {
            IdTimeLine = 0;
            foreach (TimeLine item in LvTimeLine)
            {
                if (DateTime.Now.TimeOfDay >= item.TimeStart && item.EndTime >= DateTime.Now.TimeOfDay)
                {
                    TimeWork = item.TimeStart.ToString() + " - " + item.EndTime.ToString();
                    IdTimeLine = item.Id;
                    break;
                }
            }

            int getIdStaffWork = 0;

            if (LvTimeTable != null)
            {
                foreach (TimeTable item in LvTimeTable)
                {
                    if (item.IdTimeLine == IdTimeLine)
                    {
                        string day = DateTime.Now.DayOfWeek.ToString();

                        if (day == "Sunday")
                        {
                            getIdStaffWork = item.IdHumanSunday;
                        }
                        else if (day == "Monday")
                        {
                            getIdStaffWork = item.IdHumanMonday;
                        }
                        else if (day == "Tuesday")
                        {
                            getIdStaffWork = item.IdHumanTuesday;
                        }
                        else if (day == "Wednesday")
                        {
                            getIdStaffWork = item.IdHumanWednesday;
                        }
                        else if (day == "Thursday")
                        {
                            getIdStaffWork = item.IdHumanThursday;
                        }
                        else if (day == "Friday")
                        {
                            getIdStaffWork = item.IdHumanFriday;
                        }
                        else
                        {
                            getIdStaffWork = item.IdHumanSaturday;
                        }
                    }

                }
            }
            




            var getIdHuman = DataProvider.Ins.DB.UserStaffs.Where(x => x.IdHuman == getIdStaffWork).SingleOrDefault();

            var getDisplayNameStaffWork = DataProvider.Ins.DB.Humen.Where(x => x.Id == getIdHuman.IdHuman).SingleOrDefault();

            DisplayName = getDisplayNameStaffWork.DisplayName;
            return DisplayName;
        }

        public void CheckIn(int idAuthority, int idTimeLine, string option, int idlist)
        {
            var CheckIn = DataProvider.Ins.DB.TimeTables.Where(x => x.CountDelete == 0 && x.IdAuthorityStaff == idAuthority && x.Id == idlist - 3 + idTimeLine).SingleOrDefault();
            string day = DateTime.Now.DayOfWeek.ToString();
            if (day == "Sunday")
            {
                CheckIn.CheckIdHumanSundayWork = option;
            }
            else if (day == "Monday")
            {
                CheckIn.CheckIdHumanMondayWork = option;
            }
            else if (day == "Tuesday")
            {
                CheckIn.CheckIdHumanTuesdayWork = option;
            }
            else if (day == "Wednesday")
            {
                CheckIn.CheckIdHumanWednesdayWork = option;
            }
            else if (day == "Thursday")
            {
                CheckIn.CheckIdHumanThursdayWork = option;
            }
            else if (day == "Friday")
            {
                CheckIn.CheckIdHumanFridayWork = option;
            }
            else
            {
                CheckIn.CheckIdHumanSaturdayWork = option;
            }

            DataProvider.Ins.DB.SaveChanges();
        }

        public bool CheckTime(ObservableCollection<Model.TimeLine> LvTimeLine)
        {
            foreach (TimeLine item in LvTimeLine)
            {
                if (DateTime.Now.TimeOfDay >= item.TimeStart && item.EndTime >= DateTime.Now.TimeOfDay)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckStaffWork(ObservableCollection<Model.TimeTable> LvTimeTable, int IdTimeLine)
        {
            string day = DateTime.Now.DayOfWeek.ToString();

            TimeTable getTimeTable = LvTimeTable[IdTimeLine - 1];

            var idHumanStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.Id == IdStaff);

            if (day == "Sunday")
            {
                if (getTimeTable.IdHumanSunday == GetIdStaffHuman)
                    return true;
            }
            else if (day == "Monday")
            {
                if (getTimeTable.IdHumanMonday == GetIdStaffHuman)
                    return true;
            }
            else if (day == "Tuesday")
            {
                if (getTimeTable.IdHumanTuesday == GetIdStaffHuman)
                    return true;
            }
            else if (day == "Wednesday")
            {
                if (getTimeTable.IdHumanWednesday == GetIdStaffHuman)
                    return true;
            }
            else if (day == "Thursday")
            {
                if (getTimeTable.IdHumanThursday == GetIdStaffHuman)
                    return true;
            }
            else if (day == "Friday")
            {
                if (getTimeTable.IdHumanFriday == GetIdStaffHuman)
                    return true;
            }
            else
            {
                if (getTimeTable.IdHumanSaturday == GetIdStaffHuman)
                    return true;
            }

            return false;
        }

        public string GetFilePathWord()
        {
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "World | *.docx | Word 2003 | *.doc";

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return null;
            }
            return filePath;
        }

        public void OuputWord(int RowCount, int ColumnCount, string[] arrColumnHeader, string nameList, string headerWord, int option, string filePath, ObservableCollection<Model.TimeTable> LvTimeTable)
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


            foreach (TimeTable item in LvTimeTable)
            {
                oTemp = oTemp + item.TimeLine.TimeStart.ToString() + "\t" + item.TimeLine.EndTime.ToString() + "\t" + item.Human3.DisplayName + "\t" + item.Human1.DisplayName + "\t" +
                     item.Human5.DisplayName + "\t" + item.Human6.DisplayName + "\t" + item.Human4.DisplayName + "\t" + item.Human2.DisplayName + "\t" + item.Human.DisplayName + "\t" +
                     item.InnitiatedDate.ToString("dd'/'MM'/'yyyy") + "\t" + item.EndDate.ToString("dd'/'MM'/'yyyy") + "\t";
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
        
    }

    public class HumanManager : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private int _IdHuman;
        public int IdHuman { get => _IdHuman; set { _IdHuman = value; OnPropertyChanged(); } }

        private int _CountWork;
        public int CountWork { get => _CountWork; set { _CountWork = value; OnPropertyChanged(); } }

        private int _CountShiftsWorked;
        public int CountShiftsWorked { get => _CountShiftsWorked; set { _CountShiftsWorked = value; OnPropertyChanged(); } }
    }
}
