using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SaleRecord
    {
        public Car Car { get; set; }
        public DateTime SaleDate { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }

        public SaleRecord(Car car, DateTime saleDate, string buyerName, string sellerName)
        {
            Car = car;
            SaleDate = saleDate;
            BuyerName = buyerName;
            SellerName = sellerName;
        }

        public override string ToString()
        {
            return $"Vandut: {Car}, Cumparator: {BuyerName}, Vanzator: {SellerName}, Data vanzarii: {SaleDate}";
        }


    }
}
