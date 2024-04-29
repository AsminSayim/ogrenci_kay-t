using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ogrenci_liste
{
    public partial class FormEkle : Form
    {
        public FormEkle()
        {
            InitializeComponent();
        }
        string baglanti = "Server=localhost;Database=school;Uid=root;Pwd=''";

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string cins = "E";
            if (rbKiz.Checked)
            {
                cins = "K";
            }
            using (MySqlConnection con = new MySqlConnection(baglanti))
            {

                string sql = "INSERT INTO ogrenciler (numara,ad,soyad,dtarih,cinsiyet,mezun_durum) " +
                    "VALUES (@numara,@ad,@soyad,@dtarih,@cinsiyet,@mezun_durum)";

                

                con.Open();

               

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@numara",txtNumara .Text);
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@dtarih", dtpDogumTarih.Text);       
                cmd.Parameters.AddWithValue("@cinsiyet",cins);
                cmd.Parameters.AddWithValue("@mezun_durum", chkMezun.Checked);
               


                //cmd.ExecuteNonQuery();

                DialogResult result = MessageBox.Show("Öğrenci eklensin mi?", "Öğrenci Ekle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();

                }

                txtNumara.Clear();
                txtAd.Clear();
                txtSoyad.Clear();
                chkMezun.Checked = false;
                


            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    
}
