using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Library_Management.Book
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private string _StringSubject;
        public string StringSubject { get => _StringSubject; set { _StringSubject = value; } }

        private int _CountString;
        public int CountString { get => _CountString; set { _CountString = value; } }

        public EditBookWindow()
        {
            InitializeComponent();
        }

        private void CB_Subject_Click(object sender, RoutedEventArgs e)
        {
            CountString++;
            if (((CheckBox)sender).IsChecked == true)
            {
                if (CountString == 1)
                {
                    StringSubject += ((CheckBox)sender).Content.ToString();

                    StringSubject = StringSubject.Replace(", ,", ",");
                }
                else
                {
                    StringSubject += ", " + ((CheckBox)sender).Content.ToString();

                    StringSubject = StringSubject.Replace(", ,", ",");
                }
                Text.Text = StringSubject;
            }
            else
            {
                StringSubject = StringSubject.Replace(((CheckBox)sender).Content.ToString(), "");

                StringSubject = StringSubject.Replace(", ,", ",");
                Text.Text = StringSubject;
            }

        }
    }
}
