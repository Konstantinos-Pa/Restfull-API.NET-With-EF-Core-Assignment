using Assignment.Models;
using AuthenticationDemo.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Assignment.Service
{
    public class ApplicationDbContext
          : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Define your DbSets here
        // public DbSet<YourEntity> YourEntities { get; set; }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CandidatesAnalytics> CandidatesAnalytics { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<PhotoId> photoIds { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasOne(e => e.Candidate)
                    .WithOne(e => e.AppUser)
                    .HasForeignKey<AppUser>(e => e.CandidateId)
                    .IsRequired(false);

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("CURRENT_DATE");
            });
            // Certificate entity configuration
            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary key

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.AssessmentTestCode)
                      .IsRequired();

                entity.Property(e => e.MaximumScore);

                entity.Property(e => e.ExaminationDate)
                      .HasColumnType("date");

                entity.Property(e => e.ScoreReportDate)
                      .HasColumnType("date");

                // Relationships
                entity.HasMany(c => c.Candidates)
                      .WithMany(c => c.Certificates)
                      .UsingEntity(j => j.ToTable("CertificateCandidates"));

                entity.HasMany(c => c.CandidatesAnalytics)
                      .WithOne(ca => ca.Certificate)
                      .HasForeignKey(ca => ca.CertificateId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Mobile>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e=> e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(c => c.Candidate)
                    .WithMany(c => c.Mobiles)
                    .HasForeignKey(c =>c.CandidateNumber);
            });

            modelBuilder.Entity<PhotoId>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PhotoIdImage)
                   .IsRequired();

                entity.Property(e => e.PhotoIdNumber)
                   .IsRequired();

                entity.Property(e => e.DateOfIssue)
                   .IsRequired()
                   .HasColumnType("date");

                entity.HasOne(e => e.Candidate)
                    .WithOne(e => e.PhotoId)
                    .HasForeignKey<PhotoId>(e => e.CandidateNumber);

            });

            modelBuilder.Entity<CandidatesAnalytics>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Certificate)
                    .WithMany(e => e.CandidatesAnalytics)
                    .HasForeignKey(e => e.CertificateId);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e=>e.Id);

                entity.Property(e => e.City)
                    .IsRequired();

                entity.Property(e => e.Country)
                .IsRequired();

                entity.Property(e => e.State)
                .IsRequired();

                entity.Property(e => e.Street)
                .IsRequired();

                entity.Property(e => e.LandlineNumber)
                .IsRequired();

                entity.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(5);

                entity.HasOne(e=>e.Candidate)
                    .WithMany(e=>e.Addresses)
                    .HasForeignKey (e => e.CandidateNumber);

            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                // Primary key
                entity.HasKey(c => c.CandidateNumber);

                // Properties
                entity.Property(c => c.FirstName)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(c => c.MiddleName)
                      .HasMaxLength(20);

                entity.Property(c => c.LastName)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(c => c.Gender)
                      .IsRequired();

                entity.Property(c => c.DateOfBirth)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.NativeLanguage)
                      .IsRequired()
                      .HasMaxLength(50);

            });



            base.OnModelCreating(modelBuilder);
        }


    }
}
