using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DinhAnhMinh_2020607249
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        XmlDocument doc = new XmlDocument();
        string tenTep = @"D:\DinhAnhMinh_2020607249\DinhAnhMinh_2020607249\XMLFile1.xml";

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void HienThi()
        {
            doc.Load(tenTep);
            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");
            dgvNhanVien.Rows.Clear();
            dgvNhanVien.ColumnCount = 4;
            dgvNhanVien.Rows.Add();
            int sd = 0;
            foreach (XmlNode nhanvien in DS)
            {
                XmlNode maNV = nhanvien.SelectSingleNode("@manv");
                dgvNhanVien.Rows[sd].Cells[0].Value = maNV.InnerText;
                XmlNode ho = nhanvien.SelectSingleNode("hoten/ho");
                dgvNhanVien.Rows[sd].Cells[1].Value = ho.InnerText;
                XmlNode ten = nhanvien.SelectSingleNode("hoten/ten");
                dgvNhanVien.Rows[sd].Cells[2].Value = ten.InnerText;
                XmlNode diaChi = nhanvien.SelectSingleNode("diachi");
                dgvNhanVien.Rows[sd].Cells[3].Value = diaChi.InnerText;
                sd++;
                dgvNhanVien.Rows.Add();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            doc.Load(tenTep);
            XmlElement goc = doc.DocumentElement;
            XmlNode nhanVienThem = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");
            if (nhanVienThem == null)
            {
                XmlNode nhanVien = doc.CreateElement("nhanvien");
                XmlAttribute maNV = doc.CreateAttribute("manv");
                maNV.InnerText = txtMaNV.Text;

                XmlNode hoten = doc.CreateElement("hoten");
                XmlNode ho = doc.CreateElement("ho");
                ho.InnerText = txtHo.Text;

                XmlNode ten = doc.CreateElement("ten");
                ten.InnerText = txtTen.Text;

                XmlNode diaChi = doc.CreateElement("diachi");
                diaChi.InnerText = txtDiaChi.Text;

                hoten.AppendChild(ho);
                hoten.AppendChild(ten);
                nhanVien.Attributes.Append(maNV);
                nhanVien.AppendChild(hoten);
                nhanVien.AppendChild(diaChi);
                goc.AppendChild(nhanVien);
                doc.Save(tenTep);
                HienThi();
                MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            else
                MessageBox.Show("Nhân viên này đã tồn tại!", "Thông báo");
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txtMaNV.Text = dgvNhanVien.Rows[d].Cells[0].Value.ToString();
            txtHo.Text = dgvNhanVien.Rows[d].Cells[1].Value.ToString();
            txtTen.Text = dgvNhanVien.Rows[d].Cells[2].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.Rows[d].Cells[3].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            doc.Load(tenTep);
            XmlElement goc = doc.DocumentElement;
            XmlNode nhanVienSua = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");
            if (nhanVienSua != null)
            {
                nhanVienSua.SelectSingleNode("hoten/ho").InnerText = txtHo.Text;
                nhanVienSua.SelectSingleNode("hoten/ten").InnerText = txtTen.Text;
                nhanVienSua.SelectSingleNode("diachi").InnerText = txtDiaChi.Text;
                doc.Save(tenTep);
                HienThi();
                MessageBox.Show("Sửa thành công!", "Thông báo");
            }
            else
                MessageBox.Show("Không tìm thấy nhân viên cần sửa!", "Thông báo");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            doc.Load(tenTep);
            XmlElement goc = doc.DocumentElement;
            XmlNode nhanVienXoa = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");
            if (nhanVienXoa != null)
            {
                goc.RemoveChild(nhanVienXoa);
                doc.Save(tenTep);
                HienThi();
                MessageBox.Show("Xóa thành công", "Thông báo");
            }
            else
                MessageBox.Show("Không thấy nhân viên cần xóa", "Thông báo");
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            doc.Load(tenTep);
            XmlElement goc = doc.DocumentElement;
            XmlNode nhanVienTim = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");
            if (nhanVienTim != null)
            {
                dgvNhanVien.Rows.Clear();
                dgvNhanVien.ColumnCount = 4;
                dgvNhanVien.Rows.Add();

                dgvNhanVien.Rows[0].Cells[0].Value = nhanVienTim.SelectSingleNode("@manv").InnerText;
                dgvNhanVien.Rows[0].Cells[1].Value = nhanVienTim.SelectSingleNode("hoten/ho").InnerText;
                dgvNhanVien.Rows[0].Cells[2].Value = nhanVienTim.SelectSingleNode("hoten/ten").InnerText;
                dgvNhanVien.Rows[0].Cells[3].Value = nhanVienTim.SelectSingleNode("diachi").InnerText;
            }
            else
                MessageBox.Show("Không tìm thấy nhân viên này!", "Thông báo");
        }
    }
}
