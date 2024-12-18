using QLKS_CK.ViewModels.Reservation;

using System.Windows.Controls;


namespace QLKS_CK.Views.ReservationView
{
   
    public partial class ReservationView : UserControl
    {
        public ReservationView()
        {
            InitializeComponent();
            ReservationViewModel viewModel = new ReservationViewModel(this);
            this.DataContext = viewModel;
        }
    }
}
