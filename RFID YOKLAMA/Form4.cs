using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFID_YOKLAMA
{
    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        private static Form4 instance; // Singleton için referans

        private SerialPort serialPort;
        public static Form4 GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Form4();
            }
            return instance;
        }

        private string kartID;
        public Form4()
        {
            InitializeComponent();

            dt = new DataTable();

        }

        public void AddKartData(string kartID)
        {
            string SqlCon = @"Data Source=LAPTOP-PAFIK8Q4\SQLEXPRESS;Initial Catalog=kisiler;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(SqlCon))
                {
                    con.Open();
                    kartID = System.Text.RegularExpressions.Regex.Replace(kartID, @"\s+", " ").Trim();
                    if (!kartID.StartsWith(""))
                    {
                        kartID = "" + kartID.Trim();
                    }

                    string query = "SELECT * FROM ogrencilistesi WHERE KartID = @KartID";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@KartID", kartID);

                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable kartTable = new DataTable();
                    kartTable.Load(dr);

                    if (kartTable.Rows.Count > 0)
                    {
                        // Yeni veriyi DataGridView'e ekle
                        foreach (DataRow row in kartTable.Rows)
                        {
                            dt.ImportRow(row); // Mevcut DataTable'a satır ekle
                        }
                        dataGridView1.DataSource = dt; // DataGridView'i güncelle

                        
                    }
                    else
                    {
                        MessageBox.Show("Bu kart kayıtlı değil!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Tabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Form4_Load(object sender, EventArgs e)
        {
            

            string SqlCon = @"Data Source=LAPTOP-PAFIK8Q4\SQLEXPRESS;Initial Catalog=kisiler;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(SqlCon))
                {
                    dt.Columns.Add("KartID");         // Örnek kolonlar
                    dt.Columns.Add("AdSoyad");
                    dt.Columns.Add("OgrenciNo");
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Tabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // TXT dosyasının adı bugünün tarihiyle olacak
                string fileName = $"Yoklama_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                // Dosyanın kaydedileceği klasör yolu
                string folderPath = @"C:\Users\asus\OneDrive\Masaüstü\Yoklamalar";

                // Klasör yoksa oluştur
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Tam dosya yolu
                string filePath = Path.Combine(folderPath, fileName);

                // Dosyayı oluştur ve yazmaya başla
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // Başlık olarak bugünün tarihini ekle
                    sw.WriteLine($"Yoklama Tarihi: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                    sw.WriteLine("=========================================");

                    // DataGridView'deki tüm satırları oku ve yaz
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow) // Yeni satır olmadığından emin ol
                        {
                            // Sütunlar arasında tab boşluğu koyarak satır yaz
                            List<string> rowData = new List<string>();
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                rowData.Add(cell.Value?.ToString() ?? "");
                            }

                            // Satırı dosyaya yaz
                            sw.WriteLine(string.Join("\t", rowData));
                        }
                    }
                }

                // Başarılı mesajı göster
                MessageBox.Show("Yoklama başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}