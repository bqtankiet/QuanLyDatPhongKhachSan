using project_v3.Models;
using project_v3.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System;
using project_v3.ViewModels;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace project_v3.Services
{
    public class CustomerManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Phương thức để thông báo khi thuộc tính thay đổi
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //private readonly AppDbContext _context;

        public ObservableCollection<Customer> Customers { get; set; }

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

       

        // Constructor
        public CustomerManagementVM()
        {
          //  _context = new AppDbContext(); // Khởi tạo DbContext
            Customers = new ObservableCollection<Customer>();
            _= LoadCustomersAsync();
        }

        // Load danh sách khách hàng
        public async Task LoadCustomersAsync()
        {
            try
            {
                using (var _context = new AppDbContext())
                {


                    var customers = await _context.Customers.ToListAsync();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Customers.Clear();
                        foreach (var customer in customers)
                        {
                            Customers.Add(customer);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi bằng MessageBox
                MessageBox.Show($"Đã xảy ra lỗi khi tải danh sách khách hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Thêm khách hàng
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            try
            {
                using (var _context = new AppDbContext())
                {
                    // Thêm khách hàng mới từ tham số 'customer'
                    _context.Customers.Add(customer);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    int result = await _context.SaveChangesAsync();

                    // Kiểm tra kết quả
                    if (result > 0)
                    {
                        MessageBox.Show("Khách hàng đã được thêm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        await LoadCustomersAsync();
                        return true;
                        // Tải lại danh sách khách hàng sau khi thêm
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm khách hàng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ xảy ra

                MessageBox.Show($"Lỗi khi thêm khách hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        // Xóa khách hàng
        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            try
            {
                using (var _context = new AppDbContext())
                {
                    var customer = await _context.Customers.FindAsync(customerId);
                    if (customer != null)
                    {
                        _context.Customers.Remove(customer);
                        int result = await _context.SaveChangesAsync();
                        if (result > 0)
                        {
                            MessageBox.Show("Khách hàng đã được xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            await LoadCustomersAsync();  // Tải lại danh sách sau khi xóa
                            return true;  // Trả về true nếu xóa thành công
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa khách hàng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return false;  // Trả về false nếu không thể xóa
                        }
                    }
                    else
                    {
                        MessageBox.Show("Khách hàng không tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;  // Trả về false nếu khách hàng không tồn tại
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi xóa khách hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;  // Trả về false khi có lỗi
            }
        }


        // Cập nhật khách hàng
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                MessageBox.Show("Khách hàng không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                using (var _context = new AppDbContext())
                {
                    // Cập nhật thông tin khách hàng
                    _context.Customers.Update(customer);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        MessageBox.Show("Khách hàng đã được cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        await LoadCustomersAsync();  // Tải lại danh sách khách hàng sau khi cập nhật
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật khách hàng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có ngoại lệ xảy ra
                MessageBox.Show($"Lỗi khi cập nhật khách hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}
