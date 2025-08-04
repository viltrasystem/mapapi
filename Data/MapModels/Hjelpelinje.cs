using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

public partial class Hjelpelinje
{
    public int Objid { get; set; }

    public string? Objtype { get; set; }

    public long Teiggrenseid { get; set; }

    public string Navnerom { get; set; } = null!;

    public int? Versjonid { get; set; }

    public DateTime? Datafangstdato { get; set; }

    public DateTime Oppdateringsdato { get; set; }

    public DateTime? Datauttaksdato { get; set; }

    public string? Malemetode { get; set; }

    public int? Noyaktighet { get; set; }

    public Geometry Grense { get; set; } = null!;

    public string Administrativgrense { get; set; } = null!;

    public bool Omtvistet { get; set; }

    public string Noyaktighetsklasse { get; set; } = null!;

    public string Uuidteiggrense { get; set; } = null!;

    public string Hjelpelinjetype { get; set; } = null!;
}
