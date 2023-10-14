using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CuahangNongduoc.DataLayer
{
    public class DonViTinhFactory
    {
        private readonly DataService m_Ds;

        public DonViTinhFactory()
        {
            this.m_Ds = new DataService();
        }
        private DataTable QueryDonViTinh(string query, OleDbParameter[] parameters = null)
        {
            OleDbCommand cmd = new OleDbCommand(query);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            m_Ds.Load(cmd);
            return m_Ds;
        }
        public DataTable DanhsachDVT()
        {
            return QueryDonViTinh("SELECT * FROM DON_VI_TINH");
        }
        public DataTable LayDVT(int id)
        {
            OleDbParameter[] parameters = { new OleDbParameter("@id", OleDbType.Integer) { Value = id } };
            return QueryDonViTinh("SELECT * FROM DON_VI_TINH WHERE ID = @id", parameters);
        }
        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
