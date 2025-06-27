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
    public partial class Form2 : Form
    {
       
        public static string connectionString = @"Data Source=LAPTOP-PAFIK8Q4\SQLEXPRESS;Initial Catalog=kisiler;Integrated Security=True;TrustServerCertificate=True";


        private SerialPort serialPort;
        public static string portismi, banthizi;
        string[] ports = SerialPort.GetPortNames();
        //var olan port isimlerini buraya attık.


        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = "COM3"; // Arduino'nun bağlı olduğu COM portu buraya yazın
                    serialPort.BaudRate = 9600;   // Arduino ile aynı baud hızını kullanın (ör. 9600)
                    serialPort.DataBits = 8;      // Veri bit sayısı
                    serialPort.Parity = Parity.None; // Parity kontrolü yok
                    serialPort.StopBits = StopBits.One; // Tek durdurma biti
                    serialPort.Open();           // Portu aç

                    MessageBox.Show("Arduino'ya bağlanıldı!");
                }
                else
                {
                    MessageBox.Show("Arduino zaten bağlı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
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

        
        private void button1_Click(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort.ReadExisting();

            if (sonuc != "")
            {
                label2.Text = sonuc;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(label2.Text))
            {
                MessageBox.Show("Lütfen önce bir kart okutun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Ad ve Soyad birleştiriliyor
                string adSoyad = textBox1.Text.Trim();

                // SQL bağlantısını aç
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Veriyi tabloya ekle
                    string query = "INSERT INTO ogrencilistesi (KartID, AdSoyad, OgrenciNo) VALUES (@KartID, @AdSoyad, @OgrenciNo)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KartID", label2.Text);
                        command.Parameters.AddWithValue("@AdSoyad", adSoyad);
                        command.Parameters.AddWithValue("@OgrenciNo", textBox2.Text.Trim());

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kayıt başarıyla tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
            {
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                    //bilgisayarımıza bağlı olan bütün portlar combobox1 e aktarılıyor.

                }
                //kullandığımız bant hızını combobox2 ye eklıyoruz.
                comboBox2.Items.Add("2400");
                comboBox2.Items.Add("4800");
                comboBox2.Items.Add("9600");
                comboBox2.Items.Add("19200");
                comboBox2.Items.Add("115200");

                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 2;
                serialPort = new SerialPort();


        }

        }
    }


