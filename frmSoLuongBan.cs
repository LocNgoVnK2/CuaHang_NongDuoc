﻿using System;
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
    public partial class frmSoLuongBan : Form
    {
        public frmSoLuongBan()
        {
            InitializeComponent();
            reportViewer.LocalReport.ExecuteReportInCurrentAppDomain(System.Reflection.Assembly.GetExecutingAssembly().Evidence);
        }

        private void frmSoLuongBan_Load(object sender, EventArgs e)
        {
            cmbThang.SelectedIndex = DateTime.Now.Month - 1;
            numNam.Value = DateTime.Now.Year;
        }

        ChiTietPhieuBanController ctrl = new ChiTietPhieuBanController();

        
        private void btnXemNgay_Click(object sender, EventArgs e)
        {
            if(dtTuNgay.Value >= dtDenNgay.Value)
            {
                MessageBox.Show("'Từ ngày' phải nhỏ hơn 'Đến ngày'");
                return;
            }
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ngay", "Ngày " + dtTuNgay.Value.Date.ToString("dd/MM/yyyy")));

            this.reportViewer.LocalReport.SetParameters(param);
            this.ChiTietPhieuBanBindingSource.DataSource = ctrl.ChiTietPhieuBanTheoNgayBan(dtTuNgay.Value.Date, dtDenNgay.Value.Date);
            this.reportViewer.RefreshReport();
        }

        private void btnXemThang_Click(object sender, EventArgs e)
        {
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ngay",
                "Tháng " + Convert.ToString(cmbThang.SelectedIndex + 1) + "/" + numNam.Value.ToString()));

            this.reportViewer.LocalReport.SetParameters(param);


            this.ChiTietPhieuBanBindingSource.DataSource = ctrl.ChiTietPhieuBanTheoThangVaNam(cmbThang.SelectedIndex + 1, Convert.ToInt32(numNam.Value));
            this.reportViewer.RefreshReport();
        }

    }
}