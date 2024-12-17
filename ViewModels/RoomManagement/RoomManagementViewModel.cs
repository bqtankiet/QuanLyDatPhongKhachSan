using QLKS_CK.Models;
using QLKS_CK.Views;
using QLKS_CK.Views.RoomManagementView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS_CK.ViewModels.RoomManagement
{
    public class RoomManagementViewModel : BaseViewModel
    {
        //private ObservableCollection<Room> roomList;

        //public ObservableCollection<Room> RoomList
        //{
        //    get { return roomList; }
        //    set { roomList = value; OnPropertyChanged(); }
        //}

        private RoomManagementView roomManagementView;

		public RoomManagementViewModel(RoomManagementView roomManagementView)
		{
			//LoadData();
			this.roomManagementView = roomManagementView;
        }

        //private void LoadData()
        //{
        //    RoomList = new ObservableCollection<Room>();
        //    RoomList.Add(new Room() { Id = 1, RoomNumber = "101", RoomType = "Phòng đơn", Price = "100000", Capacity = "1", Status = "Trống" });
        //    RoomList.Add(new Room() { Id = 2, RoomNumber = "102", RoomType = "Phòng vip", Price = "10000000", Capacity = "2", Status = "Trống" });
        //    RoomList.Add(new Room() { Id = 3, RoomNumber = "103", RoomType = "Phòng đơn", Price = "100000", Capacity = "1", Status = "Trống" });
        //}
    }

}
