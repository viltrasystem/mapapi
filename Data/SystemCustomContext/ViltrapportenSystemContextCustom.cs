using Microsoft.EntityFrameworkCore;
using ViltrapportenApi.Data.CustomModels;

namespace ViltrapportenApi.Data.SystemModels
{
    public class ViltrapportenSystemContextCustom
    {
    }

    public partial class ViltrapportenSystemContext
    {
        public virtual DbSet<UserUnit> UserUnits { get; set; }
        public virtual DbSet<ChildUnitModal> ChildUnitModals { get; set; }
    }
}
