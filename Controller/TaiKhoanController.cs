﻿using System;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Data;

namespace CuahangNongduoc.Controller
{
    public class TaiKhoanController
    {
        private TaiKhoanFactory taiKhoanFactory = new TaiKhoanFactory();

        public TaiKhoan DangNhap(string tenTaiKhoan, string matKhau)
        {
        
            DataTable dataTable = taiKhoanFactory.LayTaiKhoanTheoTen(tenTaiKhoan);
          
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                string storedPassword = row["MatKhau"].ToString();

                if (matKhau == storedPassword)
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk.TenNhanVien = row["TenNhanVien"].ToString();
                    tk.ID = row["ID"].ToString();
                    return tk;
                }
            }
            return null;
        }
        public TaiKhoan LayTaiKhoanTheoID(string idTaiKhoan)
        {
            DataTable dataTable = taiKhoanFactory.LayTaiKhoanTheoID(idTaiKhoan);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return taiKhoanFactory.ConvertToTaiKhoan(row);
            }

            return null;
        }
    }
}