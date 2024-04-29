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
    public partial class FormListe : Form
    {
        public FormListe()
        {
            InitializeComponent();
        }
        string baglanti = "Server=localhost;Database=school;Uid=root;Pwd=''";
        DataTable dt;

        void TumVerileriGetir()
        {
            using (MySqlConnection con = new MySqlConnection(baglanti))
            {

                string sql = "SELECT *FROM ogrenciler";
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql, con);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                dt = new DataTable();

                da.Fill(dt);

                dgvListe.DataSource = dt;
                dgvListe.Invalidate();
                dgvListe.Refresh();

            }
        }


        void VeriFiltrele(string sql)
        {
            using (MySqlConnection con = new MySqlConnection(baglanti))
            {

                
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql, con);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                dt = new DataTable();

                da.Fill(dt);

                dgvListe.DataSource = dt;
                dgvListe.Invalidate();
                dgvListe.Refresh();

            }
        }

        private void dgvListe_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
             txtNumara.Text = dgvListe.CurrentRow.Cells["numara"].Value.ToString();
             txtAd.Text = dgvListe.CurrentRow.Cells["ad"].Value.ToString();
             txtSoyad.Text = dgvListe.CurrentRow.Cells["soyad"].Value.ToString();
             dtpDogumTarih.Value = Convert.ToDateTime(dgvListe.CurrentRow.Cells["dtarih"].Value);
             chkMezun.Checked = Convert.ToBoolean(dgvListe.CurrentRow.Cells["mezun_durum"].Value);

            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(baglanti))
            {
                string sql = "DELETE FROM ogrenciler WHERE numara=@numara";
                int secilenNumara = Convert.ToInt32(txtNumara.Text);
                
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@numara", secilenNumara);

                

                DialogResult result = MessageBox.Show("Öğrenci silinsin mi?", "Öğrenci Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    TumVerileriGetir();
                }


            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string cins = "E";
            using (MySqlConnection con = new MySqlConnection(baglanti))
            {
                string sql = "UPDATE ogrenciler SET numara=@numara, ad=@ad,soyad=@soyad,dtarih=@dtarih,cinsiyet=@cinsiyet,mezun_durum=@mezun_durum,WHERE numara=@numara;";
                int secilenNumara = Convert.ToInt32(txtNumara.Text);

                if (rbKiz.Checked)
                {
                    cins = "K";
                }
                
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql, con);


                cmd.Parameters.AddWithValue("@numara", txtNumara.Text);
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@dtarih", dtpDogumTarih.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", cins);
                cmd.Parameters.AddWithValue("@mezun_durum", chkMezun.Checked);



                DialogResult result = MessageBox.Show("Kayıt güncellensin mi?", "Kayıt Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    TumVerileriGetir();
                }


            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FormEkle formEkle = new FormEkle();
            formEkle.ShowDialog();
            TumVerileriGetir();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string sql = "";

            if (rbAd.Checked)
            {
                sql = "SELECT *FROM ogrenciler WHERE ad LIKE '%" + txtAra.Text + "%'";
            }

            else
            {
                sql = "SELECT *FROM ogrenciler WHERE numara LIKE '%" + txtAra.Text + "%'";
            }

            VeriFiltrele(sql);
        }

        private void FormListe_Load(object sender, EventArgs e)
        {
            TumVerileriGetir();
        }
    }
}
