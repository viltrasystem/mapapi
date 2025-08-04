using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class ViltraUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? UserPassword { get; set; }

    public string ContactNumber { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public int? DnnUserId { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsDeactivationMailSent { get; set; }

    public bool IsLandOwner { get; set; }

    public bool IsActiveUser { get; set; }

    public bool IsRegistrationMailSent { get; set; }

    public int? HunterId { get; set; }

    public DateTime? BirthDay { get; set; }

    public virtual ICollection<LandOwnerDetail> LandOwnerDetails { get; set; } = new List<LandOwnerDetail>();

    public virtual ICollection<LandOwner> LandOwners { get; set; } = new List<LandOwner>();

    public virtual ICollection<UnitHasUser> UnitHasUsers { get; set; } = new List<UnitHasUser>();

    public virtual ICollection<UsersLandUnitWithoutLand> UsersLandUnitWithoutLands { get; set; } = new List<UsersLandUnitWithoutLand>();
}
