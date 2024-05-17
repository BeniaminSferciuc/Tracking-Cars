using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
                writer.WriteLine($"{sale.Car.Id},{sale.Car.Manufacturer},{sale.Car.Model},{sale.Car.Year},{sale.Car.Price},{sale.Car.Color},{sale.Car.Options},{sale.BuyerName},{sale.SellerName},{sale.SaleDate}");
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

                        if (parts.Length == 10)
                        {
                            string id = parts[0];
                            string manufacturer = parts[1];
                            string model = parts[2];
                            int year = int.Parse(parts[3]);
                            decimal price = decimal.Parse(parts[4]);
                            string color = parts[5];
                            string options = parts[6];
                            string buyerName = parts[7];
                            string sellerName = parts[8];
                            DateTime saleDate = DateTime.Parse(parts[9]);

                            Car car = new Car(id, model, manufacturer, year, price, color, options);
                            SaleRecord sale = new SaleRecord(car, saleDate, buyerName, sellerName);
                            sales.Add(sale);
                        }
                        else
                        {
                            // Linia nu are suficiente câmpuri, ignorăm linia sau tratăm eroarea
                            Debug.WriteLine($"Linie incompletă: {line}");
                        }
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
                        string id = parts[0];
                        string manufacturer = parts[1];
                        string model = parts[2];
                        int year = int.Parse(parts[3]);
                        decimal price = decimal.Parse(parts[4]);
                        string color = parts[5];
                        string options = parts[6];
                        string buyerName = parts[7];
                        string sellerName = parts[8];
                        DateTime saleDate = DateTime.Parse(parts[9]);

                        Car car = new Car(id, model, manufacturer, year, price, color, options);
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
                        if (parts.Length >= 5 && parts[1].Equals(manufacturer, StringComparison.OrdinalIgnoreCase))
                        {
                            string id = parts[0];
                            string model = parts[2];
                            int year = int.Parse(parts[3]);
                            decimal price = decimal.Parse(parts[4]);
                            string color = parts[5];
                            string options = parts[6];
                            string buyerName = parts[7];
                            string sellerName = parts[8];
                            DateTime saleDate = DateTime.Parse(parts[9]);

                            Car car = new Car(id, model, manufacturer, year, price, color, options);
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
