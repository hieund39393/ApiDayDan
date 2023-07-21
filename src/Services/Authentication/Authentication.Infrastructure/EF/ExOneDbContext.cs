using Authentication.Infrastructure.EF.EntityConfigurations;
using Authentication.Infrastructure.EntityConfigurations;
using EVN.Core.Common;
using EVN.Core.Extensions;
using EVN.Core.Models.Interface;
using Authentication.Infrastructure.AggregatesModel.ActionsAggregate;
using Authentication.Infrastructure.AggregatesModel.LogAggregate;
using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
using Authentication.Infrastructure.AggregatesModel.PositionAggregate;
using Authentication.Infrastructure.AggregatesModel.PermissionAggregate;
using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate;
using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.AggregatesModel.CauHinhAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate;

namespace Authentication.Infrastructure.EF
{
    public class ExOneDbContext : IdentityDbContext<User, Role, Guid,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public ExOneDbContext() : base()
        { }
        //Entities



        public ExOneDbContext(DbContextOptions<ExOneDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile($"appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString(AppConstants.MainConnectionString);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Permission> Permission { get; set; }

        public DbSet<DM_LoaiBieuGia> DM_LoaiBieuGia { get; set; }
        public DbSet<DM_KhuVuc> DM_KhuVuc { get; set; }
        public DbSet<DM_CongViec> DM_CongViec { get; set; }
        public DbSet<ChiTietBieuGia> ChiTietBieuGia { get; set; }
        public DbSet<BieuGiaCongViec> BieuGiaCongViec { get; set; }
        public DbSet<DM_LoaiCap> DM_LoaiCap { get; set; }
        public DbSet<DM_VatLieu> DM_VatLieu { get; set; }
        public DbSet<GiaCap_CapNgam> GiaCap_CapNgam { get; set; }
        public DbSet<GiaCap> GiaCap { get; set; }
        public DbSet<DonGiaVatLieu> DonGiaVatLieu { get; set; }
        public DbSet<DonGiaNhanCong> DonGiaNhanCong { get; set; }
        public DbSet<DonGiaChietTinh> DonGiaChietTinh { get; set; }
        public DbSet<BieuGiaTongHop> BieuGiaTongHop { get; set; }

        public DbSet<DM_BieuGia> DM_BieuGia { get; set; }

        // Cáp ngầm

        public DbSet<DM_LoaiBieuGia_CapNgam> DM_LoaiBieuGia_CapNgam { get; set; }
        public DbSet<DM_BieuGia_CapNgam> DM_BieuGia_CapNgam { get; set; }
        public DbSet<DM_CongViec_CapNgam> DM_CongViec_CapNgam { get; set; }
        public DbSet<BieuGiaCongViec_CapNgam> BieuGiaCongViec_CapNgam { get; set; }
        public DbSet<DM_VatLieu_CapNgam> DM_VatLieu_CapNgam { get; set; }
        public DbSet<DonGiaVatLieu_CapNgam> DonGiaVatLieu_CapNgam { get; set; }
        public DbSet<DonGiaNhanCong_CapNgam> DonGiaNhanCong_CapNgam { get; set; }
        public DbSet<ChiTietBieuGia_CapNgam> ChiTietBieuGia_CapNgam { get; set; }
        public DbSet<BieuGiaTongHop_CapNgam> BieuGiaTongHop_CapNgam { get; set; }

        public DbSet<CauHinhBieuGia> CauHinhBieuGia { get; set; }
        public DbSet<CauHinhChietTinh> CauHinhChietTinh { get; set; }
        public DbSet<CauHinhChietTinh_CapNgam> CauHinhChietTinh_CapNgam { get; set; }

        public DbSet<DM_MTC> DM_MTC { get; set; }
        public DbSet<DM_MTC_CapNgam> DM_MTC_CapNgam { get; set; }

        public DbSet<DonGiaMTC> DonGiaMTC { get; set; }
        public DbSet<DonGiaMTC_CapNgam> DonGiaMTC_CapNgam { get; set; }

        public DbSet<DM_NhanCong> DM_NhanCong { get; set; }
        public DbSet<DM_NhanCong_CapNgam> DM_NhanCong_CapNgam { get; set; }
        public DbSet<DM_LoaiCap_CapNgam> DM_LoaiCap_CapNgam { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ActionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new RoleClaimConfiguration());
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.ApplyConfiguration(new UserLoginConfiguration());
            builder.ApplyConfiguration(new UserTokenConfiguration());
            builder.ApplyConfiguration(new SystemLogConfiguration());
            builder.ApplyConfiguration(new MenuConfiguration());
            builder.ApplyConfiguration(new ModuleConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());

            builder.ApplyConfiguration(new DM_LoaiBieuGiaConfiguration());
            builder.ApplyConfiguration(new DM_KhuVucConfiguration());
            builder.ApplyConfiguration(new DM_CongViecConfiguration());
            builder.ApplyConfiguration(new ChiTietBieuGiaConfiguration());
            builder.ApplyConfiguration(new BieuGiaCongViecConfiguration());
            builder.ApplyConfiguration(new DM_VatLieuConfiguration());
            builder.ApplyConfiguration(new DM_LoaiCapConfiguration());
            builder.ApplyConfiguration(new GiaCap_CapNgamConfiguration());
            builder.ApplyConfiguration(new GiaCapConfiguration());
            builder.ApplyConfiguration(new DonGiaVatLieuConfiguration());
            builder.ApplyConfiguration(new DonGiaNhanCongConfiguration());
            builder.ApplyConfiguration(new DonGiaChietTinhConfiguration());
            builder.ApplyConfiguration(new BieuGiaTongHopConfiguration());

            builder.ApplyConfiguration(new DM_LoaiBieuGia_CapNgamConfiguration());
            builder.ApplyConfiguration(new DM_BieuGia_CapNgamConfiguration());

            builder.ApplyConfiguration(new DM_VatLieu_CapNgamConfiguration());
            builder.ApplyConfiguration(new DM_CongViec_CapNgamConfiguration());

            builder.ApplyConfiguration(new BieuGiaCongViec_CapNgamConfiguration());

            builder.ApplyConfiguration(new DonGiaVatLieu_CapNgamConfiguration());
            builder.ApplyConfiguration(new DonGiaNhanCong_CapNgamConfiguration());

            builder.ApplyConfiguration(new ChiTietBieuGia_CapNgamConfiguration());
            builder.ApplyConfiguration(new BieuGiaTongHop_CapNgamConfiguration());


            //Cau hinh cua Kem M5
            builder.ApplyConfiguration(new DM_BieuGiaConfiguration());
            builder.ApplyConfiguration(new CauHinhBieuGiaConfiguration());

            builder.ApplyConfiguration(new CauHinhChietTinhConfiguration());
            builder.ApplyConfiguration(new CauHinhChietTinh_CapNgamConfiguration());
            builder.ApplyConfiguration(new DM_MTCConfiguration());
            builder.ApplyConfiguration(new DM_MTC_CapNgamConfiguration());
            builder.ApplyConfiguration(new DonGiaMTC_CapNgamConfiguration());
            builder.ApplyConfiguration(new DonGiaMTCConfiguration());

            builder.ApplyConfiguration(new DM_NhanCongConfiguration());
            builder.ApplyConfiguration(new DM_NhanCong_CapNgamConfiguration());
            builder.ApplyConfiguration(new GiaCap_CapNgamConfiguration());
        }

        /// <summary>
        /// SaveChangesAsync
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /*
         * DB Transaction
         */
        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IName name)
                {
                    name.NameUnsigned = name.Name.RemoveSignedVietnameseString();
                }
                if (entry.Entity is IEntity trackable)
                {
                    var now = DateTime.UtcNow;

                    var id = TokenExtensions.GetUserId();
                    if (string.IsNullOrEmpty(id)) return;

                    var userId = Guid.Parse(id);

                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.CreatedDate = now;
                            trackable.CreatedBy = userId;
                            break;

                        case EntityState.Added:
                            trackable.UpdatedDate = now;
                            trackable.UpdatedBy = userId;
                            break;
                    }
                }
            }
        }
    }
}
