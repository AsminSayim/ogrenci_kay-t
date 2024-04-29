using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ogrenci_liste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       

        

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnTablo_Click(object sender, EventArgs e)
        {
            FormListe formliste = new FormListe();
            formliste.ShowDialog();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            FormEkle formekle = new FormEkle();
            formekle.ShowDialog();
        }
    }
}
