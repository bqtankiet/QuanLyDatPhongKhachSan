using QLKS_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS_CK.ViewModels
{
    public class SharedViewModel : BaseViewModel
    {
        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
    }
}
