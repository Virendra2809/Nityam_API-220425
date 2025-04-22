using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HealthIndex.Entity.DataModels
{
    public partial class HealthIndexDbContext : DbContext
    {
        public HealthIndexDbContext()
        {
        }

        public HealthIndexDbContext(DbContextOptions<HealthIndexDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppDataBackup> AppDataBackups { get; set; }
        public virtual DbSet<AppUserMaster> AppUserMasters { get; set; }
        public virtual DbSet<ConsultantDetail> ConsultantDetails { get; set; }
        public virtual DbSet<ConsultingCategory> ConsultingCategories { get; set; }
        public virtual DbSet<FeedbackDetail> FeedbackDetails { get; set; }
        public virtual DbSet<FeedbackReply> FeedbackReplies { get; set; }
        public virtual DbSet<FirmDetail> FirmDetails { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionMaster> QuestionMasters { get; set; }
        public virtual DbSet<QuestionSubscription> QuestionSubscriptions { get; set; }
        public virtual DbSet<QuestionnaireCategory> QuestionnaireCategories { get; set; }
        public virtual DbSet<RoleMaster> RoleMasters { get; set; }
        public virtual DbSet<SubscriptionOrder> SubscriptionOrders { get; set; }
        public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=meshbasqldb.c5nzv8jpq3sq.ap-south-1.rds.amazonaws.com;Database=HealthIO;User Id=HealthIOAdmin;Password=Admin1234;TrustServerCertificate=True;Trusted_Connection=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppDataBackup>(entity =>
            {
                entity.HasKey(e => e.DataBackupId);

                entity.ToTable("AppDataBackup");

                entity.Property(e => e.BackupDate).HasColumnType("datetime");

                entity.Property(e => e.RestoreDate).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.AppDataBackups)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_AppDataBackup_AppUserMaster");
            });

            modelBuilder.Entity<AppUserMaster>(entity =>
            {
                entity.HasKey(e => e.AppUserId);

                entity.ToTable("AppUserMaster");

                entity.Property(e => e.AppPassword).HasMaxLength(50);

                entity.Property(e => e.CountryCode).HasMaxLength(5);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(250);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.FirebaseUserToken).HasMaxLength(200);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.Guid)
                    .HasMaxLength(50)
                    .HasColumnName("GUID");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(15);

                entity.Property(e => e.OldPassword).HasMaxLength(50);

                entity.Property(e => e.OtpforLogin)
                    .HasMaxLength(6)
                    .HasColumnName("OTPforLogin");

                entity.Property(e => e.PasswordRenewDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserAuthToken).HasMaxLength(200);

                entity.Property(e => e.UserPhoto).HasMaxLength(500);
            });

            modelBuilder.Entity<ConsultantDetail>(entity =>
            {
                entity.HasKey(e => e.ConsultantId);

                entity.Property(e => e.ConsultantName).HasMaxLength(200);

                entity.Property(e => e.Consultantcode)
                    .HasMaxLength(10)
                    .HasColumnName("consultantcode");

                entity.Property(e => e.EmailId).HasMaxLength(250);

                entity.Property(e => e.MobileNo).HasMaxLength(15);

                entity.Property(e => e.OrgName).HasMaxLength(1000);

                entity.HasOne(d => d.ConsultingCategory)
                    .WithMany(p => p.ConsultantDetails)
                    .HasForeignKey(d => d.ConsultingCategoryId)
                    .HasConstraintName("FK_ConsultantDetails_ConsultingCategory");
            });

            modelBuilder.Entity<ConsultingCategory>(entity =>
            {
                entity.ToTable("ConsultingCategory");

                entity.Property(e => e.ConsultingCategory1)
                    .HasMaxLength(100)
                    .HasColumnName("ConsultingCategory");
            });

            modelBuilder.Entity<FeedbackDetail>(entity =>
            {
                entity.HasKey(e => e.FeedbackId);

                entity.ToTable("FeedbackDetail");

                entity.Property(e => e.Feedbackdate).HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FeedbackDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_FeedbackDetail_UserMaster");
            });

            modelBuilder.Entity<FeedbackReply>(entity =>
            {
                entity.ToTable("FeedbackReply");

                entity.Property(e => e.ReplyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackReplies)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK_FeedbackReply_FeedbackDetail");
            });

            modelBuilder.Entity<FirmDetail>(entity =>
            {
                entity.HasKey(e => e.FirmId);

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.FirmBranchName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirmConnectionPath)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirmEmailIid).HasMaxLength(50);

                entity.Property(e => e.FirmFaxNumber).HasMaxLength(15);

                entity.Property(e => e.FirmLogo).HasMaxLength(450);

                entity.Property(e => e.FirmName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.FirmOfficeAddress)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirmPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.FirmRegDate).HasColumnType("datetime");

                entity.Property(e => e.FirmRegNumber)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.MailPassword).HasMaxLength(30);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Question1).HasColumnName("Question");

                entity.Property(e => e.QuestionImageName).HasMaxLength(100);

                entity.Property(e => e.QuestionImageUrl).HasMaxLength(500);

                entity.Property(e => e.Weightage).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.QuestionMaster)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionMasterId)
                    .HasConstraintName("FK_Questions_QuestionMaster");
            });

            modelBuilder.Entity<QuestionMaster>(entity =>
            {
                entity.ToTable("QuestionMaster");

                entity.HasOne(d => d.Consultant)
                    .WithMany(p => p.QuestionMasters)
                    .HasForeignKey(d => d.ConsultantId)
                    .HasConstraintName("FK_QuestionMaster_ConsultantDetails");
            });

            modelBuilder.Entity<QuestionSubscription>(entity =>
            {
                entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.QuestionSubscriptions)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_QuestionSubscriptions_AppUserMaster");

                entity.HasOne(d => d.QuestionMaster)
                    .WithMany(p => p.QuestionSubscriptions)
                    .HasForeignKey(d => d.QuestionMasterId)
                    .HasConstraintName("FK_QuestionSubscriptions_QuestionMaster");
            });

            modelBuilder.Entity<QuestionnaireCategory>(entity =>
            {
                entity.ToTable("QuestionnaireCategory");

                entity.Property(e => e.QuestionnaireCategory1)
                    .HasMaxLength(100)
                    .HasColumnName("QuestionnaireCategory");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("RoleMaster");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SubscriptionOrder>(entity =>
            {
                entity.ToTable("SubscriptionOrder");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ModeOfPayment)
                    .HasMaxLength(200)
                    .IsFixedLength(true);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.PaymentEncrData).HasMaxLength(500);

                entity.Property(e => e.PaymentId)
                    .IsUnicode(false)
                    .HasColumnName("paymentId");

                entity.Property(e => e.PaymentLink).IsUnicode(false);

                entity.Property(e => e.TransactionReferanceNo)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.TransactionToken)
                    .HasMaxLength(200)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SubscriptionPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK_dbo.SubscriptionPlan");

                entity.ToTable("SubscriptionPlan");

                entity.Property(e => e.Amount)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Code).HasMaxLength(8);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OfferCode)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.PaymentEncrData).HasMaxLength(500);

                entity.Property(e => e.Validity).HasColumnName("validity");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserMaster");

                entity.Property(e => e.ChangedBy).HasMaxLength(50);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.DeviceUuid)
                    .HasMaxLength(50)
                    .HasColumnName("DeviceUUID");

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.EnteredBy).HasMaxLength(50);

                entity.Property(e => e.EnteredDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.OldPassword).HasMaxLength(50);

                entity.Property(e => e.PasswordRenewDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPhoto).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
