using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApp1;

namespace UI
{
    public partial class Form1 : Form
    {
        SalesManager salesManager = new SalesManager("../../../ConsoleApp1/bin/Debug/sales.txt");

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;

            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataFromFile("../../../ConsoleApp1/bin/Debug/sales.txt");
            AddLastSale(salesManager);
        }

        private void LoadDataFromFile(string filePath)
        {
            // Verificăm dacă fișierul există
            if (!File.Exists(filePath))
            {
                // Dacă fișierul nu există, îl creăm
                using (FileStream fs = File.Create(filePath))
                {
                    // Nu avem nevoie să facem nimic aici, deoarece creăm doar fișierul
                }
            }

            // Verificăm din nou existența fișierului, deoarece ar putea să nu fi fost creat în cazul în care nu exista anterior
            if (File.Exists(filePath))
            {
                // Dacă fișierul există acum, îl putem citi
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(',');
                        dataGridView1.Rows.Add(fields);
                    }
                }
            }
            else
            {
                // Dacă fișierul nu există încă, putem afișa un mesaj sau lua alte măsuri
                MessageBox.Show("Fișierul nu a putut fi creat sau citit.");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SaveDataToFile(SalesManager salesManager)
        {
            decimal price;
            int year;

            string manufacturer = textBox1.Text;
            string model = textBox2.Text;
            int.TryParse(comboBox1.Text, out year);
            decimal.TryParse(textBox4.Text, out price);
            string color = comboBox2.Text;

            string options = "";
            foreach (Control control in groupBox1.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    if (checkBox.Checked)
                    {
                        options += checkBox.Text + "|"; // Adaugă textul opțiunii la șirul de opțiuni dacă este bifată
                    }
                }
            }

            // Elimină ultima virgulă și spațiul adăugate în exces
            if (!string.IsNullOrEmpty(options))
            {
                options = options.Substring(0, options.Length - 2);
            }


            string buyerName = textBox6.Text;
            string sellerName = textBox7.Text;

            string id = Car.GenerateUniqueId();

            SaleRecord sale = new SaleRecord(new Car(id, model, manufacturer, year, price, color, options), DateTime.Now, buyerName, sellerName);
            salesManager.AddSale(sale);

            string[] rowData = {
                sale.Car.Id,
                sale.Car.Manufacturer,
                sale.Car.Model,
                sale.Car.Year.ToString(),
                sale.Car.Price.ToString(),
                sale.Car.Color,
                sale.Car.Options,
                sale.BuyerName,
                sale.SellerName,
                sale.SaleDate.ToString(),
            };

            dataGridView1.Rows.Add(rowData);

            CleanInputs();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                SalesManager salesManager = new SalesManager("../../../ConsoleApp1/bin/Debug/sales.txt");
                SaveDataToFile(salesManager);
                AddLastSale(salesManager);
            }
        }
        private bool ValidateInput()
        {
            bool isValid = true;

            // Validare marca
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Te rugăm să completezi câmpul.");
                isValid = false;
            }
            else if (textBox1.Text.Length < 2)
            {
                errorProvider1.SetError(textBox1, "Te rugăm să introduci o marcă validă.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
            }

            // Validare model
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Te rugăm să completezi câmpul.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBox2, "");
            }

            // Validare an fabricatie
            if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox1, "Te rog selectează un an.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(comboBox1, "");
            }

            // Validare pret
            decimal price;
            if (!decimal.TryParse(textBox4.Text, out price) || price <= 0)
            {
                errorProvider1.SetError(textBox4, "Te rugăm să introduci un preț valid.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBox4, "");
            }

            // Validare culoare
            if (comboBox2.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox2, "Te rog selectează o culoare.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(comboBox2, "");
            }

            // Validare nume cumparator
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                errorProvider1.SetError(textBox6, "Te rugăm să completezi câmpul.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBox6, "");
            }

            // Validare nume vanzator
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                errorProvider1.SetError(textBox7, "Te rugăm să completezi câmpul.");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBox7, "");
            }

            return isValid;
        }
        private void CleanInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            textBox4.Clear();
            comboBox2.SelectedIndex = -1;
            groupBox1.Controls.OfType<CheckBox>().ToList().ForEach(cb => cb.Checked = false);
            textBox6.Clear();
            textBox7.Clear();
        }
        private void AddLastSale(SalesManager saleManager)
        {
            SaleRecord lastSale = saleManager.GetLastSale();

            if (lastSale != null)
            {
                label19.Text = lastSale.Car.Manufacturer;
                label20.Text = lastSale.Car.Model;
                label21.Text = lastSale.Car.Year.ToString();
                label22.Text = lastSale.Car.Price.ToString() + " EUR";
                label23.Text = lastSale.Car.Color;

                // Split pe caracterul "|"
                string[] optionsArray = lastSale.Car.Options.Split('|');

                // Join folosind ", " pentru a uni elementele
                string formattedOptions = string.Join(", ", optionsArray);

                // Setează textul label-ului
                label24.Text = formattedOptions;

                label25.Text = lastSale.BuyerName;
                label26.Text = lastSale.SellerName;
                label27.Text = lastSale.SaleDate.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificăm dacă evenimentul a fost declanșat pentru o celulă validă
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Obținem ID-ul mașinii din celula "Id" a rândului selectat
                string id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                // Căutăm rândul cu ID-ul corespunzător în dataGridView1
                DataGridViewRow selectedRow = null;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Id"].Value.ToString() == id)
                    {
                        selectedRow = row;
                        break;
                    }
                }

                // Verificăm dacă am găsit rândul cu ID-ul corespunzător
                if (selectedRow != null)
                {
                    if (tabControl1.TabPages.Contains(tabPage3))
                    {
                        tabControl1.SelectedTab = tabPage3;

                        // Actualizăm label-urile cu valorile din rândul selectat
                        label47.Text = selectedRow.Cells["Id"].Value.ToString();
                        label36.Text = selectedRow.Cells["Column1"].Value.ToString();
                        label35.Text = selectedRow.Cells["Column2"].Value.ToString();
                        label34.Text = selectedRow.Cells["Column3"].Value.ToString();
                        label33.Text = selectedRow.Cells["Column4"].Value.ToString();
                        label32.Text = selectedRow.Cells["Column5"].Value.ToString();
                        label31.Text = selectedRow.Cells["Column6"].Value.ToString();
                        label30.Text = selectedRow.Cells["Column7"].Value.ToString();
                        label29.Text = selectedRow.Cells["Column8"].Value.ToString();
                        label28.Text = selectedRow.Cells["Column9"].Value.ToString();
                    }
                }
                else
                {
                    // Rândul nu a fost găsit - puteți afișa un mesaj sau efectuați o altă acțiune
                    MessageBox.Show("ID-ul specificat nu a fost găsit în DataGridView.");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Verificăm dacă label47.Text este null sau gol
            if (label47.Text == "null")
            {
                // Dacă este null sau gol, afișăm un mesaj că nu există nicio vânzare curentă
                MessageBox.Show("Nu există nicio vânzare curentă de șters.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Ieșim din metoda de gestionare a evenimentului
            }

            // Afisăm un mesaj de confirmare
            DialogResult result = MessageBox.Show("Sunteți sigur că doriți să ștergeți vânzarea curentă?", "Confirmare ștergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificăm răspunsul utilizatorului
            if (result == DialogResult.Yes)
            {
                // Apelăm funcția DeleteSaleFromFile pentru a șterge 
                string id = label47.Text;
                DeleteSaleFromFile(id);
            }
        }
        private void DeleteSaleFromFile(string carId)
        {
            string filePath = "../../../ConsoleApp1/bin/Debug/sales.txt";

            SalesManager salesManager = new SalesManager(filePath);

            List<SaleRecord> sales = salesManager.ReadSales();

            // Lista actualizată de vânzări care va conține tranzacțiile actualizate și cele neschimbate
            List<SaleRecord> updatedSales = new List<SaleRecord>();

            foreach (SaleRecord sale in sales)
            {
                if (sale.Car.Id != carId)
                {
                    updatedSales.Add(sale);
                }
            }

            try
            {
                // Rescriem fișierul cu toate tranzacțiile actualizate și cele neschimbate
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (SaleRecord sale in updatedSales)
                    {
                        writer.WriteLine($"{sale.Car.Id},{sale.Car.Manufacturer},{sale.Car.Model},{sale.Car.Year},{sale.Car.Price},{sale.Car.Color},{sale.Car.Options},{sale.BuyerName},{sale.SellerName},{sale.SaleDate}");
                    }
                }

                // Curățăm datele din dataGridView1 și încărcăm datele actualizate din fișier
                dataGridView1.Rows.Clear();
                LoadDataFromFile(filePath);

                // Redirecționăm către TabPage2
                tabControl1.SelectedTab = tabPage2;
            }
            catch (Exception ex)
            {
                // Tratează orice excepție care ar putea apărea în timpul operațiunilor de ștergere și de încărcare a datelor
                MessageBox.Show($"A apărut o eroare în timpul ștergerii vânzării: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetMostSoldCarBrand(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, int> brandCount = new Dictionary<string, int>();

            // Parcurgem fiecare rând din dataGridView1
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Verificăm dacă celula corespunzătoare coloanei "Column1" are o valoare
                if (row.Cells["Column1"].Value != null)
                {
                    // Obținem data vânzării din celula corespunzătoare coloanei "Column9"
                    if (DateTime.TryParse(row.Cells["Column9"].Value.ToString(), out DateTime saleDate))
                    {
                        // Verificăm dacă data vânzării este în interiorul intervalului selectat
                        if (saleDate >= startDate && saleDate <= endDate)
                        {
                            string brand = row.Cells["Column1"].Value.ToString();

                            // Actualizăm numărul de apariții pentru fiecare brand
                            if (brandCount.ContainsKey(brand))
                            {
                                brandCount[brand]++;
                            }
                            else
                            {
                                brandCount[brand] = 1;
                            }
                        }
                    }
                }
            }

            // Găsim brand-ul cu cele mai multe apariții în intervalul selectat
            string mostSoldBrand = "";
            int maxCount = 0;

            foreach (var kvp in brandCount)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mostSoldBrand = kvp.Key;
                }
            }

            return mostSoldBrand;
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label49.Visible = true;
            dateTimePicker2.Visible = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime dataInceput = dateTimePicker1.Value;
            DateTime dataSfarsit = dateTimePicker2.Value;

            if (dataInceput <= dataSfarsit)
            {
                errorProvider1.Clear();
                label51.Visible = true;
                label50.Visible = true;

                label50.Text = $"Cea mai vanduta masina in perioada {dataInceput.ToString("dd/MM/yyyy")} - {dataSfarsit.ToString("dd /MM/yyyy")} este: ";
                
                if (GetMostSoldCarBrand(dataInceput, dataSfarsit) != "")
                {
                    label51.Text = GetMostSoldCarBrand(dataInceput, dataSfarsit);
                } else
                {
                    label51.Text = "Nu s-au gasit inregistrari";
                }
            }
            else
            {
                errorProvider1.SetError(dateTimePicker2, "Data de început trebuie să fie mai mică sau egală cu data de sfârșit.");
                dateTimePicker2.Value = DateTime.Now;
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectDate = dateTimePicker3.Value;

            List<SaleRecord> sales = salesManager.ReadSales();
            dataGridView2.Rows.Clear();

            foreach (SaleRecord sale in sales)
            {
                if (sale.SaleDate.Date == selectDate.Date)
                {
                    dataGridView2.Rows.Add(sale.Car.Id, sale.Car.Manufacturer, sale.Car.Model, sale.Car.Year, sale.Car.Price, sale.Car.Color, sale.Car.Options, sale.BuyerName, sale.SellerName, sale.SaleDate);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime dataInceput = dateTimePicker4.Value;
            DateTime dataSfarsit = dateTimePicker5.Value;

            List<SaleRecord> sales = salesManager.ReadSales();
            List<SaleRecord> matchedSales = new List<SaleRecord>();

            string brand = textBox3.Text;

            foreach (SaleRecord sale in sales)
            {
                // Verificăm dacă marca și data vânzării corespund criteriilor
                if (sale.Car.Manufacturer == brand && sale.SaleDate >= dataInceput && sale.SaleDate <= dataSfarsit)
                {
                    // Adăugăm vânzarea care îndeplinește criteriile în lista de vânzări potrivite
                    matchedSales.Add(sale);
                }
            }

            //matchedSales.Sort((x, y) => x.SaleDate.CompareTo(y.SaleDate));

            // Curățăm seriile existente din grafic pentru a adăuga una nouă
            chart1.Series.Clear();

            // Adăugăm o nouă serie pentru a reprezenta evoluția prețului
            chart1.Series.Add("Evoluția prețului");

            // Adăugăm puncte pentru fiecare vânzare din lista potrivită
            foreach (SaleRecord sale in matchedSales)
            {
                chart1.Series["Evoluția prețului"].Points.AddXY(sale.SaleDate, 0);
                // Setăm valoarea reală pe axa y la prețul mașinii
                chart1.Series["Evoluția prețului"].Points.Last().YValues[0] = Convert.ToDouble(sale.Car.Price);
            }

            // Setăm tipul de grafic la LineChart
            chart1.Series["Evoluția prețului"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            // Setăm alinierea etichetelor pe axa X pentru a afișa eticheta finală în partea de jos
            chart1.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
        }
    }
    }
