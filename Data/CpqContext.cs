using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mtd.Cpq.Manager.Data
{
    public partial class CpqContext : DbContext
    {
        public CpqContext()
        {
        }

        public CpqContext(DbContextOptions<CpqContext> options)
            : base(options)
        {
           // Database.SetCommandTimeout(150000);
        }

        public virtual DbSet<MtdCpqCatalog> MtdCpqCatalog { get; set; }
        public virtual DbSet<MtdCpqCounter> MtdCpqCounter { get; set; }        
        public virtual DbSet<MtdCpqImport> MtdCpqImport { get; set; }
        public virtual DbSet<MtdCpqImportData> MtdCpqImportData { get; set; }
        public virtual DbSet<MtdCpqImportParam> MtdCpqImportParams { get; set; }
        public virtual DbSet<MtdCpqOneOf> MtdCpqOneOfs { get; set; }
        public virtual DbSet<MtdCpqProduct> MtdCpqProduct { get; set; }
        public virtual DbSet<MtdCpqProposal> MtdCpqProposal { get; set; }
        public virtual DbSet<MtdCpqProposalCatalog> MtdCpqProposalCatalogs { get; set; }
        public virtual DbSet<MtdCpqProposalOneOf> MtdCpqProposalOneOf { get; set; }
        public virtual DbSet<MtdCpqProposalAnchor> MtdCpqProposalAnchor { get; set; }
        public virtual DbSet<MtdCpqProposalItem> MtdCpqProposalItem { get; set; }
        public virtual DbSet<MtdCpqRuleAnchor> MtdCpqRuleAnchor { get; set; }
        public virtual DbSet<MtdCpqRuleAnchorBind> MtdCpqRuleAnchorBind { get; set; }
        public virtual DbSet<MtdCpqRuleAvailable> MtdCpqRuleAvailable { get; set; }
        public virtual DbSet<MtdCpqTitles> MtdCpqTitles { get; set; }
        public virtual DbSet<MtdCpqConfig> MtdCpqConfig { get; set; }
        public virtual DbSet<MtdCpqConfigFile> MtdCpqConfigFiles { get; set; }
        public virtual DbSet<MtdCpqNotification> MtdCpqNotifications { get; set; }
        public virtual DbSet<MtdCpqReaderUser> MtdCpqReaderUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MtdCpqCatalog>(entity =>
            {
                entity.ToTable("mtd_cpq_catalog");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdNumber)
                    .HasName("ix_number");

                entity.HasIndex(e => e.Sequence)
                    .HasName("ix_sequence");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("varchar(768)");

                entity.Property(e => e.Sequence)
                    .HasColumnName("sequence")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ImportTag)
                    .HasColumnName("import_tag")
                    .HasColumnType("varchar(50)");

            });

            modelBuilder.Entity<MtdCpqCounter>(entity =>
            {
                entity.ToTable("mtd_cpq_counter");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Counter)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("bigint");                
            });

            modelBuilder.Entity<MtdCpqImport>(entity =>
            {
                entity.ToTable("mtd_cpq_import");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StatusProcess)
                    .HasName("idx_process");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TimeCr)
                    .HasColumnName("time_cr")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.StatusText)
                    .HasColumnName("status_text")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NoteLoad)
                    .HasColumnName("note_load")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DatasheetLoad)
                    .HasColumnName("datasheet_load")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OldToArchive)
                    .HasColumnName("old_to_archive")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.StatusProcess)
                    .IsRequired()
                    .HasColumnName("status_process")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<MtdCpqImportData>(entity =>
            {
                entity.ToTable("mtd_cpq_import_data");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdNumber)
                    .HasName("idx_number");

                entity.HasIndex(e => e.MtdCpqImportId)
                    .HasName("fk_import_data_history_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.MtdCpqImportId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_import_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.Datasheet)
                    .HasColumnName("datasheet")
                    .HasColumnType("text");

                entity.Property(e => e.Parent)
                    .HasColumnName("parent")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(20,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Required)
                    .HasColumnName("required")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TagCatalog)
                    .HasColumnName("catalog_tag")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TagOneOf)
                    .HasColumnName("one_of")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.MasterProduct)
                    .HasColumnName("master_product")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.MtdCpqImport)
                    .WithMany(p => p.MtdCpqImportData)
                    .HasForeignKey(d => d.MtdCpqImportId)
                    .HasConstraintName("fk_import_data_history");

            });

            modelBuilder.Entity<MtdCpqImportParam>(entity =>
            {
                entity.ToTable("mtd_cpq_import_param");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TagData)
                    .IsRequired()
                    .HasColumnName("tag_data")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ColName)
                    .IsRequired()
                    .HasColumnName("col_name")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ColNote)
                    .IsRequired()
                    .HasColumnName("col_note")
                    .HasColumnType("int(11)");
                
                entity.Property(e => e.ColData)
                    .IsRequired()
                    .HasColumnName("col_data")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ColNumber)
                    .IsRequired()
                    .HasColumnName("col_number")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ColPrice)
                    .HasColumnName("col_price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TagMaster)
                    .IsRequired()
                    .HasColumnName("tag_master")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TagRequired)
                    .IsRequired()
                    .HasColumnName("tag_required")
                    .HasColumnType("varchar(50)");

            });

            modelBuilder.Entity<MtdCpqOneOf>(entity =>
            {
                entity.ToTable("mtd_cpq_one_of");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Color)                    
                    .HasColumnName("color")
                    .HasColumnType("varchar(45)");
               
                entity.Property(e => e.ImportTag)               
                    .HasColumnName("import_tag")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

            });

            modelBuilder.Entity<MtdCpqProduct>(entity =>
            {
                entity.ToTable("mtd_cpq_product");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdNumber)
                    .HasName("id_number_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.MtdCpqCatalogId)
                    .HasName("fk_mtd_cpq_item_mtd_cpq_catalog1_idx");

                entity.HasIndex(e => e.Som)
                    .HasName("ix_som_index");

                entity.HasIndex(e => e.Archive)
                    .HasName("ix_archive");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.MtdCpqCatalogId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_catalog_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.Datasheet)
                    .HasColumnName("datasheet")
                    .HasColumnType("text");

                entity.Property(e => e.Som)
                    .IsRequired()
                    .HasColumnName("som")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Trial)
                    .IsRequired()
                    .HasColumnName("trial")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnName("price")
                    .HasColumnType("decimal(20,2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Archive)
                    .IsRequired()
                    .HasColumnName("archive")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sequence)
                    .IsRequired()
                    .HasColumnName("sequence")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.MtdCpqCatalog)
                    .WithMany(p => p.MtdCpqProduct)
                    .HasForeignKey(d => d.MtdCpqCatalogId)
                    .HasConstraintName("fk_mtd_cpq_item_mtd_cpq_catalog1");
            });

            modelBuilder.Entity<MtdCpqProposal>(entity =>
            {
                entity.ToTable("mtd_cpq_proposal");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdNumber)
                    .HasName("ix_number");

                entity.HasIndex(e => e.DateCreation)
                    .HasName("ix_datecreation");

                entity.HasIndex(e => e.MasterId)
                    .HasName("idx_master_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasColumnName("contact_email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasColumnName("contact_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasColumnName("contact_phone")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DateCreation)
                    .HasColumnName("date_creation")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(3072)");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.LogoWidth)
                    .IsRequired()
                    .HasColumnName("logo_width")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LogoHeight)
                    .IsRequired()
                    .HasColumnName("logo_height")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LogoFlexible)
                    .IsRequired()
                    .HasColumnName("logo_flexible")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PreparedBy)
                    .IsRequired()
                    .HasColumnName("prepared_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PreparedFor)
                    .IsRequired()
                    .HasColumnName("prepared_for")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TitleName)
                    .HasColumnName("title_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TitleNote)
                    .HasColumnName("title_note")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PriceCustomer)
                    .HasColumnName("price_customer")
                    .HasColumnType("decimal(20,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.CustomerCurrency)
                    .HasColumnName("customer_currency")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ConfigMasterInluded)
                    .IsRequired()
                    .HasColumnName("config_master_included")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ConfigChangeRule)
                    .IsRequired()
                    .HasColumnName("config_change_rule")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DeliveryCondition)
                    .HasColumnName("delivery_condition")
                    .HasColumnType("text");

                entity.Property(e => e.MasterPrice)
                    .HasColumnName("master_price")
                    .HasColumnType("decimal(20,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.MasterQty)
                    .HasColumnName("master_qty")
                    .HasColumnType("int")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ViewNote)
                    .IsRequired()
                    .HasColumnName("view_note")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ViewPriceGross)
                    .IsRequired()
                    .HasColumnName("view_price_gross")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ViewPriceCustomer)
                    .IsRequired()
                    .HasColumnName("view_price_customer")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ViewDelivery)
                    .IsRequired()
                    .HasColumnName("view_delivery")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ViewQty)
                    .HasColumnName("view_qty")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ViewImages)
                    .HasColumnName("view_images")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ViewProposal)
                    .IsRequired()
                    .HasColumnName("view_proposal")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ViewDatasheet)
                    .IsRequired()
                    .HasColumnName("view_datasheet")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");


                entity.Property(e => e.MasterId)
                    .HasColumnName("master_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.MasterNumber)
                    .HasColumnName("master_number")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MasterName)
                    .HasColumnName("master_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MasterNote)
                    .HasColumnName("master_note")
                    .HasColumnType("text");

                entity.Property(e => e.MasterDatasheet)
                    .HasColumnName("master_datasheet")
                    .HasColumnType("text");

                entity.Property(e => e.MasterImage)
                    .HasColumnName("master_image")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.MasterImage)
                    .HasColumnName("master_image")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .HasColumnType("varchar(50)");

            });

            modelBuilder.Entity<MtdCpqProposalAnchor>(entity =>
            {
                entity.ToTable("mtd_cpq_proposal_anchor");

                entity.HasIndex(e => e.Cid)
                    .HasName("fk_proposal_anchor_idx");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Cid)
                    .IsRequired()
                    .HasColumnName("cid")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MtdCpqProductId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_product_id")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Include)
                    .IsRequired()
                    .HasColumnName("include")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Required)
                    .IsRequired()
                    .HasColumnName("required")
                    .HasColumnType("tinyint(4)");                  

                entity.HasOne(d => d.MtdCpqProposalItem)
                    .WithMany(p => p.MtdCpqProposalAnchor)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("fk_proposal_anchor");
            });

            modelBuilder.Entity<MtdCpqProposalCatalog>(entity =>
            {
                entity.ToTable("mtd_cpq_proposal_catalog");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CId)
                    .HasName("idx_cid");

                entity.HasIndex(e => e.MtdCpqProposalId)
                    .HasName("fk_mtd_cpq_proposal_catalog_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.MtdCpqProposalId)
                    .HasColumnName("mtd_cpq_proposal_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CId)
                    .IsRequired()
                    .HasColumnName("cid")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("varchar(768)");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.IdNumber)
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Sequence)
                    .IsRequired()
                    .HasColumnName("sequence")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.MtdCpqProposal)
                    .WithMany(p => p.MtdCpqProposalCatalog)
                    .HasForeignKey(d => d.MtdCpqProposalId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_mtd_cpq_proposal_catalog");
            });
            
            modelBuilder.Entity<MtdCpqProposalOneOf>(entity =>
            {
                entity.ToTable("mtd_cpq_proposal_one_of");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CId)
                    .HasName("idx_cid");

                entity.HasIndex(e => e.MtdCpqProposalId)
                    .HasName("fk_mtd_cpq_proposal_one_of_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.MtdCpqProposalId)
                    .HasColumnName("mtd_cpq_proposal_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CId)
                    .IsRequired()
                    .HasColumnName("cid")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.MtdCpqProposal)
                    .WithMany(p => p.MtdCpqProposalOneOf)
                    .HasForeignKey(d => d.MtdCpqProposalId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_mtd_cpq_proposal_one_of");
            });

            modelBuilder.Entity<MtdCpqProposalItem>(entity =>
            {
                entity.ToTable("mtd_cpq_proposal_item");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.MtdCpqProductId)
                    .HasName("fk_mtd_cpq_rule_idx");

                entity.HasIndex(e => e.MtdCpqProposalId)
                    .HasName("fk_mtd_cpq_complectation_mtd_cpq_proposal1_idx");

                entity.HasIndex(e => e.ProposalCatalogId)
                    .HasName("fk_mtd_cpq_proposalitems_catalog_idx");

                entity.HasIndex(e => e.MtdCpqProposalOneOfId)
                    .HasName("fk_mtd_cpq_proposalitems_one_of_idx");

                entity.HasIndex(e => e.Selected).HasName("IX_SELECTED");
                entity.HasIndex(e => e.Required).HasName("IX_REQUIRED");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.MtdCpqProductId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_product_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.MtdCpqProposalId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_proposal_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("text");

                entity.Property(e => e.Datasheet)
                    .HasColumnName("datasheet")
                    .HasColumnType("text");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("mediumblob");


                entity.Property(e => e.Qty)
                    .IsRequired()
                    .HasColumnName("qty")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(20,2)");

                entity.Property(e => e.Selected)
                    .HasColumnName("selected")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ProposalCatalogId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_proposal_catalog_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Required)
                    .HasColumnName("required")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sequence)
                    .IsRequired()
                    .HasColumnName("sequence")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MtdCpqProposalOneOfId)
                    .HasColumnName("mtd_cpq_proposal_one_of_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.AnchorNotice)
                    .HasColumnName("anchor_notice")
                    .HasColumnType("text");

                entity.Property(e => e.AnchorHistory)
                    .IsRequired()
                    .HasColumnName("anchor_history")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");
                
                entity.Property(e => e.Forbidden)
                    .IsRequired()
                    .HasColumnName("forbidden")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.MtdCpqProposal)
                    .WithMany(p => p.MtdCpqProposalItem)
                    .HasForeignKey(d => d.MtdCpqProposalId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_mtd_cpq_complectation_mtd_cpq_proposal1");

                entity.HasOne(d => d.MtdCpqProposalCatalog)
                    .WithMany(p => p.MtdCpqProposalItem)
                    .HasForeignKey(d => d.ProposalCatalogId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_mtd_cpq_proposalitems_catalog");

                entity.HasOne(d => d.MtdCpqProposalOneOf)
                    .WithMany(p => p.MtdCpqProposalItem)
                    .HasForeignKey(d => d.MtdCpqProposalOneOfId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_mtd_cpq_proposalitems_one_of");
            });

            modelBuilder.Entity<MtdCpqRuleAnchor>(entity =>
            {
                entity.ToTable("mtd_cpq_rule_anchor");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProductAnchor)
                    .HasName("fk_anchor_product_anchor_idx");

                entity.HasIndex(e => e.ProductMaster)
                    .HasName("fk_anchor_product_master_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Notice)
                    .HasColumnName("notice")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProductAnchor)
                    .IsRequired()
                    .HasColumnName("product_anchor")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProductMaster)
                    .HasColumnName("product_master")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.ProductAnchorNavigation)
                    .WithMany(p => p.MtdCpqRuleAnchorProductAnchorNavigation)
                    .HasForeignKey(d => d.ProductAnchor)
                    .HasConstraintName("fk_anchor_product_anchor");

                entity.HasOne(d => d.ProductMasterNavigation)
                    .WithMany(p => p.MtdCpqRuleAnchorProductMasterNavigation)
                    .HasForeignKey(d => d.ProductMaster)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_anchor_product_master");
            });

            modelBuilder.Entity<MtdCpqRuleAnchorBind>(entity =>
            {
                entity.ToTable("mtd_cpq_rule_anchor_bind");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.MtdCpqProductId)
                    .HasName("fk_anchor_required_idx");

                entity.HasIndex(e => e.MtdCpqRuleAnchorId)
                    .HasName("fk_anchor_required_anchor_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MtdCpqProductId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_product_id")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MtdCpqRuleAnchorId)
                    .IsRequired()
                    .HasColumnName("mtd_cpq_rule_anchor_id")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Include)
                    .IsRequired()
                    .HasColumnName("include")
                    .HasColumnType("tinyint(4)");                    

                entity.Property(e => e.Required)
                    .IsRequired()
                    .HasColumnName("required")
                    .HasColumnType("tinyint(4)");      

                entity.HasOne(d => d.MtdCpqProduct)
                    .WithMany(p => p.MtdCpqRuleAnchorBind)
                    .HasForeignKey(d => d.MtdCpqProductId)
                    .HasConstraintName("fk_anchor_required_product");

                entity.HasOne(d => d.MtdCpqRuleAnchor)
                    .WithMany(p => p.MtdCpqRuleAnchorBind)
                    .HasForeignKey(d => d.MtdCpqRuleAnchorId)
                    .HasConstraintName("fk_anchor_required_anchor");
            });

            modelBuilder.Entity<MtdCpqRuleAvailable>(entity =>
            {
                entity.ToTable("mtd_cpq_rule_available");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProductIdChild)
                    .HasName("fk_mtd_cpq_rule_available_mtd_cpq_product1_idx");

                entity.HasIndex(e => e.ProductIdParent)
                    .HasName("fk_mtd_cpq_available_mtd_cpq_product1_idx");

                entity.HasIndex(e => e.OneOfId)
                    .HasName("fk_mtd_cpq_rule_availablel_item_one_of_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ProductIdChild)
                    .IsRequired()
                    .HasColumnName("product_id_child")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ProductIdParent)
                    .IsRequired()
                    .HasColumnName("product_id_parent")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Required)
                    .HasColumnName("required")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OneOfId)
                    .HasColumnName("one_of_id")
                    .HasColumnType("varchar(36)");

                entity.HasOne(d => d.ProductIdChildNavigation)
                    .WithMany(p => p.MtdCpqRuleAvailableProductIdChildNavigation)
                    .HasForeignKey(d => d.ProductIdChild)
                    .HasConstraintName("fk_mtd_cpq_rule_available_mtd_cpq_product1");

                entity.HasOne(d => d.ProductIdParentNavigation)
                    .WithMany(p => p.MtdCpqRuleAvailableProductIdParentNavigation)
                    .HasForeignKey(d => d.ProductIdParent)
                    .HasConstraintName("fk_mtd_cpq_available_mtd_cpq_product1");

                entity.HasOne(d => d.MtdCpqOneOf)
                    .WithMany(p => p.MtdCpqRuleAvailable)
                    .HasForeignKey(d => d.OneOfId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_mtd_cpq_rule_available_item_one_of");
            });

            modelBuilder.Entity<MtdCpqTitles>(entity =>
            {
                entity.ToTable("mtd_cpq_titles");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ContactEmail)
                    .HasColumnName("contact_email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ContactName)
                    .HasColumnName("contact_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasColumnName("contact_phone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.LogoWidth)
                    .IsRequired()
                    .HasColumnName("logo_width")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LogoHeight)
                    .IsRequired()
                    .HasColumnName("logo_height")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LogoFlexible)
                    .IsRequired()
                    .HasColumnName("logo_flexible")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PreparedBy)
                    .IsRequired()
                    .HasColumnName("prepared_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .HasColumnType("varchar(250)");

            });

            modelBuilder.Entity<MtdCpqConfig>(entity =>
            {
                entity.ToTable("mtd_cpq_config");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");
                
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("longtext");

                entity.Property(e => e.ValueType)
                    .HasColumnName("value_type")
                    .HasColumnType("varchar(45)");

            });

            modelBuilder.Entity<MtdCpqConfigFile>(entity =>
            {
                entity.ToTable("mtd_cpq_config_file");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("file_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FileSize)
                    .IsRequired()
                    .HasColumnName("file_size")
                    .HasColumnType("bigint");

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasColumnName("file_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FileData)
                    .IsRequired()
                    .HasColumnName("file_data")
                    .HasColumnType("mediumblob");

            });

            modelBuilder.Entity<MtdCpqNotification>(entity =>
            {
                entity.ToTable("mtd_cpq_notification");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasColumnType("longtext");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TimeCr)
                    .IsRequired()
                    .HasColumnName("timecr")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MtdCpqReaderUser>(entity =>
            {
                entity.ToTable("mtd_cpq_reader_user");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("idx-username");

                entity.HasIndex(e => e.MessageId)
                    .HasName("fk_notification_user_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.MessageId)
                    .IsRequired()
                    .HasColumnName("message_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TimeCr)
                    .IsRequired()
                    .HasColumnName("timecr")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.MtdCpqNotification)
                    .WithMany(p => p.MtdCpqReaderUsers)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_notification_user");

            });
        }
    }
}
