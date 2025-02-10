﻿// <auto-generated />
using System;
using ConstantReminders.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstantReminders.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250203180651_PRUpdatesFeb3Part3")]
    partial class PRUpdatesFeb3Part3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConstantReminders.Contracts.Models.DaysOfWeekEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<Guid>("NotificationScheduleId")
                        .HasColumnType("uuid")
                        .HasColumnName("notification_schedule_id");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date_time");

                    b.HasKey("Id")
                        .HasName("pk_days_of_week_entity");

                    b.HasIndex("NotificationScheduleId")
                        .HasDatabaseName("ix_days_of_week_entity_notification_schedule_id");

                    b.ToTable("days_of_week_entity", (string)null);
                });

            modelBuilder.Entity("ConstantReminders.Contracts.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date_time");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("ConstantReminders.Contracts.Models.NotificationSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time");

                    b.Property<int?>("DurationInDays")
                        .HasColumnType("integer")
                        .HasColumnName("duration_in_days");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<TimeSpan>("FrequencyWithinDay")
                        .HasColumnType("interval")
                        .HasColumnName("frequency_within_day");

                    b.Property<int>("NotificationType")
                        .HasColumnType("integer")
                        .HasColumnName("notification_type");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date_time");

                    b.HasKey("Id")
                        .HasName("pk_notification_schedule");

                    b.HasIndex("EventId")
                        .IsUnique()
                        .HasDatabaseName("ix_notification_schedule_event_id");

                    b.ToTable("notification_schedule", (string)null);
                });

            modelBuilder.Entity("ConstantReminders.Contracts.Models.DaysOfWeekEntity", b =>
                {
                    b.HasOne("ConstantReminders.Contracts.Models.NotificationSchedule", "NotificationSchedule")
                        .WithMany("DaysOfWeek")
                        .HasForeignKey("NotificationScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_days_of_week_entity_notification_schedule_notification_sche");

                    b.Navigation("NotificationSchedule");
                });

            modelBuilder.Entity("ConstantReminders.Contracts.Models.NotificationSchedule", b =>
                {
                    b.HasOne("ConstantReminders.Contracts.Models.Event", "Event")
                        .WithOne("NotificationSchedule")
                        .HasForeignKey("ConstantReminders.Contracts.Models.NotificationSchedule", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_notification_schedule_events_event_id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ConstantReminders.Contracts.Models.Event", b =>
                {
                    b.Navigation("NotificationSchedule");
                });

            modelBuilder.Entity("ConstantReminders.Contracts.Models.NotificationSchedule", b =>
                {
                    b.Navigation("DaysOfWeek");
                });
#pragma warning restore 612, 618
        }
    }
}
