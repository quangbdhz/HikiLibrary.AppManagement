using Library_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Library_Management.UserControlLibrary
{
    class MyFilesViewModel : BaseViewModel
    {
        public ICommand OpenExplorerCommand { get; set; }

        //Resource Dictionary 
        ResourceDictionary dict = Application.LoadComponent(new Uri("Library_Management;component/ResourceXAML/icons.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;
        public ObservableCollection<GetFileDetails> getFileDetails
        {
            get
            {
                return new ObservableCollection<GetFileDetails> {
                    new GetFileDetails(){ FileThumb=(PathGeometry)dict["Pdf"], FileName="File 1", Fill="Red", FileProgram="Adobe Acrobat", ModifiedOn="12.01.2020", FileType=".pdf"},
                    new GetFileDetails(){ FileThumb=(PathGeometry)dict["Png"], FileName="File 2", Fill="Green", FileProgram="Photo Viewer", ModifiedOn="18.02.2020", FileType=".png"},
                    new GetFileDetails(){ FileThumb=(PathGeometry)dict["txt"], FileName="File 3", Fill="CadetBlue", FileProgram="Notepad", ModifiedOn="15.07.2020", FileType=".txt"},
                    new GetFileDetails(){ FileThumb=(PathGeometry)dict["Pdf"], FileName="File 4", Fill="Green", FileProgram="Adobe Acrobat", ModifiedOn="22.07.2020", FileType=".pdf"}
                };
            }
        }

        private ObservableCollection<GetFileDetails> _LvBook;
        public ObservableCollection<GetFileDetails> LvBook { get => _LvBook; set { _LvBook = value; OnPropertyChanged(); } }

        public MyFilesViewModel()
        {
            LvBook = new ObservableCollection<GetFileDetails>();
            LvBook.Add(new GetFileDetails() { FileThumb = (PathGeometry)dict["Pdf"], FileName = "File 1", Fill = "Red", FileProgram = "Adobe Acrobat", ModifiedOn = "12.01.2020", FileType = ".pdf" });

            OpenExplorerCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                //Process.Start(@"F:\Library_Management\Library_Management\Library_Management\bin\Debug\DataImageBook");
                MessageBox.Show("a");
            });
        }
    }

    class GetFileDetails
    {
        public PathGeometry FileThumb { get; set; }
        public string Fill { get; set; }
        public string FileName { get; set; }
        public string FileProgram { get; set; }
        public string ModifiedOn { get; set; }
        public string FileType { get; set; }
    }
}
