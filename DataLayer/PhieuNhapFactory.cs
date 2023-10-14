using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class PhieuNhapFactory
    {
        DataService m_Ds = new DataService();

        public void LoadSchema()
        {
           OleDbCommand cmd = new OleDbCommand("SELECT * FROM PHIEU_NHAP WHERE ID='-1'");
            m_Ds.Load(cmd);

        }
        private OleDbType GetOleDbType(object value)
        {
            if (value is string)
            {
                return OleDbType.VarChar;
            }
            else if (value is DateTime)
            {
                return OleDbType.Date;
            }
            return OleDbType.VarChar; // Default to string
        }

        private DataTable QueryPhieuNhap(string query, params object[] parameters)
        {
            OleDbCommand cmd = new OleDbCommand(query);
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add("param" + i, GetOleDbType(parameters[i]), 50).Value = parameters[i];
            }

            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable DanhsachPhieuNhap()
        {
            return QueryPhieuNhap("SELECT * FROM PHIEU_NHAP");
        }

        public DataTable TimPhieuNhap(string maNCC, DateTime ngay)
        {
            string sql = "SELECT * FROM PHIEU_NHAP WHERE NGAY_NHAP = @ngay AND ID_NHA_CUNG_CAP = @ncc";
            return QueryPhieuNhap(sql, ngay, maNCC);
        }

        public DataTable LayPhieuNhap(string id)
        {
            string sql = "SELECT * FROM PHIEU_NHAP WHERE ID = @id";
            return QueryPhieuNhap(sql, id);
        }
        
        public DataRow NewRow()
        {
            return m_Ds.NewRow();
        }
        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }
       public bool Save()
        {
           
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
