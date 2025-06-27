using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFID_YOKLAMA
{
    public partial class Form3 : Form
    {
        public static string SqlCon = @"Data Source=LAPTOP-PAFIK8Q4\SQLEXPRESS;Initial Catalog=kisiler;Integrated Security=True;TrustServerCertificate=True";
        
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        List<string> gelenlerListesi = new List<string>();
        private SerialPort serialPort;
        public static string portismi, banthizi;
        string[] ports = SerialPort.GetPortNames();
        private string kartID;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Seri portları listele
            foreach (string port in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(port);
            }

            // Kullanılan baud hızlarını ekle
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("115200");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SerialPort ayarları
            serialPort = new SerialPort
            {
                PortName = "COM3", // Kart okuyucunun bağlı olduğu COM port
                BaudRate = 9600,  // Kart okuyucu baud hızı
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };

            serialPort.DataReceived += SerialPort_DataReceived;

            try
            {
                serialPort.Open();
                MessageBox.Show("Bağlantı başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seri port bağlantısı açılamadı: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close(); // Portu kapat
                    MessageBox.Show("Bağlantı kapatıldı.");
                }
                else
                {
                    MessageBox.Show("Zaten bağlantı yok.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı kesme hatası: " + ex.Message);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                string kartID = serialPort.ReadExisting().Trim();
            if (!kartID.StartsWith("Kart UID: "))
            {
                kartID = "Kart UID: " + kartID.Trim();
            }

            if (!string.IsNullOrEmpty(kartID))
                {
                    Invoke(new Action(() =>
                    {
                        Form4 form4 = Form4.GetInstance(); // Singleton örneği alınıyor
                        form4.Show();
                        form4.AddKartData(kartID); // Kart ID'si işleniyor
                    }));
                }
            }

        }

        
    }
