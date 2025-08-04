using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ViltrapportenApi.Data.MapModels;

public partial class ViltrapportenMapContext : DbContext
{
    public ViltrapportenMapContext()
    {
    }

    public ViltrapportenMapContext(DbContextOptions<ViltrapportenMapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrativgrensekode> Administrativgrensekodes { get; set; }

    public virtual DbSet<Anleggsprojeksjonsflate> Anleggsprojeksjonsflates { get; set; }

    public virtual DbSet<Anleggsprojeksjonsgrense> Anleggsprojeksjonsgrenses { get; set; }

    public virtual DbSet<Anleggsprojeksjonspunkt> Anleggsprojeksjonspunkts { get; set; }

    public virtual DbSet<Anleggstypekode> Anleggstypekodes { get; set; }

    public virtual DbSet<Annenmatrikkelenhetskode> Annenmatrikkelenhetskodes { get; set; }

    public virtual DbSet<Arealmerknadkode> Arealmerknadkodes { get; set; }

    public virtual DbSet<Eiendomsgrense> Eiendomsgrenses { get; set; }

    public virtual DbSet<FeatureDrawn> FeatureDrawns { get; set; }

    public virtual DbSet<Grensemerkenedsattikode> Grensemerkenedsattikodes { get; set; }

    public virtual DbSet<Grensepunkttypekode> Grensepunkttypekodes { get; set; }

    public virtual DbSet<Hjelpelinje> Hjelpelinjes { get; set; }

    public virtual DbSet<Hjelpelinjetypekode> Hjelpelinjetypekodes { get; set; }

    public virtual DbSet<Kommunenummer> Kommunenummers { get; set; }

    public virtual DbSet<LandDrawn> LandDrawns { get; set; }

    public virtual DbSet<Malemetode> Malemetodes { get; set; }

    public virtual DbSet<Matrikkelenhet> Matrikkelenhets { get; set; }

    public virtual DbSet<Matrikkelenhetstype> Matrikkelenhetstypes { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Noyaktighetsklassekode> Noyaktighetsklassekodes { get; set; }

    public virtual DbSet<Noyaktighetsklasseteigkode> Noyaktighetsklasseteigkodes { get; set; }

    public virtual DbSet<Teig> Teigs { get; set; }

    public virtual DbSet<TeigArealmerknad> TeigArealmerknads { get; set; }

    public virtual DbSet<Teiggrensepunkt> Teiggrensepunkts { get; set; }

    public virtual DbSet<Terrengdetaljkode> Terrengdetaljkodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=ViltrapportenPostgre;User Id=postgres;Password=Postgre@123;", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<Administrativgrensekode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("administrativgrensekode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Anleggsprojeksjonsflate>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("anleggsprojeksjonsflate_pkey");

            entity.ToTable("anleggsprojeksjonsflate", "map");

            entity.HasIndex(e => e.Anleggsprojeksjonsflateid, "anleggsprojeksjonsflate_anleggsprojeksjonsflateid_idx");

            entity.HasIndex(e => e.MatrikkelenhetMatrikkelenhetid, "anleggsprojeksjonsflate_matrikkelenhet_matrikkelenhetid_idx");

            entity.HasIndex(e => e.Omrade, "anleggsprojeksjonsflate_omrade_gix").HasMethod("gist");

            entity.HasIndex(e => e.Representasjonspunkt, "anleggsprojeksjonsflate_representasjonspunkt_gix").HasMethod("gist");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Anleggsprojeksjonsareal).HasColumnName("anleggsprojeksjonsareal");
            entity.Property(e => e.Anleggsprojeksjonsflateid).HasColumnName("anleggsprojeksjonsflateid");
            entity.Property(e => e.Datauttaksdato).HasColumnName("datauttaksdato");
            entity.Property(e => e.Kommentar).HasColumnName("kommentar");
            entity.Property(e => e.Kommunenavn).HasColumnName("kommunenavn");
            entity.Property(e => e.Kommunenummer).HasColumnName("kommunenummer");
            entity.Property(e => e.MatrikkelenhetAnleggstype).HasColumnName("matrikkelenhet_anleggstype");
            entity.Property(e => e.MatrikkelenhetAnnenmatrikkelenhetstype).HasColumnName("matrikkelenhet_annenmatrikkelenhetstype");
            entity.Property(e => e.MatrikkelenhetAvklartandeler).HasColumnName("matrikkelenhet_avklartandeler");
            entity.Property(e => e.MatrikkelenhetAvklarteiere).HasColumnName("matrikkelenhet_avklarteiere");
            entity.Property(e => e.MatrikkelenhetBruksnavn).HasColumnName("matrikkelenhet_bruksnavn");
            entity.Property(e => e.MatrikkelenhetBruksnummer).HasColumnName("matrikkelenhet_bruksnummer");
            entity.Property(e => e.MatrikkelenhetFestenummer).HasColumnName("matrikkelenhet_festenummer");
            entity.Property(e => e.MatrikkelenhetGardsnummer).HasColumnName("matrikkelenhet_gardsnummer");
            entity.Property(e => e.MatrikkelenhetHaravtalegrensepunktfeste).HasColumnName("matrikkelenhet_haravtalegrensepunktfeste");
            entity.Property(e => e.MatrikkelenhetHaravtalestedbundenrettighet).HasColumnName("matrikkelenhet_haravtalestedbundenrettighet");
            entity.Property(e => e.MatrikkelenhetHargrunnforurensing).HasColumnName("matrikkelenhet_hargrunnforurensing");
            entity.Property(e => e.MatrikkelenhetHarkulturminne).HasColumnName("matrikkelenhet_harkulturminne");
            entity.Property(e => e.MatrikkelenhetHarregistrertgrunnerverv).HasColumnName("matrikkelenhet_harregistrertgrunnerverv");
            entity.Property(e => e.MatrikkelenhetHarregistrertjordskiftekrevd).HasColumnName("matrikkelenhet_harregistrertjordskiftekrevd");
            entity.Property(e => e.MatrikkelenhetKommunenummer).HasColumnName("matrikkelenhet_kommunenummer");
            entity.Property(e => e.MatrikkelenhetMatrikkelenhetid).HasColumnName("matrikkelenhet_matrikkelenhetid");
            entity.Property(e => e.MatrikkelenhetMatrikkelenhetstype).HasColumnName("matrikkelenhet_matrikkelenhetstype");
            entity.Property(e => e.MatrikkelenhetPunktfeste).HasColumnName("matrikkelenhet_punktfeste");
            entity.Property(e => e.MatrikkelenhetSeksjonsnummer).HasColumnName("matrikkelenhet_seksjonsnummer");
            entity.Property(e => e.MatrikkelenhetUuidmatrikkelenhet).HasColumnName("matrikkelenhet_uuidmatrikkelenhet");
            entity.Property(e => e.Matrikkelnummertekst).HasColumnName("matrikkelnummertekst");
            entity.Property(e => e.Medium).HasColumnName("medium");
            entity.Property(e => e.Navnerom).HasColumnName("navnerom");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Anleggsprojeksjonsflate'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Omrade)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("omrade");
            entity.Property(e => e.Oppdateringsdato).HasColumnName("oppdateringsdato");
            entity.Property(e => e.Oppgittvolum).HasColumnName("oppgittvolum");
            entity.Property(e => e.Representasjonspunkt)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("representasjonspunkt");
            entity.Property(e => e.Uuidanleggsprojeksjonsflate).HasColumnName("uuidanleggsprojeksjonsflate");
            entity.Property(e => e.Versjonid).HasColumnName("versjonid");
        });

        modelBuilder.Entity<Anleggsprojeksjonsgrense>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("anleggsprojeksjonsgrense_pkey");

            entity.ToTable("anleggsprojeksjonsgrense", "map");

            entity.HasIndex(e => e.Anleggsprojeksjonsgrenseid, "anleggsprojeksjonsgrense_anleggsprojeksjonsgrenseid_idx");

            entity.HasIndex(e => e.Grense, "anleggsprojeksjonsgrense_grense_gix").HasMethod("gist");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Anleggsprojeksjonsgrenseid).HasColumnName("anleggsprojeksjonsgrenseid");
            entity.Property(e => e.Datafangstdato).HasColumnName("datafangstdato");
            entity.Property(e => e.Datauttaksdato).HasColumnName("datauttaksdato");
            entity.Property(e => e.Grense)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("grense");
            entity.Property(e => e.Malemetode).HasColumnName("malemetode");
            entity.Property(e => e.Navnerom).HasColumnName("navnerom");
            entity.Property(e => e.Noyaktighet).HasColumnName("noyaktighet");
            entity.Property(e => e.Noyaktighetsklasse).HasColumnName("noyaktighetsklasse");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Anleggsprojeksjonsgrense'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Oppdateringsdato).HasColumnName("oppdateringsdato");
            entity.Property(e => e.Uuidanleggsprojeksjonsgrense).HasColumnName("uuidanleggsprojeksjonsgrense");
            entity.Property(e => e.Versjonid).HasColumnName("versjonid");
        });

        modelBuilder.Entity<Anleggsprojeksjonspunkt>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("anleggsprojeksjonspunkt_pkey");

            entity.ToTable("anleggsprojeksjonspunkt", "map");

            entity.HasIndex(e => e.Anleggsprojeksjonspunktid, "anleggsprojeksjonspunkt_anleggsprojeksjonspunktid_idx");

            entity.HasIndex(e => e.Posisjon, "anleggsprojeksjonspunkt_posisjon_gix").HasMethod("gist");

            entity.HasIndex(e => e.Versjonid, "anleggsprojeksjonspunkt_versjonid_idx");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Anleggsprojeksjonspunktid).HasColumnName("anleggsprojeksjonspunktid");
            entity.Property(e => e.Datafangstdato).HasColumnName("datafangstdato");
            entity.Property(e => e.Datauttaksdato).HasColumnName("datauttaksdato");
            entity.Property(e => e.Grensemerkenedsatti).HasColumnName("grensemerkenedsatti");
            entity.Property(e => e.Grensepunkttype).HasColumnName("grensepunkttype");
            entity.Property(e => e.Malemetode).HasColumnName("malemetode");
            entity.Property(e => e.Navnerom).HasColumnName("navnerom");
            entity.Property(e => e.Noyaktighet).HasColumnName("noyaktighet");
            entity.Property(e => e.Noyaktighetsklasse).HasColumnName("noyaktighetsklasse");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Anleggsprojeksjonspunkt'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Oppdateringsdato).HasColumnName("oppdateringsdato");
            entity.Property(e => e.Posisjon)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("posisjon");
            entity.Property(e => e.Uuidanleggsprojeksjonspunkt).HasColumnName("uuidanleggsprojeksjonspunkt");
            entity.Property(e => e.Versjonid).HasColumnName("versjonid");
        });

        modelBuilder.Entity<Anleggstypekode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("anleggstypekode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Annenmatrikkelenhetskode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("annenmatrikkelenhetskode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Arealmerknadkode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("arealmerknadkode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Eiendomsgrense>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("eiendomsgrense_pkey");

            entity.ToTable("eiendomsgrense", "map");

            entity.HasIndex(e => e.Grense, "eiendomsgrense_grense_gix").HasMethod("gist");

            entity.HasIndex(e => e.Teiggrenseid, "eiendomsgrense_teiggrenseid_idx");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Administrativgrense).HasColumnName("administrativgrense");
            entity.Property(e => e.Datafangstdato).HasColumnName("datafangstdato");
            entity.Property(e => e.Datauttaksdato).HasColumnName("datauttaksdato");
            entity.Property(e => e.Folgerterrengdetalj).HasColumnName("folgerterrengdetalj");
            entity.Property(e => e.Grense)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("grense");
            entity.Property(e => e.Malemetode).HasColumnName("malemetode");
            entity.Property(e => e.Navnerom).HasColumnName("navnerom");
            entity.Property(e => e.Noyaktighet).HasColumnName("noyaktighet");
            entity.Property(e => e.Noyaktighetsklasse).HasColumnName("noyaktighetsklasse");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Eiendomsgrense'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Omtvistet).HasColumnName("omtvistet");
            entity.Property(e => e.Oppdateringsdato).HasColumnName("oppdateringsdato");
            entity.Property(e => e.Teiggrenseid).HasColumnName("teiggrenseid");
            entity.Property(e => e.Uuidteiggrense).HasColumnName("uuidteiggrense");
            entity.Property(e => e.Versjonid).HasColumnName("versjonid");
        });

        modelBuilder.Entity<FeatureDrawn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeatureDrawn_pkey");

            entity.ToTable("FeatureDrawn", "viltrapporten");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Properties).HasColumnType("jsonb");
            entity.Property(e => e.Style).HasColumnType("jsonb");
        });

        modelBuilder.Entity<Grensemerkenedsattikode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("grensemerkenedsattikode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Grensepunkttypekode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("grensepunkttypekode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Hjelpelinje>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("hjelpelinje_pkey");

            entity.ToTable("hjelpelinje", "map");

            entity.HasIndex(e => e.Grense, "hjelpelinje_grense_gix").HasMethod("gist");

            entity.HasIndex(e => e.Teiggrenseid, "hjelpelinje_teiggrenseid_idx");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Administrativgrense).HasColumnName("administrativgrense");
            entity.Property(e => e.Datafangstdato).HasColumnName("datafangstdato");
            entity.Property(e => e.Datauttaksdato).HasColumnName("datauttaksdato");
            entity.Property(e => e.Grense)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("grense");
            entity.Property(e => e.Hjelpelinjetype).HasColumnName("hjelpelinjetype");
            entity.Property(e => e.Malemetode).HasColumnName("malemetode");
            entity.Property(e => e.Navnerom).HasColumnName("navnerom");
            entity.Property(e => e.Noyaktighet).HasColumnName("noyaktighet");
            entity.Property(e => e.Noyaktighetsklasse).HasColumnName("noyaktighetsklasse");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Hjelpelinje'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Omtvistet).HasColumnName("omtvistet");
            entity.Property(e => e.Oppdateringsdato).HasColumnName("oppdateringsdato");
            entity.Property(e => e.Teiggrenseid).HasColumnName("teiggrenseid");
            entity.Property(e => e.Uuidteiggrense).HasColumnName("uuidteiggrense");
            entity.Property(e => e.Versjonid).HasColumnName("versjonid");
        });

        modelBuilder.Entity<Hjelpelinjetypekode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("hjelpelinjetypekode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Kommunenummer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("kommunenummer", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<LandDrawn>(entity =>
        {
            entity.HasKey(e => e.LandDrawnId).HasName("LandDrawn_pkey");

            entity.ToTable("LandDrawn", "viltrapporten", tb => tb.HasComment("land drawn by edit users using existing layer"));

            entity.Property(e => e.LandDrawnId).UseIdentityAlwaysColumn();
            entity.Property(e => e.MainNo).HasMaxLength(4);
            entity.Property(e => e.MunicipalityName).HasMaxLength(25);
            entity.Property(e => e.MunicipalityNo).HasMaxLength(4);
            entity.Property(e => e.PlotNo).HasMaxLength(4);
            entity.Property(e => e.Properties).HasColumnType("jsonb");
            entity.Property(e => e.SubNo).HasMaxLength(4);
        });

        modelBuilder.Entity<Malemetode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("malemetode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Matrikkelenhet>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("matrikkelenhet_pkey");

            entity.ToTable("matrikkelenhet", "map");

            entity.HasIndex(e => e.Matrikkelenhetid, "matrikkelenhet_matrikkelenhetid_idx");

            entity.HasIndex(e => e.TeigFk, "matrikkelenhet_teig_fk_idx");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Anleggstype).HasColumnName("anleggstype");
            entity.Property(e => e.Annenmatrikkelenhetstype).HasColumnName("annenmatrikkelenhetstype");
            entity.Property(e => e.Avklartandeler).HasColumnName("avklartandeler");
            entity.Property(e => e.Avklarteiere).HasColumnName("avklarteiere");
            entity.Property(e => e.Bruksnavn).HasColumnName("bruksnavn");
            entity.Property(e => e.Bruksnummer).HasColumnName("bruksnummer");
            entity.Property(e => e.Festenummer).HasColumnName("festenummer");
            entity.Property(e => e.Gardsnummer).HasColumnName("gardsnummer");
            entity.Property(e => e.Haravtalegrensepunktfeste).HasColumnName("haravtalegrensepunktfeste");
            entity.Property(e => e.Haravtalestedbundenrettighet).HasColumnName("haravtalestedbundenrettighet");
            entity.Property(e => e.Hargrunnforurensing).HasColumnName("hargrunnforurensing");
            entity.Property(e => e.Harkulturminne).HasColumnName("harkulturminne");
            entity.Property(e => e.Harregistrertgrunnerverv).HasColumnName("harregistrertgrunnerverv");
            entity.Property(e => e.Harregistrertjordskiftekrevd).HasColumnName("harregistrertjordskiftekrevd");
            entity.Property(e => e.Kommunenummer).HasColumnName("kommunenummer");
            entity.Property(e => e.Matrikkelenhetid).HasColumnName("matrikkelenhetid");
            entity.Property(e => e.Matrikkelenhetstype).HasColumnName("matrikkelenhetstype");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Matrikkelenhet'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Punktfeste).HasColumnName("punktfeste");
            entity.Property(e => e.Seksjonsnummer).HasColumnName("seksjonsnummer");
            entity.Property(e => e.TeigFk).HasColumnName("teig_fk");
            entity.Property(e => e.Uuidmatrikkelenhet).HasColumnName("uuidmatrikkelenhet");
        });

        modelBuilder.Entity<Matrikkelenhetstype>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("matrikkelenhetstype", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("medium", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Noyaktighetsklassekode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("noyaktighetsklassekode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Noyaktighetsklasseteigkode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("noyaktighetsklasseteigkode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<Teig>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("teig_pkey");

            entity.ToTable("teig", "map");

            entity.HasIndex(e => e.Omrade, "teig_omrade_gix").HasMethod("gist");

            entity.HasIndex(e => e.Representasjonspunkt, "teig_representasjonspunkt_gix").HasMethod("gist");

            entity.HasIndex(e => e.Teigid, "teig_teigid_idx");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Arealmerknadtekst).HasColumnName("arealmerknadtekst");
            entity.Property(e => e.Avklarteiere).HasColumnName("avklarteiere");
            entity.Property(e => e.Datafangstdato).HasColumnName("datafangstdato");
            entity.Property(e => e.Datauttaksdato).HasColumnName("datauttaksdato");
            entity.Property(e => e.EditedBy)
                .HasComment("Defined for record modified or deleted user dnn id")
                .HasColumnName("editedBy");
            entity.Property(e => e.IsArchived)
                .HasDefaultValue(false)
                .HasComment("Defined for delete existing land layers by users")
                .HasColumnName("isArchived");
            entity.Property(e => e.IsModified)
                .HasDefaultValue(false)
                .HasComment("Defined for modified existing land layer by user")
                .HasColumnName("isModified");
            entity.Property(e => e.Kommunenavn).HasColumnName("kommunenavn");
            entity.Property(e => e.Kommunenummer).HasColumnName("kommunenummer");
            entity.Property(e => e.Lagretberegnetareal).HasColumnName("lagretberegnetareal");
            entity.Property(e => e.Malemetode).HasColumnName("malemetode");
            entity.Property(e => e.Matrikkelnummertekst).HasColumnName("matrikkelnummertekst");
            entity.Property(e => e.Navnerom).HasColumnName("navnerom");
            entity.Property(e => e.Noyaktighet).HasColumnName("noyaktighet");
            entity.Property(e => e.Noyaktighetsklasseteig).HasColumnName("noyaktighetsklasseteig");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Teig'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Omrade)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("omrade");
            entity.Property(e => e.Oppdateringsdato).HasColumnName("oppdateringsdato");
            entity.Property(e => e.Representasjonspunkt)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("representasjonspunkt");
            entity.Property(e => e.Teigid).HasColumnName("teigid");
            entity.Property(e => e.Teigmedflerematrikkelenheter).HasColumnName("teigmedflerematrikkelenheter");
            entity.Property(e => e.Tvist).HasColumnName("tvist");
            entity.Property(e => e.Uregistrertjordsameie).HasColumnName("uregistrertjordsameie");
            entity.Property(e => e.Uuidteig).HasColumnName("uuidteig");
            entity.Property(e => e.Versjonid).HasColumnName("versjonid");
        });

        modelBuilder.Entity<TeigArealmerknad>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("teig_arealmerknad", "map");

            entity.HasIndex(e => e.TeigFk, "teig_arealmerknad_teig_fk_idx");

            entity.Property(e => e.ArealmerknadkodeFk).HasColumnName("arealmerknadkode_fk");
            entity.Property(e => e.TeigFk).HasColumnName("teig_fk");
        });

        modelBuilder.Entity<Teiggrensepunkt>(entity =>
        {
            entity.HasKey(e => e.Objid).HasName("teiggrensepunkt_pkey");

            entity.ToTable("teiggrensepunkt", "map");

            entity.HasIndex(e => e.Posisjon, "teiggrensepunkt_posisjon_gix").HasMethod("gist");

            entity.HasIndex(e => e.Teiggrensepunktid, "teiggrensepunkt_teiggrensepunktid_idx");

            entity.Property(e => e.Objid).HasColumnName("objid");
            entity.Property(e => e.Datafangstdato).HasColumnName("datafangstdato");
            entity.Property(e => e.Datauttaksdato).HasColumnName("datauttaksdato");
            entity.Property(e => e.Grensemerkenedsatti).HasColumnName("grensemerkenedsatti");
            entity.Property(e => e.Grensepunkttype).HasColumnName("grensepunkttype");
            entity.Property(e => e.Malemetode).HasColumnName("malemetode");
            entity.Property(e => e.Navnerom).HasColumnName("navnerom");
            entity.Property(e => e.Noyaktighet).HasColumnName("noyaktighet");
            entity.Property(e => e.Noyaktighetsklasse).HasColumnName("noyaktighetsklasse");
            entity.Property(e => e.Objtype)
                .HasDefaultValueSql("'Teiggrensepunkt'::text")
                .HasColumnName("objtype");
            entity.Property(e => e.Oppdateringsdato).HasColumnName("oppdateringsdato");
            entity.Property(e => e.Posisjon)
                .HasColumnType("geometry(Geometry,25832)")
                .HasColumnName("posisjon");
            entity.Property(e => e.Teiggrensepunktid).HasColumnName("teiggrensepunktid");
            entity.Property(e => e.Uuidteiggrensepunkt).HasColumnName("uuidteiggrensepunkt");
            entity.Property(e => e.Versjonid).HasColumnName("versjonid");
        });

        modelBuilder.Entity<Terrengdetaljkode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("terrengdetaljkode", "map");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
