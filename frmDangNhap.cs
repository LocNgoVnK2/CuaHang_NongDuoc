using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmDangNhap : Form
    {
        private TaiKhoanController taiKhoanController = new TaiKhoanController();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;

            string userName = taiKhoanController.DangNhap(tenTaiKhoan, matKhau);
            bool loginSuccessful = userName != null;

            if (loginSuccessful)
            {
              
                this.Hide();

                using (var frmMain = new frmMain(userName, this))
                {
                    frmMain.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra thông tin đăng nhập.");
            }
        }
        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
