using LiveCharts;
using LiveCharts.Wpf;
using Npgsql;
using project_v3.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


using System.Windows.Media;

namespace project_v3.Views
{
    public partial class Report : Window, INotifyPropertyChanged
    {
        private ReportVM _reportVM;
        public string _totalCustomers;
        public string _totalBookings;



        public string TotalCustomers
        {
            get { return _totalCustomers; }
            set
            {
                if (_totalCustomers != value)
                {
                    _totalCustomers = value;
                    OnPropertyChanged(nameof(TotalCustomers));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string TotalBookings
        {
            get { return _totalBookings; }
            set
            {
                if (_totalBookings != value)
                {
                    _totalBookings = value;
                    OnPropertyChanged(nameof(TotalBookings));
                }
            }
        }


        public Report()
        {
            InitializeComponent();

            _reportVM = new ReportVM(); // Khởi tạo ViewModel
            this.DataContext = this; // Gán ViewModel làm DataContext
          

            // Gọi phương thức LoadRoomStatus từ ViewModel để tải trạng thái phòng
            LoadRoomStatus();
            LoadCustomerAndBookingData();
        }
        // Phương thức để load trạng thái phòng từ Code-behind
        public async void LoadRoomStatus()
        {
            try
            {
                // Lấy số lượng phòng trống và đã đặt từ ReportVM
                int emptyRooms = await _reportVM.GetEmptyRoomsAsync(); // Lấy số phòng trống
                int bookedRooms = await _reportVM.GetBookedRoomsAsync(); // Lấy số phòng đã đặt
                int outOfServiceRooms = await _reportVM.GetOutOfServiceRoomsAsync();

                // Kiểm tra nếu không có dữ liệu
                if (emptyRooms == 0 && bookedRooms == 0)
                {
                    MessageBox.Show("Không có phòng nào trong hệ thống.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Xóa các dữ liệu cũ trên biểu đồ
                RoomChart.Series.Clear();

                // Thêm dữ liệu cho phòng trống
                RoomChart.Series.Add(new PieSeries
                {
                    Title = "Phòng trống",
                    Values = new ChartValues<double> { emptyRooms },
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y} phòng", // Hiển thị số lượng phòng
                    Fill = new SolidColorBrush(Color.FromRgb(0, 128, 0)) // Màu xanh cho phòng trống
                });

                // Thêm dữ liệu cho phòng đã đặt
                RoomChart.Series.Add(new PieSeries
                {
                    Title = "Phòng đã đặt",
                    Values = new ChartValues<double> { bookedRooms },
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y} phòng", // Hiển thị số lượng phòng
                    Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0)) // Màu đỏ cho phòng đã đặt
                });
                // Thêm dữ liệu cho phòng đã ngưng hoạt động
                RoomChart.Series.Add(new PieSeries
                {
                    Title = "Phòng ngưng hoạt động",
                    Values = new ChartValues<double> { outOfServiceRooms },
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y} phòng", // Hiển thị số lượng phòng
                    Fill = new SolidColorBrush(Color.FromRgb(255, 128, 0)) // Màu đỏ cho phòng đã đặt
                });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi tải trạng thái phòng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //hiển thị tổng số khách hàng và tổng số lượt đặt phòng
        public async void LoadCustomerAndBookingData()
        {
            

            // Lấy số liệu từ cơ sở dữ liệu
            int totalCustomersCount = await _reportVM.GetTotalCustomersAsync();
            int totalBookingsCount = await _reportVM.GetTotalReservationsAsync();
           

            // Cập nhật dữ liệu vào các thuộc tính
            TotalCustomers = totalCustomersCount.ToString();
            TotalBookings = totalBookingsCount.ToString();   
           

        }

        //sự hiện nút hiển thị biểu đồ
        private void ShowChartButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy loại thống kê từ ComboBox (Ngày, Tháng, Năm)
            var selectedItem = StatisticsTypeComboBox.SelectedItem as ComboBoxItem;
            string filterType = selectedItem?.Tag.ToString(); // "Day", "Month", or "Year"

            // Kiểm tra nếu filterType không null và gọi phương thức LoadChartData để cập nhật biểu đồ
            if (!string.IsNullOrEmpty(filterType))
            {
                // Gọi phương thức LoadChartData để tải dữ liệu và cập nhật biểu đồ
                LoadChartData(filterType);
            }
        }
        //dữ liệu hiển thị biểu đồ
        public async void LoadChartData(string filterType)
        {
           
            List<KeyValuePair<string, double>> data = null;

            // Lấy dữ liệu theo loại thống kê (Ngày, Tháng, Năm)
            if (filterType == "Day")
            {
                data = await _reportVM.GetRevenueByDayAsync();
            }
            else if (filterType == "Month")
            {
                data = await _reportVM.GetRevenueByMonthAsync();
            }
            else
            {
                data = await _reportVM.GetRevenueByYearAsync();
            }

            // Cập nhật dữ liệu cho DataGrid
            if (data != null && data.Any())
            {
                // Thiết lập dữ liệu cho ColumnSeries
                var columnSeries = new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double>(data.Select(d => d.Value))
                };

                // Cập nhật dữ liệu cho biểu đồ
                RevenueChart.Series = new SeriesCollection { columnSeries };

                // Cập nhật nhãn trục X
                RevenueChart.AxisX[0].Labels = data.Select(d => d.Key).ToList();
            }
        }
    }
    /*
     public SeriesCollection SeriesCollection { get; set; }


    public List<string> Labels { get; set; }
    public SeriesCollection ColumnValues { get; set; }
    public int EmptyRooms { get; set; }
    public int BookedRooms { get; set; }
   // public string TotalCustomers { get; set; }
    //public string TotalBookings {  get; set; }

    // Sự kiện thông báo thay đổi thuộc tính
    public event PropertyChangedEventHandler PropertyChanged;

    // Phương thức thông báo thay đổi
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string _totalCustomers;
    public string TotalCustomers
    {
        get { return _totalCustomers; }
        set
        {
            if (_totalCustomers != value)
            {
                _totalCustomers = value;
                OnPropertyChanged(nameof(TotalCustomers));  // Thông báo khi thay đổi
            }
        }
    }

    private string _totalBookings;
    public string TotalBookings
    {
        get { return _totalBookings; }
        set
        {
            if (_totalBookings != value)
            {
                _totalBookings = value;
                OnPropertyChanged(nameof(TotalBookings));  // Thông báo khi thay đổi
            }
        }
    }

    public Report()
    {
        InitializeComponent();
        DataContext = this;
        LoadRoomStatus();
       // LoadCustomerAndBookingData();

    }
    */




    //Lấy tổng số khách hàng và tổng số lượt đặt phòng


    
    // Phương thức cập nhật dữ liệu cho biểu đồ doanh thu
       
    
}


