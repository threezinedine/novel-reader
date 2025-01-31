﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NovelReader.API.Context;

#nullable disable

namespace NovelReader.API.Migrations
{
    [DbContext(typeof(CustomDbContext))]
    partial class CustomDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.17");

            modelBuilder.Entity("NovelReader.API.Models.Chapter", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("ChapterNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("NovelId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NovelId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("NovelReader.API.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("NovelId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NovelId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("NovelReader.API.Models.Novel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AvatarURL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverURL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Novels");
                });

            modelBuilder.Entity("NovelReader.API.Models.Rate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("NovelId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rate")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NovelId");

                    b.HasIndex("UserId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("NovelReader.API.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = "79a2f444-3396-455f-938d-344d53beaa08",
                            Description = "",
                            Name = "Action"
                        },
                        new
                        {
                            Id = "c347fd42-acab-46e2-9d5d-c78d87ba288b",
                            Description = "",
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = "be340ffe-53ce-4415-83c0-c1fb17b3c55b",
                            Description = "",
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = "a02b59be-01ce-42cb-806f-18a267296c66",
                            Description = "",
                            Name = "Drama"
                        });
                });

            modelBuilder.Entity("NovelReader.API.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AvatarURL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NovelReader.API.Models.UserNovel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("NovelId")
                        .HasColumnType("TEXT");

                    b.Property<int>("CurrentChapter")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastReadAt")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "NovelId");

                    b.HasIndex("NovelId");

                    b.ToTable("UserNovels");
                });

            modelBuilder.Entity("NovelTag", b =>
                {
                    b.Property<string>("NovelsId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagsId")
                        .HasColumnType("TEXT");

                    b.HasKey("NovelsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("NovelTag");
                });

            modelBuilder.Entity("NovelReader.API.Models.Chapter", b =>
                {
                    b.HasOne("NovelReader.API.Models.Novel", "Novel")
                        .WithMany("Chapters")
                        .HasForeignKey("NovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Novel");
                });

            modelBuilder.Entity("NovelReader.API.Models.Comment", b =>
                {
                    b.HasOne("NovelReader.API.Models.Novel", "Novel")
                        .WithMany("Comments")
                        .HasForeignKey("NovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NovelReader.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Novel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NovelReader.API.Models.Novel", b =>
                {
                    b.HasOne("NovelReader.API.Models.User", "Author")
                        .WithMany("OwnedNovels")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("NovelReader.API.Models.Rate", b =>
                {
                    b.HasOne("NovelReader.API.Models.Novel", "Novel")
                        .WithMany("Rate")
                        .HasForeignKey("NovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NovelReader.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Novel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NovelReader.API.Models.UserNovel", b =>
                {
                    b.HasOne("NovelReader.API.Models.Novel", "Novel")
                        .WithMany("Readers")
                        .HasForeignKey("NovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NovelReader.API.Models.User", "User")
                        .WithMany("ReadNovels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Novel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NovelTag", b =>
                {
                    b.HasOne("NovelReader.API.Models.Novel", null)
                        .WithMany()
                        .HasForeignKey("NovelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NovelReader.API.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NovelReader.API.Models.Novel", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("Comments");

                    b.Navigation("Rate");

                    b.Navigation("Readers");
                });

            modelBuilder.Entity("NovelReader.API.Models.User", b =>
                {
                    b.Navigation("OwnedNovels");

                    b.Navigation("ReadNovels");
                });
#pragma warning restore 612, 618
        }
    }
}
