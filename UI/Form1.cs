    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ConsoleApp1;

    namespace UI
    {
    public partial class Form1 : Form
    {
        private DataGridView dataGridView;

        private Label label;

        private TextBox textBoxManufacturer;
        private Label labelManufacturer;

        private Label labelYear;
        private TextBox textBoxYear;

        private TextBox textBoxModel;
        private Label labelModel;

        private Label labelPrice;
        private TextBox textBoxPrice;

        private Label labelColor;
        private TextBox textBoxColor;

        private Label labelBuyer;
        private TextBox textBoxBuyer;

        private Label labelSeller;
        private TextBox textBoxSeller;

        private Button buttonAddSale;

        public Form1()
        {
            InitializeComponent();

            label = new Label();
            label.Text = "Vânzările de mașini";
            label.Font = new Font(label.Font, FontStyle.Bold);
            label.Dock = DockStyle.Top;
            Controls.Add(label);

            int labelHeight = label.Height;
            int spacing = 10;

            // Adaugăm spațiu între label și controalele de introducere a datelor
            int inputControlsTop = label.Bottom + spacing;

            // Adaugăm label și controalele de introducere a datelor
            labelManufacturer = new Label();
            labelManufacturer.Text = "Producător:";
            labelManufacturer.Left = 900;
            labelManufacturer.Size = new Size(70, labelHeight);
            labelManufacturer.Top = 40;
            Controls.Add(labelManufacturer);

            textBoxManufacturer = new TextBox();
            textBoxManufacturer.Size = new Size(150, labelHeight);
            textBoxManufacturer.Location = new Point(labelManufacturer.Left, labelManufacturer.Bottom + spacing);
            Controls.Add(textBoxManufacturer);

            labelModel = new Label();
            labelModel.Text = "Model:";
            labelModel.Size = new Size(70, labelHeight);
            labelModel.Location = new Point(labelManufacturer.Left, textBoxManufacturer.Bottom + spacing);
            Controls.Add(labelModel);

            textBoxModel = new TextBox();
            textBoxModel.Size = new Size(150, labelHeight);
            textBoxModel.Location = new Point(labelManufacturer.Left, labelModel.Bottom + spacing);
            Controls.Add(textBoxModel);

            labelYear = new Label();
            labelYear.Text = "An:";
            labelYear.Size = new Size(70, labelHeight);
            labelYear.Location = new Point(labelManufacturer.Left, textBoxModel.Bottom + spacing);
            Controls.Add(labelYear);

            textBoxYear = new TextBox();
            textBoxYear.Size = new Size(150, labelHeight);
            textBoxYear.Location = new Point(labelManufacturer.Left, labelYear.Bottom + spacing);
            Controls.Add(textBoxYear);

            // Adaugăm label și controale pentru preț
            labelPrice = new Label();
            labelPrice.Text = "Preț:";
            labelPrice.Size = new Size(70, labelHeight);
            labelPrice.Location = new Point(labelManufacturer.Left, textBoxYear.Bottom + spacing);
            Controls.Add(labelPrice);

            textBoxPrice = new TextBox();
            textBoxPrice.Size = new Size(150, labelHeight);
            textBoxPrice.Location = new Point(labelManufacturer.Left, labelPrice.Bottom + spacing);
            Controls.Add(textBoxPrice);

            // Adaugăm label și controale pentru culoare
            labelColor = new Label();
            labelColor.Text = "Culoare:";
            labelColor.Size = new Size(70, labelHeight);
            labelColor.Location = new Point(labelManufacturer.Left, textBoxPrice.Bottom + spacing);
            Controls.Add(labelColor);

            textBoxColor = new TextBox();
            textBoxColor.Size = new Size(150, labelHeight);
            textBoxColor.Location = new Point(labelManufacturer.Left, labelColor.Bottom + spacing);
            Controls.Add(textBoxColor);

            // Adaugăm label și controale pentru cumpărător
            labelBuyer = new Label();
            labelBuyer.Text = "Cumpărător:";
            labelBuyer.Size = new Size(70, labelHeight);
            labelBuyer.Location = new Point(labelManufacturer.Left, textBoxColor.Bottom + spacing);
            Controls.Add(labelBuyer);

            textBoxBuyer = new TextBox();
            textBoxBuyer.Size = new Size(150, labelHeight);
            textBoxBuyer.Location = new Point(labelManufacturer.Left, labelBuyer.Bottom + spacing);
            Controls.Add(textBoxBuyer);

            // Adaugăm label și controale pentru vânzător
            labelSeller = new Label();
            labelSeller.Text = "Vânzător:";
            labelSeller.Size = new Size(70, labelHeight);
            labelSeller.Location = new Point(labelManufacturer.Left, textBoxBuyer.Bottom + spacing);
            Controls.Add(labelSeller);

            textBoxSeller = new TextBox();
            textBoxSeller.Size = new Size(150, labelHeight);
            textBoxSeller.Location = new Point(labelManufacturer.Left, labelSeller.Bottom + spacing);
            Controls.Add(textBoxSeller);


            buttonAddSale = new Button();
            buttonAddSale.Text = "Adăugare vânzare";
            buttonAddSale.Location = new Point(textBoxModel.Right + spacing * 2, inputControlsTop);
            buttonAddSale.Click += ButtonAddSale_Click;
            Controls.Add(buttonAddSale);

            // Adaugăm tabelul sub controalele de introducere a datelor
            int dataGridViewTop = inputControlsTop + labelHeight + spacing;
            dataGridView = new DataGridView();
            dataGridView.Top = dataGridViewTop;
            dataGridView.Left = 20; // setează poziția orizontală a DataGridView
            dataGridView.Width = this.ClientSize.Width - 20; // ajustează lățimea DataGridView pentru a se potrivi cu fereastra
            dataGridView.Height = this.ClientSize.Height - dataGridViewTop + 100; // ajustează înălțimea DataGridView pentru a se potrivi cu spațiul disponibil în fereastra
            dataGridView.ReadOnly = true;
            Controls.Add(dataGridView);

            LoadSalesData();

            // Ajustăm dimensiunea formei pentru a se potrivi conținutului
            int formHeight = dataGridViewTop + dataGridView.Height + spacing * 2; // Adăugăm spațiu și în partea de jos
            Height = formHeight;

            WindowState = FormWindowState.Maximized;
        }

        private void LoadSalesData()
        {
            // Creăm un DataTable pentru a stoca datele
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Producător");
            dataTable.Columns.Add("Model");
            dataTable.Columns.Add("An");
            dataTable.Columns.Add("Pret");
            dataTable.Columns.Add("Culoare");
            dataTable.Columns.Add("Cumpărător");
            dataTable.Columns.Add("Vanzator");
            dataTable.Columns.Add("Data vânzării");

            // Citim vânzările din fișier și le adăugăm în DataTable
            string filePath = "../../../ConsoleApp1/bin/Debug/sales.txt";

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line); // Afișăm linia citită pentru a verifica formatul datelor
                        string[] parts = line.Split(',');
                        if (parts.Length >= 6)
                        {
                            dataTable.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);
                        }
                    }
                }
            }

            dataGridView.DataSource = dataTable;

            // Adaugăm textul header-ului pentru coloanele din DataGridView
            foreach (DataColumn column in dataTable.Columns)
            {
                dataGridView.Columns[column.ColumnName].HeaderText = column.ColumnName;
            }
        }

        private void ButtonAddSale_Click(object sender, EventArgs e)
        {
            // Obținem valorile introduse de utilizator din controalele de textbox
            string manufacturer = textBoxManufacturer.Text;
            string model = textBoxModel.Text;
            string year = textBoxYear.Text;
            string price = textBoxPrice.Text;
            string color = textBoxColor.Text;
            string buyer = textBoxBuyer.Text;
            string seller = textBoxSeller.Text;

            // Verificăm dacă toate câmpurile sunt completate
            if (string.IsNullOrWhiteSpace(manufacturer) || string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(year) ||
                string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(color) || string.IsNullOrWhiteSpace(buyer) || string.IsNullOrWhiteSpace(seller))
            {
                MessageBox.Show("Te rog să completezi toate câmpurile.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Ieșim din metoda pentru a nu continua să salvăm datele
            }

            // Creăm un obiect SaleRecord pentru noua vânzare
            Car car = new Car(model, manufacturer, int.Parse(year), decimal.Parse(price), color);
            SaleRecord saleRecord = new SaleRecord(car, DateTime.Now, buyer, seller);

            // Specificăm calea către fișierul în care vom salva vânzările
            string filePath = "../../../ConsoleApp1/bin/Debug/sales.txt";

            try
            {
                // Creăm un obiect SalesManager pentru a adăuga vânzarea în fișier
                SalesManager salesManager = new SalesManager(filePath);

                // Adăugăm noua vânzare folosind SalesManager
                salesManager.AddSale(saleRecord);

                // Afisăm un mesaj de confirmare că vânzarea a fost adăugată cu succes
                MessageBox.Show("Vânzarea a fost adăugată cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reîncărcăm datele în DataGridView pentru a reflecta noua vânzare adăugată
                LoadSalesData();

                textBoxManufacturer.Clear();
                textBoxModel.Clear();
                textBoxYear.Clear();
                textBoxPrice.Clear();
                textBoxColor.Clear();
                textBoxBuyer.Clear();
                textBoxSeller.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare în timpul salvării vânzării: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
