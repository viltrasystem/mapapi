using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ViltrapportenApi.Data.SystemModels;

public partial class ViltrapportenSystemContext : DbContext
{
    public ViltrapportenSystemContext()
    {
    }

    public ViltrapportenSystemContext(DbContextOptions<ViltrapportenSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnimalAction> AnimalActions { get; set; }

    public virtual DbSet<AnimalActionAnimalDetail> AnimalActionAnimalDetails { get; set; }

    public virtual DbSet<AnimalActionDetail> AnimalActionDetails { get; set; }

    public virtual DbSet<AnimalDetailsWeightChangedLog> AnimalDetailsWeightChangedLogs { get; set; }

    public virtual DbSet<AnimalHasAntler> AnimalHasAntlers { get; set; }

    public virtual DbSet<AnimalHasHuntingMethod> AnimalHasHuntingMethods { get; set; }

    public virtual DbSet<AnimalHasWeightMethod> AnimalHasWeightMethods { get; set; }

    public virtual DbSet<AnimalMappedToShotOb> AnimalMappedToShotObs { get; set; }

    public virtual DbSet<AnimalSubType> AnimalSubTypes { get; set; }

    public virtual DbSet<AnimalType> AnimalTypes { get; set; }

    public virtual DbSet<AnimalTypeHasHuntingSeason> AnimalTypeHasHuntingSeasons { get; set; }

    public virtual DbSet<ChartingGroup> ChartingGroups { get; set; }

    public virtual DbSet<CommonQuotaCount> CommonQuotaCounts { get; set; }

    public virtual DbSet<CommonQuotaGroup> CommonQuotaGroups { get; set; }

    public virtual DbSet<CommonQuotaGroupHasUnit> CommonQuotaGroupHasUnits { get; set; }

    public virtual DbSet<CommonQuotum> CommonQuota { get; set; }

    public virtual DbSet<CompleteHunting> CompleteHuntings { get; set; }

    public virtual DbSet<DistributionContactList> DistributionContactLists { get; set; }

    public virtual DbSet<DistributionContactState> DistributionContactStates { get; set; }

    public virtual DbSet<DistributionFilterList> DistributionFilterLists { get; set; }

    public virtual DbSet<DistributionFilterUnit> DistributionFilterUnits { get; set; }

    public virtual DbSet<DistributionList> DistributionLists { get; set; }

    public virtual DbSet<DistributionSmsLog> DistributionSmsLogs { get; set; }

    public virtual DbSet<FailedNewsLetter> FailedNewsLetters { get; set; }

    public virtual DbSet<FormFieldAvailability> FormFieldAvailabilities { get; set; }

    public virtual DbSet<GameHasAnimal> GameHasAnimals { get; set; }

    public virtual DbSet<HuntersAvailableForSeason> HuntersAvailableForSeasons { get; set; }

    public virtual DbSet<HuntingDocSigned> HuntingDocSigneds { get; set; }

    public virtual DbSet<HuntingDocSignedReminder> HuntingDocSignedReminders { get; set; }

    public virtual DbSet<HuntingDogRegister> HuntingDogRegisters { get; set; }

    public virtual DbSet<HuntingGame> HuntingGames { get; set; }

    public virtual DbSet<HuntingReminder> HuntingReminders { get; set; }

    public virtual DbSet<HuntingReminderType> HuntingReminderTypes { get; set; }

    public virtual DbSet<HuntingTeamStayInfo> HuntingTeamStayInfos { get; set; }

    public virtual DbSet<IllogicalReportNotification> IllogicalReportNotifications { get; set; }

    public virtual DbSet<IllogicalReportNotifiedUser> IllogicalReportNotifiedUsers { get; set; }

    public virtual DbSet<InfoType> InfoTypes { get; set; }

    public virtual DbSet<InfoUnit> InfoUnits { get; set; }

    public virtual DbSet<InfoUser> InfoUsers { get; set; }

    public virtual DbSet<Land> Lands { get; set; }

    public virtual DbSet<LandOwner> LandOwners { get; set; }

    public virtual DbSet<LandOwnerDetail> LandOwnerDetails { get; set; }

    public virtual DbSet<LandType> LandTypes { get; set; }

    public virtual DbSet<LandUnit> LandUnits { get; set; }

    public virtual DbSet<LandownershipType> LandownershipTypes { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MailBoxFolder> MailBoxFolders { get; set; }

    public virtual DbSet<MailBoxMessage> MailBoxMessages { get; set; }

    public virtual DbSet<MailBoxUsersMail> MailBoxUsersMails { get; set; }

    public virtual DbSet<MobilePasswordReset> MobilePasswordResets { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsType> NewsTypes { get; set; }

    public virtual DbSet<NotificationMonitor> NotificationMonitors { get; set; }

    public virtual DbSet<PriceAccountRegister> PriceAccountRegisters { get; set; }

    public virtual DbSet<PriceCompanyRegister> PriceCompanyRegisters { get; set; }

    public virtual DbSet<PriceExtraFee> PriceExtraFees { get; set; }

    public virtual DbSet<PricePayment> PricePayments { get; set; }

    public virtual DbSet<PricePerQuotum> PricePerQuota { get; set; }

    public virtual DbSet<PricePerShotKilo> PricePerShotKilos { get; set; }

    public virtual DbSet<PriceRegister> PriceRegisters { get; set; }

    public virtual DbSet<PriceTeamExtraFee> PriceTeamExtraFees { get; set; }

    public virtual DbSet<PriceUnit> PriceUnits { get; set; }

    public virtual DbSet<PriceWeightRange> PriceWeightRanges { get; set; }

    public virtual DbSet<PublicRegistryAuthentication> PublicRegistryAuthentications { get; set; }

    public virtual DbSet<PublicRegistryErrorLog> PublicRegistryErrorLogs { get; set; }

    public virtual DbSet<PublicRegistryHunterValidationAuthentication> PublicRegistryHunterValidationAuthentications { get; set; }

    public virtual DbSet<QuotaAnimalType> QuotaAnimalTypes { get; set; }

    public virtual DbSet<QuotaAnimalTypeUnit> QuotaAnimalTypeUnits { get; set; }

    public virtual DbSet<QuotaCount> QuotaCounts { get; set; }

    public virtual DbSet<QuotaTypeMappedToNormalType> QuotaTypeMappedToNormalTypes { get; set; }

    public virtual DbSet<QuotaTypesMappedToAntlersType> QuotaTypesMappedToAntlersTypes { get; set; }

    public virtual DbSet<QuotaTypesMappedToTag> QuotaTypesMappedToTags { get; set; }

    public virtual DbSet<QuotaTypesMappedToWeight> QuotaTypesMappedToWeights { get; set; }

    public virtual DbSet<Quotum> Quota { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<ReportExport> ReportExports { get; set; }

    public virtual DbSet<ReportImage> ReportImages { get; set; }

    public virtual DbSet<ReportImageDownloadLog> ReportImageDownloadLogs { get; set; }

    public virtual DbSet<Smslog> Smslogs { get; set; }

    public virtual DbSet<Smsresponse> Smsresponses { get; set; }

    public virtual DbSet<StatisticsExcelImportHuntingMethodDetail> StatisticsExcelImportHuntingMethodDetails { get; set; }

    public virtual DbSet<StatisticsExcelImportMaster> StatisticsExcelImportMasters { get; set; }

    public virtual DbSet<StatisticsExcelImportSubTypeDetail> StatisticsExcelImportSubTypeDetails { get; set; }

    public virtual DbSet<TeamHasDogRegisterAnimal> TeamHasDogRegisterAnimals { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitAnimalSeasonText> UnitAnimalSeasonTexts { get; set; }

    public virtual DbSet<UnitAnimalsWeightType> UnitAnimalsWeightTypes { get; set; }

    public virtual DbSet<UnitAvailabilityForSeason> UnitAvailabilityForSeasons { get; set; }

    public virtual DbSet<UnitGameAnimalReprortAccess> UnitGameAnimalReprortAccesses { get; set; }

    public virtual DbSet<UnitGroup> UnitGroups { get; set; }

    public virtual DbSet<UnitGroupUnit> UnitGroupUnits { get; set; }

    public virtual DbSet<UnitHasGame> UnitHasGames { get; set; }

    public virtual DbSet<UnitHasUser> UnitHasUsers { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    public virtual DbSet<UnitsInfomation> UnitsInfomations { get; set; }

    public virtual DbSet<UsersLandUnitWithoutLand> UsersLandUnitWithoutLands { get; set; }

    public virtual DbSet<VatRegistration> VatRegistrations { get; set; }

    public virtual DbSet<ViltraUser> ViltraUsers { get; set; }

    public virtual DbSet<WeightType> WeightTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ViltrapportenSystemConnection", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalAction>(entity =>
        {
            entity.HasKey(e => e.ActionId);

            entity.ToTable("AnimalAction");

            entity.HasIndex(e => new { e.IsShot, e.AnimalTypeId, e.IsActive, e.GameId, e.ActionDate }, "IX_AnimalAction_IsShot_AnimalTypeId_IsActive_GameID_ActionDate");

            entity.HasIndex(e => new { e.IsShot, e.AnimalTypeId, e.IsActive, e.GameId, e.ActionDate }, "IX_AnimalAction_IsShot_AnimalTypeId_IsActive_GameID_ActionDate_Includes_HuntingTeamID_NoOfHuntersWithGun_NoOfHoursSpent");

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_AnimalAction_AnimalType_AnimalTypeId");

            entity.HasIndex(e => e.GameId, "IX_FK_AnimalAction_HuntingGame_GameId");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.ActionTime)
                .HasMaxLength(10)
                .HasDefaultValue("");
            entity.Property(e => e.Comments).HasColumnType("text");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.HunterId).HasColumnName("HunterID");
            entity.Property(e => e.HunterNameOptional).HasMaxLength(100);
            entity.Property(e => e.HuntingTeamId).HasColumnName("HuntingTeamID");
            entity.Property(e => e.OldId).HasColumnName("OldID");
            entity.Property(e => e.ReportedNumber).HasMaxLength(50);
            entity.Property(e => e.ReporterDnnId).HasColumnName("ReporterDnnID");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.AnimalActions)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.AnimalActions)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalAction_HuntingGame_GameId");

            entity.HasOne(d => d.HuntingTeam).WithMany(p => p.AnimalActions)
                .HasForeignKey(d => d.HuntingTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalAction_Units_HuntingTeamId");
        });

        modelBuilder.Entity<AnimalActionAnimalDetail>(entity =>
        {
            entity.HasKey(e => e.AnimalDetailsId);

            entity.ToTable(tb => tb.HasTrigger("AnimalDetailsWeightChangedLogTrigger"));

            entity.HasIndex(e => e.ActionId, "IX_FK_AnimalActionAnimalDetails_AnimalAction_ActionId");

            entity.HasIndex(e => e.AnimalSubTypeId, "IX_FK_AnimalActionAnimalDetails_AnimalSubType_AnimalSubTypeID");

            entity.Property(e => e.AnimalDetailsId).HasColumnName("AnimalDetailsID");
            entity.Property(e => e.ApprovedorNotApproved).HasMaxLength(50);
            entity.Property(e => e.DateWeighted).HasColumnType("datetime");
            entity.Property(e => e.HairLoss).HasMaxLength(50);
            entity.Property(e => e.Hjortelusflue).HasMaxLength(50);
            entity.Property(e => e.HpComment).HasColumnName("HP_Comment");
            entity.Property(e => e.HpCommentDate)
                .HasColumnType("datetime")
                .HasColumnName("HP_CommentDate");
            entity.Property(e => e.HuntingPoliceUserId).HasColumnName("HuntingPoliceUserID");
            entity.Property(e => e.Tags).HasMaxLength(50);
            entity.Property(e => e.Ticks).HasMaxLength(50);
            entity.Property(e => e.VetTestId).HasMaxLength(12);

            entity.HasOne(d => d.Action).WithMany(p => p.AnimalActionAnimalDetails)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.AnimalSubType).WithMany(p => p.AnimalActionAnimalDetails)
                .HasForeignKey(d => d.AnimalSubTypeId)
                .HasConstraintName("FK_AnimalActionAnimalDetails_AnimalSubType_AnimalSubTypeID");
        });

        modelBuilder.Entity<AnimalActionDetail>(entity =>
        {
            entity.ToTable("AnimalActionDetail");

            entity.HasIndex(e => e.AnimalActionId, "IX_AnimalActionDetail_AnimalActionID");

            entity.HasIndex(e => e.AnimalSubTypeId, "IX_FK_AnimalActionDetail_AnimalSubType");

            entity.Property(e => e.AnimalActionDetailId).HasColumnName("AnimalActionDetailID");
            entity.Property(e => e.AnimalActionId).HasColumnName("AnimalActionID");
            entity.Property(e => e.AnimalSubTypeId).HasColumnName("AnimalSubTypeID");

            entity.HasOne(d => d.AnimalAction).WithMany(p => p.AnimalActionDetails)
                .HasForeignKey(d => d.AnimalActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalActionDetail_AnimalAction_ActionId");

            entity.HasOne(d => d.AnimalSubType).WithMany(p => p.AnimalActionDetails)
                .HasForeignKey(d => d.AnimalSubTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalActionDetail_AnimalSubType");
        });

        modelBuilder.Entity<AnimalDetailsWeightChangedLog>(entity =>
        {
            entity.ToTable("AnimalDetailsWeightChangedLog");

            entity.HasIndex(e => e.AnimalDetailsId, "IX_FK_AnimalDetailsWeightChangedLog_AnimalActionAnimalDetails_AnimalDetailsID");

            entity.HasIndex(e => e.ActionId, "IX_FK_AnimalDetailsWeightChangedLog_AnimalAction_ActionId");

            entity.HasIndex(e => e.AnimalSubTypeId, "IX_FK_AnimalDetailsWeightChangedLog_AnimalSubType_AnimalSubTypeID");

            entity.Property(e => e.AnimalDetailsWeightChangedLogId).HasColumnName("AnimalDetailsWeightChangedLogID");
            entity.Property(e => e.AnimalDetailsId).HasColumnName("AnimalDetailsID");
            entity.Property(e => e.DateWeighted).HasColumnType("datetime");

            entity.HasOne(d => d.Action).WithMany(p => p.AnimalDetailsWeightChangedLogs)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.AnimalDetails).WithMany(p => p.AnimalDetailsWeightChangedLogs).HasForeignKey(d => d.AnimalDetailsId);

            entity.HasOne(d => d.AnimalSubType).WithMany(p => p.AnimalDetailsWeightChangedLogs)
                .HasForeignKey(d => d.AnimalSubTypeId)
                .HasConstraintName("FK_AnimalDetailsWeightChangedLog_AnimalSubType_AnimalSubTypeID");
        });

        modelBuilder.Entity<AnimalHasAntler>(entity =>
        {
            entity.HasKey(e => e.AntlersId);

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_AnimalHasAntlers_AnimalType_AnimalTypeID");

            entity.Property(e => e.AntlersId).HasColumnName("AntlersID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.AntlersType).HasMaxLength(50);
            entity.Property(e => e.SmsCode).HasMaxLength(10);

            entity.HasOne(d => d.AnimalType).WithMany(p => p.AnimalHasAntlers)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AnimalHasHuntingMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId);

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_AnimalHasHuntingMethods_AnimalType_AnimalTypeID");

            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.MethodRef).HasMaxLength(50);
            entity.Property(e => e.MethodType).HasMaxLength(50);

            entity.HasOne(d => d.AnimalType).WithMany(p => p.AnimalHasHuntingMethods)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AnimalHasWeightMethod>(entity =>
        {
            entity.HasKey(e => e.WeightMethodId);

            entity.Property(e => e.WeightMethodId).HasColumnName("WeightMethodID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.WeightMethod).HasMaxLength(50);
            entity.Property(e => e.WeightMethodRef).HasMaxLength(50);

            entity.HasOne(d => d.AnimalType).WithMany(p => p.AnimalHasWeightMethods)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalHasWeightMethods_AnimalTypeID_AnimalType_AnimalTypeID");
        });

        modelBuilder.Entity<AnimalMappedToShotOb>(entity =>
        {
            entity.HasKey(e => e.AnimalMappedToShotObsId);

            entity.Property(e => e.AnimalMappedToShotObsId).HasColumnName("AnimalMappedToShotObsID");
            entity.Property(e => e.ObsSubtypeId).HasColumnName("ObsSubtypeID");
            entity.Property(e => e.ShotSubtypeId).HasColumnName("ShotSubtypeID");

            entity.HasOne(d => d.ObsSubtype).WithMany(p => p.AnimalMappedToShotObObsSubtypes)
                .HasForeignKey(d => d.ObsSubtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalMappedToShotObs_ObsSubTypeID_AnimalSubType_AnimalSubtypeID");

            entity.HasOne(d => d.ShotSubtype).WithMany(p => p.AnimalMappedToShotObShotSubtypes)
                .HasForeignKey(d => d.ShotSubtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalMappedToShotObs_ShotSubTypeID_AnimalSubType_AnimalSubtypeID");
        });

        modelBuilder.Entity<AnimalSubType>(entity =>
        {
            entity.ToTable("AnimalSubType");

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_AnimalSubType_AnimalType_AnimalTypeID");

            entity.Property(e => e.AnimalSubTypeId).HasColumnName("AnimalSubTypeID");
            entity.Property(e => e.AnimalSubType1)
                .HasMaxLength(50)
                .HasColumnName("AnimalSubType");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Smscode)
                .HasMaxLength(50)
                .HasColumnName("SMSCode");
            entity.Property(e => e.Smssequence).HasColumnName("SMSSequence");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.AnimalSubTypes)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AnimalType>(entity =>
        {
            entity.ToTable("AnimalType");

            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.AnimalType1)
                .HasMaxLength(50)
                .HasColumnName("AnimalType");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.SmsCodes).HasMaxLength(500);
        });

        modelBuilder.Entity<AnimalTypeHasHuntingSeason>(entity =>
        {
            entity.HasKey(e => e.SeasonId);

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_AnimalTypeHasHuntingSeasons_AnimalType_AnimalTypeID");

            entity.Property(e => e.SeasonId).HasColumnName("SeasonID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.EndDate).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasMaxLength(50);

            entity.HasOne(d => d.AnimalType).WithMany(p => p.AnimalTypeHasHuntingSeasons)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ChartingGroup>(entity =>
        {
            entity.ToTable("ChartingGroup");

            entity.Property(e => e.GroupName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Value).HasMaxLength(500);
        });

        modelBuilder.Entity<CommonQuotaCount>(entity =>
        {
            entity.ToTable("CommonQuotaCount");

            entity.HasIndex(e => e.CommonQuotaId, "IX_FK_CommonQuotaCount_CommonQuota_CommonQuotaId");

            entity.HasIndex(e => e.QuotaTypeId, "IX_FK_CommonQuotaCount_QuotaAnimalTypes_QuotaTypeId");

            entity.Property(e => e.CommonQuotaCount1).HasColumnName("CommonQuotaCount");

            entity.HasOne(d => d.CommonQuota).WithMany(p => p.CommonQuotaCounts)
                .HasForeignKey(d => d.CommonQuotaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.QuotaType).WithMany(p => p.CommonQuotaCounts)
                .HasForeignKey(d => d.QuotaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CommonQuotaGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId);

            entity.ToTable("CommonQuotaGroup");

            entity.HasIndex(e => e.OwnerUnitId, "IX_FK_CommonQuotaGroup_Units_UnitId");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(50);

            entity.HasOne(d => d.OwnerUnit).WithMany(p => p.CommonQuotaGroups)
                .HasForeignKey(d => d.OwnerUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommonQuotaGroup_Units_UnitId");
        });

        modelBuilder.Entity<CommonQuotaGroupHasUnit>(entity =>
        {
            entity.HasKey(e => e.GroupHasUnitId);

            entity.ToTable("CommonQuotaGroupHasUnit");

            entity.HasIndex(e => e.GroupId, "IX_FK_CommonQuotaGroupHasUnit_CommonQuotaGroup_GroupId");

            entity.HasIndex(e => e.UnitId, "IX_FK_CommonQuotaGroupHasUnit_Units_UnitId");

            entity.HasOne(d => d.Group).WithMany(p => p.CommonQuotaGroupHasUnits)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.CommonQuotaGroupHasUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CommonQuotum>(entity =>
        {
            entity.HasKey(e => e.CommonQuotaId);

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_CommonQuota_AnimalType_AnimalTypeId");

            entity.HasIndex(e => e.GroupId, "IX_FK_CommonQuota_CommonQuotaGroup_GroupId");

            entity.HasIndex(e => e.GameId, "IX_FK_CommonQuota_HuntingGame_GameId");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.CommonQuota)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.CommonQuota)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Group).WithMany(p => p.CommonQuota)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CompleteHunting>(entity =>
        {
            entity.ToTable("CompleteHunting");

            entity.HasIndex(e => e.UnitId, "IX_FK_CompleteHunting_Units_UnitId");

            entity.HasOne(d => d.Unit).WithMany(p => p.CompleteHuntings)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DistributionContactList>(entity =>
        {
            entity.ToTable("DistributionContactList");

            entity.HasIndex(e => e.DistributionListId, "IX_FK_DistributionContactList_DistributionList_ListID");

            entity.Property(e => e.DistributionContactListId).HasColumnName("DistributionContactListID");
            entity.Property(e => e.ContactName).HasMaxLength(100);
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.DistributionListId).HasColumnName("DistributionListID");

            entity.HasOne(d => d.DistributionList).WithMany(p => p.DistributionContactLists)
                .HasForeignKey(d => d.DistributionListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DistributionContactList_DistributionList_ListID");
        });

        modelBuilder.Entity<DistributionContactState>(entity =>
        {
            entity.ToTable("DistributionContactState");

            entity.Property(e => e.DistributionContactStateId).HasColumnName("DistributionContactStateID");
            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.KeyWord).HasMaxLength(15);
        });

        modelBuilder.Entity<DistributionFilterList>(entity =>
        {
            entity.ToTable("DistributionFilterList");

            entity.HasIndex(e => e.DistributionListId, "IX_FK_DistributionFilterList_DistributionListID_DistributionList_ListID");

            entity.Property(e => e.DistributionFilterListId).HasColumnName("DistributionFilterListID");
            entity.Property(e => e.DistributionListId).HasColumnName("DistributionListID");

            entity.HasOne(d => d.DistributionList).WithMany(p => p.DistributionFilterLists)
                .HasForeignKey(d => d.DistributionListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DistributionFilterList_DistributionListID_DistributionList_ListID");
        });

        modelBuilder.Entity<DistributionFilterUnit>(entity =>
        {
            entity.HasKey(e => e.DistributionUnitId);

            entity.ToTable("DistributionFilterUnit");

            entity.HasIndex(e => e.DistributionId, "IX_FK_DistributionFilterUnit_DistributionID_DistributionList_ListID");

            entity.Property(e => e.DistributionUnitId).HasColumnName("DistributionUnitID");
            entity.Property(e => e.DistributionId).HasColumnName("DistributionID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Distribution).WithMany(p => p.DistributionFilterUnits)
                .HasForeignKey(d => d.DistributionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DistributionFilterUnit_DistributionID_DistributionList_ListID");
        });

        modelBuilder.Entity<DistributionList>(entity =>
        {
            entity.HasKey(e => e.ListId);

            entity.ToTable("DistributionList");

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_DistributionList_AnimalType_AnimalTypeID");

            entity.HasIndex(e => e.GameId, "IX_FK_DistributionList_HuntingGame_GameID");

            entity.HasIndex(e => e.UnitId, "IX_FK_DistributionList_Units_UnitID");

            entity.Property(e => e.ListId).HasColumnName("ListID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.IsEnable).HasDefaultValue(true);
            entity.Property(e => e.ListName).HasMaxLength(100);
            entity.Property(e => e.Smscategory)
                .HasMaxLength(50)
                .HasColumnName("SMSCategory");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.DistributionLists)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.DistributionLists)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.DistributionLists)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DistributionSmsLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("DistributionSmsLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.HuntingTeamId).HasColumnName("HuntingTeamID");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.RecieverNumber)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Smsdate).HasColumnName("SMSDate");
        });

        modelBuilder.Entity<FailedNewsLetter>(entity =>
        {
            entity.HasKey(e => e.FailedId).HasName("PK_FailedNewsLetters");

            entity.ToTable("FailedNewsLetter");

            entity.HasIndex(e => e.InfoId, "IX_FK_FailedNewsLetter_InfoID_UnitsInfomation_InfoID");

            entity.Property(e => e.FailedId).HasColumnName("FailedID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FailedDateTime).HasColumnType("datetime");
            entity.Property(e => e.InfoId).HasColumnName("InfoID");
            entity.Property(e => e.Language).HasMaxLength(10);
            entity.Property(e => e.ModifiedMailBody).HasColumnType("ntext");
            entity.Property(e => e.SentDateTime).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasColumnType("ntext");

            entity.HasOne(d => d.Info).WithMany(p => p.FailedNewsLetters)
                .HasForeignKey(d => d.InfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FailedNewsLetter_InfoID_UnitsInfomation_InfoID");
        });

        modelBuilder.Entity<FormFieldAvailability>(entity =>
        {
            entity.ToTable("FormFieldAvailability");

            entity.HasIndex(e => e.GameId, "IX_FK_FormFieldAvailability_HuntingGame_GameId");

            entity.HasOne(d => d.Game).WithMany(p => p.FormFieldAvailabilities)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<GameHasAnimal>(entity =>
        {
            entity.ToTable("GameHasAnimal");

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_GameHasAnimal_AnimalType_AnimalTypeID");

            entity.HasIndex(e => e.GameId, "IX_FK_GameHasAnimal_HuntingGame_GameID");

            entity.Property(e => e.GameHasAnimalId).HasColumnName("GameHasAnimalID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.GameId).HasColumnName("GameID");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.GameHasAnimals)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.GameHasAnimals)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<HuntersAvailableForSeason>(entity =>
        {
            entity.HasKey(e => e.HunterSeasonId);

            entity.ToTable("HuntersAvailableForSeason");

            entity.Property(e => e.HunterSeasonId).HasColumnName("HunterSeasonID");
            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EditedDate).HasColumnType("datetime");
            entity.Property(e => e.HunterDnnId).HasColumnName("HunterDnnID");
            entity.Property(e => e.HunterFirstName).HasMaxLength(50);
            entity.Property(e => e.HunterId).HasColumnName("HunterID");
            entity.Property(e => e.HunterLastName).HasMaxLength(50);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Team).WithMany(p => p.HuntersAvailableForSeasons)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HuntersAvailableForSeason__TeamID_Units_UnitID");
        });

        modelBuilder.Entity<HuntingDocSigned>(entity =>
        {
            entity.HasKey(e => e.DocumentSignedId);

            entity.ToTable("HuntingDocSigned");

            entity.Property(e => e.DocumentSignedId).HasColumnName("DocumentSignedID");
            entity.Property(e => e.InfoId).HasColumnName("InfoID");
            entity.Property(e => e.SignedDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Info).WithMany(p => p.HuntingDocSigneds)
                .HasForeignKey(d => d.InfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HuntingDocSigned_InfoID_UnitsInfomation_InfoID");

            entity.HasOne(d => d.Unit).WithMany(p => p.HuntingDocSigneds)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HuntingDocSigned_UnitID_Units_UnitID");
        });

        modelBuilder.Entity<HuntingDocSignedReminder>(entity =>
        {
            entity.HasKey(e => e.DocSignedReminderId);

            entity.ToTable("HuntingDocSignedReminder");

            entity.Property(e => e.DocSignedReminderId).HasColumnName("DocSignedReminderID");
            entity.Property(e => e.DnnUserId).HasColumnName("DnnUserID");
            entity.Property(e => e.InfoId).HasColumnName("InfoID");
            entity.Property(e => e.ReminderSendDate).HasColumnType("datetime");
            entity.Property(e => e.ReminderUnitId).HasColumnName("ReminderUnitID");
        });

        modelBuilder.Entity<HuntingDogRegister>(entity =>
        {
            entity.HasKey(e => e.HuntingDogRegId);

            entity.ToTable("HuntingDogRegister");

            entity.Property(e => e.HuntingDogRegId).HasColumnName("HuntingDogRegID");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DogId)
                .HasMaxLength(20)
                .HasColumnName("DogID");
            entity.Property(e => e.EditedDate).HasColumnType("datetime");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
        });

        modelBuilder.Entity<HuntingGame>(entity =>
        {
            entity.HasKey(e => e.GameId);

            entity.ToTable("HuntingGame");

            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeActivatedOn).HasColumnType("datetime");
            entity.Property(e => e.GameInfo).HasColumnType("text");
            entity.Property(e => e.GameName).HasMaxLength(50);
            entity.Property(e => e.GameRef).HasMaxLength(50);
            entity.Property(e => e.IsAvailableOnSmsdistribution).HasColumnName("IsAvailableOnSMSDistribution");
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.ObsSmsCode).HasMaxLength(25);
            entity.Property(e => e.ShotSmsCode).HasMaxLength(25);
            entity.Property(e => e.SmsCodes).HasMaxLength(500);
        });

        modelBuilder.Entity<HuntingReminder>(entity =>
        {
            entity.ToTable("HuntingReminder");

            entity.Property(e => e.HuntingReminderId).HasColumnName("HuntingReminderID");
            entity.Property(e => e.SendDateTime).HasColumnType("datetime");
            entity.Property(e => e.SenderDnnId).HasColumnName("SenderDnnID");
            entity.Property(e => e.SenderUnitId).HasColumnName("SenderUnitID");
        });

        modelBuilder.Entity<HuntingReminderType>(entity =>
        {
            entity.ToTable("HuntingReminderType");

            entity.Property(e => e.HuntingReminderTypeId).HasColumnName("HuntingReminderTypeID");
            entity.Property(e => e.HunterDnnId).HasColumnName("HunterDnnID");
            entity.Property(e => e.HuntingReminderId).HasColumnName("HuntingReminderID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.HuntingReminder).WithMany(p => p.HuntingReminderTypes)
                .HasForeignKey(d => d.HuntingReminderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HuntingReminderType_HuntingReminderID_HuntingReminder_HuntingReminderID");
        });

        modelBuilder.Entity<HuntingTeamStayInfo>(entity =>
        {
            entity.ToTable("HuntingTeamStayInfo");

            entity.Property(e => e.HuntingTeamStayInfoId).HasColumnName("HuntingTeamStayInfoID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EditedDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Unit).WithMany(p => p.HuntingTeamStayInfos)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HuntingTeamStayInfo__UnitID_Units_UnitID");
        });

        modelBuilder.Entity<IllogicalReportNotification>(entity =>
        {
            entity.HasKey(e => e.IllogicalNotificationId);

            entity.ToTable("IllogicalReportNotification");

            entity.Property(e => e.IllogicalNotificationId).HasColumnName("IllogicalNotificationID");
            entity.Property(e => e.NotifiedActionId).HasColumnName("NotifiedActionID");
        });

        modelBuilder.Entity<IllogicalReportNotifiedUser>(entity =>
        {
            entity.ToTable("IllogicalReportNotifiedUser");

            entity.Property(e => e.IllogicalReportNotifiedUserId).HasColumnName("IllogicalReportNotifiedUserID");
            entity.Property(e => e.DnnUserId).HasColumnName("DnnUserID");
            entity.Property(e => e.IllogicalNotificationId).HasColumnName("IllogicalNotificationID");

            entity.HasOne(d => d.IllogicalNotification).WithMany(p => p.IllogicalReportNotifiedUsers)
                .HasForeignKey(d => d.IllogicalNotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IllogicalReportNotifiedUser_IllogicalNotificationID_IllogicalNotification_IllogicalNotificationID");
        });

        modelBuilder.Entity<InfoType>(entity =>
        {
            entity.ToTable("InfoType");

            entity.Property(e => e.InfoTypeId).HasColumnName("InfoTypeID");
            entity.Property(e => e.Info).HasMaxLength(100);
        });

        modelBuilder.Entity<InfoUnit>(entity =>
        {
            entity.HasIndex(e => e.InfoId, "IX_FK_InfoUnits_InfoID_UnitsInfomation_InfoID");

            entity.Property(e => e.InfoId).HasColumnName("InfoID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Info).WithMany(p => p.InfoUnits)
                .HasForeignKey(d => d.InfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InfoUnits_InfoID_UnitsInfomation_InfoID");
        });

        modelBuilder.Entity<InfoUser>(entity =>
        {
            entity.HasIndex(e => e.InfoId, "IX_FK_InfoUsers_InfoID_UnitsInfomation_InfoID");

            entity.Property(e => e.InfoUserId).HasColumnName("InfoUserID");
            entity.Property(e => e.InfoId).HasColumnName("InfoID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Info).WithMany(p => p.InfoUsers)
                .HasForeignKey(d => d.InfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InfoUsers_InfoID_UnitsInfomation_InfoID");
        });

        modelBuilder.Entity<Land>(entity =>
        {
            entity.ToTable("Land");

            entity.HasIndex(e => e.OwnershipTypeId, "IX_FK_Land_LandownershipType_OwnershipTypeId");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.MainNo).HasMaxLength(10);
            entity.Property(e => e.Municipality).HasMaxLength(10);
            entity.Property(e => e.MunicipalityName).HasMaxLength(100);
            entity.Property(e => e.PlotNo).HasMaxLength(10);
            entity.Property(e => e.SubNo).HasMaxLength(10);

            entity.HasOne(d => d.OwnershipType).WithMany(p => p.Lands)
                .HasForeignKey(d => d.OwnershipTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LandOwner>(entity =>
        {
            entity.ToTable("LandOwner");

            entity.HasIndex(e => e.LandId, "IX_FK_LandOwner_Land_LandId");

            entity.HasIndex(e => e.SystemUserId, "IX_FK_LandOwner_ViltraUser_UserId");

            entity.Property(e => e.AssignedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Land).WithMany(p => p.LandOwners)
                .HasForeignKey(d => d.LandId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SystemUser).WithMany(p => p.LandOwners)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LandOwner_ViltraUser_UserId");
        });

        modelBuilder.Entity<LandOwnerDetail>(entity =>
        {
            entity.HasIndex(e => e.SystemUserId, "IX_FK_LandOwnerDetails_ViltraUser_UserId");

            entity.Property(e => e.AddressCity).HasMaxLength(100);
            entity.Property(e => e.AddressLine1).HasMaxLength(100);
            entity.Property(e => e.AddressLine2).HasMaxLength(100);
            entity.Property(e => e.BankAccountNo).HasMaxLength(100);

            entity.HasOne(d => d.SystemUser).WithMany(p => p.LandOwnerDetails)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LandOwnerDetails_ViltraUser_UserId");
        });

        modelBuilder.Entity<LandType>(entity =>
        {
            entity.Property(e => e.LandTypeId).HasColumnName("LandTypeID");
            entity.Property(e => e.LandTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<LandUnit>(entity =>
        {
            entity.ToTable("LandUnit");

            entity.HasIndex(e => e.LandId, "IX_FK_LandUnit_Land_LandId");

            entity.HasIndex(e => e.UnitId, "IX_FK_LandUnit_Units_UnitId");

            entity.HasOne(d => d.Land).WithMany(p => p.LandUnits)
                .HasForeignKey(d => d.LandId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.LandType).WithMany(p => p.LandUnits)
                .HasForeignKey(d => d.LandTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.LandUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LandownershipType>(entity =>
        {
            entity.HasKey(e => e.OwnershipTypeId);

            entity.ToTable("LandownershipType");

            entity.Property(e => e.OwnershipType).HasMaxLength(100);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.LogType).HasMaxLength(20);
            entity.Property(e => e.Method).HasMaxLength(1000);
            entity.Property(e => e.Page).HasMaxLength(1000);
        });

        modelBuilder.Entity<MailBoxFolder>(entity =>
        {
            entity.HasKey(e => e.FolderId);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FolderName).HasMaxLength(100);
        });

        modelBuilder.Entity<MailBoxMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId);

            entity.ToTable("MailBoxMessage");

            entity.Property(e => e.BodyHtml).HasColumnType("text");
            entity.Property(e => e.BodyPlain).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Subject).HasColumnType("text");
        });

        modelBuilder.Entity<MailBoxUsersMail>(entity =>
        {
            entity.HasKey(e => e.ReceivedMailId).HasName("PK_ReceivedMail");

            entity.ToTable("MailBoxUsersMail");

            entity.HasIndex(e => e.FolderId, "IX_FK_ReceivedMail_MailBoxFolders_FolderId");

            entity.HasIndex(e => e.MessageId, "IX_FK_ReceivedMail_MailBoxMessage_MessageId");

            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Folder).WithMany(p => p.MailBoxUsersMails)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceivedMail_MailBoxFolders_FolderId");

            entity.HasOne(d => d.Message).WithMany(p => p.MailBoxUsersMails)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceivedMail_MailBoxMessage_MessageId");
        });

        modelBuilder.Entity<MobilePasswordReset>(entity =>
        {
            entity.ToTable("MobilePasswordReset");

            entity.Property(e => e.MobilePasswordResetId).HasColumnName("MobilePasswordResetID");
            entity.Property(e => e.DnnUserId).HasColumnName("DnnUserID");
            entity.Property(e => e.ResetTimeBigin).HasColumnType("datetime");
            entity.Property(e => e.ResetTimeEnd).HasColumnType("datetime");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasIndex(e => e.NewsTypeId, "IX_FK_News_NewsType_NewsTypeId");

            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.NewsDate).HasColumnType("datetime");
            entity.Property(e => e.Summary).HasColumnType("ntext");
            entity.Property(e => e.Title).HasColumnType("ntext");

            entity.HasOne(d => d.NewsType).WithMany(p => p.News)
                .HasForeignKey(d => d.NewsTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<NewsType>(entity =>
        {
            entity.ToTable("NewsType");

            entity.Property(e => e.NewsType1)
                .HasMaxLength(100)
                .HasColumnName("NewsType");
        });

        modelBuilder.Entity<NotificationMonitor>(entity =>
        {
            entity.ToTable("NotificationMonitor");

            entity.Property(e => e.AcceptedDate).HasColumnType("datetime");
            entity.Property(e => e.NotifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PriceAccountRegister>(entity =>
        {
            entity.HasKey(e => e.AccountRegisterId);

            entity.ToTable("PriceAccountRegister");

            entity.Property(e => e.AccountRegisterId).HasColumnName("AccountRegisterID");
            entity.Property(e => e.AccountNumber).HasMaxLength(15);
            entity.Property(e => e.CompanyRegisterId).HasColumnName("CompanyRegisterID");

            entity.HasOne(d => d.CompanyRegister).WithMany(p => p.PriceAccountRegisters)
                .HasForeignKey(d => d.CompanyRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceAccountRegister_CompanyRegisterID_PriceCompanyRegister_CompanyRegisterID");
        });

        modelBuilder.Entity<PriceCompanyRegister>(entity =>
        {
            entity.HasKey(e => e.CompanyRegisterId);

            entity.ToTable("PriceCompanyRegister");

            entity.Property(e => e.CompanyRegisterId).HasColumnName("CompanyRegisterID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDnnId).HasColumnName("CreatedDnnID");
            entity.Property(e => e.CreatedUnitId).HasColumnName("CreatedUnitID");
            entity.Property(e => e.EditedDate).HasColumnType("datetime");
            entity.Property(e => e.EditedDnnId).HasColumnName("EditedDnnID");
            entity.Property(e => e.PriceDnnUserId).HasColumnName("PriceDnnUserID");
        });

        modelBuilder.Entity<PriceExtraFee>(entity =>
        {
            entity.ToTable("PriceExtraFee");

            entity.Property(e => e.PriceExtraFeeId).HasColumnName("PriceExtraFeeID");
            entity.Property(e => e.ExtraFee).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ExtraFeeVatId).HasColumnName("ExtraFeeVatID");
            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.ExtraFeeVat).WithMany(p => p.PriceExtraFees)
                .HasForeignKey(d => d.ExtraFeeVatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceExtraFee_ExtraFeeVatID_VatRegistration_VatRegisterID");

            entity.HasOne(d => d.PriceRegister).WithMany(p => p.PriceExtraFees)
                .HasForeignKey(d => d.PriceRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceExtraFee_PriceRegisterID_PriceRegister_PriceRegisterID");
        });

        modelBuilder.Entity<PricePayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.ToTable("PricePayment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.AccountRegisterId).HasColumnName("AccountRegisterID");
            entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.CreatedDnnId).HasColumnName("CreatedDnnID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDnnId).HasColumnName("ModifiedDnnID");
            entity.Property(e => e.PayedDate).HasColumnType("datetime");
            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.AccountRegister).WithMany(p => p.PricePayments)
                .HasForeignKey(d => d.AccountRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PricePayment_AccountRegisterID_PriceAccountRegister_AccountRegisterID");

            entity.HasOne(d => d.PriceRegister).WithMany(p => p.PricePayments)
                .HasForeignKey(d => d.PriceRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PricePayment_PriceRegisterID_PriceRegister_PriceRegisterID");
        });

        modelBuilder.Entity<PricePerQuotum>(entity =>
        {
            entity.HasKey(e => e.PriceQuotaId);

            entity.Property(e => e.PriceQuotaId).HasColumnName("PriceQuotaID");
            entity.Property(e => e.DescriptionFee).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.PricePerQuotaValue).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.QuotaCategoryId).HasColumnName("QuotaCategoryID");

            entity.HasOne(d => d.PriceRegister).WithMany(p => p.PricePerQuota)
                .HasForeignKey(d => d.PriceRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PricePerQuota_PriceRegisterID_PriceRegister_PriceRegisterID");
        });

        modelBuilder.Entity<PricePerShotKilo>(entity =>
        {
            entity.HasKey(e => e.PricePerShotId);

            entity.ToTable("PricePerShotKilo");

            entity.Property(e => e.PricePerShotId).HasColumnName("PricePerShotID");
            entity.Property(e => e.DescriptionFee).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.PricePerShot).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.ShotCategoryId).HasColumnName("ShotCategoryID");

            entity.HasOne(d => d.PriceRegister).WithMany(p => p.PricePerShotKilos)
                .HasForeignKey(d => d.PriceRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PricePerShotKilo_PriceRegisterID_PriceRegister_PriceRegisterID");
        });

        modelBuilder.Entity<PriceRegister>(entity =>
        {
            entity.ToTable("PriceRegister");

            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.AccountRegisterId).HasColumnName("AccountRegisterID");
            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");
            entity.Property(e => e.CompanyRegisterId).HasColumnName("CompanyRegisterID");
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDnnId).HasColumnName("CreatedDnnID");
            entity.Property(e => e.EditedDate).HasColumnType("datetime");
            entity.Property(e => e.EditedDnnId).HasColumnName("EditedDnnID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.GenaralVatId).HasColumnName("GenaralVatID");
            entity.Property(e => e.PriceGroupName).HasMaxLength(50);
            entity.Property(e => e.QuotaDescVatId).HasColumnName("QuotaDescVatID");
            entity.Property(e => e.QuotaDescriptionTitle).HasMaxLength(30);
            entity.Property(e => e.ShotDescVatId).HasColumnName("ShotDescVatID");
            entity.Property(e => e.ShotDescriptionTitle).HasMaxLength(30);
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.AccountRegister).WithMany(p => p.PriceRegisters)
                .HasForeignKey(d => d.AccountRegisterId)
                .HasConstraintName("FK_PriceRegister_AccuntRegisterID_PriceAccountRegister_AccuntRegisterID");

            entity.HasOne(d => d.CompanyRegister).WithMany(p => p.PriceRegisters)
                .HasForeignKey(d => d.CompanyRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceRegister_CompanyRegisterID_PriceCompanyRegister_CompanyRegisterID");

            entity.HasOne(d => d.Unit).WithMany(p => p.PriceRegisters)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PriceTeamExtraFee>(entity =>
        {
            entity.HasKey(e => e.TeamExtraFeeId);

            entity.ToTable("PriceTeamExtraFee");

            entity.Property(e => e.TeamExtraFeeId).HasColumnName("TeamExtraFeeID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDnnId).HasColumnName("CreatedDnnID");
            entity.Property(e => e.ExtraFee).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ExtraFeeVatId).HasColumnName("ExtraFeeVatID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDnnId).HasColumnName("ModifiedDnnID");
            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.ExtraFeeVat).WithMany(p => p.PriceTeamExtraFees)
                .HasForeignKey(d => d.ExtraFeeVatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceTeamExtraFee_ExtraFeeVatID_VatRegistration_VatRegisterID");

            entity.HasOne(d => d.PriceRegister).WithMany(p => p.PriceTeamExtraFees)
                .HasForeignKey(d => d.PriceRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceTeamExtraFee_PriceRegisterID_PriceRegister_PriceRegisterID");
        });

        modelBuilder.Entity<PriceUnit>(entity =>
        {
            entity.ToTable("PriceUnit");

            entity.Property(e => e.PriceUnitId).HasColumnName("PriceUnitID");
            entity.Property(e => e.AdvTotal).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.AdvanceMailSentDate).HasColumnType("datetime");
            entity.Property(e => e.AdvanceSenderDnnId).HasColumnName("AdvanceSenderDnnID");
            entity.Property(e => e.FinalMailSentDate).HasColumnType("datetime");
            entity.Property(e => e.FinalSenderDnnId).HasColumnName("FinalSenderDnnID");
            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.SumTurnOver).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.PriceRegister).WithMany(p => p.PriceUnits)
                .HasForeignKey(d => d.PriceRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceUnit_PriceRegisterID_PriceRegister_PriceRegisterID");
        });

        modelBuilder.Entity<PriceWeightRange>(entity =>
        {
            entity.ToTable("PriceWeightRange");

            entity.Property(e => e.PriceWeightRangeId).HasColumnName("PriceWeightRangeID");
            entity.Property(e => e.PricePerKilo).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PriceRegisterId).HasColumnName("PriceRegisterID");
            entity.Property(e => e.ShotCategoryId).HasColumnName("ShotCategoryID");

            entity.HasOne(d => d.PriceRegister).WithMany(p => p.PriceWeightRanges)
                .HasForeignKey(d => d.PriceRegisterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceWeightRange_PriceRegisterID_PriceRegister_PriceRegisterID");
        });

        modelBuilder.Entity<PublicRegistryAuthentication>(entity =>
        {
            entity.HasKey(e => e.PrUserId).HasName("PK_PublicRegistryAuthenticate");

            entity.ToTable("PublicRegistryAuthentication");

            entity.Property(e => e.PrUserId).HasColumnName("PrUserID");
        });

        modelBuilder.Entity<PublicRegistryErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorId);

            entity.ToTable("PublicRegistryErrorLog");

            entity.Property(e => e.ErrorId).HasColumnName("ErrorID");
            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.ManagementReferenceId)
                .HasMaxLength(20)
                .HasColumnName("ManagementReferenceID");
            entity.Property(e => e.ManagementUnitId).HasColumnName("ManagementUnitID");
            entity.Property(e => e.ReferenceId)
                .HasMaxLength(20)
                .HasColumnName("ReferenceID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UserDnnId).HasColumnName("UserDnnID");
        });

        modelBuilder.Entity<PublicRegistryHunterValidationAuthentication>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("PublicRegistryHunterValidationAuthentication");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<QuotaAnimalType>(entity =>
        {
            entity.HasKey(e => e.QuotaTypeId);

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_QuotaAnimalTypes_AnimalType_AnimalTypeID");

            entity.HasIndex(e => e.GameId, "IX_FK_QuotaAnimalTypes_HuntingGame_GameId");

            entity.HasIndex(e => e.SharedFrom, "IX_FK_QuotaAnimalTypes_Units_SharedFrom");

            entity.Property(e => e.QuotaTypeId).HasColumnName("QuotaTypeID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.QuotaType).HasMaxLength(50);

            entity.HasOne(d => d.AnimalType).WithMany(p => p.QuotaAnimalTypes)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.QuotaAnimalTypes)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SharedFromNavigation).WithMany(p => p.QuotaAnimalTypes).HasForeignKey(d => d.SharedFrom);
        });

        modelBuilder.Entity<QuotaAnimalTypeUnit>(entity =>
        {
            entity.ToTable("QuotaAnimalTypeUnit");

            entity.HasIndex(e => e.QuotaAnimalTypeId, "IX_FK_QuotaAnimalTypeUnit_QuotaAnimalTypes_QuotaAnimalTypeId");

            entity.HasIndex(e => e.UnitId, "IX_FK_QuotaAnimalTypeUnit_Units_UnitId");

            entity.HasOne(d => d.QuotaAnimalType).WithMany(p => p.QuotaAnimalTypeUnits).HasForeignKey(d => d.QuotaAnimalTypeId);

            entity.HasOne(d => d.Unit).WithMany(p => p.QuotaAnimalTypeUnits).HasForeignKey(d => d.UnitId);
        });

        modelBuilder.Entity<QuotaCount>(entity =>
        {
            entity.ToTable("QuotaCount");

            entity.HasIndex(e => e.QuotaAnimalTypeId, "IX_FK_QuotaCount_QuotaAnimalTypes_QuotaAnimalTypeId");

            entity.HasIndex(e => e.QuotaId, "IX_FK_QuotaCount_Quota_QuotaId");

            entity.HasOne(d => d.QuotaAnimalType).WithMany(p => p.QuotaCounts)
                .HasForeignKey(d => d.QuotaAnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Quota).WithMany(p => p.QuotaCounts)
                .HasForeignKey(d => d.QuotaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<QuotaTypeMappedToNormalType>(entity =>
        {
            entity.HasKey(e => e.QuotaMappedTypeId);

            entity.ToTable("QuotaTypeMappedToNormalType");

            entity.HasIndex(e => e.NormalTypeId, "IX_FK_QuotaTypeMappedToNormalType_AnimalSubType_NormalTypeID");

            entity.HasIndex(e => e.QuotaTypeId, "IX_FK_QuotaTypeMappedToNormalType_QuotaAnimalTypes_QuotaTypeID");

            entity.Property(e => e.QuotaMappedTypeId).HasColumnName("QuotaMappedTypeID");
            entity.Property(e => e.NormalTypeId).HasColumnName("NormalTypeID");
            entity.Property(e => e.QuotaTypeId).HasColumnName("QuotaTypeID");

            entity.HasOne(d => d.NormalType).WithMany(p => p.QuotaTypeMappedToNormalTypes)
                .HasForeignKey(d => d.NormalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.QuotaType).WithMany(p => p.QuotaTypeMappedToNormalTypes)
                .HasForeignKey(d => d.QuotaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<QuotaTypesMappedToAntlersType>(entity =>
        {
            entity.HasKey(e => e.QuotaTypesMappedToAntlersId);

            entity.HasIndex(e => e.AntlersTypeId, "IX_FK_QuotaTypesMappedToAntlersTypes_AnimalHasAntlers_AntlersId");

            entity.HasIndex(e => e.QuotaTypeId, "IX_FK_QuotaTypesMappedToAntlersTypes_QuotaAnimalTypes_QuotaTypeId");

            entity.HasOne(d => d.AntlersType).WithMany(p => p.QuotaTypesMappedToAntlersTypes)
                .HasForeignKey(d => d.AntlersTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuotaTypesMappedToAntlersTypes_AnimalHasAntlers_AntlersId");

            entity.HasOne(d => d.QuotaType).WithMany(p => p.QuotaTypesMappedToAntlersTypes)
                .HasForeignKey(d => d.QuotaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<QuotaTypesMappedToTag>(entity =>
        {
            entity.HasKey(e => e.QuotaTypesMappedToTagsId);

            entity.HasIndex(e => e.QuotaTypeId, "IX_FK_QuotaTypesMappedToTags_QuotaAnimalTypes_QuotaTypeId");

            entity.HasOne(d => d.QuotaType).WithMany(p => p.QuotaTypesMappedToTags)
                .HasForeignKey(d => d.QuotaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<QuotaTypesMappedToWeight>(entity =>
        {
            entity.ToTable("QuotaTypesMappedToWeight");

            entity.HasIndex(e => e.QuotaTypeId, "IX_FK_QuotaTypesMappedToWeight_QuotaAnimalTypes_QuotaTypeId");

            entity.HasOne(d => d.QuotaType).WithMany(p => p.QuotaTypesMappedToWeights)
                .HasForeignKey(d => d.QuotaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Quotum>(entity =>
        {
            entity.HasKey(e => e.QuotaId);

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_Quota_AnimalType_AnimalTypeID");

            entity.HasIndex(e => e.GameId, "IX_FK_Quota_HuntingGame_GameID");

            entity.HasIndex(e => e.UnitId, "IX_FK_Quota_Units_UnitID");

            entity.Property(e => e.QuotaId).HasColumnName("QuotaID");
            entity.Property(e => e.AnimalTypeId).HasColumnName("AnimalTypeID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.Quota)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.Quota)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.Quota)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Token);

            entity.ToTable("RefreshToken");

            entity.Property(e => e.Token).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReportExport>(entity =>
        {
            entity.HasKey(e => e.ExportId);

            entity.ToTable("ReportExport");

            entity.Property(e => e.ExportId).HasColumnName("ExportID");
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");
            entity.Property(e => e.HuntingGroundId)
                .HasMaxLength(20)
                .HasColumnName("HuntingGroundID");
            entity.Property(e => e.ManagementAreaId)
                .HasMaxLength(20)
                .HasColumnName("ManagementAreaID");
            entity.Property(e => e.MappedObsId).HasColumnName("MappedObsID");
            entity.Property(e => e.PrActionId).HasColumnName("PrActionID");
            entity.Property(e => e.PrMappedObsId).HasColumnName("PrMappedObsID");
            entity.Property(e => e.PublicRegistryId).HasColumnName("PublicRegistryID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Action).WithMany(p => p.ReportExports)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("FK_ReportExport_ActionID_AnimalAction_ActionId");

            entity.HasOne(d => d.Animal).WithMany(p => p.ReportExports)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportExport_AnimalID_AnimalType_AnimalTypeID");
        });

        modelBuilder.Entity<ReportImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Image");

            entity.ToTable("ReportImage");

            entity.Property(e => e.DnnUserId).HasColumnName("DnnUserID");
            entity.Property(e => e.FileCreated).HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 8)");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
        });

        modelBuilder.Entity<ReportImageDownloadLog>(entity =>
        {
            entity.HasKey(e => e.ImageLogId);

            entity.ToTable("ReportImageDownloadLog");

            entity.Property(e => e.ImageLogId).HasColumnName("ImageLogID");
            entity.Property(e => e.DnnUserId).HasColumnName("DnnUserID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
        });

        modelBuilder.Entity<Smslog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("SMSLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.KeyWord).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Reply).HasColumnType("text");
            entity.Property(e => e.Sms)
                .HasColumnType("text")
                .HasColumnName("SMS");
            entity.Property(e => e.Smsdate).HasColumnName("SMSDate");
        });

        modelBuilder.Entity<Smsresponse>(entity =>
        {
            entity.ToTable("SMSResponse");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(8, 0)");
            entity.Property(e => e.DestinationAddress).HasMaxLength(20);
            entity.Property(e => e.ErrorMessage).HasMaxLength(2000);
            entity.Property(e => e.Message).HasMaxLength(2000);
            entity.Property(e => e.SourceAddress).HasMaxLength(20);
        });

        modelBuilder.Entity<StatisticsExcelImportHuntingMethodDetail>(entity =>
        {
            entity.HasKey(e => e.ExcelImpHuntingMethodDetailId);

            entity.ToTable("StatisticsExcelImport_HuntingMethod_Detail");

            entity.HasIndex(e => e.HuntingMethodId, "IX_FK_StatisticsExcelImport_HuntingMethod_Detail_AnimalHasHuntingMethods_MethodId");

            entity.HasIndex(e => e.ExcelImpMasterId, "IX_FK_StatisticsExcelImport_HuntingMethod_Detail_StatisticsExcelImport_Master_MasterId");

            entity.Property(e => e.KeyWordInExcelFile).HasMaxLength(100);

            entity.HasOne(d => d.ExcelImpMaster).WithMany(p => p.StatisticsExcelImportHuntingMethodDetails)
                .HasForeignKey(d => d.ExcelImpMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatisticsExcelImport_HuntingMethod_Detail_StatisticsExcelImport_Master_MasterId");

            entity.HasOne(d => d.HuntingMethod).WithMany(p => p.StatisticsExcelImportHuntingMethodDetails)
                .HasForeignKey(d => d.HuntingMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatisticsExcelImport_HuntingMethod_Detail_AnimalHasHuntingMethods_MethodId");
        });

        modelBuilder.Entity<StatisticsExcelImportMaster>(entity =>
        {
            entity.HasKey(e => e.ExcelImpMasterId);

            entity.ToTable("StatisticsExcelImport_Master");

            entity.HasIndex(e => e.AnimalTypeId, "IX_FK_StatisticsExcelImport_Master_AnimalType_AnimalTypeId");

            entity.Property(e => e.DateColumnId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HoursCountColumnId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HuntersCountColumnId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HuntingMethodColumnId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsActionAtForestColumnId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SampleFileName).HasMaxLength(100);
            entity.Property(e => e.WeightColumnId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.AnimalType).WithMany(p => p.StatisticsExcelImportMasters)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StatisticsExcelImportSubTypeDetail>(entity =>
        {
            entity.HasKey(e => e.ExcelImpSubTypeDetailId).HasName("PK_StatisticsExcelImport_Detail");

            entity.ToTable("StatisticsExcelImport_SubType_Detail");

            entity.HasIndex(e => e.AnimalSubTypeId, "IX_FK_StatisticsExcelImport_Detail_AnimalSubType_AnimalSubTypeId");

            entity.HasIndex(e => e.ExcelImpMasterId, "IX_FK_StatisticsExcelImport_SubType_Detail_StatisticsExcelImport_Master_MasterId");

            entity.Property(e => e.ColumnId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.AnimalSubType).WithMany(p => p.StatisticsExcelImportSubTypeDetails)
                .HasForeignKey(d => d.AnimalSubTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatisticsExcelImport_Detail_AnimalSubType_AnimalSubTypeId");

            entity.HasOne(d => d.ExcelImpMaster).WithMany(p => p.StatisticsExcelImportSubTypeDetails)
                .HasForeignKey(d => d.ExcelImpMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatisticsExcelImport_SubType_Detail_StatisticsExcelImport_Master_MasterId");
        });

        modelBuilder.Entity<TeamHasDogRegisterAnimal>(entity =>
        {
            entity.HasKey(e => e.HteamDogAnimalId);

            entity.ToTable("TeamHasDogRegisterAnimal");

            entity.Property(e => e.HteamDogAnimalId).HasColumnName("HTeamDogAnimalID");
            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");
            entity.Property(e => e.HuntingDogRegId).HasColumnName("HuntingDogRegID");

            entity.HasOne(d => d.Animal).WithMany(p => p.TeamHasDogRegisterAnimals)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamHasDogRegisterAnimal_AnimalID_AnimalType_AnimalTypeIID");

            entity.HasOne(d => d.HuntingDogReg).WithMany(p => p.TeamHasDogRegisterAnimals)
                .HasForeignKey(d => d.HuntingDogRegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamHasDogRegisterAnimal_HuntingDogRegID_HuntingDogRegister__HuntingDogRegID");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasIndex(e => e.TypeId, "IX_FK_Units_UnitTypes_TypeId");

            entity.HasIndex(e => e.ParentId, "IX_FK_Units_Units_ParentId");

            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.ReferenceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ReferenceID");
            entity.Property(e => e.Unit1)
                .HasMaxLength(50)
                .HasColumnName("Unit");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasForeignKey(d => d.ParentId);

            entity.HasOne(d => d.Type).WithMany(p => p.Units)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UnitAnimalSeasonText>(entity =>
        {
            entity.HasKey(e => e.UnitAnimalSeasonId);

            entity.ToTable("UnitAnimalSeasonText");

            entity.Property(e => e.UnitAnimalSeasonId).HasColumnName("UnitAnimalSeasonID");
            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");
            entity.Property(e => e.SeasonText).HasMaxLength(30);
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Animal).WithMany(p => p.UnitAnimalSeasonTexts)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitAnimalSeasonText_AnimalType_AnimalTypeID");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitAnimalSeasonTexts)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UnitAnimalsWeightType>(entity =>
        {
            entity.HasKey(e => e.UnitWeightTypeId).HasName("PK_AnimalWeightType");

            entity.ToTable("UnitAnimalsWeightType");

            entity.Property(e => e.UnitWeightTypeId).HasColumnName("UnitWeightTypeID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EditedDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.WeightTypeId).HasColumnName("WeightTypeID");
        });

        modelBuilder.Entity<UnitAvailabilityForSeason>(entity =>
        {
            entity.ToTable("UnitAvailabilityForSeason");

            entity.HasIndex(e => e.UnitId, "IX_FK_UnitAvailabilityForSeason_Units_UnitId");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitAvailabilityForSeasons)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UnitGameAnimalReprortAccess>(entity =>
        {
            entity.HasKey(e => e.UnitGameAnimalRepAccessId);

            entity.ToTable("UnitGameAnimalReprortAccess");

            entity.Property(e => e.UnitGameAnimalRepAccessId).HasColumnName("UnitGameAnimalRepAccessID");
            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Animal).WithMany(p => p.UnitGameAnimalReprortAccesses)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitGameAnimalReprortAccess_AnimalType");

            entity.HasOne(d => d.Game).WithMany(p => p.UnitGameAnimalReprortAccesses)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitGameAnimalReprortAccess_HuntingGame");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitGameAnimalReprortAccesses)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitGameAnimalReprortAccess_Units");
        });

        modelBuilder.Entity<UnitGroup>(entity =>
        {
            entity.ToTable("UnitGroup");

            entity.HasIndex(e => e.AdministrationUnitId, "IX_FK_UnitGroup_Units_AdministrationUnitId");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.UnitGroupName).HasMaxLength(50);

            entity.HasOne(d => d.AdministrationUnit).WithMany(p => p.UnitGroups).HasForeignKey(d => d.AdministrationUnitId);
        });

        modelBuilder.Entity<UnitGroupUnit>(entity =>
        {
            entity.ToTable("UnitGroupUnit");

            entity.HasIndex(e => e.UnitGroupId, "IX_FK_UnitGroupUnit_UnitGroup_UnitGroupId");

            entity.HasIndex(e => e.UnitId, "IX_FK_UnitGroupUnit_Units_UnitId");

            entity.HasOne(d => d.UnitGroup).WithMany(p => p.UnitGroupUnits)
                .HasForeignKey(d => d.UnitGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitGroupUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UnitHasGame>(entity =>
        {
            entity.ToTable("UnitHasGame");

            entity.HasIndex(e => e.GameId, "IX_FK_GameID_UnitHasGame_HuntingGame");

            entity.HasIndex(e => new { e.UnitId, e.GameId, e.IsActive }, "IX_UnitHasGame_UnitID_GameID_IsActive");

            entity.Property(e => e.UnitHasGameId).HasColumnName("UnitHasGameID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Game).WithMany(p => p.UnitHasGames)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GameID_UnitHasGame_HuntingGame");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitHasGames)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitID_UnitHasGame_Units");
        });

        modelBuilder.Entity<UnitHasUser>(entity =>
        {
            entity.HasKey(e => e.UnitHasUsersId);

            entity.HasIndex(e => e.UnitId, "IX_FK_UnitHasUsers_Units_UnitID");

            entity.HasIndex(e => e.UserId, "IX_FK_UnitHasUsers_ViltraUsers_UserID");

            entity.Property(e => e.UnitHasUsersId).HasColumnName("UnitHasUsersID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UnitUserName).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitHasUsers)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UnitHasUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitHasUsers_ViltraUsers_UserID");
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.Property(e => e.UnitTypeId).HasColumnName("UnitTypeID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DnnRoleId).HasColumnName("DnnRoleID");
            entity.Property(e => e.ImgUrl).HasMaxLength(100);
            entity.Property(e => e.UnitType1)
                .HasMaxLength(50)
                .HasColumnName("UnitType");
        });

        modelBuilder.Entity<UnitsInfomation>(entity =>
        {
            entity.HasKey(e => e.InfoId);

            entity.ToTable("UnitsInfomation");

            entity.HasIndex(e => e.InfoTypeId, "IX_FK_UnitsInfomation_InfoTypeID_InfoType_InfoTypeID");

            entity.HasIndex(e => e.UnitId, "IX_FK_UnitsInfomation_UnitID_Units_UnitID");

            entity.Property(e => e.InfoId).HasColumnName("InfoID");
            entity.Property(e => e.InfoDate).HasColumnType("datetime");
            entity.Property(e => e.InfoTypeId).HasColumnName("InfoTypeID");
            entity.Property(e => e.IsOtherthanHt).HasColumnName("IsOtherthanHT");
            entity.Property(e => e.ModifiedUserDnnId).HasColumnName("ModifiedUserDnnID");
            entity.Property(e => e.ReminderCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Summary).HasColumnType("ntext");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UserDnnId).HasColumnName("UserDnnID");

            entity.HasOne(d => d.InfoType).WithMany(p => p.UnitsInfomations)
                .HasForeignKey(d => d.InfoTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitsInfomation_InfoTypeID_InfoType_InfoTypeID");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitsInfomations)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnitsInfomation_UnitID_Units_UnitID");
        });

        modelBuilder.Entity<UsersLandUnitWithoutLand>(entity =>
        {
            entity.HasKey(e => e.UserLandUnitWithoutLandId);

            entity.ToTable("UsersLandUnitWithoutLand");

            entity.HasIndex(e => e.LandUnitId, "IX_FK_UsersLandUnitWithoutLand_Units_UnitId");

            entity.HasIndex(e => e.UserSysId, "IX_FK_UsersLandUnitWithoutLand_ViltraUser_UserId");

            entity.HasOne(d => d.LandUnit).WithMany(p => p.UsersLandUnitWithoutLands)
                .HasForeignKey(d => d.LandUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersLandUnitWithoutLand_Units_UnitId");

            entity.HasOne(d => d.UserSys).WithMany(p => p.UsersLandUnitWithoutLands)
                .HasForeignKey(d => d.UserSysId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersLandUnitWithoutLand_ViltraUser_UserId");
        });

        modelBuilder.Entity<VatRegistration>(entity =>
        {
            entity.HasKey(e => e.VatRegisterId);

            entity.ToTable("VatRegistration");

            entity.Property(e => e.VatRegisterId).HasColumnName("VatRegisterID");
            entity.Property(e => e.Season).HasMaxLength(54);
            entity.Property(e => e.ShortCode).HasMaxLength(10);
        });

        modelBuilder.Entity<ViltraUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.ToTable("ViltraUser");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.DnnUserId).HasColumnName("DnnUserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActiveUser).HasDefaultValue(true);
            entity.Property(e => e.IsRegistrationMailSent).HasDefaultValue(true);
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasMaxLength(100);
        });

        modelBuilder.Entity<WeightType>(entity =>
        {
            entity.HasKey(e => e.WeightTypeId).HasName("PK_WeightType");

            entity.Property(e => e.WeightTypeId).HasColumnName("WeightTypeID");
            entity.Property(e => e.WeightType1)
                .HasMaxLength(50)
                .HasColumnName("WeightType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
