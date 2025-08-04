using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.MapModels;

public partial class Matrikkelenhet
{
    public int Objid { get; set; }

    public string? Objtype { get; set; }

    public string Kommunenummer { get; set; } = null!;

    public int Gardsnummer { get; set; }

    public int Bruksnummer { get; set; }

    public int? Festenummer { get; set; }

    public int? Seksjonsnummer { get; set; }

    public string? Bruksnavn { get; set; }

    public string? Matrikkelenhetstype { get; set; }

    public bool Punktfeste { get; set; }

    public string? Anleggstype { get; set; }

    public string? Annenmatrikkelenhetstype { get; set; }

    public bool Avklarteiere { get; set; }

    public bool Avklartandeler { get; set; }

    public bool Haravtalegrensepunktfeste { get; set; }

    public bool Haravtalestedbundenrettighet { get; set; }

    public bool Hargrunnforurensing { get; set; }

    public bool Harkulturminne { get; set; }

    public bool Harregistrertgrunnerverv { get; set; }

    public bool Harregistrertjordskiftekrevd { get; set; }

    public long Matrikkelenhetid { get; set; }

    public string Uuidmatrikkelenhet { get; set; } = null!;

    public long? TeigFk { get; set; }
}
