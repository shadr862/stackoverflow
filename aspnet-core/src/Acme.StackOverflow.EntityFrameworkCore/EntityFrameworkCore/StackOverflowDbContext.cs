using System.Reflection.Emit;
using Acme.StackOverflow.Comments;
using Acme.StackOverflow.Posts;
using Acme.StackOverflow.PostTags;
using Acme.StackOverflow.Tags;
using Acme.StackOverflow.AppUsers;
using Acme.StackOverflow.Votes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Acme.StackOverflow.Answers;

namespace Acme.StackOverflow.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class StackOverflowDbContext :
    AbpDbContext<StackOverflowDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

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

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<Answer> Answers { get; set; }


    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public StackOverflowDbContext(DbContextOptions<StackOverflowDbContext> options)
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

        builder.Entity<AppUser>(b =>
        {
            b.HasMany(u => u.Posts).WithOne(p => p.AppUser).HasForeignKey(p => p.AppUserId);
            b.HasMany(u => u.Comments).WithOne(p => p.AppUser).HasForeignKey(p => p.AppUserId);
            b.HasMany(u => u.Votes).WithOne(p => p.AppUser).HasForeignKey(p => p.AppUserId);
        });


        // Composite key for PostTag
        builder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });

        // Optional relation to avoid query filter conflicts
        builder.Entity<PostTag>()
            .HasOne(p=>p.Post)
            .WithMany(p=>p.PostTags)
            .HasForeignKey(pt => pt.PostId)
            .OnDelete(DeleteBehavior.Restrict) // Prevent cascade cycles
            .IsRequired(false); // Make optional to match soft-delete filter on Post

        // Optional relation to avoid query filter conflicts
        builder.Entity<PostTag>()
            .HasOne(p => p.Tag)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.TagId)
            .OnDelete(DeleteBehavior.Restrict) // Prevent cascade cycles
            .IsRequired(false); // Make optional to match soft-delete filter on Post



        builder.Entity<Post>(b =>
        {
            b.HasOne(p => p.Parent)
             .WithMany(p => p.ChildPosts)
             .HasForeignKey(p => p.ParentId);

            b.HasOne(p => p.AppUser)
             .WithMany(u => u.Posts)
             .HasForeignKey(p => p.AppUserId);

            b.HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId);
            b.HasMany(p => p.Votes).WithOne(v => v.Post).HasForeignKey(v => v.PostId);
            b.HasMany(p => p.PostTags).WithOne(pt => pt.Post).HasForeignKey(pt => pt.PostId);
        });



        // Avoid cascade delete from Post → Comment
        builder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        // Avoid cascade delete from Vote → Post
        builder.Entity<Vote>()
            .HasOne(v => v.Post)
            .WithMany(p => p.Votes)
            .HasForeignKey(v => v.PostId)
            .OnDelete(DeleteBehavior.Restrict); // ❗Fix cascade path error

        builder.Entity<Answer>()
            .HasOne(a=>a.Post)
            .WithMany(p=>p.Answers)
            .HasForeignKey(a=>a.PostId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
