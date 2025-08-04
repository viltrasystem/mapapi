using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Modal
{
    public class LandTeigDTO
    {
        public int LandDrawnId { get; set; }

        public int LandId { get; set; }
        public Guid UuidLandDrawn { get; set; }
        public Geometry Geometry { get; set; } = null!;

        public int CreatedBy { get; set; }

        public int? EditedBy { get; set; }

        public double TeigId { get; set; }

        public string? Properties { get; set; }

        public string MunicipalityName { get; set; } = null!;

        public string MunicipalityNo { get; set; } = null!;

        public string MainNo { get; set; } = null!;

        public string SubNo { get; set; } = null!;

        public string? PlotNo { get; set; }
    }
}
