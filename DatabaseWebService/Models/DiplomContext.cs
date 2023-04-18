using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebService;

public partial class DiplomContext : DbContext
{
    public DiplomContext()
    {
    }

    public DiplomContext(DbContextOptions<DiplomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wanted> Wanteds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-U9I41QJ\\SQLEXPRESS;Database=Diplom;Trusted_Connection=True; TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.AppId);

            entity.ToTable("Application");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.IdApp).HasColumnName("Id_App");
            entity.Property(e => e.IdUser).HasColumnName("Id_User");

            entity.HasOne(d => d.IdAppNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdApp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Application");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_User");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.CommentText).HasMaxLength(1000);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.IdApp).HasColumnName("Id_App");
            entity.Property(e => e.IdUser).HasColumnName("Id_User");

            entity.HasOne(d => d.IdAppNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdApp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Application");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.ToTable("Friendship");

            entity.Property(e => e.IdUser1).HasColumnName("Id_User1");
            entity.Property(e => e.IdUser2).HasColumnName("Id_User2");

            entity.HasOne(d => d.IdUser1Navigation).WithMany(p => p.FriendshipIdUser1Navigations)
                .HasForeignKey(d => d.IdUser1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friendship_User");

            entity.HasOne(d => d.IdUser2Navigation).WithMany(p => p.FriendshipIdUser2Navigations)
                .HasForeignKey(d => d.IdUser2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friendship_User1");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.ToTable("Library");

            entity.Property(e => e.IdApp).HasColumnName("Id_App");
            entity.Property(e => e.IdUser).HasColumnName("Id_User");

            entity.HasOne(d => d.IdAppNavigation).WithMany(p => p.Libraries)
                .HasForeignKey(d => d.IdApp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Library_Application");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Libraries)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Library_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.PermissionLevel).HasMaxLength(20);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
        });

        modelBuilder.Entity<Wanted>(entity =>
        {
            entity.ToTable("Wanted");

            entity.Property(e => e.DateAdded).HasColumnType("date");
            entity.Property(e => e.IdApp).HasColumnName("Id_App");
            entity.Property(e => e.IdUser).HasColumnName("Id_User");

            entity.HasOne(d => d.IdAppNavigation).WithMany(p => p.Wanteds)
                .HasForeignKey(d => d.IdApp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wanted_Application");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Wanteds)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wanted_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
