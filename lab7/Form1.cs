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
            simulationTimer.Interval = 1000; // Set timer interval in milliseconds (e.g., every 1 second)
            simulationTimer.Tick += SimulationTimer_Tick;
        }
        private void InitializeChart()
        {
            // Initialize the chart
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            simulationTimer.Start();
        }

        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            double goodServer = random.NextDouble() * 10; // Good server
            int purchase = random.Next(100); // Purchase
            double foodQuality = random.NextDouble(); // Food quality (random value between 0 and 1)
            double foodStorage = random.NextDouble(); // Food storage (random value between 0 and 1)

            // Call functions to calculate simulation results
            double income = CalculateIncome(goodServer, purchase, foodQuality, foodStorage);
            int customerNumbers = CalculateCustomerNumbers(goodServer, purchase, foodQuality, foodStorage);

            chart1.Series[0].Points.AddY(income);
            chart1.Series[1].Points.AddY(customerNumbers);

            const int maxDataPoints = 10; // Maximum of 10 data points
            while (chart1.Series[0].Points.Count > maxDataPoints)
            {
                chart1.Series[0].Points.RemoveAt(0);
                chart1.Series[1].Points.RemoveAt(0);
            }

            // Update the chart
            chart1.Invalidate();
        }

        private double CalculateIncome(double goodServer, int purchase, double foodQuality, double foodStorage)
        {

            double baseIncome = goodServer * purchase * foodQuality * foodStorage * 10;

            // Adjustment based on food storage
            double storageAdjustment = 1 + (1 - foodStorage) * 0.2; // Income decreases proportionally with less food storage
            double income = baseIncome * storageAdjustment;

            // Adjustment based on purchase
            double purchaseAdjustment = 1 + Math.Log(purchase + 1); // Income increases proportionally with higher purchase (logarithmic growth)
            income *= purchaseAdjustment;

            return income;
        }

        private int CalculateCustomerNumbers(double goodServer, int purchase, double foodQuality, double foodStorage)
        {
            // Formula to calculate complex customer numbers based on good server, purchase, food quality, and food storage
            int baseCustomerNumbers = (int)(goodServer * purchase * foodQuality * foodStorage * 0.5);

            // Adjustment based on food quality
            double qualityAdjustment = 1 + (foodQuality - 0.5) * 2; // Customer numbers increase proportionally with higher food quality
            int customerNumbers = (int)(baseCustomerNumbers * qualityAdjustment);

            return customerNumbers;
        }
    }
}
