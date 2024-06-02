﻿// <auto-generated />
using System;
using Fit_Trac.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace teamcmdersbackend.Migrations
{
    [DbContext(typeof(GoalDbContext))]
    [Migration("20240601013635_DailyGoalInputsCreation")]
    partial class DailyGoalInputsCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Fit_Trac.Models.DailyGoalInputs", b =>
                {
                    b.Property<int>("InputId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("GoalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProgressInput")
                        .HasColumnType("INTEGER");

                    b.HasKey("InputId");

                    b.HasIndex("GoalId");

                    b.ToTable("DailyGoalInputs");
                });

            modelBuilder.Entity("Fit_Trac.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DateCreated")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("GoalToReach")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Goal");
                });

            modelBuilder.Entity("Fit_Trac.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Fit_Trac.Models.DailyGoalInputs", b =>
                {
                    b.HasOne("Fit_Trac.Models.Goal", "Goal")
                        .WithMany("DailyGoalInputs")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goal");
                });

            modelBuilder.Entity("Fit_Trac.Models.Goal", b =>
                {
                    b.HasOne("Fit_Trac.Models.User", "User")
                        .WithMany("Goal")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fit_Trac.Models.Goal", b =>
                {
                    b.Navigation("DailyGoalInputs");
                });

            modelBuilder.Entity("Fit_Trac.Models.User", b =>
                {
                    b.Navigation("Goal");
                });
#pragma warning restore 612, 618
        }
    }
}
