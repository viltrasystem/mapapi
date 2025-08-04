using System.ComponentModel.DataAnnotations;

namespace ViltrapportenApi.Data.CustomModels
{
    public class UserUnit
    {
        [Key]
        public int UnitID { get; set; }
        public string Unit { get; set; }
        public int UnitTypeID { get; set; }
        public string ReferenceID { get; set; }
        public string ImgUrl { get; set; }
        public string ParentUnit { get; set; }
        public int ChildCount { get; set; }
        public int ChildTeamsCount { get; set; }

        public bool IsActiveForHunting { get; set; }
        public bool IsHuntingComplete { get; set; }

        public bool IsArchived { get; set; }
        public bool IsAllowedToRegisterLands { get; set; }

        public bool IsMainUnit { get; set; }
        public bool IsMunicipalityUser { get; set; }
        public bool IsExporter { get; set; }
        public bool IsPriceUser { get; set; }
        public bool IsLandAssignableUser { get; set; }
        public bool IsLandOwner { get; set; }
        public bool IsReporter { get; set; }
        public bool IsHead { get; set; }
        public bool IsGuest { get; set; }
        //public bool IsLandUnit { get; set; }
        public bool IsHuntingPolice { get; set; }
        //public bool UserUnitSelected { get; set; }

    }
}
