using BatchJob.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BatchJob.Repository
{
    public class BatchJobDbContext : DbContext
    {
        private const string CONTEXT_NAME = "BatchJobDbContext";
        private TransactionScope _transactionScope;
        private bool _transactionCommited;

        public DbSet<TriggerBaseEntity> TriggerBases { get; set; }

        public DbSet<CronTriggerEntity> CronTriggers { get; set; }

        public DbSet<ExcuteHistoryEntity> ExcuteHistories { get; set; }

        public DbSet<JobEntity> Jobs { get; set; }

        public DbSet<JobGroupEntity> JobGroups { get; set; }

        public DbSet<SchedulerEntity> Schedulers { get; set; }

        public DbSet<ServiceEntity> Services { get; set; }

        public DbSet<ServiceParameterEntity> ServiceParameters { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public BatchJobDbContext() :
            base(CONTEXT_NAME)
        {
            CallContext.SetData(DomainContext.AXGIO_DB_CONTEXT, this);
        }

        public BatchJobDbContext(bool isTransactionRequired)
            : base(CONTEXT_NAME)
        {
            CallContext.SetData(DomainContext.AXGIO_DB_CONTEXT, this);
            if (isTransactionRequired)
            {
                if (isTransactionRequired)
                    _transactionScope = new TransactionScope();
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<CronTriggerEntity>().ToTable("CronTriggerEntity");

            modelBuilder.Entity<SchedulerEntity>()
                .HasMany(a => a.JobGroups)
                .WithRequired(a => a.Scheduler)
                .WillCascadeOnDelete();

            modelBuilder.Entity<JobGroupEntity>()
                .HasMany(a => a.Jobs)
                .WithRequired(a => a.JobGroup)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UserEntity>()
                .HasOptional(a => a.CreatedBy)
                .WithOptionalPrincipal()
                .Map(a => a.MapKey("CreatedBy_Id"));

            modelBuilder.Entity<UserEntity>()
                .HasOptional(a => a.UpdatedBy)
                .WithOptionalPrincipal()
                .Map(a => a.MapKey("UpdatedBy_Id"));

            modelBuilder.Entity<TriggerBaseEntity>()
                .HasRequired(a => a.Job)
                .WithMany(a => a.Triggers);

            modelBuilder.Entity<JobEntity>()
                .HasMany(a => a.Services)
                .WithRequired(a => a.Job)
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }

        public new void SaveChanges()
        {
            FillBaseInfo();
            base.SaveChanges();
        }

        public void Commit()
        {
            if (_transactionScope != null)
            {
                _transactionCommited = true;
            }
        }

        public void Rollback()
        {
            if (_transactionScope != null)
            {
                _transactionCommited = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_transactionScope != null)
            {
                try
                {
                    if (_transactionCommited)
                        _transactionScope.Complete();
                }
                finally
                {
                    _transactionScope.Dispose();
                }
            }
        }

        private void FillBaseInfo()
        {
            var nowDate = DateTime.Now;
            UserEntity author = UserPrincipal.CurrentUser;
            //实体变体
            var changeEntities = DomainContext.Current.ChangeTracker
                .Entries()
                .Where(a => (a.State & (EntityState.Added | EntityState.Modified | EntityState.Deleted)) != 0)
                .ToList();
            foreach (var entry in changeEntities)
            {
                var baseEntity = entry.Entity as BaseEntity;
                if (baseEntity == null) continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        baseEntity.CreatedDate = nowDate;
                        baseEntity.UpdatedDate = nowDate;
                        if (author != null)
                        {
                            baseEntity.CreatedBy = author;
                            baseEntity.UpdatedBy = author;
                        }
                        break;
                    case EntityState.Modified:
                        baseEntity.UpdatedDate = nowDate;
                        if (author != null)
                        {
                            baseEntity.UpdatedBy = author;
                        }
                        break;
                }
            }
        }
    }
}
