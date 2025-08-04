using System.ComponentModel.DataAnnotations;

namespace ViltrapportenApi.Data.CustomModels
{
    public class ChildUnitModal
    {
        [Key]
        public int UnitID { get; set; }
        public string Unit { get; set; }
        public int UnitTypeID { get; set; }
        public string? ReferenceID { get; set; }
        public string ImgUrl { get; set; }
        public int unitCount { get; set; }
        public bool IsActiveForHunting { get; set; }
        public bool IsHuntingComplete { get; set; }
        public bool IsArchived { get; set; }
        public bool IsAllowedToRegisterLands { get; set; }

    }
}
