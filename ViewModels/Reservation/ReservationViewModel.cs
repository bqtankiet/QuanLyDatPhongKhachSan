using QLKS_CK.Models;
using QLKS_CK.Views.BillView;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QLKS_CK.ViewModels.Reservation
{
    public class ReservationViewModel : BaseViewModel
    {
        public ICommand RoomClickCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

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

        private string _selectedTypeOption;
        public string SelectedTypeOption
        {
            get => _selectedTypeOption;
            set
            {
                if (_selectedTypeOption != value)
                {
                    _selectedTypeOption = value;
                    OnPropertyChanged(); 
                    FilterRooms();
                }
            }
        }
        
        private string _selectedStatusOption;
        public string SelectedStatusOption
        {
            get => _selectedStatusOption;
            set
            {
                if (_selectedStatusOption != value)
                {
                    _selectedStatusOption = value;
                    OnPropertyChanged();
                    FilterRooms();
                }
            }
        }

        private string _searchQuery;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set 
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    FilterRooms();
                }
            }
        }


        public ReservationViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;
            LoadRoomData();
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
            if (clickedRoom != null && clickedRoom.RoomStatus == "Trống")
            {
                _sharedViewModel.SelectedRoom = clickedRoom; 
                SelectedRoom = clickedRoom; 
            }
            else
            {
                MessageBox.Show("Phòng đã được đặt hoặc đang sử dụng", "Thông báo!");
            }
        }

        private void FilterRooms()
        {
            var filteredRooms = AllRooms;

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                {
                    filteredRooms = new ObservableCollection<Room>(filteredRooms.Where(r =>
                        r.RoomNumber.IndexOf(SearchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        r.RoomType.IndexOf(SearchQuery, StringComparison.OrdinalIgnoreCase) >= 0));
                }
            }

            // Lọc theo loại phòng đã chọn
            if (!string.IsNullOrEmpty(SelectedTypeOption) && SelectedTypeOption != "Tất cả")
            {
                filteredRooms = new ObservableCollection<Room>(filteredRooms.Where(r => r.RoomType == SelectedTypeOption));
            }

            // Lọc theo trạng thái phòng đã chọn
            if (!string.IsNullOrEmpty(SelectedStatusOption) && SelectedStatusOption != "Tất cả")
            {
                filteredRooms = new ObservableCollection<Room>(filteredRooms.Where(r => r.RoomStatus == SelectedStatusOption));
            }

            RoomList = filteredRooms;
            OnPropertyChanged(nameof(RoomList));
        }


        private void LoadRoomData()
        {
            AllRooms = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.ToList());
        }
    }
}
