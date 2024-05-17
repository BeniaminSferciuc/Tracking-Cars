using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Car
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Options { get; set; }

        public Car(string id, string model, string manufacturer, int year, decimal price, string color, string options)
        {
            Id = id;
            Model = model;
            Manufacturer = manufacturer;
            Year = year;
            Price = price;
            Color = color;
            Options = options;
        }

        public override string ToString()
        {
            return $"Id: {Id}, {Manufacturer} {Model}, Anul: {Year}, Pret: {Price:C}, Color: {Color}, Options: {Options} ";
        }

        static Random random = new Random();

        static public string GenerateUniqueId()
        {
            // Generăm un șir de caractere aleatorii folosind numere și litere mici
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            // Generăm un șir de 8 caractere
            char[] idChars = new char[8];
            for (int i = 0; i < 8; i++)
            {
                idChars[i] = chars[random.Next(chars.Length)];
            }

            // Convertim șirul de caractere într-un șir și îl returnăm
            return new string(idChars);
        }
    }
}
