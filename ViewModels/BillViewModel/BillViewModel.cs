using QLKS_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS_CK.ViewModels.BillViewModel
{
    public class BillViewModel : BaseViewModel
    {
        private readonly SharedViewModel _sharedViewModel;

        public Room SelectedRoom => _sharedViewModel.SelectedRoom;

        public BillViewModel(SharedViewModel sharedViewModel)
        {
            _sharedViewModel = sharedViewModel;
        }
    }
}
