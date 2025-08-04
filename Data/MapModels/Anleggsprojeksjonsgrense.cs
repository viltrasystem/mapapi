using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

public partial class Anleggsprojeksjonsgrense
{
    public int Objid { get; set; }

    public string? Objtype { get; set; }

    public long Anleggsprojeksjonsgrenseid { get; set; }

    public string Navnerom { get; set; } = null!;

    public int? Versjonid { get; set; }

    public DateTime? Datafangstdato { get; set; }

    public DateTime Oppdateringsdato { get; set; }

    public DateTime? Datauttaksdato { get; set; }

    public string? Malemetode { get; set; }

    public int? Noyaktighet { get; set; }

    public Geometry Grense { get; set; } = null!;

    public string Noyaktighetsklasse { get; set; } = null!;

    public string Uuidanleggsprojeksjonsgrense { get; set; } = null!;
}
