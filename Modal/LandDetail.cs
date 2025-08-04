namespace ViltrapportenApi.Modal
{
    public class LandDetail
    {
        public LandDetail()
        {
            this.Landowners = new List<Landowner>();
            this.ArchivedLandowners = new List<Landowner>();
            this.LandUnits = new List<LandUnitInfo>();
        }

        public int Id { get; set; }
        public int TeigId { get; set; }
        public string Municipality { get; set; }
        public string MainNo { get; set; }
        public string SubNo { get; set; }
        public string PlotNo { get; set; }
        public int OwnershipTypeId { get; set; }
        public float AreaInForest { get; set; }
        public float AreaInAgriculture { get; set; }
        public float AreaInMountain { get; set; }
        public float TotalArea => AreaInForest + AreaInAgriculture + AreaInMountain;

        public string Notes { get; set; }
        public int CreatedBy { get; set; }

        public IList<Landowner> Landowners { get; set; }
        public IList<Landowner> ArchivedLandowners { get; set; }
        public IList<LandUnitInfo> LandUnits { get; set; }
    }
}
