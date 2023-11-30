using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmThongKe : Form
    {
        private string idNhanVien;
        TaiKhoanController tkCtrl = new TaiKhoanController();
        public frmThongKe(string idNhanVien)
        {
            InitializeComponent();
            this.idNhanVien = idNhanVien;
            
           
            reportViewer.LocalReport.ExecuteReportInCurrentAppDomain(System.Reflection.Assembly.GetExecutingAssembly().Evidence);
        }

        private void frmSoLuongBan_Load(object sender, EventArgs e)
        {
        }

        PhieuBanController ctrl = new PhieuBanController();

        
        private void btnXemNgay_Click(object sender, EventArgs e)
        {
            if(dtTuNgay.Value >= dtDenNgay.Value)
            {
                MessageBox.Show("'Từ ngày' phải nhỏ hơn 'Đến ngày'");
                return;
            }
            TaiKhoanController tkCtrl = new TaiKhoanController();
            TaiKhoan tk = tkCtrl.LayTaiKhoanTheoID(idNhanVien);
            
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("fromDate", dtTuNgay.Value.Date.ToString("dd/MM/yyyy")));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("toDate", dtDenNgay.Value.Date.ToString("dd/MM/yyyy")));
            this.reportViewer.LocalReport.SetParameters(param);
            this.ChiTietPhieuBanBindingSource.DataSource = ctrl.LayPhieuBanTheoNgayBan(dtTuNgay.Value.Date, dtDenNgay.Value.Date,tk);
            this.reportViewer.RefreshReport();
        }

    }
}