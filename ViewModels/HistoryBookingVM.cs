using Microsoft.EntityFrameworkCore;
using Npgsql;
using project_v3.Database;
using project_v3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace project_v3.Services
{
    internal class HistoryBookingVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Phương thức để thông báo khi thuộc tính thay đổi
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly AppDbContext _context;

        public ObservableCollection<Reservation> Reservations { get; set; }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }
        public HistoryBookingVM()
        {
            _context = new AppDbContext(); // Khởi tạo DbContext
            Reservations = new ObservableCollection<Reservation>();
            
        }

        public async Task<List<Reservation>> GetReservationHistoryAsync(int customerId)
        {
            try
            {
                // Lấy danh sách đặt phòng của khách hàng với customerId
                var reservations = await _context.Reservations
                    .Where(r => r.CustomerId == customerId)
                    .OrderByDescending(r => r.ReservationDate)
                    .ToListAsync();

                // Kiểm tra nếu không có lịch sử đặt phòng
                if (reservations == null || !reservations.Any())
                {
                    MessageBox.Show("Không có lịch sử đặt phòng cho khách hàng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return new List<Reservation>();
                }

                // Trả về danh sách các đặt phòng
                return reservations;
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi lấy lịch sử đặt phòng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public async Task<List<Room>> getRoomHistoryAsync(int customerId)
        {
            try
            {
                // Lấy danh sách đặt phòng của khách hàng với customerId
                var rooms = await _context.Rooms
     .Where(r => r.Reservations.Any(res => res.CustomerId == customerId))
     .OrderByDescending(r => r.Reservations
                               .Where(res => res.CustomerId == customerId)
                               .Max(res => res.ReservationDate))
     .ToListAsync();

                // Kiểm tra nếu không có lịch sử đặt phòng
                if (rooms == null || !rooms.Any())
                {
                    MessageBox.Show("Không có lịch sử đặt phòng cho khách hàng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return new List<Room>();
                }

                // Trả về danh sách các đặt phòng
                return rooms;
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi lấy lịch sử đặt phòng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }



        /*
        private readonly AppDbContext _context;
        public BookingDAO(AppDbContext context)
        {
            _context = context;
        }
        //lấy doanh thu theo ngày
        public async Task<List<KeyValuePair<string, double>>> GetRevenueByDayAsync()
        {
            var revenueDataList = new List<KeyValuePair<string, double>>();

            using (var context = new AppDbContext())
            {
                var revenueData = await context.Bookings
                    .GroupBy(b => b.BookingDate.Date) // Nhóm theo ngày
                    .Select(g => new
                    {
                        Date = g.Key,
                        TotalRevenue = g.Sum(b => b.TotalPrice)
                    })
                    .OrderBy(r => r.Date)
                    .ToListAsync();

                foreach (var data in revenueData)
                {
                    revenueDataList.Add(new KeyValuePair<string, double>(data.Date.ToString("yyyy-MM-dd"), data.TotalRevenue));
                }
            }

            return revenueDataList;
        }

        //lấy doanh thu theo tháng
        public async Task<List<KeyValuePair<string, double>>> GetRevenueByMonthAsync()
        {
            var revenueDataList = new List<KeyValuePair<string, double>>();

            using (var context = new AppDbContext())
            {
                var revenueData = await context.Bookings
                    .GroupBy(b => b.BookingDate.Month) // Nhóm theo tháng
                    .Select(g => new
                    {
                        Month = g.Key,
                        TotalRevenue = g.Sum(b => b.TotalPrice)
                    })
                    .OrderBy(r => r.Month)
                    .ToListAsync();

                foreach (var data in revenueData)
                {
                    revenueDataList.Add(new KeyValuePair<string, double>(data.Month.ToString(), data.TotalRevenue));
                }
            }

            return revenueDataList;
        }













        //lấy doanh thu theo năm
        public async Task<List<KeyValuePair<string, double>>> GetRevenueByYearAsync()
        {
            var revenueDataList = new List<KeyValuePair<string, double>>();

            using (var context = new AppDbContext())
            {
                var revenueData = await context.Bookings
                    .GroupBy(b => b.BookingDate.Year) // Nhóm theo năm
                    .Select(g => new
                    {
                        Year = g.Key,
                        TotalRevenue = g.Sum(b => b.TotalPrice)
                    })
                    .OrderBy(r => r.Year)
                    .ToListAsync();

                foreach (var data in revenueData)
                {
                    revenueDataList.Add(new KeyValuePair<string, double>(data.Year.ToString(), data.TotalRevenue));
                }
            }

            return revenueDataList;
        }


        //lấy tổng số lượt đặt phòng 
        public async Task<int> GetTotalBookingsAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.Bookings.CountAsync();
            }
        }

*/
    }
}
