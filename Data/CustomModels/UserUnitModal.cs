namespace ViltrapportenApi.Data.CustomModels
{
    public class UserUnitModel
    {
        public UserUnitModel()
        {
            //CombinedGroupCount = 0;
            NormalUnits = new List<UserUnit>();
           // LandUnits = new List<UserUnit>();
        }

       // public int CombinedGroupCount { get; set; }
        public IList<UserUnit> NormalUnits { get; set; }
       // public IList<UserUnit> LandUnits { get; set; }
    }
}
