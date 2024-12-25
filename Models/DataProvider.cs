using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKS_CK.Models
{
    public class DataProvider
    {
        private static DataProvider instance { get; set; }

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();

                return instance;
            }
            set { instance = value; }
        }

        public QLKSDbContext DB { get; set; }

        public DataProvider()
        {
            DB = new QLKSDbContext();
        }

    }
}
