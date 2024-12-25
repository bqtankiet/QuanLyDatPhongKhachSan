using QLKS_CK.Models;
using QLKS_CK.Views.BillView;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QLKS_CK.ViewModels.Reservation
{
    public class ReservationViewModel : BaseViewModel
    {
        private readonly SharedViewModel _sharedViewModel;

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set 
            { 
                selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public ICommand RoomClickCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private ObservableCollection<Room> _allRooms;
        public ObservableCollection<Room> AllRooms
        {
            get => _allRooms;
            set {  _allRooms = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Room> _roomList;
        public ObservableCollection<Room> RoomList
        {
            get { return _roomList; }
            set { _roomList = value; OnPropertyChanged(); }
        }

        private string _selectedSortOption;
        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                if (_selectedSortOption != value)
                {
                    _selectedSortOption = value;
                    OnPropertyChanged(); 
                    SortRooms(); 
                }
            }
        }

        public ReservationViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;
            LoadData();
            RoomList = AllRooms;
            RoomClickCommand = new RelayCommand(RoomClick);
            ConfirmCommand = new RelayCommand(OpenBillView);
        }

        private void OpenBillView(object obj)
        {
            var billView = new BillView(_sharedViewModel);
            billView.Show();
        }


        private void RoomClick(object obj)
        {
            var clickedRoom = obj as Room;
            if (clickedRoom != null && clickedRoom.Status == "Trống")
            {
                _sharedViewModel.SelectedRoom = clickedRoom; 
                SelectedRoom = clickedRoom; 
            }
            else
            {
                MessageBox.Show("Phòng đã được đặt hoặc đang sử dụng", "Thông báo!");
            }
        }
        private void SortRooms()
        {
            switch (SelectedSortOption)
            {
                case "Phòng trống":
                    RoomList = new ObservableCollection<Room>(AllRooms.Where(r => r.Status == "Trống"));
                    break;
                case "Phòng đơn":
                    RoomList = new ObservableCollection<Room>(AllRooms.Where(r => r.RoomType == "Phòng đơn"));
                    break;
                case "Phòng đôi":
                    RoomList = new ObservableCollection<Room>(AllRooms.Where(r => r.RoomType == "Phòng đôi"));
                    break;
                case "Phòng gia đình":
                    RoomList = new ObservableCollection<Room>(AllRooms.Where(r => r.RoomType == "Phòng gia đình"));
                    break;
                case "Phòng VIP":
                    RoomList = new ObservableCollection<Room>(AllRooms.Where(r => r.RoomType == "Phòng vip"));
                    break;
                case "Tất cả":
                    RoomList = new ObservableCollection<Room>(AllRooms);
                    break;
                default:
                    break;
            }

            OnPropertyChanged(nameof(RoomList));
        }


        private void LoadData()
        {
            AllRooms = new ObservableCollection<Room>();
            AllRooms.Add(new Room() { Id = 1, RoomNumber = "101", RoomType = "Phòng đơn", Price = 100000, Capacity = "1", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 2, RoomNumber = "102", RoomType = "Phòng vip", Price = 10000000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 3, RoomNumber = "103", RoomType = "Phòng đơn", Price = 100000, Capacity = "1", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 4, RoomNumber = "104", RoomType = "Phòng đôi", Price = 150000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 5, RoomNumber = "105", RoomType = "Phòng đơn", Price = 120000, Capacity = "1", Status = "Đã đặt" });
            AllRooms.Add(new Room() { Id = 6, RoomNumber = "106", RoomType = "Phòng vip", Price = 20000000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 7, RoomNumber = "107", RoomType = "Phòng gia đình", Price = 500000, Capacity = "4", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 8, RoomNumber = "108", RoomType = "Phòng đôi", Price = 180000, Capacity = "2", Status = "Đã đặt" });
            AllRooms.Add(new Room() { Id = 9, RoomNumber = "109", RoomType = "Phòng đơn", Price = 90000, Capacity = "1", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 10, RoomNumber = "110", RoomType = "Phòng vip", Price = 25000000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 11, RoomNumber = "111", RoomType = "Phòng đơn", Price = 110000, Capacity = "1", Status = "Đã đặt" });
            AllRooms.Add(new Room() { Id = 12, RoomNumber = "112", RoomType = "Phòng gia đình", Price = 700000, Capacity = "4", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 13, RoomNumber = "113", RoomType = "Phòng vip", Price = 30000000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 14, RoomNumber = "114", RoomType = "Phòng đôi", Price = 170000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 15, RoomNumber = "115", RoomType = "Phòng đơn", Price = 130000, Capacity = "1", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 16, RoomNumber = "116", RoomType = "Phòng gia đình", Price = 600000, Capacity = "4", Status = "Đã đặt" });
            AllRooms.Add(new Room() { Id = 17, RoomNumber = "117", RoomType = "Phòng vip", Price = 22000000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 18, RoomNumber = "118", RoomType = "Phòng đôi", Price = 160000, Capacity = "2", Status = "Trống" });
            AllRooms.Add(new Room() { Id = 19, RoomNumber = "119", RoomType = "Phòng đơn", Price = 95000, Capacity = "1", Status = "Đã đặt" });
            AllRooms.Add(new Room() { Id = 20, RoomNumber = "120", RoomType = "Phòng gia đình", Price = 750000, Capacity = "4", Status = "Trống" });
        }
    }
}
