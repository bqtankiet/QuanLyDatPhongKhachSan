using QLKS_CK.Views.BillView;
using QLKS_CK.Views.ReservationView.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLKS_CK.ViewModels.Reservation.InfoCustomerViewModel
{
    public class InfoCustomerViewModel : BaseViewModel
    {
        public ICommand ConfirmCommnad { get; set; }

        InfoCustomerView infoCustomerView;
        public InfoCustomerViewModel(InfoCustomerView infoCustomerView)
        {
            this.infoCustomerView = infoCustomerView;
            ConfirmCommnad = new RelayCommand(OpenBillView);
        }

        private void OpenBillView(object obj)
        {
           var billView = new BillView();
           billView.Show();

        }
    }
}
