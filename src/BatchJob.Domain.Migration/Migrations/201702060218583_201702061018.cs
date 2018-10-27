namespace BatchJob.Domain.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201702061018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TriggerBaseEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.UserEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        LastLoginDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.ExcuteHistoryEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Comment = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                        JobEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .ForeignKey("dbo.JobEntity", t => t.JobEntity_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id)
                .Index(t => t.JobEntity_Id);
            
            CreateTable(
                "dbo.JobGroupEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        Scheduler_Id = c.Int(nullable: false),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.SchedulerEntity", t => t.Scheduler_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Scheduler_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.JobEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 200),
                        Comment = c.String(),
                        PreviousExecuteTime = c.DateTime(),
                        NextExecuteTime = c.DateTime(),
                        IsRunning = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                        JobGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .ForeignKey("dbo.JobGroupEntity", t => t.JobGroup_Id, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id)
                .Index(t => t.JobGroup_Id);
            
            CreateTable(
                "dbo.ServiceEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceAddress = c.String(nullable: false, maxLength: 1000),
                        OperationContractName = c.String(nullable: false),
                        MethodName = c.String(nullable: false, maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                        JobEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .ForeignKey("dbo.JobEntity", t => t.JobEntity_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id)
                .Index(t => t.JobEntity_Id);
            
            CreateTable(
                "dbo.ServiceParameterEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 500),
                        Name = c.String(nullable: false, maxLength: 200),
                        Value = c.String(nullable: false, maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                        ServiceEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .ForeignKey("dbo.ServiceEntity", t => t.ServiceEntity_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id)
                .Index(t => t.ServiceEntity_Id);
            
            CreateTable(
                "dbo.SchedulerEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsRunning = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        ThreadPoolSize = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntity", t => t.CreatedBy_Id)
                .ForeignKey("dbo.UserEntity", t => t.UpdatedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.JobEntity_TiggerEntity",
                c => new
                    {
                        JobEntity_Id = c.Int(nullable: false),
                        TriggerBaseEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobEntity_Id, t.TriggerBaseEntity_Id })
                .ForeignKey("dbo.JobEntity", t => t.JobEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.TriggerBaseEntity", t => t.TriggerBaseEntity_Id, cascadeDelete: true)
                .Index(t => t.JobEntity_Id)
                .Index(t => t.TriggerBaseEntity_Id);
            
            CreateTable(
                "dbo.CronTriggerEntity",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CronExpression = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TriggerBaseEntity", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CronTriggerEntity", "Id", "dbo.TriggerBaseEntity");
            DropForeignKey("dbo.JobGroupEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.SchedulerEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.JobGroupEntity", "Scheduler_Id", "dbo.SchedulerEntity");
            DropForeignKey("dbo.SchedulerEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.JobEntity", "JobGroup_Id", "dbo.JobGroupEntity");
            DropForeignKey("dbo.JobEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.JobEntity_TiggerEntity", "TriggerBaseEntity_Id", "dbo.TriggerBaseEntity");
            DropForeignKey("dbo.JobEntity_TiggerEntity", "JobEntity_Id", "dbo.JobEntity");
            DropForeignKey("dbo.TriggerBaseEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.TriggerBaseEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.ServiceEntity", "JobEntity_Id", "dbo.JobEntity");
            DropForeignKey("dbo.ServiceEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.ServiceParameterEntity", "ServiceEntity_Id", "dbo.ServiceEntity");
            DropForeignKey("dbo.ServiceParameterEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.ServiceParameterEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.ServiceEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.ExcuteHistoryEntity", "JobEntity_Id", "dbo.JobEntity");
            DropForeignKey("dbo.JobEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.JobGroupEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.ExcuteHistoryEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.ExcuteHistoryEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.UserEntity", "UpdatedBy_Id", "dbo.UserEntity");
            DropForeignKey("dbo.UserEntity", "CreatedBy_Id", "dbo.UserEntity");
            DropIndex("dbo.CronTriggerEntity", new[] { "Id" });
            DropIndex("dbo.JobEntity_TiggerEntity", new[] { "TriggerBaseEntity_Id" });
            DropIndex("dbo.JobEntity_TiggerEntity", new[] { "JobEntity_Id" });
            DropIndex("dbo.SchedulerEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.SchedulerEntity", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ServiceParameterEntity", new[] { "ServiceEntity_Id" });
            DropIndex("dbo.ServiceParameterEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.ServiceParameterEntity", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ServiceEntity", new[] { "JobEntity_Id" });
            DropIndex("dbo.ServiceEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.ServiceEntity", new[] { "CreatedBy_Id" });
            DropIndex("dbo.JobEntity", new[] { "JobGroup_Id" });
            DropIndex("dbo.JobEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.JobEntity", new[] { "CreatedBy_Id" });
            DropIndex("dbo.JobEntity", new[] { "Code" });
            DropIndex("dbo.JobGroupEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.JobGroupEntity", new[] { "Scheduler_Id" });
            DropIndex("dbo.JobGroupEntity", new[] { "CreatedBy_Id" });
            DropIndex("dbo.ExcuteHistoryEntity", new[] { "JobEntity_Id" });
            DropIndex("dbo.ExcuteHistoryEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.ExcuteHistoryEntity", new[] { "CreatedBy_Id" });
            DropIndex("dbo.UserEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.UserEntity", new[] { "CreatedBy_Id" });
            DropIndex("dbo.UserEntity", new[] { "Email" });
            DropIndex("dbo.UserEntity", new[] { "UserName" });
            DropIndex("dbo.TriggerBaseEntity", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.TriggerBaseEntity", new[] { "CreatedBy_Id" });
            DropTable("dbo.CronTriggerEntity");
            DropTable("dbo.JobEntity_TiggerEntity");
            DropTable("dbo.SchedulerEntity");
            DropTable("dbo.ServiceParameterEntity");
            DropTable("dbo.ServiceEntity");
            DropTable("dbo.JobEntity");
            DropTable("dbo.JobGroupEntity");
            DropTable("dbo.ExcuteHistoryEntity");
            DropTable("dbo.UserEntity");
            DropTable("dbo.TriggerBaseEntity");
        }
    }
}
