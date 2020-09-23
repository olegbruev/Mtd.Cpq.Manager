﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mtd.Cpq.Manager.Data;

namespace Mtd.Cpq.Manager.Migrations
{
    [DbContext(typeof(CpqContext))]
    [Migration("20191122132922_AddParamForProposal2")]
    partial class AddParamForProposal2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqCatalog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnName("id_number")
                        .HasColumnType("varchar(45)");

                    b.Property<byte[]>("Image")
                        .HasColumnName("image")
                        .HasColumnType("mediumblob");

                    b.Property<string>("ImportTag")
                        .HasColumnName("import_tag")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .HasColumnName("note")
                        .HasColumnType("varchar(768)");

                    b.Property<int>("Sequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("sequence")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("IdNumber")
                        .HasName("ix_number");

                    b.HasIndex("Sequence")
                        .HasName("ix_sequence");

                    b.ToTable("mtd_cpq_catalog");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqGroup", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<int>("Counter")
                        .HasColumnName("counter")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .HasColumnName("note")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("name_UNIQUE");

                    b.ToTable("mtd_cpq_group");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqGroupParam", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ContactEmail")
                        .HasColumnName("contact_email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContactName")
                        .HasColumnName("contact_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnName("contact_phone")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("language")
                        .HasColumnType("varchar(45)")
                        .HasDefaultValueSql("'en-US'");

                    b.Property<byte[]>("Logo")
                        .HasColumnName("logo")
                        .HasColumnType("mediumblob");

                    b.Property<string>("MtdCpqGroupId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_config_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnName("note")
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnName("prefix")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("PreparedBy")
                        .IsRequired()
                        .HasColumnName("prepared_by")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("MtdCpqGroupId")
                        .HasName("fk_mtd_cpq_config_param_mtd_cpq_config1_idx");

                    b.ToTable("mtd_cpq_group_param");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqImport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<sbyte>("NoteLoad")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("note_load")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<sbyte>("OldToArchive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("old_to_archive")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<int>("StatusProcess")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("status_process")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("StatusText")
                        .HasColumnName("status_text")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("TimeCr")
                        .HasColumnName("time_cr")
                        .HasColumnType("datetime");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("StatusProcess")
                        .HasName("idx_process");

                    b.ToTable("mtd_cpq_import");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqImportData", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<int>("Action")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("action")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("CatalogTag")
                        .HasColumnName("catalog_tag")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnName("id_number")
                        .HasColumnType("varchar(45)");

                    b.Property<sbyte>("MasterProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("master_product")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("MtdCpqImportId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_import_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .HasColumnName("note")
                        .HasColumnType("varchar(6048)");

                    b.Property<string>("Parent")
                        .HasColumnName("parent")
                        .HasColumnType("varchar(45)");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("price")
                        .HasColumnType("decimal(20,2)")
                        .HasDefaultValueSql("'0.00'");

                    b.Property<sbyte>("Required")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("required")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("IdNumber")
                        .HasName("idx_number");

                    b.HasIndex("MtdCpqImportId")
                        .HasName("fk_import_data_history_idx");

                    b.ToTable("mtd_cpq_import_data");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqImportParam", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<int>("ColName")
                        .HasColumnName("col_name")
                        .HasColumnType("int(11)");

                    b.Property<int>("ColNote")
                        .HasColumnName("col_note")
                        .HasColumnType("int(11)");

                    b.Property<int>("ColNumber")
                        .HasColumnName("col_number")
                        .HasColumnType("int(11)");

                    b.Property<int>("ColPrice")
                        .HasColumnName("col_price")
                        .HasColumnType("int(11)");

                    b.Property<string>("TagData")
                        .IsRequired()
                        .HasColumnName("tag_data")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TagMaster")
                        .IsRequired()
                        .HasColumnName("tag_master")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TagRequired")
                        .IsRequired()
                        .HasColumnName("tag_required")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.ToTable("mtd_cpq_import_param");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProduct", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<sbyte>("Archive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("archive")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnName("id_number")
                        .HasColumnType("varchar(45)");

                    b.Property<byte[]>("Image")
                        .HasColumnName("image")
                        .HasColumnType("mediumblob");

                    b.Property<string>("MtdCpqCatalogId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_catalog_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .HasColumnName("note")
                        .HasColumnType("varchar(6048)");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("price")
                        .HasColumnType("decimal(20,2)")
                        .HasDefaultValueSql("'0'");

                    b.Property<sbyte>("Som")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("som")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("Id");

                    b.HasIndex("Archive")
                        .HasName("ix_archive");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("IdNumber")
                        .IsUnique()
                        .HasName("id_number_UNIQUE");

                    b.HasIndex("MtdCpqCatalogId")
                        .HasName("fk_mtd_cpq_item_mtd_cpq_catalog1_idx");

                    b.HasIndex("Som")
                        .HasName("ix_som_index");

                    b.ToTable("mtd_cpq_product");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProposal", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnName("contact_email")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnName("contact_name")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnName("contact_phone")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("CustomerCurrency")
                        .HasColumnName("customer_currency")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnName("date_creation")
                        .HasColumnType("datetime");

                    b.Property<string>("DeliveryCondition")
                        .HasColumnName("delivery_condition")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("varchar(3072)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnName("id_number")
                        .HasColumnType("varchar(45)");

                    b.Property<byte[]>("Logo")
                        .HasColumnName("logo")
                        .HasColumnType("mediumblob");

                    b.Property<string>("MasterId")
                        .HasColumnName("master_id")
                        .HasColumnType("varchar(36)");

                    b.Property<byte[]>("MasterImage")
                        .HasColumnName("master_image")
                        .HasColumnType("mediumblob");

                    b.Property<string>("MasterName")
                        .HasColumnName("master_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MasterNote")
                        .HasColumnName("master_note")
                        .HasColumnType("varchar(6048)");

                    b.Property<string>("MasterNumber")
                        .HasColumnName("master_number")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("MasterPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("master_price")
                        .HasColumnType("decimal(20,2)")
                        .HasDefaultValueSql("'0.00'");

                    b.Property<int>("MasterQty")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("master_qty")
                        .HasColumnType("int")
                        .HasDefaultValueSql("1");

                    b.Property<string>("MtdCpqGroupId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_group_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("MtdCpqGroupParamId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_group_param_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("PreparedBy")
                        .IsRequired()
                        .HasColumnName("prepared_by")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PreparedFor")
                        .IsRequired()
                        .HasColumnName("prepared_for")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("PriceCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("price_customer")
                        .HasColumnType("decimal(20,2)")
                        .HasDefaultValueSql("'0.00'");

                    b.Property<DateTime>("TimeCh");

                    b.Property<DateTime>("TimeCr");

                    b.Property<string>("TitleName")
                        .HasColumnName("title_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TitleNote")
                        .HasColumnName("title_note")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TypeConfig")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("type_config")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("'1'");

                    b.Property<string>("UserCh")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("UserCr")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<sbyte>("ViewDelivery")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("view_delivery")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'1'");

                    b.Property<sbyte>("ViewNote")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("view_note")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<sbyte>("ViewPriceCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("view_price_customer")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<sbyte>("ViewPriceGross")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("view_price_gross")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'1'");

                    b.Property<sbyte>("ViewQty")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("view_qty")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'1'");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("IdNumber")
                        .HasName("ix_number");

                    b.HasIndex("MasterId")
                        .HasName("idx_master_id");

                    b.HasIndex("MtdCpqGroupId")
                        .HasName("fk_mtd_cpq_proposal_mtd_cpq_config1_idx");

                    b.HasIndex("MtdCpqGroupParamId")
                        .HasName("fk_mtd_cpq_proposal_group_param_idx");

                    b.ToTable("mtd_cpq_proposal");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProposalCatalog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CId")
                        .IsRequired()
                        .HasColumnName("cid")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("IdNumber")
                        .HasColumnName("id_number")
                        .HasColumnType("varchar(45)");

                    b.Property<byte[]>("Image")
                        .HasColumnName("image")
                        .HasColumnType("mediumblob");

                    b.Property<string>("MtdCpqProposalId")
                        .HasColumnName("mtd_cpq_proposal_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .HasColumnName("note")
                        .HasColumnType("varchar(768)");

                    b.Property<int>("Sequence")
                        .HasColumnName("sequence")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("CId")
                        .HasName("idx_cid");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("MtdCpqProposalId")
                        .HasName("fk_mtd_cpq_proposal_catalog_idx");

                    b.ToTable("mtd_cpq_proposal_catalog");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProposalItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnName("id_number")
                        .HasColumnType("varchar(45)");

                    b.Property<byte[]>("Image")
                        .HasColumnName("image")
                        .HasColumnType("mediumblob");

                    b.Property<string>("MtdCpqProductId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_product_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("MtdCpqProposalId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_proposal_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .HasColumnName("note")
                        .HasColumnType("varchar(6048)");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal(20,2)");

                    b.Property<string>("ProposalCatalogId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_proposal_catalog_id")
                        .HasColumnType("varchar(36)");

                    b.Property<int>("Qty")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("qty")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("'1'");

                    b.Property<sbyte>("Required")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("required")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<sbyte>("Selected")
                        .HasColumnName("selected")
                        .HasColumnType("tinyint(4)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("MtdCpqProductId")
                        .HasName("fk_mtd_cpq_rule_idx");

                    b.HasIndex("MtdCpqProposalId")
                        .HasName("fk_mtd_cpq_complectation_mtd_cpq_proposal1_idx");

                    b.HasIndex("ProposalCatalogId")
                        .HasName("fk_mtd_cpq_proposalitems_catalog_idx");

                    b.HasIndex("Required")
                        .HasName("IX_REQUIRED");

                    b.HasIndex("Selected")
                        .HasName("IX_SELECTED");

                    b.ToTable("mtd_cpq_proposal_item");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqRuleAvailable", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ProductIdChild")
                        .IsRequired()
                        .HasColumnName("product_id_child")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ProductIdParent")
                        .IsRequired()
                        .HasColumnName("product_id_parent")
                        .HasColumnType("varchar(36)");

                    b.Property<sbyte>("Required")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("required")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("ProductIdChild")
                        .HasName("fk_mtd_cpq_rule_available_mtd_cpq_product1_idx");

                    b.HasIndex("ProductIdParent")
                        .HasName("fk_mtd_cpq_available_mtd_cpq_product1_idx");

                    b.ToTable("mtd_cpq_rule_available");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqGroupParam", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqGroup", "MtdCpqGroup")
                        .WithMany("MtdCpqGroupParam")
                        .HasForeignKey("MtdCpqGroupId")
                        .HasConstraintName("fk_mtd_cpq_config_param_mtd_cpq_config1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqImportData", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqImport", "MtdCpqImport")
                        .WithMany("MtdCpqImportData")
                        .HasForeignKey("MtdCpqImportId")
                        .HasConstraintName("fk_import_data_history")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProduct", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqCatalog", "MtdCpqCatalog")
                        .WithMany("MtdCpqProduct")
                        .HasForeignKey("MtdCpqCatalogId")
                        .HasConstraintName("fk_mtd_cpq_item_mtd_cpq_catalog1")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProposal", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqGroup", "MtdCpqGroup")
                        .WithMany("MtdCpqProposal")
                        .HasForeignKey("MtdCpqGroupId")
                        .HasConstraintName("fk_mtd_cpq_proposal_mtd_cpq_config1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqGroupParam", "MtdCpqGroupParam")
                        .WithMany("MtdCpqProposal")
                        .HasForeignKey("MtdCpqGroupParamId")
                        .HasConstraintName("fk_mtd_cpq_proposal_group_param")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProposalCatalog", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProposal", "MtdCpqProposal")
                        .WithMany("MtdCpqProposalCatalog")
                        .HasForeignKey("MtdCpqProposalId")
                        .HasConstraintName("fk_mtd_cpq_proposal_catalog")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProposalItem", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProposal", "MtdCpqProposal")
                        .WithMany("MtdCpqProposalItem")
                        .HasForeignKey("MtdCpqProposalId")
                        .HasConstraintName("fk_mtd_cpq_complectation_mtd_cpq_proposal1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProposalCatalog", "MtdCpqProposalCatalog")
                        .WithMany("MtdCpqProposalItem")
                        .HasForeignKey("ProposalCatalogId")
                        .HasConstraintName("fk_mtd_cpq_proposalitems_catalog")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqRuleAvailable", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProduct", "ProductIdChildNavigation")
                        .WithMany("MtdCpqRuleAvailableProductIdChildNavigation")
                        .HasForeignKey("ProductIdChild")
                        .HasConstraintName("fk_mtd_cpq_rule_available_mtd_cpq_product1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProduct", "ProductIdParentNavigation")
                        .WithMany("MtdCpqRuleAvailableProductIdParentNavigation")
                        .HasForeignKey("ProductIdParent")
                        .HasConstraintName("fk_mtd_cpq_available_mtd_cpq_product1")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
