namespace ViltrapportenApi.Data.CustomModels
{
    public class Owner
    {
        public int LandId { get; set; }
        public IList<LandOwnerMapped> Owners { get; set; }
    }
}
