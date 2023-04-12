using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuChietTinhAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VungAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.PermissionAggregate;
using Authentication.Infrastructure.AggregatesModel.PositionAggregate;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.EF;
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

        // 1 :Danh mục của Tiến Anh 
        IRepository<DM_LoaiBieuGia> DM_LoaiBieuGiaRepository { get; }
        IRepository<DM_KhuVuc> DM_KhuVucRepository { get; }
        IRepository<DM_Vung> DM_VungRepository { get; }
        IRepository<DM_CongViec> DM_CongViecRepository { get; }
        IRepository<BieuGiaCongViec> BieuGiaCongViecRepository { get; }
        IRepository<DM_LoaiCap> DM_LoaiCapRepository { get; }
        IRepository<DM_VatLieu> DM_VatLieuRepository { get; }
        IRepository<DM_VatLieuChietTinh> DM_VatLieuChietTinhRepository { get; }
        IRepository<GiaCap> GiaCapRepository { get; }
        IRepository<DonGiaVatLieu> DonGiaVatLieuRepository { get; }

        //1. DM bieu gia - Kem
        IRepository<DM_BieuGia> DM_BieuGiaRepository { get; }



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

        // 2 :Danh mục của Tiến Anh 
        private IRepository<DM_LoaiBieuGia> _dM_LoaiBieuGiaRepository;
        private IRepository<DM_KhuVuc> _dM_KhuVucRepository;
        private IRepository<DM_Vung> _dM_VungRepository;
        private IRepository<DM_CongViec> _dM_CongViecRepository;
        private IRepository<BieuGiaCongViec> _bieuGiaCongViecRepository;
        private IRepository<DM_LoaiCap> _dM_LoaiCapRepository;
        private IRepository<DM_VatLieu> _dM_VatLieuRepository;
        private IRepository<DM_VatLieuChietTinh> _dM_VatLieuChietTinhRepository;
        private IRepository<GiaCap> _giaCapRepository;
        private IRepository<DonGiaVatLieu> _donGiaVatLieuRepository;

        //Danh muc bieu gia - Kem
        private IRepository<DM_BieuGia> _dM_BieuGiaRepository;

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



        #region :Danh mục của Tiến Anh 
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
        public IRepository<DM_Vung> DM_VungRepository
        {
            get
            {
                if (_dM_VungRepository == null)
                {
                    _dM_VungRepository = new Repository<DM_Vung>(_context);
                }
                return _dM_VungRepository;
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
        public IRepository<DM_VatLieuChietTinh> DM_VatLieuChietTinhRepository
        {
            get
            {
                if (_dM_VatLieuChietTinhRepository == null)
                {
                    _dM_VatLieuChietTinhRepository = new Repository<DM_VatLieuChietTinh>(_context);
                }
                return _dM_VatLieuChietTinhRepository;
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
        #endregion
       
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
