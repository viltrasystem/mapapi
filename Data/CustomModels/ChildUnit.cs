using System.ComponentModel.DataAnnotations;

namespace ViltrapportenApi.Data.CustomModels
{
    public class ChildUnit
    {
        public ChildUnit()
        {
            this.Children = new List<ChildUnit>();
        }
        [Key]
            public int UnitID { get; set; }
            public string Unit { get; set; }
            public int UnitTypeID { get; set; }
            public int ParentID { get; set; }
            public string ReferenceID { get; set; }
            public int ChildCount { get; set; }
            public string ImgUrl { get; set; }
            public int unitCount { get; set; }
            public bool IsActiveForHunting { get; set; }
            public bool IsHuntingComplete { get; set; }
            public bool IsArchived { get; set; }
            public bool IsAllowedToRegisterLands { get; set; }
            public bool IsUserOnlyOnMunicipality { get; set; }
            public bool IsGuest { get; set; }
            public bool IsExpanded { get; set; }
            public IList<ChildUnit> Children { get; set; }
    }
}
