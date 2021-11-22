using Library_Management.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_Management.Human
{
    /// <summary>
    /// Interaction logic for HumanWindow.xaml
    /// </summary>
    public partial class HumanWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Model.Human> _LvHuman;
        public ObservableCollection<Model.Human> LvHuman { get => _LvHuman; set { _LvHuman = value; } }


        private string _StringSearchHuman;
        public string StringSearchHuman { get => _StringSearchHuman; set { _StringSearchHuman = value; OnPropertyChanged(); } }

        public HumanWindow()
        {
            InitializeComponent();

            StringSearchHuman = "Full Name";

            LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen);
            Lv_Human.ItemsSource = LvHuman;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Human.ItemsSource);
            view.Filter = UserFilter;
        }

        private void ListViewScrollViewerHuman_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private bool UserFilter(object item)
        {

            if (String.IsNullOrEmpty(tb_human.Text))
                return true;
            else
            {
                if (StringSearchHuman == "Full Name")
                {
                    return ((item as Model.Human).DisplayName.IndexOf(tb_human.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (StringSearchHuman == "Authority")
                {
                    if (tb_human.Text.ToLower() == "student" || tb_human.Text.ToLower() == "s" || tb_human.Text.ToLower() == "st"
                         || tb_human.Text.ToLower() == "stu" || tb_human.Text.ToLower() == "stud" || tb_human.Text.ToLower() == "stude" || tb_human.Text.ToLower() == "studen")
                    {
                        return ((item as Model.Human).IdAuthorityHuman.ToString().IndexOf("1", StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else if (tb_human.Text.ToLower() == "teacher" || tb_human.Text.ToLower() == "t" || tb_human.Text.ToLower() == "tea"
                         || tb_human.Text.ToLower() == "teac" || tb_human.Text.ToLower() == "te" || tb_human.Text.ToLower() == "teach" || tb_human.Text.ToLower() == "teache")
                    {
                        return ((item as Model.Human).IdAuthorityHuman.ToString().IndexOf("2", StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else
                    {
                        return ((item as Model.Human).IdAuthorityHuman.ToString().IndexOf(tb_human.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }

                else
                {
                    if (tb_human.Text.ToLower() == "male" || tb_human.Text.ToLower() == "m" || tb_human.Text.ToLower() == "ma" || tb_human.Text.ToLower() == "mal")
                    {
                        return ((item as Model.Human).IdGender.ToString().IndexOf("1", StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else if (tb_human.Text.ToLower() == "female" || tb_human.Text.ToLower() == "femal" || tb_human.Text.ToLower() == "fema" || tb_human.Text.ToLower() == "fem" ||
                         tb_human.Text.ToLower() == "fe" || tb_human.Text.ToLower() == "f")
                    {
                        return ((item as Model.Human).IdGender.ToString().IndexOf("2", StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else
                    {
                        return ((item as Model.Human).IdGender.ToString().IndexOf(tb_human.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_Human.ItemsSource).Refresh();
        }

        private void OptionSearch_DropDownClosed(object sender, EventArgs e)
        {
            StringSearchHuman = this.OptionSearch.Text;
        }
    }
}
