using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLNV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Kết nối cơ sở dữ liệu
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-P9S4ORN;Initial Catalog=QLNV;Integrated Security=True");
        private void openCon()
        { 
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        private void closeCon() 
        { 
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        private Boolean Exe(string cmd)
        {
            openCon();
            Boolean check;
            try
            {
                SqlCommand sc = new SqlCommand(cmd,con);
                sc.ExecuteNonQuery();
                check = true;
            }
            catch (Exception ex)
            {
                check = false;
            }
            closeCon();
            return check;
        }

        private DataTable Red (string cmd)
        {
            openCon();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand sc = new SqlCommand(cmd,con);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(dt);
            }
            catch(Exception)
            {
                dt = null;
                //throw;
            }
            closeCon();
            return dt;
        }
        //Hiển thị bảng cơ sở dữ liệu
        private void load()
        {
            DataTable dt = Red("SELECT * FROM NhanVien");
            if (dt != null)
            {
                dataGridView1.DataSource= dt;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }
        //reset
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMNV.ResetText();
            txtHT.ResetText();
            txtNS.ResetText();
            txtCV.ResetText();
        }

        //Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            Exe("INSERT INTO NhanVien(MaNV, HoTen, NamSinh, ChucVu) VALUES (N'" + txtMNV.Text + "', N'" + txtHT.Text + "', N'" +  txtNS.Text + "', N'" + txtCV.Text + "')");
            load();
        }

        //Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            Exe("UPDATE NhanVien SET HoTen = N'" + txtHT.Text + "', NamSinh = N'" + txtNS.Text + "', ChucVu = N'" + txtCV.Text + "' WHERE MaNV = '" + txtMNV.Text + "'");
            load();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMNV.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtHT.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtNS.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtCV.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        //Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            Exe("DELETE FROM NhanVien WHERE MaNV = '" + txtMNV.Text + "'");
            load();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            DataTable dt = Red("SELECT * FROM NhanVien WHERE MaNV = '" + txtMNV.Text + "'");
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }
    }
}
