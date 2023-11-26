using System;
using System.Collections.Generic;
using System.Text;

namespace CuahangNongduoc.BusinessObject
{
    public class SoLuongTon
    {
        private SanPham  m_SanPham;

        public SanPham  SanPham
        {
            get { return m_SanPham; }
            set { m_SanPham = value; }
        }
        private string m_MaSanPham;
        public string MaSanPham
        {
            get { return m_SanPham.Id; }
        }
        private string m_TenSanPham;
        public string TenSanPham
        {
            get { return m_SanPham.TenSanPham; }
        }
        private long m_GiaNhap;
        public long GiaNhap
        {
            get { return m_SanPham.DonGiaNhap; }
        }
        private long m_GiaLe;
        public long GiaLe
        {
            get { return m_SanPham.GiaBanLe; }
        }
        private long m_GiaSi;
        public long GiaSi
        {
            get { return m_SanPham.GiaBanSi; }
        }
       
        private string m_DonViTinh;
        public string DonViTinh
        {
            get { return m_SanPham.DonViTinh.Ten; }
        }
        private int m_SoLuongTon;
        public int SoLuong
        {
            get { return m_SoLuongTon; }
            set { m_SoLuongTon = value; }
        }

	
    }
}
