using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QLKS_CK.Bqtankiet.Model;
using QLKS_CK.ViewModels;

namespace QLKS_CK.Bqtankiet.ViewModel
{
    public class CounterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // model
        private CounterModel counter = new CounterModel();

        // binding data
        public int CounterValue
        {
            get { return this.counter.value; }
        }

        // binding command
        public ICommand ButtonClickCommand { get; }

        // constructor
        public CounterViewModel()
        {
            ButtonClickCommand = new RelayCommand(OnButtonClick);
        }

        // methods
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnButtonClick()
        {
            this.counter.increase(); OnPropertyChanged(nameof(CounterValue));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged;
    }
}

