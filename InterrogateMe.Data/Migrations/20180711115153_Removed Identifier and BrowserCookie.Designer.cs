﻿// <auto-generated />
using System;
using InterrogateMe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InterrogateMe.Data.Migrations
{
    [DbContext(typeof(InterrogateDbContext))]
    [Migration("20180711115153_Removed Identifier and BrowserCookie")]
    partial class RemovedIdentifierandBrowserCookie
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("InterrogateMe.Core.Models.IpAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("RequestScheme");

                    b.Property<Guid>("TopicId");

                    b.Property<string>("UserAgent");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("IpAddresses");
                });

            modelBuilder.Entity("InterrogateMe.Core.Models.Link", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<Guid>("TopicId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("InterrogateMe.Core.Models.ProfaneWord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("ProfaneWords");
                });

            modelBuilder.Entity("InterrogateMe.Core.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.Property<DateTime>("DateAsked");

                    b.Property<int>("Like");

                    b.Property<Guid?>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("InterrogateMe.Core.Models.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowMultipleLikes");

                    b.Property<int>("DuplicationCheck");

                    b.Property<bool>("PreventNSFW");

                    b.Property<bool>("PreventSpam");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(2147483647);

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("InterrogateMe.Core.Models.IpAddress", b =>
                {
                    b.HasOne("InterrogateMe.Core.Models.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterrogateMe.Core.Models.Link", b =>
                {
                    b.HasOne("InterrogateMe.Core.Models.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterrogateMe.Core.Models.Question", b =>
                {
                    b.HasOne("InterrogateMe.Core.Models.Topic")
                        .WithMany("Questions")
                        .HasForeignKey("TopicId");
                });
#pragma warning restore 612, 618
        }
    }
}
