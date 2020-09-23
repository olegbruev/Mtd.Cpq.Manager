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
    [Migration("20191018120646_ProposalMasterPrice")]
    partial class ProposalMasterPrice
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnName("note")
                        .HasColumnType("varchar(768)");

                    b.Property<string>("Parent")
                        .HasColumnName("parent")
                        .HasColumnType("varchar(36)");

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
                        .IsUnique()
                        .HasName("ix_number");

                    b.HasIndex("Parent")
                        .HasName("fk_cpq_catalog_idx");

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
                        .IsRequired()
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
                        .IsRequired()
                        .HasColumnName("contact_email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnName("contact_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnName("contact_phone")
                        .HasColumnType("varchar(255)");

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

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProduct", b =>
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

                    b.Property<string>("MtdCpqCatalogId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_catalog_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnName("note")
                        .HasColumnType("varchar(3027)");

                    b.Property<sbyte>("Som")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("som")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("Id");

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

                    b.Property<DateTime>("DateCreation")
                        .HasColumnName("date_creation")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(3072)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnName("id_number")
                        .HasColumnType("varchar(45)");

                    b.Property<byte[]>("Logo")
                        .HasColumnName("logo")
                        .HasColumnType("mediumblob");

                    b.Property<decimal>("MasterPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("master-price")
                        .HasColumnType("decimal(20,2)")
                        .HasDefaultValueSql("'0.00'");

                    b.Property<string>("MasterProductId")
                        .HasColumnName("master_product_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("MtdCpqGroupId")
                        .IsRequired()
                        .HasColumnName("mtd_cpq_group_id")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("PreparedBy")
                        .IsRequired()
                        .HasColumnName("prepared_by")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PreparedFor")
                        .IsRequired()
                        .HasColumnName("prepared_for")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("TimeCh");

                    b.Property<DateTime>("TimeCr");

                    b.Property<string>("TitleName")
                        .IsRequired()
                        .HasColumnName("title_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TitleNote")
                        .IsRequired()
                        .HasColumnName("title_note")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Total")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("total")
                        .HasColumnType("decimal(20,2)")
                        .HasDefaultValueSql("'0.00'");

                    b.Property<string>("UserCh")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("UserCr")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("IdNumber")
                        .HasName("ix_number");

                    b.HasIndex("MasterProductId");

                    b.HasIndex("MtdCpqGroupId")
                        .HasName("fk_mtd_cpq_proposal_mtd_cpq_config1_idx");

                    b.ToTable("mtd_cpq_proposal");
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
                        .IsRequired()
                        .HasColumnName("note")
                        .HasColumnType("varchar(3027)");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal(20,2)");

                    b.Property<sbyte>("Required")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("required")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<sbyte>("Selected")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("selected")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'1'");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("id_UNIQUE");

                    b.HasIndex("MtdCpqProductId")
                        .HasName("fk_mtd_cpq_rule_idx");

                    b.HasIndex("MtdCpqProposalId")
                        .HasName("fk_mtd_cpq_complectation_mtd_cpq_proposal1_idx");

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

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqCatalog", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqCatalog", "ParentNavigation")
                        .WithMany("InverseParentNavigation")
                        .HasForeignKey("Parent")
                        .HasConstraintName("fk_cpq_catalog")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqGroupParam", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqGroup", "MtdCpqGroup")
                        .WithMany("MtdCpqGroupParam")
                        .HasForeignKey("MtdCpqGroupId")
                        .HasConstraintName("fk_mtd_cpq_config_param_mtd_cpq_config1")
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
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProduct", "MasterProduct")
                        .WithMany("MtdCpqProposal")
                        .HasForeignKey("MasterProductId")
                        .HasConstraintName("fk_mtd_cpq_proposal_product")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqGroup", "MtdCpqGroup")
                        .WithMany("MtdCpqProposal")
                        .HasForeignKey("MtdCpqGroupId")
                        .HasConstraintName("fk_mtd_cpq_proposal_mtd_cpq_config1");
                });

            modelBuilder.Entity("Mtd.Cpq.Manager.Data.MtdCpqProposalItem", b =>
                {
                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProduct", "MtdCpqProduct")
                        .WithMany("MtdCpqProposalItem")
                        .HasForeignKey("MtdCpqProductId")
                        .HasConstraintName("fk_mtd_cpq_rule")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Mtd.Cpq.Manager.Data.MtdCpqProposal", "MtdCpqProposal")
                        .WithMany("MtdCpqProposalItem")
                        .HasForeignKey("MtdCpqProposalId")
                        .HasConstraintName("fk_mtd_cpq_complectation_mtd_cpq_proposal1")
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
