using Assignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Service
{
    public class ApplicationDbContext
          : IdentityDbContext<Candidate>
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
        public DbSet<SaleCertificate> SaleCertificates { get; set; }
        public DbSet<PhotoId> photoIds { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.Phone)
                      .IsRequired()
                      .HasMaxLength(13);
            });

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

                entity.Property(e => e.Price)
                      .IsRequired();

                // Relationships
                entity.HasOne(c => c.Candidates)
                      .WithMany(c => c.Certificates)
                      .HasForeignKey(c => c.CandidateId);

                entity.HasMany(c => c.CandidatesAnalytics)
                      .WithMany(ca => ca.Certificate);
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
                    .HasForeignKey<PhotoId>(e => e.CandidateId);

            });

            modelBuilder.Entity<CandidatesAnalytics>(entity =>
            {
                entity.HasKey(e => e.Id);

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
                    .WithOne(e=>e.Address)
                    .HasForeignKey<Address> (e => e.CandidateId);

            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                // Primary key
                //entity.HasKey(c => c.CandidateNumber);

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

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("CURRENT_DATE");
            });
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e=>e.Id);

                entity.Property(e => e.question)
                      .IsRequired();

                entity.Property(e => e.answer1)
                      .IsRequired();

                entity.Property(e => e.answer2)
                      .IsRequired();

                entity.Property(e => e.answer3)
                      .IsRequired();

                entity.Property(e => e.answer4)
                      .IsRequired();

                entity.Property(e => e.correct)
                      .IsRequired();

                entity.Property(e => e.correct)
                      .IsRequired();

                entity.HasOne(e => e.candidatesAnalytics)
                    .WithMany(e => e.Questions)
                    .HasForeignKey(e => e.CandidatesAnalyticsId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<SaleCertificate>(entity => 
            {

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Price)
                .IsRequired();

            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
