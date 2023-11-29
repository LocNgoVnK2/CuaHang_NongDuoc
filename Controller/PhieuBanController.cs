using System;
using System.Collections.Generic;
using System.Text;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Windows.Forms;
using System.Data;

namespace CuahangNongduoc.Controller
{
    
    public class PhieuBanController
    {
        PhieuBanFactory factory = new PhieuBanFactory();

        BindingSource bs = new BindingSource();


        public PhieuBanController()
        {
            bs.DataSource = factory.LayPhieuBan("-1");
        }
        public DataRow NewRow()
        {
            return factory.NewRow();
        }
        public void Add(DataRow row)
        {
            factory.Add(row);
        }
        public void Update()
        {
            bs.MoveNext();
            factory.Save();
        }
        public void Save()
        {
            int n = PhieuBanFactory.LaySoPhieu();
            if (n >= 50)
            {
                MessageBox.Show("Đây là bản dùng thử! Chỉ lưu được 50 phiếu bán!", "Phieu Ban", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Đây là bản dùng thử! Chỉ lưu được thêm " + Convert.ToString(50-n) + " phiếu bán!", "Phieu Ban", MessageBoxButtons.OK, MessageBoxIcon.Information);
                factory.Save();
            }
            
        }
        public void HienthiPhieuBanLe(BindingNavigator bn, DataGridView dg)
        {

            bs.DataSource = factory.DanhsachPhieuBan(false);
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public void HienthiPhieuBanSi(BindingNavigator bn, DataGridView dg)
        {

            bs.DataSource = factory.DanhsachPhieuBan(true);
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public void HienthiPhieuBan(BindingNavigator bn,ComboBox cmb, TextBox txt, DateTimePicker dt, NumericUpDown numTongTien, NumericUpDown numDatra, NumericUpDown numConNo)
        {

            bn.BindingSource = bs;

            txt.DataBindings.Clear();
            txt.DataBindings.Add("Text", bs, "ID");

            cmb.DataBindings.Clear();
            cmb.DataBindings.Add("SelectedValue", bs, "ID_KHACH_HANG");

            dt.DataBindings.Clear();
            dt.DataBindings.Add("Value", bs, "NGAY_BAN");

            numTongTien.DataBindings.Clear();
            numTongTien.DataBindings.Add("Value", bs, "TONG_TIEN");

            numDatra.DataBindings.Clear();
            numDatra.DataBindings.Add("Value", bs, "DA_TRA");

            numConNo.DataBindings.Clear();
            numConNo.DataBindings.Add("Value", bs, "CON_NO");


        }
        public IList<PhieuBan> LayPhieuBanTheoNgayBan(DateTime dtTuNgay, DateTime denNgay)
        {
            IList<PhieuBan> ds = new List<PhieuBan>();

            DataTable tbl = factory.LayPhieuBanTheoNgayBan(dtTuNgay, denNgay);
            foreach (DataRow row in tbl.Rows)
            {
                PhieuBan pb = new PhieuBan();
                KhachHangController khCtrl = new KhachHangController();
                pb.Id = Convert.ToString(row["ID"]);
                pb.NgayBan = Convert.ToDateTime(row["NGAY_BAN"]);
                pb.ChiPhiVanChuyen = Convert.ToInt64(row["CHI_PHI_VAN_CHUYEN"]);
                pb.DichVuPhu = Convert.ToInt64(row["DICH_VU_PHU"]);
                pb.GiamGia = Convert.ToInt64(row["GIAM_GIA"]);
                pb.TongTien = Convert.ToInt64(row["TONG_TIEN"]);
                pb.DaTra = Convert.ToInt64(row["DA_TRA"]);
                pb.ConNo = Convert.ToInt64(row["CON_NO"]);
                pb.KhachHang = khCtrl.LayKhachHang(Convert.ToString(row["ID_KHACH_HANG"]));
                ds.Add(pb);
            }
            return ds;
        }

        public PhieuBan LayPhieuBan(String id)
        {
            DataTable tbl = factory.LayPhieuBan(id);
            PhieuBan ph = null;
            if (tbl.Rows.Count > 0)
            {

                ph = new PhieuBan();
                ph.Id = Convert.ToString(tbl.Rows[0]["ID"]);
                
                ph.NgayBan = Convert.ToDateTime(tbl.Rows[0]["NGAY_BAN"]);
                ph.TongTien = Convert.ToInt64(tbl.Rows[0]["TONG_TIEN"]);
                ph.DaTra = Convert.ToInt64(tbl.Rows[0]["DA_TRA"]);
                ph.ConNo = Convert.ToInt64(tbl.Rows[0]["CON_NO"]);
                KhachHangController ctrlKH = new KhachHangController();
                ph.KhachHang = ctrlKH.LayKhachHang(Convert.ToString(tbl.Rows[0]["ID_KHACH_HANG"]));
                ChiTietPhieuBanController ctrl = new ChiTietPhieuBanController();
                ph.ChiTiet = ctrl.ChiTietPhieuBanTheoID(ph.Id);
            }
            return ph;
        }

        public void TimPhieuBan(String maKH, DateTime dt)
        {
            factory.TimPhieuBan(maKH, dt);

        }

    }
}
