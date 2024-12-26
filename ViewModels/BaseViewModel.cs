using System.ComponentModel;

namespace project_v3.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Sự kiện để thông báo thay đổi thuộc tính
        public event PropertyChangedEventHandler PropertyChanged;

        // Phương thức để thông báo thay đổi thuộc tính
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Thuộc tính mẫu (có thể thêm nhiều thuộc tính khác)
        private string _someProperty;
        public string SomeProperty
        {
            get => _someProperty;
            set
            {
                if (_someProperty != value)
                {
                    _someProperty = value;
                    OnPropertyChanged(nameof(SomeProperty));  // Thông báo thay đổi
                }
            }
        }
    }
}
