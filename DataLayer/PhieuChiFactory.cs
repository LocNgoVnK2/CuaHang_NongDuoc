using System;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class PhieuChiFactory
    {
        private readonly DataService m_Ds = new DataService();

        public DataTable TimPhieuChi(int lydo, DateTime ngay)
        {
            return QueryPhieuChi("SELECT * FROM PHIEU_CHI WHERE ID_LY_DO_CHI = @lydo AND NGAY_CHI = @ngay", lydo, ngay);
        }

        public DataTable DanhsachPhieuChi()
        {
            return QueryPhieuChi("SELECT * FROM PHIEU_CHI");
        }

        public DataTable LayPhieuChi(string id)
        {
            return QueryPhieuChi("SELECT * FROM PHIEU_CHI WHERE ID = @id", id);
        }

        public static long LayTongTien(string lydo, int thang, int nam)
        {
            DataService ds = new DataService();
            OleDbCommand cmd = new OleDbCommand("SELECT SUM(TONG_TIEN) FROM PHIEU_CHI WHERE ID_LY_DO_CHI = @lydo AND MONTH(NGAY_CHI) = @thang AND YEAR(NGAY_CHI) = @nam");
            cmd.Parameters.Add("lydo", OleDbType.VarChar, 50).Value = lydo;
            cmd.Parameters.Add("thang", OleDbType.Integer).Value = thang;
            cmd.Parameters.Add("nam", OleDbType.Integer).Value = nam;

            object obj = ds.ExecuteScalar(cmd);

            return obj == null ? 0 : Convert.ToInt64(obj);
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

        private DataTable QueryPhieuChi(string query, params object[] parameters)
        {
            var cmd = new OleDbCommand(query);
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add("param" + i, OleDbType.VarChar, 50).Value = parameters[i];
            }

            m_Ds.Load(cmd);

            return m_Ds;
        }
    }
}
