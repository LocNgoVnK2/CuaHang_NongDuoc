﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc
{
    public partial class frmBanLe: Form
    {
        SanPhamController ctrlSanPham = new SanPhamController();
        KhachHangController ctrlKhachHang = new KhachHangController();
        MaSanPhamController ctrlMaSanPham = new MaSanPhamController();
        PhieuBanController ctrlPhieuBan = new PhieuBanController();
        ChiTietPhieuBanController ctrlChiTiet = new ChiTietPhieuBanController();
        IList<MaSanPham> deleted = new List<MaSanPham>();
        Controll status = Controll.Normal;
        decimal baseTongTien = 0;
        String tenNhanVien;

        public frmBanLe(string tenNhanVien )
        {
            InitializeComponent();
            status = Controll.AddNew;
            this.tenNhanVien = tenNhanVien;
        }

     
        public frmBanLe(string tenNhanVien, PhieuBanController ctrlPB)
            : this(tenNhanVien)
        {
            this.ctrlPhieuBan = ctrlPB;
            status = Controll.Normal;
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {

            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
            ctrlMaSanPham.HienThiDataGridViewComboBox(colMaSanPham);

            cmbSanPham.SelectedIndexChanged += new EventHandler(cmbSanPham_SelectedIndexChanged);

            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, false);

            ctrlPhieuBan.HienthiPhieuBan(bindingNavigator,cmbKhachHang, txtMaPhieu, dtNgayLapPhieu, numTongTien, numDaTra, numConNo);

            bindingNavigator.BindingSource.CurrentChanged -= new EventHandler(BindingSource_CurrentChanged);
            bindingNavigator.BindingSource.CurrentChanged += new EventHandler(BindingSource_CurrentChanged);
            
            ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, txtMaPhieu.Text);


            if (status == Controll.AddNew)
            {
                txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
            }
            else
            {
                this.Allow(false);
            }

        }

        void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (status == Controll.Normal)
            {
                ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, txtMaPhieu.Text);
            }
        }


        void cmbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSanPham.SelectedValue != null)
            {
                MaSanPhamController ctrlMSP = new MaSanPhamController();

                cmbMaSanPham.SelectedIndexChanged -= new EventHandler(cmbMaSanPham_SelectedIndexChanged);
                ctrlMSP.HienThiAutoComboBox(cmbSanPham.SelectedValue.ToString(), cmbMaSanPham);
                cmbMaSanPham.SelectedIndexChanged += new EventHandler(cmbMaSanPham_SelectedIndexChanged);
                rdHetHanXuatTruoc.Enabled = true;
                rdNhapTruocXuatTruoc.Enabled = true;
                rdXuatChiDinh.Enabled = true;
            }
        }

        void cmbMaSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaSanPham.SelectedValue == null)
                return;
            try
            {
                MaSanPhamController ctrl = new MaSanPhamController();
                MaSanPham masp = ctrl.LayMaSanPham(cmbMaSanPham.SelectedValue.ToString());

                numDonGia.Value = masp.SanPham.GiaBanLe;

                string format = "#,###0";
                txtGiaNhap.Text = masp.GiaNhap.ToString(format);
                txtGiaBanSi.Text = masp.SanPham.GiaBanSi.ToString(format);
                txtGiaBanLe.Text = masp.SanPham.GiaBanLe.ToString(format);
                txtGiaBQGQ.Text = masp.SanPham.DonGiaNhap.ToString(format);
                numSoLuong.Maximum = masp.SoLuong;
                dtpNgayHetHan.Value = masp.NgayHetHan;
            }
            catch (Exception ex)
            {
             
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (cmbMaSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Mã sản phẩm !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numDonGia.Value * numSoLuong.Value != numThanhTien.Value)
            {
                MessageBox.Show("Thành tiền sai!", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // numTongTien.Value += numThanhTien.Value;
                baseTongTien += numThanhTien.Value;
                TinhTongTien();
                DataRow row = ctrlChiTiet.NewRow();
               
                row["ID_MA_SAN_PHAM"] = cmbMaSanPham.SelectedValue;
                row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
                row["DON_GIA"] = numDonGia.Value;
                row["SO_LUONG"] = numSoLuong.Value;
                row["THANH_TIEN"] = numThanhTien.Value;
                ctrlChiTiet.Add(row);

            }

        }

        private void numDonGia_ValueChanged(object sender, EventArgs e)
        {
            numThanhTien.Value = numSoLuong.Value * numDonGia.Value;
        }

        private void numTongTien_ValueChanged(object sender, EventArgs e)
        {
            numConNo.Value = numTongTien.Value - numDaTra.Value;
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            this.Luu();
            status = Controll.Normal;
            this.Allow(false);
        }

        void Luu()
        {
            if (status == Controll.AddNew)
            {
                ThemMoi();
            }
            else
            {
                CapNhat();
            }
        }

        void CapNhat()
        {
            foreach (MaSanPham masp in deleted)
            {
                CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(masp.Id, masp.SoLuong);
            }
            deleted.Clear();

            ctrlChiTiet.Save();

            ctrlPhieuBan.Update();

        }
        void ThemMoi()
        {

            DataRow row = ctrlPhieuBan.NewRow();
            row["NHAN_VIEN"] = tenNhanVien;
            row["ID"] = txtMaPhieu.Text;
            row["ID_KHACH_HANG"] = cmbKhachHang.SelectedValue;
            row["NGAY_BAN"] = dtNgayLapPhieu.Value.Date;
            row["TONG_TIEN"] = numTongTien.Value;
            row["DA_TRA"] = numDaTra.Value;
            row["CON_NO"] = numConNo.Value;
            row["CHI_PHI_VAN_CHUYEN"] = nmrVanChuyen.Value;
            row["DICH_VU_PHU"] = nmrDichVu.Value;
            decimal thanhTien = baseTongTien + nmrVanChuyen.Value + nmrDichVu.Value;
            decimal soTienChietKhau = thanhTien * nmrChietKhau.Value / 100;
            row["GIAM_GIA"] = soTienChietKhau;
            ctrlPhieuBan.Add(row);

            PhieuBanController ctrl = new PhieuBanController();

            if (ctrl.LayPhieuBan(txtMaPhieu.Text) != null)
            {
                MessageBox.Show("Mã Phiếu bán này đã tồn tại !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            long so = Convert.ToInt64(txtMaPhieu.Text);// nếu text không phải số return 0
            if (so != 0)
            {
                if (so >= ThamSo.LayMaPhieuBan())
                {
                    ThamSo.GanMaPhieuBan(so + 1);
                }
            }
               
            

            ctrlPhieuBan.Save();

            ctrlChiTiet.Save();

        }

        private void toolLuu_Them_Click(object sender, EventArgs e)
        {
            ctrlPhieuBan = new PhieuBanController();
            status = Controll.AddNew;
            txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
            numTongTien.Value = 0;
            ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, txtMaPhieu.Text);
            this.Allow(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BindingSource bs = ((BindingSource)dgvDanhsachSP.DataSource);
                DataRowView row = (DataRowView)bs.Current;
                //    numTongTien.Value -= Convert.ToInt64(row["THANH_TIEN"]);
                baseTongTien -= Convert.ToInt64(row["THANH_TIEN"]);
                TinhTongTien();
                deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));
                bs.RemoveCurrent();
            }
           
        }

        private void dgvDanhsachSP_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                BindingSource bs = ((BindingSource)dgvDanhsachSP.DataSource);
                DataRowView row = (DataRowView)bs.Current;
                numTongTien.Value -= Convert.ToInt64(row["THANH_TIEN"]);
                deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));

            }
        }

        private void toolLuuIn_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                MessageBox.Show("Vui lòng lưu lại Phiếu bán hiện tại!", "Phieu Ban Le", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String ma_phieu = txtMaPhieu.Text;

                PhieuBanController ctrlPB = new PhieuBanController();

                CuahangNongduoc.BusinessObject.PhieuBan ph = ctrlPB.LayPhieuBan(ma_phieu);

                frmInPhieuBan InPhieuBan = new frmInPhieuBan(ph);

                InPhieuBan.Show();

            }
        }

        private void dgvDanhsachSP_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void toolChinhSua_Click(object sender, EventArgs e)
        {
            status = Controll.Edit;
            this.Allow(true);
        }

        void Allow(bool val)
        {
            txtMaPhieu.Enabled = val;
            dtNgayLapPhieu.Enabled = val;
            numTongTien.Enabled = val;
            btnAdd.Enabled = val;
            btnRemove.Enabled = val;
            dgvDanhsachSP.Enabled = val;
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                if (MessageBox.Show("Bạn có muốn lưu lại Phiếu bán này không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Luu();
                }
            }
            this.Close();
        }

        private void toolXoa_Click(object sender, EventArgs e)
        {
            DataRowView view =  (DataRowView)bindingNavigator.BindingSource.Current;
            if (view != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ChiTietPhieuBanController ctrl = new ChiTietPhieuBanController();
                    IList<ChiTietPhieuBan> ds = ctrl.ChiTietPhieuBanTheoID(view["ID"].ToString());
                    foreach (ChiTietPhieuBan ct in ds)
                    {
                        CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(ct.MaSanPham.Id, ct.SoLuong);
                    }
                    bindingNavigator.BindingSource.RemoveCurrent();
                    ctrlPhieuBan.Save();
                }
            }
        }

        private void btnThemDaiLy_Click(object sender, EventArgs e)
        {
            frmKhachHang KhachHang = new frmKhachHang();
            KhachHang.ShowDialog();
            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, false);
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmSanPham SanPham = new frmSanPham();
            SanPham.ShowDialog();
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
        }

        private void rdNhapTruocXuatTruoc_CheckedChanged(object sender, EventArgs e)
        {
            cmbMaSanPham.Enabled = !rdNhapTruocXuatTruoc.Checked;
            ctrlMaSanPham.HienThiAutoComboBox(cmbSanPham.SelectedValue.ToString(), cmbMaSanPham);
            cmbMaSanPham.SelectedIndex = 0;
            cmbMaSanPham_SelectedIndexChanged(sender, e);
            //cmbMaSanPham.SelectedItem = cmbMaSanPham.Items[0];

        }

        private void rdHetHanXuatTruoc_CheckedChanged(object sender, EventArgs e)
        {
            cmbMaSanPham.Enabled = !rdHetHanXuatTruoc.Checked;
            ctrlMaSanPham.HienThiAutoComboBoxTheoNgayHetHan(cmbSanPham.SelectedValue.ToString(),cmbMaSanPham);
            cmbMaSanPham.SelectedIndex = 0;
            cmbMaSanPham_SelectedIndexChanged(sender, e);
            //cmbMaSanPham.SelectedItem = cmbMaSanPham.Items[0];


        }

        private void rdXuatChiDinh_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void nmrVanChuyen_ValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void nmrDichVu_ValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void nmrChietKhau_ValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }
        private void TinhTongTien()
        {
            decimal thanhTien = baseTongTien + nmrVanChuyen.Value + nmrDichVu.Value;
            decimal soTienChietKhau = thanhTien * nmrChietKhau.Value / 100;
            numTongTien.Value = thanhTien - soTienChietKhau;
        }
  
    }
}
