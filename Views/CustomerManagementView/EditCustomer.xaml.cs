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
    public partial class EditCustomer : Window
    {

        private Customer _customer;
        private CustomerManagementVM _viewModel;

        // Constructor nhận đối tượng customer từ bên ngoài
        public EditCustomer(Customer customer, CustomerManagementVM viewModel)
        {
            InitializeComponent();
            _customer = customer; // Lưu thông tin khách hàng
            _viewModel = viewModel;
            if (customer == null)
            {
                MessageBox.Show("customer null");
            }

            FullNameTextBox.Text = _customer.FullName;
            IdCardNumberTextBox.Text = _customer.IdCardNumber;
            PhoneTextBox.Text = _customer.Phone;
            EmailTextBox.Text = _customer.Email;
            AddressTextBox.Text = _customer.Address;
            if (_customer.Gender == "Nam")
            {
                MaleRadioButton.IsChecked = true;
            }
            else if (_customer.Gender == "Nữ")
            {
                FemaleRadioButton.IsChecked = true;
            }

            
        }
        // Phương thức để gán customer cho cửa sổ


        // Sự kiện khi người dùng nhấn nút "Lưu"
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _customer.FullName = FullNameTextBox.Text;
            _customer.IdCardNumber = IdCardNumberTextBox.Text;
            _customer.Phone = PhoneTextBox.Text;
            _customer.Email = EmailTextBox.Text;
            _customer.Address = AddressTextBox.Text;
            _customer.Gender = MaleRadioButton.IsChecked == true ? "Nam" : "Nữ";
            
            if (string.IsNullOrEmpty(_customer.FullName) || string.IsNullOrEmpty(_customer.Phone))
            {
                MessageBox.Show("Tên và số điện thoại là bắt buộc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Gọi phương thức cập nhật từ DataService
            
            bool isUpdated = await _viewModel.UpdateCustomerAsync(_customer);
            
            if (isUpdated)
            {
                MessageBox.Show("Sửa thông tin khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; // Đóng cửa sổ và trả kết quả thành công
            }
            else
            {
                MessageBox.Show("Sửa thông tin khách hàng không thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = false; // Đóng cửa sổ và trả kết quả thất bại
            }
        }
        
    }

}