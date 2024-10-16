using Microsoft.EntityFrameworkCore;
using MovieManagementApp.MyAccounts;
using MovieManagementApp.Actors;
using MovieManagementApp.Categories;
using MovieManagementApp.MovieActors;
using MovieManagementApp.MovieCategories;
using MovieManagementApp.Movies;
using MovieManagementApp.MyLists;
using MovieManagementApp.Ratings;
using MovieManagementApp.UserMovieInteractions;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MovieManagementApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MovieManagementAppDbContext :
    AbpDbContext<MovieManagementAppDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<MyAccount> MyAccounts { get; set; }
    public DbSet<MyList> MyLists { get; set; }
    public DbSet<UserMovieInteraction> UserMovieInteractions { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieCategory> MovieCategories { get; set; }
    public DbSet<Category> Categories { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public MovieManagementAppDbContext(DbContextOptions<MovieManagementAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

             /* Configure your own tables/entities inside here */
        builder.Entity<MyAccount>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "Accounts",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention();  //auto configure for the base class props
            b.HasOne<IdentityUser>().WithOne().HasForeignKey<MyAccount>(x => x.UserId).IsRequired();

            // b.Property(x => x.).IsRequired().HasMaxLength(128);
        });
        builder.Entity<MyList>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "MyLists",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.HasOne(x => x.MyAccount)
                .WithMany() // Assuming User can have multiple MyLists
                .HasForeignKey(x => x.MyAccountId)
                .IsRequired();
            b.HasOne(x => x.Movie)
                .WithMany() // Assuming Movie can be in multiple MyLists
                .HasForeignKey(x => x.MovieId)
                .IsRequired();
        });
        builder.Entity<UserMovieInteraction>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "UserMovieInteractions",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.HasOne(x => x.MyAccount)
                .WithMany() // Assuming User can have multiple interactions
                .HasForeignKey(x => x.MyAccountId)
                .IsRequired();
            b.HasOne(x => x.Movie)
                .WithMany() // Assuming Movie can have multiple interactions
                .HasForeignKey(x => x.MovieId)
                .IsRequired();
        });
        builder.Entity<Movie>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "Movies",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(255);
            b.Property(x => x.Duration).IsRequired();
            b.Property(x => x.AgeRating).IsRequired().HasMaxLength(10);
            b.Property(x => x.PosterUrl).IsRequired();
            b.Property(x => x.VideoUrl).IsRequired();

        });
        builder.Entity<Rating>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "Ratings",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.HasOne(x => x.MyAccount)
                .WithMany() // Assuming User can have multiple ratings
                .HasForeignKey(x => x.MyAccountId)
                .IsRequired();
            b.HasOne(x => x.Movie)
                .WithMany() // Assuming Movie can have multiple ratings
                .HasForeignKey(x => x.MovieId)
                .IsRequired();
            b.Property(x => x.RatingValue).IsRequired().HasDefaultValue(0);


        }); 
        builder.Entity<MovieActor>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "MovieActors",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Movie)
                .WithMany() // Assuming Movie can have multiple actors
                .HasForeignKey(x => x.MovieId)
                .IsRequired();
            b.HasOne(x => x.Actor)
                .WithMany() // Assuming Actor can act in multiple movies
                .HasForeignKey(x => x.ActorId)
                .IsRequired();
        });
        builder.Entity<Actor>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "Actors",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.Property(x => x.ActorName).IsRequired();
        });
        builder.Entity<MovieCategory>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "MovieCategories",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Movie)
                .WithMany() // Assuming Movie can belong to multiple categories
                .HasForeignKey(x => x.MovieId)
                .IsRequired();
            b.HasOne(x => x.Category)
                .WithMany() // Assuming Category can have multiple movies
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();
        });
        builder.Entity<Category>(b =>
        {
            b.ToTable(MovieManagementAppConsts.DbTablePrefix + "Categories",
                MovieManagementAppConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasKey(x => x.Id);
            b.Property(x => x.CategoryName).IsRequired();
        });

    }
}
