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

namespace Bài_tập_lớn
{
    public partial class frmDMKhachHang : Form
    {
        public frmDMKhachHang()
        {
            InitializeComponent();
        }

        private void frmDMKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                Functions.Connect();
                ///  MessageBox.Show("Ket noi thanh cong");
                ///  load data to gridview
                LoadDataToGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDataToGridView()
        {
            string sql = "select * from tblKhachHang";
            DataTable dt = new DataTable();
            dt = Functions.LoadDataToTable(sql);
            dataGridViewKH.DataSource = dt;
        }

        private void Format()
        {
            mskDienthoai.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string DienThoai = mskDienthoai.Text;
        }

        private void dataGridViewKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewKH.Rows.Count > 0)
            {
                txtMakhachhang.Text = dataGridViewKH.CurrentRow.Cells[0].Value.ToString();
                txtTenkhachhang.Text = dataGridViewKH.CurrentRow.Cells[1].Value.ToString();
                txtDiachi.Text = dataGridViewKH.CurrentRow.Cells[2].Value.ToString();
                mskDienthoai.Text = dataGridViewKH.CurrentRow.Cells[3].Value.ToString();
                txtMakhachhang.Enabled = false;
            }
        }

        private void Resetvalues()
        {
            txtMakhachhang.Text = "";
            txtTenkhachhang.Text = "";
            txtDiachi.Text = "";
            mskDienthoai.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Resetvalues();
            btnBoqua.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            txtMakhachhang.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnBoqua.Enabled = true;
            if (txtMakhachhang.Text == "")
            {
                MessageBox.Show("Chọn dữ liệu cần xóa");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string sql = "DELETE FROM tblKhachHang WHERE MaKhach = N'"
                    + txtMakhachhang.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, Functions.Con);
                try
                {
                    cmd.ExecuteNonQuery();
                    LoadDataToGridView();
                    MessageBox.Show("Xóa thành công!");
                    Resetvalues();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnBoqua.Enabled = true;
            if (txtMakhachhang.Text == "")
            {
                MessageBox.Show("Chọn dữ liệu cần sửa");
                return;
            }
            string sql = "UPDATE tblKhachHang SET " +
                         "TenKhach = N'" + txtTenkhachhang.Text.Trim() + "', " +
                         "DiaChi = N'" + txtDiachi.Text.Trim() + "', " +
                         "DienThoai = '" + mskDienthoai.Text.Trim() + "' " +
                         "WHERE MaKhach = N'" + txtMakhachhang.Text.Trim() + "'";
            Functions.Connect();
            SqlCommand cmd = new SqlCommand(sql, Functions.Con);
            try
            {
                cmd.ExecuteNonQuery();
                Format();
                LoadDataToGridView();
                MessageBox.Show("Sửa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMakhachhang.Text == "")
            {
                MessageBox.Show("Chưa nhập mã!");
            }
            Format();
            // luu
            string sql = "INSERT INTO tblKhachHang  (MaKhach, TenKhach, DiaChi, DienThoai) " +
              "VALUES (N'" + txtMakhachhang.Text.Trim() + "', N'" + txtTenkhachhang.Text.Trim() + "', N'" +
              txtDiachi.Text.Trim() + "', N'" + mskDienthoai.Text.Trim() + "')";
            SqlCommand cmd = new SqlCommand(sql, Functions.Con);
            try
            {
                cmd.ExecuteNonQuery();
                
                LoadDataToGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Resetvalues();
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            Resetvalues();
            txtMakhachhang.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form hiện tại
            }
        }
    }
}
