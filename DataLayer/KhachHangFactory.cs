using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class KhachHangFactory
    {
        private readonly DataService m_Ds;
        public KhachHangFactory()
        {
            this.m_Ds = new DataService();
        }
        private DataTable QueryPhieuChi(string query, OleDbParameter[] parameters)
        {
            OleDbCommand cmd = new OleDbCommand(query);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            m_Ds.Load(cmd);
            return m_Ds;
        }
        public DataTable DanhsachKhachHang(bool loai)
        {
            return QueryPhieuChi("SELECT * FROM KHACH_HANG WHERE LOAI_KH = " + loai, null);
        }
        public DataTable TimHoTen(string hoten, bool loai)
        {
            OleDbParameter[] parameters = { new OleDbParameter("@hoten", OleDbType.VarChar) { Value = hoten } };
            return QueryPhieuChi("SELECT * FROM KHACH_HANG WHERE HO_TEN LIKE '%' + @hoten + '%' AND LOAI_KH = " + loai, parameters);
        }
        public DataTable TimDiaChi(string diachi, bool loai)
        {
            OleDbParameter[] parameters = { new OleDbParameter("@diachi", OleDbType.VarChar) { Value = diachi } };
            return QueryPhieuChi("SELECT * FROM KHACH_HANG WHERE DIA_CHI LIKE '%' + @diachi + '%' AND LOAI_KH = " + loai, parameters);
        }
        public DataTable DanhsachKhachHang()
        {
            return QueryPhieuChi("SELECT * FROM KHACH_HANG", null);
        }
        public DataTable LayKhachHang(string id)
        {
            OleDbParameter[] parameters = { new OleDbParameter("@id", OleDbType.VarChar, 50) { Value = id } };
            return QueryPhieuChi("SELECT * FROM KHACH_HANG WHERE ID = @id", parameters);
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
