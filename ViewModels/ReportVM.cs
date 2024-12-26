using LiveCharts.Wpf;
using LiveCharts;
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
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace project_v3.Services
{
    

    public class ReportVM
    {
        
        private readonly AppDbContext _context;

        public ObservableCollection<Room> Rooms { get; set; }

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
                

        // Phương thức để thông báo khi thuộc tính thay đổi
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            MessageBox.Show($"Property changed: {propertyName} đã thay đổi");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReportVM()
        {
            _context = new AppDbContext(); // Khởi tạo DbContext
            Rooms = new ObservableCollection<Room>();

        }
        //lấy danh sách phòng
        public async Task<List<Room>> GetRoomsAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Truy vấn dữ liệu từ bảng Room bằng LINQ
                    List<Room> rooms = await context.Rooms.ToListAsync();

                    // Cập nhật vào ObservableCollection nếu cần
                    Rooms.Clear();  // Xóa danh sách hiện tại (nếu có)
                    foreach (var room in rooms)
                    {
                        Rooms.Add(room);  // Thêm các phòng vào ObservableCollection
                    }

                    return rooms;
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi khi có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi lấy dữ liệu phòng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Room>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }
        //lấy số phòng trống
        public async Task<int> GetEmptyRoomsAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Đếm số phòng có trạng thái "Available"
                    int emptyRooms = await context.Rooms.CountAsync(r => r.Status == "Available");
                    return emptyRooms;
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi lấy số lượng phòng trống: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0; // Trả về 0 nếu có lỗi
            }
        }
        //lấy số phòng đã đặt
        public async Task<int> GetBookedRoomsAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Đếm số phòng có trạng thái "Occupied"
                    int bookedRooms = await context.Rooms.CountAsync(r => r.Status == "Occupied");
                    return bookedRooms;
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi lấy số lượng phòng đã đặt: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0; // Trả về 0 nếu có lỗi
            }
        }
        //lấy số phòng ngừng hoạt động
        public async Task<int> GetOutOfServiceRoomsAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Đếm số phòng có trạng thái "Occupied"
                    int bookedRooms = await context.Rooms.CountAsync(r => r.Status == "OutOfService");
                    return bookedRooms;
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi lấy số lượng phòng đã ngừng hoạt động: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0; // Trả về 0 nếu có lỗi
            }
        }
        //lấy doanh thu theo ngày
        public async Task<List<KeyValuePair<string, double>>> GetRevenueByDayAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var revenueData = await context.Reservations
                        .GroupBy(r => r.ReservationDate.Date)
                        .Select(g => new KeyValuePair<string, double>(
                            g.Key.ToString("yyyy-MM-dd"),
                            g.Sum(r =>
                                context.Rooms.Where(room => room.Id == r.RoomId).Select(room => room.Price).FirstOrDefault()
                                + context.ServiceUsages.Where(su => su.ReservationId == r.Id)
                                    .Join(context.Services, su => su.ServiceId, s => s.Id, (su, s) => su.Quantity * s.Price)
                                    .Sum()
                            )
                        ))
                        .ToListAsync();

                    return revenueData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy doanh thu theo ngày: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<KeyValuePair<string, double>>();
            }
        }

        //lấy doanh thu theo tháng
        public async Task<List<KeyValuePair<string, double>>> GetRevenueByMonthAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var revenueData = await context.Reservations
                        .GroupBy(r => r.ReservationDate.Month)
                        .Select(g => new KeyValuePair<string, double>(
                            g.Key.ToString(),
                            g.Sum(r =>
                                context.Rooms.Where(room => room.Id == r.RoomId).Select(room => room.Price).FirstOrDefault()
                                + context.ServiceUsages.Where(su => su.ReservationId == r.Id)
                                    .Join(context.Services, su => su.ServiceId, s => s.Id, (su, s) => su.Quantity * s.Price)
                                    .Sum()
                            )
                        ))
                        .ToListAsync();

                    return revenueData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy doanh thu theo tháng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<KeyValuePair<string, double>>();
            }
        }
        //lấy doanh thu theo năm
        public async Task<List<KeyValuePair<string, double>>> GetRevenueByYearAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var revenueData = await context.Reservations
                        .GroupBy(r => r.ReservationDate.Year)
                        .Select(g => new KeyValuePair<string, double>(
                            g.Key.ToString(),
                            g.Sum(r =>
                                context.Rooms.Where(room => room.Id == r.RoomId).Select(room => room.Price).FirstOrDefault()
                                + context.ServiceUsages.Where(su => su.ReservationId == r.Id)
                                    .Join(context.Services, su => su.ServiceId, s => s.Id, (su, s) => su.Quantity * s.Price)
                                    .Sum()
                            )
                        ))
                        .ToListAsync();

                    return revenueData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy doanh thu theo năm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<KeyValuePair<string, double>>();
            }
        }
        // Lấy tổng số lượt đặt phòng
        public async Task<int> GetTotalReservationsAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    return await context.Reservations.CountAsync(); // Đếm số lượt đặt phòng từ bảng Reservations
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy tổng số lượt đặt phòng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0; // Trả về 0 nếu có lỗi
            }
        }
        //
        // Lấy tổng số khách hàng
        public async Task<int> GetTotalCustomersAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    return await context.Customers.CountAsync(); // Đếm số khách hàng từ bảng Customers
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy tổng số khách hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0; // Trả về 0 nếu có lỗi
            }
        }





        /*
        private readonly AppDbContext _context;
        public RoomDAO(AppDbContext context)
        {
            _context = context;
        }
        //lấy số lượng phòng trống và đã đặt
        public (int EmptyRooms, int BookedRooms) GetRoomStatus()
        {
            int emptyRooms = 0;
            int bookedRooms = 0;

            DatabaseConnection conn = new DatabaseConnection();
            using (var connection = conn.GetConnection())
            {
                connection.Open();

                // Query để lấy số lượng phòng trống và đã đặt
                string query = @"
                SELECT 
                    COUNT(CASE WHEN Status = 'trong' THEN 1 END) AS EmptyRooms,
                    COUNT(CASE WHEN Status = 'da_dat' THEN 1 END) AS BookedRooms
                FROM Room;
            ";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            emptyRooms = reader.GetInt32(reader.GetOrdinal("EmptyRooms"));
                            bookedRooms = reader.GetInt32(reader.GetOrdinal("BookedRooms"));
                        }
                    }
                }
            }

            return (emptyRooms, bookedRooms);
        }
        //lấy danh sách phòng
        public async Task<List<Room>> GetRoomsAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Truy vấn dữ liệu từ bảng Room bằng LINQ
                    List<Room> rooms = await context.Rooms.ToListAsync();

                    return rooms;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu phòng: {ex.Message}");
                return new List<Room>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }

        //thêm phòng
        public async Task<bool> AddRoomAsync(Room room)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Thêm phòng mới vào DbSet Rooms
                    context.Rooms.Add(room);

                    // Lưu thay đổi vào cơ sở dữ liệu một cách bất đồng bộ
                    int rowCount = await context.SaveChangesAsync();

                    return rowCount > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật thông tin phòng
        public async Task<bool> UpdateRoomAsync(Room room)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Tìm phòng cần cập nhật từ cơ sở dữ liệu
                    var existingRoom = await context.Rooms.FirstOrDefaultAsync(r => r.RoomId == room.RoomId);

                    if (existingRoom != null)
                    {
                        // Cập nhật các trường của phòng
                        existingRoom.RoomNumber = room.RoomNumber;
                        existingRoom.RoomType = room.RoomType;
                        existingRoom.PricePerNight = room.PricePerNight;
                        existingRoom.Status = room.Status;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        int rowCount = await context.SaveChangesAsync();
                        return rowCount > 0;
                    }
                    return false; // Không tìm thấy phòng với RoomId tương ứng
                }
            }
            catch (Exception ex)
            {
                // Bạn có thể log hoặc hiển thị thông tin lỗi chi tiết ở đây
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Xóa phòng  theo ID
        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            try
            {
                using (var context = new AppDbContext()) // Khởi tạo DbContext
                {
                    // Lấy phòng cần xóa từ cơ sở dữ liệu
                    var room = await context.Rooms
                                             .FirstOrDefaultAsync(r => r.RoomId == roomId);

                    if (room != null)
                    {
                        // Nếu phòng tồn tại, xóa phòng
                        context.Rooms.Remove(room);
                        await context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                        return true;
                    }
                    else
                    {
                        // Nếu không tìm thấy phòng, trả về false
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Lỗi khi xóa phòng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        //lấy phòng theo id
        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            try
            {
                using (var context = new AppDbContext()) // Khởi tạo DbContext
                {
                    // Truy vấn bất đồng bộ để lấy phòng theo RoomId
                    Room room = await context.Rooms
                                              .FirstOrDefaultAsync(r => r.RoomId == roomId); // Dùng FirstOrDefaultAsync để lấy phòng đầu tiên hoặc null

                    return room; // Trả về phòng nếu tìm thấy, hoặc null nếu không có phòng với roomId đó
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Lỗi khi lấy thông tin phòng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // Trả về null nếu có lỗi xảy ra
            }
        }


        //lấy phòng trống
        public async Task<int> GetEmptyRoomsAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.Rooms.CountAsync(r => r.Status == "Available");
            }
        }

        //lấy phòng đã dặt
        public async Task<int> GetBookedRoomsAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.Rooms.CountAsync(r => r.Status == "Occupied");
            }
        }
*/

    }

}
        
