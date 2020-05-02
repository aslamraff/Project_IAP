﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_IAP.Context;

namespace Project_IAP.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project_IAP.Models.BootCamp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Batch");

                    b.Property<string>("Class");

                    b.HasKey("Id");

                    b.ToTable("TB_M_BootCamp");
                });

            modelBuilder.Entity("Project_IAP.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Company");
                });

            modelBuilder.Entity("Project_IAP.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("BirthDate");

                    b.Property<int>("BootCampId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Religion");

                    b.Property<bool>("WorkStatus");

                    b.HasKey("Id");

                    b.HasIndex("BootCampId");

                    b.ToTable("TB_M_Employee");
                });

            modelBuilder.Entity("Project_IAP.Models.Interview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Description");

                    b.Property<string>("DescriptionInterview");

                    b.Property<string>("Division");

                    b.Property<string>("Experience");

                    b.Property<string>("Gender");

                    b.Property<DateTime>("InterviewDate");

                    b.Property<string>("JobDesk");

                    b.Property<string>("LastEducation");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("TB_T_Interview");
                });

            modelBuilder.Entity("Project_IAP.Models.Placement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("ConfirmationCompany");

                    b.Property<bool?>("ConfirmationEmp");

                    b.Property<int>("EmployeeId");

                    b.Property<DateTime?>("EndContract");

                    b.Property<int>("InterviewId");

                    b.Property<DateTime?>("StartContract");

                    b.Property<bool?>("Status");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("InterviewId");

                    b.ToTable("TB_T_Placement");
                });

            modelBuilder.Entity("Project_IAP.Models.Replacement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Confirmation");

                    b.Property<string>("Detail");

                    b.Property<int>("EmployeeId");

                    b.Property<string>("ReplacementReason");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TB_T_Replacement");
                });

            modelBuilder.Entity("Project_IAP.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Role");
                });

            modelBuilder.Entity("Project_IAP.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("TB_M_User");
                });

            modelBuilder.Entity("Project_IAP.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("TB_T_UserRole");
                });

            modelBuilder.Entity("Project_IAP.Models.Employee", b =>
                {
                    b.HasOne("Project_IAP.Models.BootCamp", "BootCamp")
                        .WithMany()
                        .HasForeignKey("BootCampId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_IAP.Models.Interview", b =>
                {
                    b.HasOne("Project_IAP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_IAP.Models.Placement", b =>
                {
                    b.HasOne("Project_IAP.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project_IAP.Models.Interview", "Interview")
                        .WithMany()
                        .HasForeignKey("InterviewId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_IAP.Models.Replacement", b =>
                {
                    b.HasOne("Project_IAP.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_IAP.Models.UserRole", b =>
                {
                    b.HasOne("Project_IAP.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project_IAP.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
