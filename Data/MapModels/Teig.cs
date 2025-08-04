using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

public partial class Teig
{
    public int Objid { get; set; }

    public string? Objtype { get; set; }

    public long Teigid { get; set; }

    public string Navnerom { get; set; } = null!;

    public int? Versjonid { get; set; }

    public DateTime? Datafangstdato { get; set; }

    public DateTime Oppdateringsdato { get; set; }

    public DateTime? Datauttaksdato { get; set; }

    public string? Malemetode { get; set; }

    public int? Noyaktighet { get; set; }

    public Geometry? Representasjonspunkt { get; set; }

    public Geometry Omrade { get; set; } = null!;

    public string Kommunenummer { get; set; } = null!;

    public string Kommunenavn { get; set; } = null!;

    public string Matrikkelnummertekst { get; set; } = null!;

    public bool Teigmedflerematrikkelenheter { get; set; }

    public bool Tvist { get; set; }

    public bool Uregistrertjordsameie { get; set; }

    public bool? Avklarteiere { get; set; }

    public double? Lagretberegnetareal { get; set; }

    public string? Arealmerknadtekst { get; set; }

    public string? Noyaktighetsklasseteig { get; set; }

    public string Uuidteig { get; set; } = null!;

    /// <summary>
    /// Defined for delete existing land layers by users
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Defined for modified existing land layer by user
    /// </summary>
    public bool IsModified { get; set; }

    /// <summary>
    /// Defined for record modified or deleted user dnn id
    /// </summary>
    public int? EditedBy { get; set; }
}
