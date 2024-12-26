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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        

        // Constructor nhận vào ViewModel
        public AddCustomer()
        {
            InitializeComponent();
           
        }
          
        
            private async void AddButton_Click(object sender, RoutedEventArgs e)
            {
                string fullName = FullNameTextBox.Text.Trim();
                string idCardNumber = IdCardNumberTextBox.Text.Trim();
                string phone = PhoneTextBox.Text.Trim();
                string email = EmailTextBox.Text.Trim();
                string address = AddressTextBox.Text.Trim();
                 string gender = MaleRadioButton.IsChecked == true ? "Nam" : "Nữ";

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Tên và số điện thoại là bắt buộc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Thêm logic lưu thông tin khách hàng vào database
                Customer cus = new Customer (
                     fullName,
                     idCardNumber,
                     phone,
                     email,
                     address,
                     gender
                );
            CustomerManagementVM _viewModel = new CustomerManagementVM();
            bool isAdded = await _viewModel.AddCustomerAsync(cus);
            if (isAdded)
            {


                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true; // Đóng cửa sổ và trả về kết quả
            }
            else
            {
                MessageBox.Show("Thêm khách hàng không thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult= false;
            }
            }
        
        }
    }

