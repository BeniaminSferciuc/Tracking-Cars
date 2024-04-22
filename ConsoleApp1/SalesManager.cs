using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SalesManager
    {
        private string filePath;

        public SalesManager(string filePath)
        {
            this.filePath = filePath;
        }

        // Metoda pentru a adăuga o vânzare și a o scrie în fișier
        public void AddSale(SaleRecord sale)
        {
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{sale.Car.Manufacturer},{sale.Car.Model},{sale.Car.Year},{sale.Car.Price},{sale.Car.Color},{sale.BuyerName},{sale.SellerName},{sale.SaleDate}");
            }
        }

        // Metoda pentru a citi vânzările din fișier
        public List<SaleRecord> ReadSales()
        {
            List<SaleRecord> sales = new List<SaleRecord>();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        string manufacturer = parts[0];
                        string model = parts[1];
                        int year = int.Parse(parts[2]);
                        decimal price = decimal.Parse(parts[3]);
                        string color = parts[4];
                        string buyerName = parts[5];
                        string sellerName = parts[6];
                        DateTime saleDate = DateTime.Parse(parts[7]);

                        Car car = new Car(model, manufacturer, year, price, color);
                        SaleRecord sale = new SaleRecord(car, saleDate, buyerName, sellerName);
                        sales.Add(sale);
                    }
                }
            }

            return sales;
        }

        public SaleRecord GetLastSale()
        {
            SaleRecord lastSale = null;
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string lastLine = null;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lastLine = line;
                    }

                    if (lastLine != null)
                    {
                        string[] parts = lastLine.Split(',');
                        string manufacturer = parts[0];
                        string model = parts[1];
                        int year = int.Parse(parts[2]);
                        decimal price = decimal.Parse(parts[3]);
                        string color = parts[4];
                        string buyerName = parts[5];
                        string sellerName = parts[6];
                        DateTime saleDate = DateTime.Parse(parts[7]);

                        Car car = new Car(model, manufacturer, year, price, color);
                        lastSale = new SaleRecord(car, saleDate, buyerName, sellerName);
                    }
                }
            }

            return lastSale;
        }

        public List<SaleRecord> SearchByManufacturer(string manufacturer)
        {
            List<SaleRecord> matchingSales = new List<SaleRecord>();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 5 && parts[0].Equals(manufacturer, StringComparison.OrdinalIgnoreCase))
                        {
                            string model = parts[1];
                            int year = int.Parse(parts[2]);
                            decimal price = decimal.Parse(parts[3]);
                            string color = parts[4];
                            string buyerName = parts[5];
                            string sellerName = parts[6];
                            DateTime saleDate = DateTime.Parse(parts[7]);

                            Car car = new Car(model, manufacturer, year, price, color);
                            SaleRecord sale = new SaleRecord(car, saleDate, buyerName, sellerName);
                            matchingSales.Add(sale);
                        }
                    }
                }
            }

            return matchingSales;
        }
    }
}
