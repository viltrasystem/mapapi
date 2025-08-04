using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

public partial class Anleggsprojeksjonsflate
{
    public int Objid { get; set; }

    public string? Objtype { get; set; }

    public long Anleggsprojeksjonsflateid { get; set; }

    public string Navnerom { get; set; } = null!;

    public int? Versjonid { get; set; }

    public DateTime Oppdateringsdato { get; set; }

    public DateTime? Datauttaksdato { get; set; }

    public string? Medium { get; set; }

    public Geometry? Representasjonspunkt { get; set; }

    public Geometry Omrade { get; set; } = null!;

    public string Kommunenummer { get; set; } = null!;

    public string Kommunenavn { get; set; } = null!;

    public string MatrikkelenhetKommunenummer { get; set; } = null!;

    public int MatrikkelenhetGardsnummer { get; set; }

    public int MatrikkelenhetBruksnummer { get; set; }

    public int? MatrikkelenhetFestenummer { get; set; }

    public int? MatrikkelenhetSeksjonsnummer { get; set; }

    public string? MatrikkelenhetBruksnavn { get; set; }

    public string? MatrikkelenhetMatrikkelenhetstype { get; set; }

    public bool MatrikkelenhetPunktfeste { get; set; }

    public string? MatrikkelenhetAnleggstype { get; set; }

    public string? MatrikkelenhetAnnenmatrikkelenhetstype { get; set; }

    public bool MatrikkelenhetAvklarteiere { get; set; }

    public bool MatrikkelenhetAvklartandeler { get; set; }

    public bool MatrikkelenhetHaravtalegrensepunktfeste { get; set; }

    public bool MatrikkelenhetHaravtalestedbundenrettighet { get; set; }

    public bool MatrikkelenhetHargrunnforurensing { get; set; }

    public bool MatrikkelenhetHarkulturminne { get; set; }

    public bool MatrikkelenhetHarregistrertgrunnerverv { get; set; }

    public bool MatrikkelenhetHarregistrertjordskiftekrevd { get; set; }

    public long MatrikkelenhetMatrikkelenhetid { get; set; }

    public string MatrikkelenhetUuidmatrikkelenhet { get; set; } = null!;

    public string Matrikkelnummertekst { get; set; } = null!;

    public double? Anleggsprojeksjonsareal { get; set; }

    public double? Oppgittvolum { get; set; }

    public string? Kommentar { get; set; }

    public string Uuidanleggsprojeksjonsflate { get; set; } = null!;
}
