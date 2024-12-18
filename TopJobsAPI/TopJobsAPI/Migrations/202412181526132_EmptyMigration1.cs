namespace TopJobsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employers",
                c => new
                    {
                        EmployerId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        ContactNumber = c.String(),
                        Email = c.String(),
                        Organisation = c.String(),
                        OrganisationDetails = c.String(),
                    })
                .PrimaryKey(t => t.EmployerId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 50),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.FileUploads",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        UploadedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        ApplicationId = c.Int(nullable: false, identity: true),
                        JobPostId = c.Int(nullable: false),
                        JobSeekerId = c.Int(nullable: false),
                        JobApprovedStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationId)
                .ForeignKey("dbo.JobPosts", t => t.JobPostId)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeekerId)
                .Index(t => t.JobPostId)
                .Index(t => t.JobSeekerId);
            
            CreateTable(
                "dbo.JobPosts",
                c => new
                    {
                        JobPostId = c.Int(nullable: false, identity: true),
                        EmployerId = c.Int(nullable: false),
                        JobTitle = c.String(),
                        JobDetails = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        VacancyDetails = c.String(),
                        salary = c.Int(nullable: false),
                        RequiredSkills = c.String(),
                        Location = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.JobPostId)
                .ForeignKey("dbo.Employers", t => t.EmployerId, cascadeDelete: true)
                .Index(t => t.EmployerId);
            
            CreateTable(
                "dbo.JobSeekers",
                c => new
                    {
                        JobSeekerId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        PhoneNumber = c.String(maxLength: 12),
                        ResumeId = c.String(nullable: false),
                        Email = c.String(),
                        Skills = c.String(),
                        About = c.String(),
                        Experience = c.String(),
                        Education = c.String(),
                        Organisation = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.JobSeekerId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplications", "JobSeekerId", "dbo.JobSeekers");
            DropForeignKey("dbo.JobSeekers", "UserId", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "JobPostId", "dbo.JobPosts");
            DropForeignKey("dbo.JobPosts", "EmployerId", "dbo.Employers");
            DropForeignKey("dbo.Employers", "UserId", "dbo.Users");
            DropIndex("dbo.JobSeekers", new[] { "UserId" });
            DropIndex("dbo.JobPosts", new[] { "EmployerId" });
            DropIndex("dbo.JobApplications", new[] { "JobSeekerId" });
            DropIndex("dbo.JobApplications", new[] { "JobPostId" });
            DropIndex("dbo.Employers", new[] { "UserId" });
            DropTable("dbo.JobSeekers");
            DropTable("dbo.JobPosts");
            DropTable("dbo.JobApplications");
            DropTable("dbo.FileUploads");
            DropTable("dbo.Users");
            DropTable("dbo.Employers");
        }
    }
}
