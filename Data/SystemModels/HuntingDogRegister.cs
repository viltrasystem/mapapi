using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntingDogRegister
{
    public int HuntingDogRegId { get; set; }

    public string DogId { get; set; } = null!;

    public int TeamId { get; set; }

    public string? OwnerName { get; set; }

    public string? OwnerAddress { get; set; }

    public string ContactNo { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool? OwnDog { get; set; }

    public bool? AgreedDog { get; set; }

    public int Season { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? EditedBy { get; set; }

    public DateTime? EditedDate { get; set; }

    public bool IsArchive { get; set; }

    public virtual ICollection<TeamHasDogRegisterAnimal> TeamHasDogRegisterAnimals { get; set; } = new List<TeamHasDogRegisterAnimal>();
}
