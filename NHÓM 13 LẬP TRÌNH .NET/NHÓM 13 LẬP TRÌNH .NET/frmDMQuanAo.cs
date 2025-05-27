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
    public partial class frmDMQuanAo : Form
    {
        public frmDMQuanAo()
        {
            InitializeComponent();
        }

        private void frmDMQuanAo_Load(object sender, EventArgs e)
        {
            dataGridViewSP.CellClick += new DataGridViewCellEventHandler(dataGridViewSP_CellClick);
            try
            {
                Functions.Connect();
                LoadDataToGridView();
                cboMachatlieu.Items.AddRange(new object[] { "CL001", "CL002", "CL003" });
                cboMaco.Items.AddRange(new object[] { "CO004", "CO003", "CO001" });
                cboMadoituong.Items.AddRange(new object[] { "DT001", "DT002" });
                cboMaloai.Items.AddRange(new object[] { "TL001", "TL002", "TL003" });
                cboMamau.Items.AddRange(new object[] { "M002", "M003", "M004" });
                cboMamua.Items.AddRange(new object[] { "MUA001", "MUA003", "MUA004" });
                cboMaNSX.Items.AddRange(new object[] { "NSX001", "NSX002" });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDataToGridView()
        {
            string sql = "select * from tblSanpham ";
            DataTable dt = new DataTable();
            dt = Functions.LoadDataToTable(sql);
            dataGridViewSP.DataSource = dt;
        }

        private void HienThiDanhSachSanPham()
        {
            string sql = "SELECT MaQuanAo, TenQuanAo FROM tblSanpham";
            SqlDataAdapter da = new SqlDataAdapter(sql, Functions.Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewSP.DataSource = dt;
        }

        private void dataGridViewSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSP.Rows.Count > 0)
            {
                txtMaquanao.Text = dataGridViewSP.CurrentRow.Cells[0].Value.ToString();
                txtTenquanao.Text = dataGridViewSP.CurrentRow.Cells[1].Value.ToString();
                cboMachatlieu.SelectedItem = dataGridViewSP.CurrentRow.Cells[2].Value.ToString();
                cboMaloai.SelectedItem = dataGridViewSP.CurrentRow.Cells[3].Value.ToString();
                cboMaco.SelectedItem = dataGridViewSP.CurrentRow.Cells[4].Value.ToString();
                cboMamau.SelectedItem = dataGridViewSP.CurrentRow.Cells[5].Value.ToString();
                cboMadoituong.SelectedItem = dataGridViewSP.CurrentRow.Cells[6].Value.ToString();
                cboMamua.SelectedItem = dataGridViewSP.CurrentRow.Cells[7].Value.ToString();
                cboMaNSX.SelectedItem = dataGridViewSP.CurrentRow.Cells[8].Value.ToString();
                txtSoluong.Text = dataGridViewSP.CurrentRow.Cells[9].Value.ToString();
                txtDongianhap.Text = dataGridViewSP.CurrentRow.Cells[10].Value.ToString();
                txtDongiaban.Text = dataGridViewSP.CurrentRow.Cells[11].Value.ToString();
                string imagePath = dataGridViewSP.CurrentRow.Cells[12].Value.ToString();
                txtMaquanao.Enabled = false;
            }
        }

        private void ResetValues()
        {
            txtMaquanao.Text = "";
            txtTenquanao.Text = "";
            txtDongianhap.Text = "";
            txtDongiaban.Text = "";
            txtSoluong.Text = "";
            txtAnh.Text = "";
            cboMaloai.SelectedItem = -1;
            cboMachatlieu.SelectedItem = -1;
            cboMaco.SelectedItem = -1;
            cboMadoituong.SelectedItem = -1;
            cboMamau.SelectedItem = -1;
            cboMamua.SelectedItem = -1;
            cboMaNSX.SelectedItem = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            txtMaquanao.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnBoqua.Enabled = true;
            if (txtMaquanao.Text == "")
            {
                MessageBox.Show("Chọn dữ liệu cần xóa");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string sql = "DELETE FROM tblSanpham WHERE MaQuanAo = N'" + txtMaquanao.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, Functions.Con);
                try
                {
                    cmd.ExecuteNonQuery();
                    LoadDataToGridView();
                    MessageBox.Show("Xóa thành công!");
                    ResetValues();
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
            if (txtMaquanao.Text == "")
            {
                MessageBox.Show("Chọn dữ liệu cần sửa");
                return;
            }
            string sql = "UPDATE tblSanpham SET " +
                         "TenQuanAo = N'" + txtTenquanao.Text.Trim() + "', " +
                         "SoLuong = " + txtSoluong.Text.Trim() + ", " +
                         "DonGiaNhap = " + txtDongianhap.Text.Trim() + ", " +
                         "DonGiaBan = " + txtDongiaban.Text.Trim() + ", " +
                         "MaChatLieu = N'" + cboMachatlieu.SelectedItem.ToString() + "', " +
                         "MaCo = N'" + cboMaco.SelectedItem.ToString() + "', " +
                         "MaDoiTuong = N'" + cboMadoituong.SelectedItem.ToString() + "', " +
                         "MaLoai = N'" + cboMaloai.SelectedItem.ToString() + "', " +
                         "MaMau = N'" + cboMamau.SelectedItem.ToString() + "', " +
                         "MaMua = N'" + cboMamua.SelectedItem.ToString() + "', " +
                         "MaNSX = N'" + cboMaNSX.SelectedItem.ToString() + "', " +
                         "Anh = N'" + txtAnh.Text.Trim() + "' " +
                         "WHERE MaQuanAo = N'" + txtMaquanao.Text.Trim() + "'";
            Functions.Connect();
            SqlCommand cmd = new SqlCommand(sql, Functions.Con);
            try
            {
                cmd.ExecuteNonQuery();
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
            //kiểm tra dữ liệu
            if (txtMaquanao.Text == "")
            {
                MessageBox.Show("Chưa nhập mã");
            }
            // lưu
            string sql = "INSERT INTO tblSanpham (Maquanao, Tenquanao, Soluong, Dongianhap, Dongiaban, Machatlieu, Maco, Madoituong, Maloai, Mamau, Mamua, MaNSX, Anh) " +
              "VALUES (N'" + txtMaquanao.Text.Trim() + "', N'" + txtTenquanao.Text.Trim() + "', N'" + txtSoluong.Text.Trim() +
              "', N'" + txtDongianhap.Text.Trim() + "', N'" + txtDongiaban.Text.Trim() +
              "', N'" + cboMachatlieu.SelectedItem + "', N'" + cboMaco.SelectedItem +
              "', N'" + cboMadoituong.SelectedItem + "', N'" + cboMaloai.SelectedItem +
              "', N'" + cboMamau.SelectedItem + "', N'" + cboMamua.SelectedItem +
              "', N'" + cboMaNSX.SelectedItem + "', N'" + txtAnh.Text.Trim() + "')";
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
            ResetValues();
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            txtMaquanao.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = false;
            LoadDataToGridView();
            picAnh.Image = null;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh";
            ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                // Hiển thị ảnh lên PictureBox
                picAnh.Image = Image.FromFile(filePath);
                picAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                // Lưu đường dẫn vào textbox
                txtAnh.Text = filePath;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa cần tìm!", "Thông báo");
                return;
            }
            string sql = "SELECT * FROM tblSanpham WHERE Tenquanao LIKE N'%" + keyword + "%'";
            DataTable dt = Functions.LoadDataToTable(sql);
            dataGridViewSP.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào phù hợp.", "Kết quả tìm kiếm");
            }
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            btnBoqua.Enabled = true;
            try
            {
                Functions.Connect(); // đảm bảo đã kết nối
                HienThiDanhSachSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form hiện tại
            }
        }
    }
}
