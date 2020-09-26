using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturentmanagementsystem
{
    public class Salestable
    {
        public class Sales
        {
            public string Item { get; set; }
            public string Description { get; set; }
            public float price { get; set; }
            public float Qty { get; set; }
            public float Amount { get; set; }
        }

        public class Total
        {
            public decimal? Amount { get; set; }
            public string Totals { get; set; }
          
        }
    }
}
