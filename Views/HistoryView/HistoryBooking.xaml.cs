using project_v3.Services;
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

namespace project_v3.Views
{
    /// <summary>
    /// Interaction logic for HistoryBooking.xaml
    /// </summary>
    public partial class HistoryBooking : Window
    {
        private HistoryBookingVM _viewModel;

        public HistoryBooking(int customerId)
        {
            InitializeComponent();
            _viewModel=new HistoryBookingVM();
            this.DataContext = _viewModel;
            LoadReservationHistory(customerId);
        }
        private async void LoadReservationHistory(int customerId)
        {
            var reservations = await _viewModel.GetReservationHistoryAsync(customerId);
            var rooms = await _viewModel.getRoomHistoryAsync(customerId);
            // Gán dữ liệu vào DataGrid
            var combinedData = reservations.Select(reservation => new
            {
                ReservationId = reservation.Id,
                CustomerId = reservation.CustomerId,
                // Tìm kiếm Room tương ứng với RoomId của Reservation
                Room = rooms.FirstOrDefault(room => room.Id == reservation.RoomId),
                RoomNumber = rooms.FirstOrDefault(room => room.Id == reservation.RoomId)?.RoomNumber ?? "N/A",
                RoomType = rooms.FirstOrDefault(room => room.Id == reservation.RoomId)?.RoomType ?? "N/A",
                Price = rooms.FirstOrDefault(room => room.Id == reservation.RoomId)?.Price ?? 0,
                ReservationDate = reservation.ReservationDate,
                CheckInDay = reservation.CheckInDay,
                CheckOutDay = reservation.CheckOutDay,
                PaymentMethod = reservation.PaymentMethod
            }).ToList();
            ReservationDataGrid.ItemsSource = new[] { combinedData };
        }
    }
}

