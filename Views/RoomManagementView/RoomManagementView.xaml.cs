using QLKS_CK.ViewModels.RoomManagement;
using System.Windows.Controls;

namespace QLKS_CK.Views.RoomManagementView
{
   
    public partial class RoomManagementView : UserControl
    {
        public RoomManagementView()
        {
            InitializeComponent();
            RoomManagementViewModel viewModel = new RoomManagementViewModel(this);
            this.DataContext = viewModel;
        }
    }
}
