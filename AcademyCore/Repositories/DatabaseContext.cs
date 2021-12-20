using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Academy.Core.Models;


namespace Academy.Core.Repositories
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public string DbPath { get; private set; }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LanguageLevel> LanguageLevels { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberLanguage> MemberLanguages { get; set; }
        public virtual DbSet<MemberSkill> MemberSkills { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillLevel> SkillLevels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var folder = System.IO.Directory.GetCurrentDirectory();
                DbPath = $"{folder}{System.IO.Path.DirectorySeparatorChar}Database{System.IO.Path.DirectorySeparatorChar}Academy.db";

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .Build();
                optionsBuilder.UseSqlite($"Data Source={DbPath}");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.Category1)
                    .HasColumnType("varchar")
                    .HasColumnName("category");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.Language1)
                    .HasColumnType("varchar")
                    .HasColumnName("language");

                entity.Property(e => e.LanguageCode)
                    .HasColumnType("varchar")
                    .HasColumnName("language_code");
            });

            modelBuilder.Entity<LanguageLevel>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.Level)
                    .HasColumnType("varchar")
                    .HasColumnName("level");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.AdId)
                    .HasColumnType("varchar")
                    .HasColumnName("ad_id");

                entity.Property(e => e.Comments)
                    .HasColumnType("varchar")
                    .HasColumnName("comments");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar")
                    .HasColumnName("email");

                entity.Property(e => e.IsAdmin)
                    .HasColumnType("boolean")
                    .HasColumnName("is_admin");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("date")
                    .HasColumnName("last_update");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar")
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnType("varchar")
                    .HasColumnName("role");

                entity.Property(e => e.Surname)
                    .HasColumnType("varchar")
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .HasColumnType("varchar")
                    .HasColumnName("username");
            });

            modelBuilder.Entity<MemberLanguage>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.IdLanguage)
                    .HasColumnType("integer")
                    .HasColumnName("id_language");

                entity.Property(e => e.IdLanguageLevel)
                    .HasColumnType("integer")
                    .HasColumnName("id_language_level");

                entity.Property(e => e.IdMember)
                    .HasColumnType("integer")
                    .HasColumnName("id_member");

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany(p => p.MemberLanguages)
                    .HasForeignKey(d => d.IdLanguage);

                entity.HasOne(d => d.IdLanguageLevelNavigation)
                    .WithMany(p => p.MemberLanguages)
                    .HasForeignKey(d => d.IdLanguageLevel);

                entity.HasOne(d => d.IdMemberNavigation)
                    .WithMany(p => p.MemberLanguages)
                    .HasForeignKey(d => d.IdMember);
            });

            modelBuilder.Entity<MemberSkill>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.IdMember)
                    .HasColumnType("integer")
                    .HasColumnName("id_member");

                entity.Property(e => e.IdSkill)
                    .HasColumnType("integer")
                    .HasColumnName("id_skill");

                entity.Property(e => e.IdSkillLevel)
                    .HasColumnType("integer")
                    .HasColumnName("id_skill_level");

                entity.HasOne(d => d.IdMemberNavigation)
                    .WithMany(p => p.MemberSkills)
                    .HasForeignKey(d => d.IdMember);

                entity.HasOne(d => d.IdSkillNavigation)
                    .WithMany(p => p.MemberSkills)
                    .HasForeignKey(d => d.IdSkill);

                entity.HasOne(d => d.IdSkillLevelNavigation)
                    .WithMany(p => p.MemberSkills)
                    .HasForeignKey(d => d.IdSkillLevel);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.IdCategory)
                    .HasColumnType("integer")
                    .HasColumnName("id_category");

                entity.Property(e => e.Skill1)
                    .HasColumnType("varchar")
                    .HasColumnName("skill");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.IdCategory);
            });

            modelBuilder.Entity<SkillLevel>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.SkillLevel1)
                    .HasColumnType("varchar")
                    .HasColumnName("skill_level");

                entity.Property(e => e.NumericalSkillLevel)
                    .HasColumnType("varchar")
                    .HasColumnName("numeric_skill_level");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
