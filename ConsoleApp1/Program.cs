using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "sales.txt";
            SalesManager salesManager = new SalesManager(filePath);

            int choice;
            do
            {
                Console.WriteLine("Meniu:\n");
                Console.WriteLine("1. Adaugare vanzare");
                Console.WriteLine("2. Afisare vanzari");
                Console.WriteLine("3. Afisare ultima vanzare");
                Console.WriteLine("4. Curata consola");
                Console.WriteLine("5. Cauta dupa marca");
                Console.WriteLine("6. Iesire\n");
                Console.Write("Introduceti optiunea: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddSaleFromConsole(salesManager);
                            break;
                        case 2:
                            DisplaySales(salesManager);
                            break;
                        case 3:
                            DisplayLastSale(salesManager);
                            break;
                        case 4:
                            Console.Clear();
                            break;
                        case 5:
                            SearchByManufacturer(salesManager);
                            break;
                        case 6:
                            Console.WriteLine("La revedere!");
                            break;
                        default:
                            Console.WriteLine("Optiune invalida. Va rugam sa introduceti o optiune valida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Optiune invalida. Va rugam sa introduceti o optiune valida.");
                }

            } while (choice != 6);
        }

        static void DisplayLastSale(SalesManager salesManager)
        {
            SaleRecord lastSale = salesManager.GetLastSale();
            if (lastSale != null)
            {
                Console.WriteLine("Ultima vânzare:");
                Console.WriteLine(lastSale);
            }
            else
            {
                Console.WriteLine("Nu există vânzări înregistrate.");
            }
        }

        static void AddSaleFromConsole(SalesManager salesManager)
        {
            Console.WriteLine("Introduceti detaliile vânzării:");

            // Validarea producătorului
            string manufacturer;
            do
            {
                Console.Write("Producător: ");
                manufacturer = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(manufacturer))
                {
                    Console.WriteLine("Producătorul nu poate fi gol. Te rog să introduci un producător valid.");
                }
            } while (string.IsNullOrWhiteSpace(manufacturer));

            // Validarea modelului
            string model;
            do
            {
                Console.Write("Model: ");
                model = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(model))
                {
                    Console.WriteLine("Modelul nu poate fi gol. Te rog să introduci un model valid.");
                }
            } while (string.IsNullOrWhiteSpace(model));

            // Validarea anului
            int year;
            do
            {
                Console.Write("Anul fabricației: ");
                if (!int.TryParse(Console.ReadLine(), out year) || year < 1900 || year > DateTime.Now.Year)
                {
                    Console.WriteLine("Anul trebuie să fie un număr întreg între 1900 și anul curent.");
                }
            } while (year < 1900 || year > DateTime.Now.Year);

            // Validarea prețului
            decimal price;
            do
            {
                Console.Write("Preț: ");
                if (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
                {
                    Console.WriteLine("Prețul trebuie să fie un număr pozitiv.");
                }
            } while (price <= 0);

            // Validarea culorii
            string color;
            do
            {
                Console.Write("Culoare: ");
                color = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(color))
                {
                    Console.WriteLine("Culoarea este obligatorie. Te rog să introduci un culoare valida.");
                }
            } while (string.IsNullOrWhiteSpace(color));


            // Validarea numelui cumpărătorului
            string buyerName;
            do
            {
                Console.Write("Numele cumpărătorului: ");
                buyerName = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(buyerName))
                {
                    Console.WriteLine("Numele cumparatorului nu poate fi gol. Te rog sa introduci un nume valid.");
                }
            } while (string.IsNullOrWhiteSpace(buyerName));

            // Validarea numelui vanzatorului
            string sellerName;
            do
            {
                Console.Write("Numele vanzatorului: ");
                sellerName = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(sellerName))
                {
                    Console.WriteLine("Numele vanzatorului nu poate fi gol. Te rog sa introduci un nume valid.");
                }
            } while (string.IsNullOrWhiteSpace(sellerName));

            SaleRecord sale = new SaleRecord(new Car(model, manufacturer, year, price, color), DateTime.Now, buyerName, sellerName);
            salesManager.AddSale(sale);
            Console.WriteLine("Vânzare adăugată cu succes.");
        }

        static void DisplaySales(SalesManager salesManager)
        {
            List<SaleRecord> sales = salesManager.ReadSales();
            Console.WriteLine("Vânzări:\n");
            foreach (SaleRecord sale in sales)
            {
                Console.WriteLine(sale);
                Console.Write("---------------------------------------------------------------------\n");
            }
        }

        static void SearchByManufacturer(SalesManager salesManager)
        {
            Console.Write("Introduceți producătorul pentru căutare: ");
            string manufacturer = Console.ReadLine();

            List<SaleRecord> matchingSales = salesManager.SearchByManufacturer(manufacturer);

            if (matchingSales.Count > 0)
            {
                Console.WriteLine($"Vânzări pentru producătorul '{manufacturer}':");
                foreach (SaleRecord sale in matchingSales)
                {
                    Console.WriteLine(sale);
                }
            }
            else
            {
                Console.WriteLine($"Nu s-au găsit vânzări pentru producătorul '{manufacturer}'.");
            }
        }
    }
}
