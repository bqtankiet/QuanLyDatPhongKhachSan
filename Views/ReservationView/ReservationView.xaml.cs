using QLKS_CK.ViewModels;
using QLKS_CK.ViewModels.BillViewModel;
using QLKS_CK.ViewModels.Reservation;
using System.Windows.Controls;


namespace QLKS_CK.Views.ReservationView
{

    public partial class ReservationView : UserControl
    {
        private readonly SharedViewModel _sharedViewModel;
        public ReservationView()
        {
            InitializeComponent();
            _sharedViewModel = new SharedViewModel(); // Tạo SharedViewModel 1 lần duy nhất
            var viewmodel = new ReservationViewModel(_sharedViewModel);
            this.DataContext = viewmodel;
        }
    }
    
}
