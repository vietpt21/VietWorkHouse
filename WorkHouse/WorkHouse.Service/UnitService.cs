using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHouse.Service
{
    public class UnitService
    {
        public readonly NCCService NCCService;
        public readonly KhoService KhoService;
        public readonly SanPhamService SanPhamService;
        public readonly NhapKhoService NhapKhoService;
        public readonly NhapKhoCTService NhapKhoCTService;
        public readonly XuatKhoService XuatKhoService;
        public readonly XuatKhoCTService XuatKhoCTService;


        public readonly string connectionString;

        public UnitService(string _connectionString)
        {
            this.connectionString = _connectionString;
            NCCService = new NCCService(connectionString);
            KhoService = new KhoService(connectionString);
            SanPhamService = new SanPhamService(connectionString);
            NhapKhoService = new NhapKhoService(connectionString);
            NhapKhoCTService = new NhapKhoCTService(connectionString);
            XuatKhoService = new XuatKhoService(connectionString);
            XuatKhoCTService = new XuatKhoCTService(connectionString);
        }
    }
}
