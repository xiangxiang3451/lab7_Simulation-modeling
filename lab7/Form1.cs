using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab7
{
    public partial class Form1 : Form
    {
        private Timer simulationTimer;
        private Random random;

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeChart();
            random = new Random();
        }

        private void InitializeTimer()
        {
            simulationTimer = new Timer();
            simulationTimer.Interval = 1000; // Set interval timer dalam milidetik (misalnya setiap 1 detik)
            simulationTimer.Tick += SimulationTimer_Tick;
        }

        private void InitializeChart()
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Mulai atau lanjutkan timer saat tombol "Start Simulation" ditekan
            simulationTimer.Start();
        }

        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            // Mendapatkan nilai acak untuk simulasi
            double goodServer = random.NextDouble() * 10; // Misalnya, nilai acak antara 0 dan 10
            int purchase = random.Next(100); // Misalnya, nilai acak antara 0 dan 100

            // Memanggil fungsi untuk menghitung hasil simulasi
            double income = CalculateIncome(goodServer, purchase);
            int customerNumbers = CalculateCustomerNumbers(goodServer, purchase);

            // Menambahkan data baru ke series
            chart1.Series[0].Points.AddY(income);
            chart1.Series[1].Points.AddY(customerNumbers);

            // Batasi jumlah data pada series agar tidak terlalu banyak
            const int maxDataPoints = 10; // Misalnya, maksimum 10 data points
            while (chart1.Series[0].Points.Count > maxDataPoints)
            {
                chart1.Series[0].Points.RemoveAt(0);
                chart1.Series[1].Points.RemoveAt(0);
            }

            // Perbarui chart
            chart1.Invalidate();
        }

        // Fungsi untuk menghitung income
        private double CalculateIncome(double goodServer, int purchase)
        {
            // Contoh rumus sederhana
            return goodServer * purchase * 10;
        }

        // Fungsi untuk menghitung jumlah pelanggan
        private int CalculateCustomerNumbers(double goodServer, int purchase)
        {
            // Contoh rumus sederhana
            return (int)(goodServer * purchase * 0.5);
        }


    }
}
