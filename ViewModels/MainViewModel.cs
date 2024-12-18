using QLKS_CK.Models;
using QLKS_CK.Views;
using QLKS_CK.Views.CustomerManagementView;
using QLKS_CK.Views.HistoryView;
using QLKS_CK.Views.HotelInfoView;
using QLKS_CK.Views.PersonalInfo;
using QLKS_CK.Views.ReservationView;
using QLKS_CK.Views.RoomManagementView;
using QLKS_CK.Views.StatisticsView;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QLKS_CK.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private object currentView;
		public object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
		public ICommand SwitchViewCommand { get; set; }

        public MainViewModel()
        {
            CurrentView = new ReservationView(); 
            SwitchViewCommand = new RelayCommand(SwitchView);
        }

        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "RoomManagement":
                    CurrentView = new RoomManagementView();
                    break;
                case "Reservation":
                    CurrentView = new ReservationView();
                    break;
                case "ServiceManagement":
                    CurrentView = new ServiceManagementView();
                    break;
                case "CustomerManagement":
                    CurrentView = new CustomerManagementView();
                    break;
                case "Statistics":
                    CurrentView = new StatisticsView();
                    break;
                case "History":
                    CurrentView = new HistoryView();
                    break;
                case "HotelInfo":
                    CurrentView = new HotelInfoView();
                    break;
                case "PersonalInfo":
                    CurrentView = new PersonalInfoView();
                    break;
                default:
                    CurrentView = null;
                    break;
            }
        }
    }

}
