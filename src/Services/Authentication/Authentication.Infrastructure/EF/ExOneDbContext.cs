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
        public DbSet<Team> Team { get; set; }        
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<Module> Module { get; set; }

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
