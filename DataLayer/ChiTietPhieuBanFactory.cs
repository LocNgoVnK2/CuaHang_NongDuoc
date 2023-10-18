using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class ChiTietPhieuBanFactory
    {
        DataService m_Ds = new DataService();

      
        private DataTable QueryChiTietPhieuBan(string query, params object[] parameters)
        {
            var cmd = new OleDbCommand(query);
            for(int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add("param"+i, OleDbType.VarChar,50).Value = parameters[i];
            }
            m_Ds.Load(cmd);
            return m_Ds;
        }
        public DataTable LayChiTietPhieuBanTheoID(String idPhieuBan)
        {
            string query = "SELECT * FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = @id";
            return QueryChiTietPhieuBan(query, idPhieuBan);
         
        }

        public DataTable LayChiTietPhieuBanTheoNgayBan(DateTime dtNgayBan)
        {
            string query = "SELECT CT.* FROM CHI_TIET_PHIEU_BAN CT INNER JOIN PHIEU_BAN PB ON CT.ID_PHIEU_BAN = PB.ID " +
                    " WHERE PB.NGAY_BAN = @ngayban";
            return QueryChiTietPhieuBan(query, dtNgayBan);
        }

        public DataTable LayChiTietPhieuBanTheoThangVaNam(int thang, int nam)
        {
            string query = "SELECT CT.* FROM CHI_TIET_PHIEU_BAN CT INNER JOIN PHIEU_BAN PB ON CT.ID_PHIEU_BAN = PB.ID " +
                    " WHERE MONTH(PB.NGAY_BAN) = @thang AND YEAR(PB.NGAY_BAN)= @nam";
            return QueryChiTietPhieuBan(query, thang, nam);
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
            foreach (DataRow row in m_Ds.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(Convert.ToString(row["ID_MA_SAN_PHAM"]), -Convert.ToInt32(row["SO_LUONG"]));
                }
            }
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
