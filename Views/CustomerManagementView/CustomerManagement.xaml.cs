using project_v3.Models;
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
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : Window
    {
        CustomerManagementVM _viewModel;
        public CustomerManagement()
        {
            InitializeComponent();
            //  LoadCustomerData();
            _viewModel = new CustomerManagementVM();
            this.DataContext = _viewModel;
        }
        // Lấy danh sách khách hàng từ cơ sở dữ liệu
       /*
        private async void LoadCustomerData()
        {
            try
            {
                CustomerDAO cusDAO = new CustomerDAO(new AppDbContext());

                // Gọi phương thức bất đồng bộ để lấy danh sách khách hàng
                List<Customer> customers = await cusDAO.GetCustomersAsync();

                // Kiểm tra nếu danh sách rỗng hoặc null
                if (customers == null || customers.Count == 0)
                {
                    MessageBox.Show("Không có khách hàng nào trong cơ sở dữ liệu.");
                    return;
                }

                // Gán danh sách khách hàng vào DataGrid
                CustomerGrid.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
       */
        
        // Sự kiện Click của nút Thêm khách hàng
        private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerManagementVM viewModel = new CustomerManagementVM();
            AddCustomer addCustomerWindow = new AddCustomer()
            {
                Owner = this // Đặt chủ sở hữu là cửa sổ hiện tại
            };

            if (addCustomerWindow.ShowDialog() == true) // Hiển thị form thêm khách hàng
            {
                await _viewModel.LoadCustomersAsync();
                // Sau khi thêm thành công, tải lại danh sách khách hàng
               // LoadCustomerData();
            }
            else
            {
                //LoadCustomerData();
            }
        }
        
        // Sự kiện Command của nút Sửa
        private void EditCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            Customer customer = (Customer)clickedButton.Tag;

            if (customer == null)
            {
                MessageBox.Show("customer null");
                return;
            }

            EditCustomer editCustomerWindow = new EditCustomer(customer, _viewModel)
            {
                Owner = this // Đặt chủ sở hữu là cửa sổ hiện tại
            };

            if (editCustomerWindow.ShowDialog() == true) // Hiển thị form thêm khách hàng
            {
                // Sau khi thêm thành công, tải lại danh sách khách hàng
               // LoadCustomerData();
            }
            else
            {
                //LoadCustomerData();
            }
        }
        
        
        // Sự kiện Command của nút Xóa
        private async void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            Customer customer = (Customer)clickedButton.Tag;

            MessageBoxResult result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng: {customer.FullName}?", "Xác nhận", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //Xóa khách hàng từ cơ sở dữ liệu
                CustomerManagementVM viewModel = new CustomerManagementVM();
                bool isDeleted = await viewModel.DeleteCustomerAsync(customer.Id);
                if(isDeleted)
                MessageBox.Show($"Đã xóa khách hàng: {customer.FullName}");
                await _viewModel.LoadCustomersAsync();
               



            }
        }
        
        
        //nút lịch sử đặt phòng
        private void ViewHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int customerId = (int)clickedButton.Tag;
            HistoryBooking historyBooking = new HistoryBooking(customerId)
            {
                Owner = this // Đặt chủ sở hữu là cửa sổ hiện tại 

            };
            historyBooking.ShowDialog();
        }
       
    }
}
