namespace ViltrapportenApi.Modal
{
    public class LandDto
    {
        public required int LandId { get; set; }
        public required string Municipality { get; set; }
        public required int MainNo { get; set; }
        public required int SubNo { get; set; }
        public int? PlotNo { get; set; }
    }
}
