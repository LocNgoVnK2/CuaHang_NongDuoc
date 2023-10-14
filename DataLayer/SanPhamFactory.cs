using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class SanPhamFactory
    {
        private readonly DataService m_Ds;
        public SanPhamFactory()
        {
            this.m_Ds = new DataService();
        }
        private DataTable QuerySanPham(string query, OleDbParameter[] parameters = null)
        {
            OleDbCommand cmd = new OleDbCommand(query);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            m_Ds.Load(cmd);
            return m_Ds;
        }
        public DataTable DanhsachSanPham()
        {
            return QuerySanPham("SELECT * FROM SAN_PHAM");
        }
        public DataTable TimMaSanPham(string id)
        {
            OleDbParameter[] parameters = {new OleDbParameter("id", OleDbType.VarChar) { Value = id }};
            return QuerySanPham("SELECT * FROM SAN_PHAM WHERE ID LIKE '%' + @id + '%'", parameters);
        }
        public DataTable TimTenSanPham(string ten)
        {
            OleDbParameter[] parameters = {new OleDbParameter("ten", OleDbType.VarChar) { Value = ten }};
            return QuerySanPham("SELECT * FROM SAN_PHAM WHERE TEN_SAN_PHAM LIKE '%' + @ten + '%'", parameters);
        }
        public DataTable LaySanPham(string id)
        {
            OleDbParameter[] parameters = {new OleDbParameter("id", OleDbType.VarChar, 50) { Value = id }};
            return QuerySanPham("SELECT * FROM SAN_PHAM WHERE ID = @id", parameters);
        }
        public DataTable LaySoLuongTon()
        {
            return QuerySanPham("SELECT SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH, SP.SO_LUONG, SUM(MA.SO_LUONG) AS SO_LUONG_TON "
                              + " FROM SAN_PHAM SP INNER JOIN MA_SAN_PHAM MA ON SP.ID = MA.ID_SAN_PHAM "
                              + " GROUP BY SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH, SP.SO_LUONG");
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
