using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class TaiKhoanFactory
    {
        private DataService m_Ds = new DataService();

        private DataTable QueryTaiKhoan(string query, params object[] parameters)
        {
            var cmd = new OleDbCommand(query);
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add("param" + i, OleDbType.VarChar, 50).Value = parameters[i];
            }
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable LayTaiKhoanTheoID(string idTaiKhoan)
        {
            string query = "SELECT * FROM TAI_KHOAN WHERE ID = @id";
            return QueryTaiKhoan(query, idTaiKhoan);
        }

        public DataTable LayTaiKhoanTheoTen(string tenTaiKhoan)
        {
            string query = "SELECT * FROM TAI_KHOAN WHERE TenTaiKhoan = @tenTaiKhoan";
            return QueryTaiKhoan(query, tenTaiKhoan);
        }

        public DataTable LayTatCaTaiKhoan()
        {
            string query = "SELECT * FROM TAI_KHOAN";
            return QueryTaiKhoan(query);
        }

        public bool ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            DataRow row = m_Ds.NewRow();
            row["ID"] = taiKhoan.ID;
            row["TenTaiKhoan"] = taiKhoan.TenTaiKhoan;
            row["MatKhau"] = taiKhoan.MatKhau;
            row["SoDienThoai"] = taiKhoan.SoDienThoai;

            m_Ds.Rows.Add(row);

            return m_Ds.ExecuteNoneQuery() > 0;
        }

        public bool CapNhatTaiKhoan(TaiKhoan taiKhoan)
        {
            DataRow row = m_Ds.Rows.Find(taiKhoan.ID);
            if (row != null)
            {
                row["TenTaiKhoan"] = taiKhoan.TenTaiKhoan;
                row["MatKhau"] = taiKhoan.MatKhau;
                row["SoDienThoai"] = taiKhoan.SoDienThoai;

                return m_Ds.ExecuteNoneQuery() > 0;
            }

            return false;
        }

        public bool XoaTaiKhoan(string idTaiKhoan)
        {
            DataRow row = m_Ds.Rows.Find(idTaiKhoan);
            if (row != null)
            {
                row.Delete();
                return m_Ds.ExecuteNoneQuery() > 0;
            }

            return false;
        }

        public TaiKhoan ConvertToTaiKhoan(DataRow row)
        {
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.ID = row["ID"].ToString();
            taiKhoan.TenTaiKhoan = row["TenTaiKhoan"].ToString();
            taiKhoan.MatKhau = row["MatKhau"].ToString();
            taiKhoan.SoDienThoai = row["SoDienThoai"].ToString();

            return taiKhoan;
        }
    }
}
