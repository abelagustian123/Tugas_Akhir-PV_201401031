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
    public partial class DataBarang : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;

        Koneksi Konn = new Koneksi();
        public DataBarang()
        {
            InitializeComponent();
            TampilBarang();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_DATA_ROKOK_2", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_DATA_ROKOK_2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_DATA_ROKOK_2";
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
        void TampilBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBL_DATA_ROKOK_2", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_DATA_ROKOK_2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_DATA_ROKOK_2";
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
