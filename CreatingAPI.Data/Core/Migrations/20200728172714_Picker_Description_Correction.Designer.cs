﻿// <auto-generated />
using System;
using CreatingAPI.Data.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CreatingAPI.Data.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200728172714_Picker_Description_Correction")]
    partial class Picker_Description_Correction
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CreatingAPI.Domain.Bookmarks.Bookmark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PickerId")
                        .HasColumnType("int");

                    b.Property<int?>("TicTacToeId")
                        .HasColumnType("int");

                    b.Property<int?>("UnscrambleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PickerId");

                    b.HasIndex("TicTacToeId");

                    b.HasIndex("UnscrambleId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookmark");
                });

            modelBuilder.Entity("CreatingAPI.Domain.Games.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("NumberOfCorrectAnswers")
                        .HasColumnType("TINYINT");

                    b.Property<byte>("NumberOfWrongAnswers")
                        .HasColumnType("TINYINT");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UnscrambleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UnscrambleId");

                    b.HasIndex("UserId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("CreatingAPI.Domain.Pickers.Picker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Picker");
                });

            modelBuilder.Entity("CreatingAPI.Domain.Pickers.PickerTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("PickerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PickerId");

                    b.ToTable("PickerTopic");
                });

            modelBuilder.Entity("CreatingAPI.Domain.TicTacToes.TicTacToe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TicTacToe");
                });

            modelBuilder.Entity("CreatingAPI.Domain.TicTacToes.TicTacToeSquare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<byte>("Position")
                        .HasColumnType("TINYINT");

                    b.Property<int>("TicTacToeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TicTacToeId");

                    b.ToTable("TicTacToeSquare");
                });

            modelBuilder.Entity("CreatingAPI.Domain.Unscrambles.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<byte>("Position")
                        .HasColumnType("TINYINT");

                    b.Property<int>("UnscrambleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UnscrambleId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("CreatingAPI.Domain.Unscrambles.Unscramble", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Unscramble");
                });

            modelBuilder.Entity("CreatingAPI.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CreatingAPI.Domain.Bookmarks.Bookmark", b =>
                {
                    b.HasOne("CreatingAPI.Domain.Pickers.Picker", "Picker")
                        .WithMany("Bookmarks")
                        .HasForeignKey("PickerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CreatingAPI.Domain.TicTacToes.TicTacToe", "TicTacToe")
                        .WithMany("Bookmarks")
                        .HasForeignKey("TicTacToeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CreatingAPI.Domain.Unscrambles.Unscramble", "Unscramble")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UnscrambleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CreatingAPI.Domain.Users.User", "User")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.Games.Game", b =>
                {
                    b.HasOne("CreatingAPI.Domain.Unscrambles.Unscramble", "Unscramble")
                        .WithMany()
                        .HasForeignKey("UnscrambleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CreatingAPI.Domain.Users.User", "User")
                        .WithMany("Games")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.Pickers.Picker", b =>
                {
                    b.HasOne("CreatingAPI.Domain.Users.User", "User")
                        .WithMany("Pickers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.Pickers.PickerTopic", b =>
                {
                    b.HasOne("CreatingAPI.Domain.Pickers.Picker", "Picker")
                        .WithMany("Topics")
                        .HasForeignKey("PickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.TicTacToes.TicTacToe", b =>
                {
                    b.HasOne("CreatingAPI.Domain.Users.User", "User")
                        .WithMany("TicTacToes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.TicTacToes.TicTacToeSquare", b =>
                {
                    b.HasOne("CreatingAPI.Domain.TicTacToes.TicTacToe", "TicTacToe")
                        .WithMany("Squares")
                        .HasForeignKey("TicTacToeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.Unscrambles.Exercise", b =>
                {
                    b.HasOne("CreatingAPI.Domain.Unscrambles.Unscramble", "Unscramble")
                        .WithMany("Exercises")
                        .HasForeignKey("UnscrambleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.Unscrambles.Unscramble", b =>
                {
                    b.HasOne("CreatingAPI.Domain.Users.User", "User")
                        .WithMany("Unscrambles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CreatingAPI.Domain.Users.User", b =>
                {
                    b.OwnsOne("CreatingAPI.Domain.Core.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasColumnType("nvarchar(150)")
                                .HasMaxLength(150);

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("CreatingAPI.Domain.Core.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Characters")
                                .IsRequired()
                                .HasColumnName("Password")
                                .HasColumnType("nvarchar(150)")
                                .HasMaxLength(150);

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
