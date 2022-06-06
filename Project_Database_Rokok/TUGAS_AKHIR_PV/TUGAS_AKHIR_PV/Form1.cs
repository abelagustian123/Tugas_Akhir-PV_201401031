using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//3
using System.Data;
using System.Data.SqlClient;

namespace TUGAS_AKHIR_PV
{
    public partial class Form1 : Form
    {

        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;

        Koneksi Konn = new Koneksi();

        void idTransaksiOtotmatis()
        {
            long hitung;
            string urutan;
            SqlDataReader rd;
            SqlConnection conn = Konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select ID_Transaksi from TBL_ROKOK_2 where ID_Transaksi in(select max(ID_Transaksi) from TBL_ROKOK_2) order by ID_Transaksi desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["ID_Transaksi"].ToString().Length - 3, 3)) + 1;
                string kodeurutan = "000" + hitung;
                urutan = "TRB" + kodeurutan.Substring(kodeurutan.Length - 3, 3);
            }
            else
            {
                urutan = "TRB001";
            }
            rd.Close();
            textBox3.Text = urutan;
            conn.Close();
        }

        public Form1()
        {
            InitializeComponent();
            TampilBarang();
        }

        void Bersihkan()
        {
            textBox3.Text = "";
            textBox1.Text = "PMRA";
            textBox2.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox7.Text = "0";
            textBox8.Text = "0";
            comboBox1.Text = "";
            TampilBarang();
            idTransaksiOtotmatis();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilBarang();
            Bersihkan();
            idTransaksiOtotmatis();
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*****************************************************
             * Memeriksa apakah kolom text kosong ? *
             ****************************************************/
            if (textBox3.Text.Trim() == "" || textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || comboBox2.Text.Trim() == "" || comboBox3.Text.Trim() == "" || textBox7.Text.Trim() == "" || textBox8.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Mohon isikan terlebih dahulu kolom-kolom yang tersedia");

            }
            else
            {
                /*****************************************************
                 * Simpan Data *
                 ****************************************************/
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("INSERT INTO TBL_ROKOK_2 VALUES('" + textBox3.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox2.Text + "' ,'" + comboBox3.Text + "' ,'"+ textBox7.Text + "' ,'" + textBox8.Text + "' ,'" + comboBox1.Text + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Data Berhasil!!");
                    TampilBarang();
                    Bersihkan();
                }
                catch (Exception X)
                {
                    MessageBox.Show("Tidak dapat menyimpan Data");
                }
            }
        }

        void TampilBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_ROKOK_2", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_ROKOK_2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_ROKOK_2";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        void CariBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_ROKOK_2 where Kode_Barang like '%" + textBox4.Text + "%' or Nama_Barang like '%" + textBox4.Text + "%'", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_ROKOK_2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_ROKOK_2";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox3.Text = row.Cells["ID_Transaksi"].Value.ToString();
                textBox1.Text = row.Cells["ID_Pelanggan"].Value.ToString();
                textBox2.Text = row.Cells["Nama_Pelanggan"].Value.ToString();
                comboBox2.Text = row.Cells["Kode_Barang"].Value.ToString();
                comboBox3.Text = row.Cells["Nama_Barang"].Value.ToString();
                textBox7.Text = row.Cells["Harga_Jual"].Value.ToString();
                textBox8.Text = row.Cells["Jumlah"].Value.ToString();
                comboBox1.Text = row.Cells["Satuan"].Value.ToString();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "" || textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || comboBox2.Text.Trim() == "" || comboBox3.Text.Trim() == "" || textBox7.Text.Trim() == "" || textBox8.Text.Trim() == "" || comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Mohon isikan terlebih dahulu kolom-kolom yang tersedia");

            }
            else
            {
                /*****************************************************
                 * Simpan Data *
                 ****************************************************/
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("UPDATE TBL_ROKOK_2 SET ID_Transaksi='" + textBox3.Text + "', ID_Pelanggan='" + textBox1.Text + "', Nama_Pelanggan='" + textBox2.Text + "',Kode_Barang='" + comboBox2.Text + "' ,Nama_Barang='" + comboBox3.Text + "' ,Harga_Jual='"+ textBox7.Text + "' ,Jumlah='" + textBox8.Text + "' ,Satuan='" + comboBox1.Text + "' WHERE ID_Transaksi='" + textBox3.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Data Berhasil!!");
                    TampilBarang();
                    Bersihkan();
                }
                catch (Exception X)
                {
                    MessageBox.Show("Tidak dapat menyimpan Data");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(comboBox3.Text + ",Yakin ingin dihapus?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                /*****************************************************
                 * Hapus Data *
                 ****************************************************/
                SqlConnection conn = Konn.GetConn();
                cmd = new SqlCommand("DELETE TBL_ROKOK_2 WHERE ID_Transaksi='" + textBox3.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Hapus Data Berhasil!!");
                TampilBarang();
                Bersihkan();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CariBarang();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bersihkan();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DataBarang dataBarang = new DataBarang();
            dataBarang.Show();

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
    }
}