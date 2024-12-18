using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TopJobsAPI.Models;

namespace TopJobsAPI.Entities
{
    public class TopJobContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<JobSeekers> JobSeekers { get; set; }
        public DbSet<Employers> Employers { get; set; }
        public DbSet<JobPosts> JobPosts { get; set; }
        public DbSet<JobApplications> JobApplications { get; set; }
        public DbSet<FileUploads> FileUploads { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobApplications>()
                .HasRequired(j => j.JobPosts)
                .WithMany()
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<JobApplications>()
                .HasRequired(j => j.JobSeekers)
                .WithMany()
                .WillCascadeOnDelete(false); 
        }
    }
}