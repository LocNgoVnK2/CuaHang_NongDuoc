using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class NhaCungCapFactory
    {
        private readonly DataService m_Ds;
        public NhaCungCapFactory()
        {
            this.m_Ds = new DataService();
        }
        private DataTable QueryNhaCungCap(string query, OleDbParameter[] parameters = null)
        {
            OleDbCommand cmd = new OleDbCommand(query);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            m_Ds.Load(cmd);
            return m_Ds;
        }
        public DataTable DanhsachNCC()
        {
            return QueryNhaCungCap("SELECT * FROM NHA_CUNG_CAP");
        }
        public DataTable TimDiaChi(string diachi)
        {
            OleDbParameter[] parameters = {new OleDbParameter("diachi", OleDbType.VarChar) { Value = diachi }};
            return QueryNhaCungCap("SELECT * FROM NHA_CUNG_CAP WHERE DIA_CHI LIKE '%' + @diachi + '%'", parameters);
        }
        public DataTable TimHoTen(string hoten)
        {
            OleDbParameter[] parameters = {new OleDbParameter("hoten", OleDbType.VarChar) { Value = hoten }};
            return QueryNhaCungCap("SELECT * FROM NHA_CUNG_CAP WHERE HO_TEN LIKE '%' + @hoten + '%'", parameters);
        }
        public DataTable LayNCC(string id)
        {
            OleDbParameter[] parameters = { new OleDbParameter("id", OleDbType.VarChar, 50) { Value = id }};
            return QueryNhaCungCap("SELECT * FROM NHA_CUNG_CAP WHERE ID = @id", parameters);
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
