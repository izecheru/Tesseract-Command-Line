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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tesseract_OCR.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        public static DependencyProperty NumberOfProcessedFilesProperty = DependencyProperty.Register("NumberOfProcessedFiles",typeof(string), typeof(MainView));
        public string NumberOfFiles
        {
            get { return (string)GetValue(NumberOfProcessedFilesProperty); }
            set { SetValue(NumberOfProcessedFilesProperty, value); }
        }
        
        public static DependencyProperty TotalNumberOfFilesProperty = DependencyProperty.Register("TotalNumberOfFiles",typeof(string), typeof(MainView));
        public string TotalNumberOfFiles
        {
            get { return (string)GetValue(TotalNumberOfFilesProperty); }
            set { SetValue(TotalNumberOfFilesProperty, value); }
        }

    }
}
