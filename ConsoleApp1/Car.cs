using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Car
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }

        public Car(string model, string manufacturer, int year, decimal price, string color)
        {
            Model = model;
            Manufacturer = manufacturer;
            Year = year;
            Price = price;
            Color = color;
        }

        public override string ToString()
        {
            return $"{Manufacturer} {Model}, Anul: {Year}, Pret: {Price:C}, Color: {Color} ";
        }
    }
}
