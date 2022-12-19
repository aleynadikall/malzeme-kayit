using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;  // veritabanu bağlantısı için dahil edilen kütüphanedir.
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode= DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Formun Load olayına bir takım komutlar yazacağız. Ancak veritabanında yapılan her değişiklikten sonra o kayıtları yeniden görüntülemek için aynı komutları tekrar tekrar çağırmamız gerekiyor. Bu amaçla listele adında bir
            // method oluşturuyoruz. Form_Load olayında listele() methodunu çağıracağız. Böylece veritabanındaki kayıtları güncel bir şekilde yenidden görüntülemek istediğimizde sadece bu methodu çağırmamız yeterli olacaktır.
            listele();
        }


        //veritabanındaki güncel kayıtların görüntülenmesi
        private void listele()
        {
            //Oncelikle bağlantıyı açıyoruz.
            baglanti.Open();

            // Tüm kayıtları göster...
            SqlDataAdapter da = new SqlDataAdapter("Select * from Malzemeler", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;

            // Bağlantıyı kapatmaya özen gösterelim yoksa yeni bir bağlantı açarken bağlantı devam ettiğinden sıkıntı yaşarız.
            baglanti.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        // SQL Connection yapıyoruz.
        SqlConnection baglanti = new SqlConnection("Data Source= SEFA-PC; Initial Catalog=Stok; Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            //Ekle Butonu

            // Verileri string tipinde alıyorum.
            String t1 = textBox1.Text;      //Malzeme Kodu
            String t2 = textBox2.Text;      //Malzeme Adı
            String t3 = textBox3.Text;      //Yıllık Satış
            String t4 = textBox4.Text;      //Fiyat
            String t5 = textBox5.Text;      //Min Stok
            String t6 = textBox6.Text;      //Tedarik Süresi

            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Malzemeler (MalzemeKodu, MalzemeAdi, YillikSatis, BirimFiyat, MinStok, TSuresi) VALUES ('"+t1+"', '"+t2+"', '"+t3+"', '"+t4+"', '"+t5+"', '"+t6+"')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SİL 
            String t1 = textBox1.Text;      // Seçilen satırın malzeme kodunu al
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM Malzemeler WHERE MalzemeKodu=('"+t1+"')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //GUNCELLE

            // TextBoxlardaki değişkenleri string tipinde alıyorum.
            String t1 = textBox1.Text;      //Malzeme Kodu
            String t2 = textBox2.Text;      //Malzeme Adı
            String t3 = textBox3.Text;      //Yıllık Satış
            String t4 = textBox4.Text;      //Fiyat
            String t5 = textBox5.Text;      //Min Stok
            String t6 = textBox6.Text;      //Tedarik Süresi

            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE Malzemeler SET MalzemeAdi = '"+t2+"', YillikSatis = '"+t3+"', BirimFiyat =  '"+t4+"', MinStok = '"+t5+"', TSuresi = '"+t6+"' WHERE MalzemeKodu='"+t1+"' ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();

        }
    }
}
