using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

public partial class Anleggsprojeksjonspunkt
{
    public int Objid { get; set; }

    public string? Objtype { get; set; }

    public long Anleggsprojeksjonspunktid { get; set; }

    public string Navnerom { get; set; } = null!;

    public int? Versjonid { get; set; }

    public DateTime? Datafangstdato { get; set; }

    public DateTime Oppdateringsdato { get; set; }

    public DateTime? Datauttaksdato { get; set; }

    public string? Malemetode { get; set; }

    public int? Noyaktighet { get; set; }

    public Geometry Posisjon { get; set; } = null!;

    public string Grensepunkttype { get; set; } = null!;

    public string Grensemerkenedsatti { get; set; } = null!;

    public string? Noyaktighetsklasse { get; set; }

    public string Uuidanleggsprojeksjonspunkt { get; set; } = null!;
}
