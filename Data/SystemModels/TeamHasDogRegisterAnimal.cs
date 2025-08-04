using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class TeamHasDogRegisterAnimal
{
    public int HteamDogAnimalId { get; set; }

    public int HuntingDogRegId { get; set; }

    public int AnimalId { get; set; }

    public bool IsActive { get; set; }

    public virtual AnimalType Animal { get; set; } = null!;

    public virtual HuntingDogRegister HuntingDogReg { get; set; } = null!;
}
