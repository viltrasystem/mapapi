using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class GameHasAnimal
{
    public int GameHasAnimalId { get; set; }

    public int GameId { get; set; }

    public int AnimalTypeId { get; set; }

    public bool IsActive { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual HuntingGame Game { get; set; } = null!;
}
