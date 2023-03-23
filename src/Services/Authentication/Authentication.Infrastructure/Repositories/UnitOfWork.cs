using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.EF;
using EVN.Core.Models.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        IRepository<Unit> UnitRepository { get; }
        IRepository<Team> TeamRepository { get; }

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
        private IRepository<Unit> _unitRepository;
        private IRepository<Team> _teamRepository;
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
