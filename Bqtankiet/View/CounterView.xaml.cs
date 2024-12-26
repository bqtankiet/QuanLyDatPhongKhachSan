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
using QLKS_CK.Bqtankiet.ViewModel;

namespace QLKS_CK.Bqtankiet.View
{
    /// <summary>
    /// Interaction logic for AAAView.xaml
    /// </summary>
    public partial class AAAView : Window
    {
        private int clickCount = 0;

        public AAAView()
        {
            InitializeComponent();
            //DataContext = new AAAViewModel();
            //MyTextBox.Text = "0";
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    clickCount++;
        //    MyTextBox.Text = $"{clickCount}";
        //}
    }
}
