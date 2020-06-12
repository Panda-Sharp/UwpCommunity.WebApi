﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UwpCommunity.Data;

namespace UwpCommunity.Data.Migrations
{
    [DbContext(typeof(UwpCommunityDbContext))]
    [Migration("20200612130343_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("UwpCommunity.Data.Models.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AppName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("AwaitingLaunchApproval")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("ClientLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("DownloadLink")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExternalLink")
                        .HasColumnType("TEXT");

                    b.Property<string>("GithubLink")
                        .HasColumnType("TEXT");

                    b.Property<string>("HeroImage")
                        .HasColumnType("TEXT");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("LookingForRoles")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NeedsManualReview")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProjectId");

                    b.HasIndex("LastUpdated");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("UwpCommunity.Data.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ClientLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiscordId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("LastUpdated");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UwpCommunity.Data.Models.UserProject", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ClientLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("UserProject");
                });

            modelBuilder.Entity("UwpCommunity.Data.Models.UserProject", b =>
                {
                    b.HasOne("UwpCommunity.Data.Models.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UwpCommunity.Data.Models.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}