﻿// <auto-generated />
using System;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(FreelanceContext))]
    partial class FreelanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Database.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<int>("OwnerId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Database.Models.ProjectTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamId");

                    b.ToTable("ProjectTeams");
                });

            modelBuilder.Entity("Database.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompletedStoryPoints");

                    b.Property<string>("Content");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("TaskId");

                    b.Property<int?>("TeamId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Database.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Database.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsClosed");

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.Property<int>("StoryPoints");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Database.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedById");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Database.Models.TeamUser", b =>
                {
                    b.Property<int?>("UserId");

                    b.Property<int?>("TeamId");

                    b.Property<bool>("IsActivated");

                    b.Property<bool>("IsDeclined");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamUsers");
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<double?>("Rating");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Models.Project", b =>
                {
                    b.HasOne("Database.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Database.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Models.ProjectTeam", b =>
                {
                    b.HasOne("Database.Models.Project", "Project")
                        .WithMany("ProjectTeams")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Database.Models.Team", "Team")
                        .WithMany("ProjectTeams")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Database.Models.Report", b =>
                {
                    b.HasOne("Database.Models.Project", "Project")
                        .WithMany("Reports")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Database.Models.Task", "Task")
                        .WithMany("Reports")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Database.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.HasOne("Database.Models.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Database.Models.Tag", b =>
                {
                    b.HasOne("Database.Models.Project", "Project")
                        .WithMany("Tags")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Database.Models.Task", "Task")
                        .WithMany("Tags")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Database.Models.Task", b =>
                {
                    b.HasOne("Database.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Database.Models.Team", "Team")
                        .WithMany("Tasks")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Database.Models.Team", b =>
                {
                    b.HasOne("Database.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("Database.Models.TeamUser", b =>
                {
                    b.HasOne("Database.Models.Team", "Team")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Database.Models.User", "User")
                        .WithMany("TeamUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
