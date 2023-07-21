using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.AggregatesModel.CauHinhAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.PermissionAggregate;
using Authentication.Infrastructure.AggregatesModel.PositionAggregate;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.EF;
using Authentication.Infrastructure.EntityConfigurations;
using EVN.Core.Models.Interface;

namespace Authentication.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        IRepository<UserToken> UserTokenRepository { get; }
        IRepository<RoleClaim> RoleClaimRepository { get; }
        IRepository<Module> ModuleRepository { get; }
        IRepository<Menu> MenuRepository { get; }
        IRepository<Unit> UnitRepository { get; }
        IRepository<Team> TeamRepository { get; }
        IRepository<Permission> PermissionRepository { get; }
        IRepository<Position> PositionRepository { get; }

        IRepository<DM_LoaiBieuGia> DM_LoaiBieuGiaRepository { get; }
        IRepository<DM_LoaiBieuGia_CapNgam> DM_LoaiBieuGia_CapNgamRepository { get; }

        IRepository<DM_KhuVuc> DM_KhuVucRepository { get; }
        IRepository<DM_CongViec> DM_CongViecRepository { get; }
        IRepository<DM_CongViec_CapNgam> DM_CongViec_CapNgamRepository { get; }

        IRepository<BieuGiaCongViec> BieuGiaCongViecRepository { get; }
        IRepository<BieuGiaCongViec_CapNgam> BieuGiaCongViec_CapNgamRepository { get; }
        IRepository<DM_LoaiCap> DM_LoaiCapRepository { get; }
        IRepository<DM_VatLieu> DM_VatLieuRepository { get; }
        IRepository<DM_VatLieu_CapNgam> DM_VatLieu_CapNgamRepository { get; }

        IRepository<DM_NhanCong> DM_NhanCongRepository { get; }
        IRepository<DM_NhanCong_CapNgam> DM_NhanCong_CapNgamRepository { get; }

        IRepository<GiaCap> GiaCapRepository { get; }
        IRepository<GiaCap_CapNgam> GiaCap_CapNgamRepository { get; }
        IRepository<DonGiaVatLieu> DonGiaVatLieuRepository { get; }
        IRepository<DonGiaVatLieu_CapNgam> DonGiaVatLieu_CapNgamRepository { get; }
        IRepository<DonGiaNhanCong> DonGiaNhanCongRepository { get; }
        IRepository<DonGiaNhanCong_CapNgam> DonGiaNhanCong_CapNgamRepository { get; }
        IRepository<DonGiaChietTinh> DonGiaChietTinhRepository { get; }
        IRepository<ChiTietBieuGia> ChiTietBieuGiaRepository { get; }
        IRepository<ChiTietBieuGia_CapNgam> ChiTietBieuGia_CapNgamRepository { get; }
        IRepository<BieuGiaTongHop> BieuGiaTongHopRepository { get; }
        IRepository<BieuGiaTongHop_CapNgam> BieuGiaTongHop_CapNgamRepository { get; }


        IRepository<DM_BieuGia> DM_BieuGiaRepository { get; }
        IRepository<DM_BieuGia_CapNgam> DM_BieuGia_CapNgamRepository { get; }
        IRepository<CauHinhBieuGia> CauHinhBieuGiaRepository { get; }
        IRepository<CauHinhChietTinh> CauHinhChietTinhRepository { get; }
        IRepository<CauHinhChietTinh_CapNgam> CauHinhChietTinh_CapNgamRepository { get; }
        IRepository<DM_MTC> DM_MTCRepository { get; }
        IRepository<DM_MTC_CapNgam> DM_MTC_CapNgamRepository { get; }

        IRepository<DonGiaMTC> DonGiaMTCRepository { get; }
        IRepository<DonGiaMTC_CapNgam> DonGiaMTC_CapNgamRepository { get; }



        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync();
        Task Dispose();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExOneDbContext _context;
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;
        private IRepository<UserRole> _userRoleRepository;
        private IRepository<UserToken> _userTokenRepository;
        private IRepository<RoleClaim> _roleClaimRepository;
        private IRepository<Module> _moduleRepository;
        private IRepository<Menu> _menuRepository;
        private IRepository<Unit> _unitRepository;
        private IRepository<Team> _teamRepository;
        private IRepository<Permission> _permissionRepository;
        private IRepository<Position> _positionRepository;

        private IRepository<DM_LoaiBieuGia> _dM_LoaiBieuGiaRepository;
        private IRepository<DM_LoaiBieuGia_CapNgam> _dM_LoaiBieuGia_CapNgamRepository;
        private IRepository<DM_KhuVuc> _dM_KhuVucRepository;
        private IRepository<DM_CongViec> _dM_CongViecRepository;
        private IRepository<DM_CongViec_CapNgam> _dM_CongViec_CapNgamRepository;
        private IRepository<BieuGiaCongViec> _bieuGiaCongViecRepository;
        private IRepository<BieuGiaCongViec_CapNgam> _bieuGiaCongViec_CapNgamRepository;
        private IRepository<DM_LoaiCap> _dM_LoaiCapRepository;
        private IRepository<DM_VatLieu> _dM_VatLieuRepository;
        private IRepository<DM_VatLieu_CapNgam> _dM_VatLieu_CapNgamRepository;

        private IRepository<DM_NhanCong> _dM_NhanCongRepository;
        private IRepository<DM_NhanCong_CapNgam> _dM_NhanCong_CapNgamRepository;


        private IRepository<GiaCap> _giaCapRepository;
        private IRepository<GiaCap_CapNgam> _giaCap_CapNgamRepository;
        private IRepository<DonGiaVatLieu> _donGiaVatLieuRepository;
        private IRepository<DonGiaVatLieu_CapNgam> _donGiaVatLieu_CapNgamRepository;
        private IRepository<DonGiaNhanCong> _donGiaNhanCongRepository;
        private IRepository<DonGiaNhanCong_CapNgam> _donGiaNhanCong_CapNgamRepository;
        private IRepository<DonGiaChietTinh> _donGiaChietTinhRepository;
        private IRepository<ChiTietBieuGia> _chiTietBieuGiaRepository;
        private IRepository<ChiTietBieuGia_CapNgam> _chiTietBieuGia_CapNgamRepository;
        private IRepository<BieuGiaTongHop> _bieuGiaTongHopRepository;
        private IRepository<BieuGiaTongHop_CapNgam> _bieuGiaTongHop_CapNgamRepository;

        private IRepository<DM_BieuGia> _dM_BieuGiaRepository;
        private IRepository<DM_BieuGia_CapNgam> _dM_BieuGia_CapNgamRepository;
        private IRepository<CauHinhBieuGia> _cauHinhBieuGiaRepository;

        private IRepository<CauHinhChietTinh> _cauHinhChietTinhRepository;
        private IRepository<CauHinhChietTinh_CapNgam> _cauHinhChietTinh_CapNgamRepository;
        private IRepository<DM_MTC> _dm_MTCRepository;
        private IRepository<DM_MTC_CapNgam> _dm_MTC_CapNgamRepository;

        private IRepository<DonGiaMTC> _donGiaMTCRepository;
        private IRepository<DonGiaMTC_CapNgam> _donGiaMTC_CapNgamRepository;

        public UnitOfWork(ExOneDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get AppDbContext
        /// </summary>
        public ExOneDbContext ExOneDbContext => _context;

        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new Repository<User>(_context);
                }
                return _userRepository;
            }
        }
        public IRepository<Role> RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new Repository<Role>(_context);
                }
                return _roleRepository;
            }
        }

        public IRepository<Module> ModuleRepository
        {
            get
            {
                if (_moduleRepository == null)
                {
                    _moduleRepository = new Repository<Module>(_context);
                }
                return _moduleRepository;
            }
        }

        public IRepository<Menu> MenuRepository
        {
            get
            {
                if (_menuRepository == null)
                {
                    _menuRepository = new Repository<Menu>(_context);
                }
                return _menuRepository;
            }
        }


        public IRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (_userRoleRepository == null)
                {
                    _userRoleRepository = new Repository<UserRole>(_context);
                }
                return _userRoleRepository;
            }
        }

        public IRepository<RoleClaim> RoleClaimRepository
        {
            get
            {
                if (_roleClaimRepository == null)
                {
                    _roleClaimRepository = new Repository<RoleClaim>(_context);
                }
                return _roleClaimRepository;
            }
        }

        public IRepository<UserToken> UserTokenRepository
        {
            get
            {
                if (_userTokenRepository == null)
                {
                    _userTokenRepository = new Repository<UserToken>(_context);
                }
                return _userTokenRepository;
            }
        }

        public IRepository<Unit> UnitRepository
        {
            get
            {
                if (_unitRepository == null)
                {
                    _unitRepository = new Repository<Unit>(_context);
                }
                return _unitRepository;
            }
        }
        public IRepository<Team> TeamRepository
        {
            get
            {
                if (_teamRepository == null)
                {
                    _teamRepository = new Repository<Team>(_context);
                }
                return _teamRepository;
            }
        }
        public IRepository<Permission> PermissionRepository
        {
            get
            {
                if (_permissionRepository == null)
                {
                    _permissionRepository = new Repository<Permission>(_context);
                }
                return _permissionRepository;
            }
        }
        public IRepository<Position> PositionRepository
        {
            get
            {
                if (_positionRepository == null)
                {
                    _positionRepository = new Repository<Position>(_context);
                }
                return _positionRepository;
            }
        }




        public IRepository<DM_LoaiBieuGia> DM_LoaiBieuGiaRepository
        {
            get
            {
                if (_dM_LoaiBieuGiaRepository == null)
                {
                    _dM_LoaiBieuGiaRepository = new Repository<DM_LoaiBieuGia>(_context);
                }
                return _dM_LoaiBieuGiaRepository;
            }
        }

        public IRepository<DM_LoaiBieuGia_CapNgam> DM_LoaiBieuGia_CapNgamRepository
        {
            get
            {
                if (_dM_LoaiBieuGia_CapNgamRepository == null)
                {
                    _dM_LoaiBieuGia_CapNgamRepository = new Repository<DM_LoaiBieuGia_CapNgam>(_context);
                }
                return _dM_LoaiBieuGia_CapNgamRepository;
            }
        }
        public IRepository<DM_KhuVuc> DM_KhuVucRepository
        {
            get
            {
                if (_dM_KhuVucRepository == null)
                {
                    _dM_KhuVucRepository = new Repository<DM_KhuVuc>(_context);
                }
                return _dM_KhuVucRepository;
            }
        }

        public IRepository<DM_CongViec> DM_CongViecRepository
        {
            get
            {
                if (_dM_CongViecRepository == null)
                {
                    _dM_CongViecRepository = new Repository<DM_CongViec>(_context);
                }
                return _dM_CongViecRepository;
            }
        }
        public IRepository<DM_CongViec_CapNgam> DM_CongViec_CapNgamRepository
        {
            get
            {
                if (_dM_CongViec_CapNgamRepository == null)
                {
                    _dM_CongViec_CapNgamRepository = new Repository<DM_CongViec_CapNgam>(_context);
                }
                return _dM_CongViec_CapNgamRepository;
            }
        }
        public IRepository<BieuGiaCongViec> BieuGiaCongViecRepository
        {
            get
            {
                if (_bieuGiaCongViecRepository == null)
                {
                    _bieuGiaCongViecRepository = new Repository<BieuGiaCongViec>(_context);
                }
                return _bieuGiaCongViecRepository;
            }
        }

        public IRepository<BieuGiaCongViec_CapNgam> BieuGiaCongViec_CapNgamRepository
        {
            get
            {
                if (_bieuGiaCongViec_CapNgamRepository == null)
                {
                    _bieuGiaCongViec_CapNgamRepository = new Repository<BieuGiaCongViec_CapNgam>(_context);
                }
                return _bieuGiaCongViec_CapNgamRepository;
            }
        }
        public IRepository<DM_LoaiCap> DM_LoaiCapRepository
        {
            get
            {
                if (_dM_LoaiCapRepository == null)
                {
                    _dM_LoaiCapRepository = new Repository<DM_LoaiCap>(_context);
                }
                return _dM_LoaiCapRepository;
            }
        }
        public IRepository<DM_VatLieu> DM_VatLieuRepository
        {
            get
            {
                if (_dM_VatLieuRepository == null)
                {
                    _dM_VatLieuRepository = new Repository<DM_VatLieu>(_context);
                }
                return _dM_VatLieuRepository;
            }
        }
        public IRepository<DM_VatLieu_CapNgam> DM_VatLieu_CapNgamRepository
        {
            get
            {
                if (_dM_VatLieu_CapNgamRepository == null)
                {
                    _dM_VatLieu_CapNgamRepository = new Repository<DM_VatLieu_CapNgam>(_context);
                }
                return _dM_VatLieu_CapNgamRepository;
            }
        }

        public IRepository<DM_NhanCong> DM_NhanCongRepository
        {
            get
            {
                if (_dM_NhanCongRepository == null)
                {
                    _dM_NhanCongRepository = new Repository<DM_NhanCong>(_context);
                }
                return _dM_NhanCongRepository;
            }
        }
        public IRepository<DM_NhanCong_CapNgam> DM_NhanCong_CapNgamRepository
        {
            get
            {
                if (_dM_NhanCong_CapNgamRepository == null)
                {
                    _dM_NhanCong_CapNgamRepository = new Repository<DM_NhanCong_CapNgam>(_context);
                }
                return _dM_NhanCong_CapNgamRepository;
            }
        }

        public IRepository<GiaCap> GiaCapRepository
        {
            get
            {
                if (_giaCapRepository == null)
                {
                    _giaCapRepository = new Repository<GiaCap>(_context);
                }
                return _giaCapRepository;
            }
        }
        public IRepository<GiaCap_CapNgam> GiaCap_CapNgamRepository
        {
            get
            {
                if (_giaCap_CapNgamRepository == null)
                {
                    _giaCap_CapNgamRepository = new Repository<GiaCap_CapNgam>(_context);
                }
                return _giaCap_CapNgamRepository;
            }
        }
        public IRepository<DonGiaVatLieu> DonGiaVatLieuRepository
        {
            get
            {
                if (_donGiaVatLieuRepository == null)
                {
                    _donGiaVatLieuRepository = new Repository<DonGiaVatLieu>(_context);
                }
                return _donGiaVatLieuRepository;
            }
        }
        public IRepository<DonGiaVatLieu_CapNgam> DonGiaVatLieu_CapNgamRepository
        {
            get
            {
                if (_donGiaVatLieu_CapNgamRepository == null)
                {
                    _donGiaVatLieu_CapNgamRepository = new Repository<DonGiaVatLieu_CapNgam>(_context);
                }
                return _donGiaVatLieu_CapNgamRepository;
            }
        }
        public IRepository<DonGiaNhanCong> DonGiaNhanCongRepository
        {
            get
            {
                if (_donGiaNhanCongRepository == null)
                {
                    _donGiaNhanCongRepository = new Repository<DonGiaNhanCong>(_context);
                }
                return _donGiaNhanCongRepository;
            }
        }
        public IRepository<DonGiaNhanCong_CapNgam> DonGiaNhanCong_CapNgamRepository
        {
            get
            {
                if (_donGiaNhanCong_CapNgamRepository == null)
                {
                    _donGiaNhanCong_CapNgamRepository = new Repository<DonGiaNhanCong_CapNgam>(_context);
                }
                return _donGiaNhanCong_CapNgamRepository;
            }
        }
        public IRepository<DonGiaChietTinh> DonGiaChietTinhRepository
        {
            get
            {
                if (_donGiaChietTinhRepository == null)
                {
                    _donGiaChietTinhRepository = new Repository<DonGiaChietTinh>(_context);
                }
                return _donGiaChietTinhRepository;
            }
        }
        public IRepository<ChiTietBieuGia> ChiTietBieuGiaRepository
        {
            get
            {
                if (_chiTietBieuGiaRepository == null)
                {
                    _chiTietBieuGiaRepository = new Repository<ChiTietBieuGia>(_context);
                }
                return _chiTietBieuGiaRepository;
            }
        }
        public IRepository<ChiTietBieuGia_CapNgam> ChiTietBieuGia_CapNgamRepository
        {
            get
            {
                if (_chiTietBieuGia_CapNgamRepository == null)
                {
                    _chiTietBieuGia_CapNgamRepository = new Repository<ChiTietBieuGia_CapNgam>(_context);
                }
                return _chiTietBieuGia_CapNgamRepository;
            }
        }
        public IRepository<BieuGiaTongHop> BieuGiaTongHopRepository
        {
            get
            {
                if (_bieuGiaTongHopRepository == null)
                {
                    _bieuGiaTongHopRepository = new Repository<BieuGiaTongHop>(_context);
                }
                return _bieuGiaTongHopRepository;
            }
        }
        public IRepository<BieuGiaTongHop_CapNgam> BieuGiaTongHop_CapNgamRepository
        {
            get
            {
                if (_bieuGiaTongHop_CapNgamRepository == null)
                {
                    _bieuGiaTongHop_CapNgamRepository = new Repository<BieuGiaTongHop_CapNgam>(_context);
                }
                return _bieuGiaTongHop_CapNgamRepository;
            }
        }
        public IRepository<CauHinhBieuGia> CauHinhBieuGiaRepository
        {
            get
            {
                if (_cauHinhBieuGiaRepository == null)
                {
                    _cauHinhBieuGiaRepository = new Repository<CauHinhBieuGia>(_context);
                }
                return _cauHinhBieuGiaRepository;
            }
        }


        // 3 :Danh mục bieu gia của Kem
        public IRepository<DM_BieuGia> DM_BieuGiaRepository
        {
            get
            {
                if (_dM_BieuGiaRepository == null)
                {
                    _dM_BieuGiaRepository = new Repository<DM_BieuGia>(_context);
                }
                return _dM_BieuGiaRepository;
            }
        }

        public IRepository<DM_BieuGia_CapNgam> DM_BieuGia_CapNgamRepository
        {
            get
            {
                if (_dM_BieuGia_CapNgamRepository == null)
                {
                    _dM_BieuGia_CapNgamRepository = new Repository<DM_BieuGia_CapNgam>(_context);
                }
                return _dM_BieuGia_CapNgamRepository;
            }
        }

        public IRepository<CauHinhChietTinh> CauHinhChietTinhRepository
        {
            get
            {
                if (_cauHinhChietTinhRepository == null)
                {
                    _cauHinhChietTinhRepository = new Repository<CauHinhChietTinh>(_context);
                }
                return _cauHinhChietTinhRepository;
            }
        }

        public IRepository<CauHinhChietTinh_CapNgam> CauHinhChietTinh_CapNgamRepository
        {
            get
            {
                if (_cauHinhChietTinh_CapNgamRepository == null)
                {
                    _cauHinhChietTinh_CapNgamRepository = new Repository<CauHinhChietTinh_CapNgam>(_context);
                }
                return _cauHinhChietTinh_CapNgamRepository;
            }
        }
        public IRepository<DM_MTC> DM_MTCRepository
        {
            get
            {
                if (_dm_MTCRepository == null)
                {
                    _dm_MTCRepository = new Repository<DM_MTC>(_context);
                }
                return _dm_MTCRepository;
            }
        }

        public IRepository<DM_MTC_CapNgam> DM_MTC_CapNgamRepository
        {
            get
            {
                if (_dm_MTC_CapNgamRepository == null)
                {
                    _dm_MTC_CapNgamRepository = new Repository<DM_MTC_CapNgam>(_context);
                }
                return _dm_MTC_CapNgamRepository;
            }
        }

        public IRepository<DonGiaMTC> DonGiaMTCRepository
        {
            get
            {
                if (_donGiaMTCRepository == null)
                {
                    _donGiaMTCRepository = new Repository<DonGiaMTC>(_context);
                }
                return _donGiaMTCRepository;
            }
        }
        public IRepository<DonGiaMTC_CapNgam> DonGiaMTC_CapNgamRepository
        {
            get
            {
                if (_donGiaMTC_CapNgamRepository == null)
                {
                    _donGiaMTC_CapNgamRepository = new Repository<DonGiaMTC_CapNgam>(_context);
                }
                return _donGiaMTC_CapNgamRepository;
            }
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;

        private async Task Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            disposed = true;
        }

        public async Task Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
