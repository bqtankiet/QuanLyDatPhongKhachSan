using QLKS_CK.ViewModels;
using QLKS_CK.ViewModels.BillViewModel;
using System.Windows;

namespace QLKS_CK.Views.BillView
{
 
    public partial class BillView : Window
    {
        public BillView(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            this.DataContext = new BillViewModel(sharedViewModel);
        }
  
    }
}
