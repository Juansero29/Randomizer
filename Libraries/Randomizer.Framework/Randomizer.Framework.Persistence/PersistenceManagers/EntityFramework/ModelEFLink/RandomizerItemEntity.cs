using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework.ModelEFLink
{
    public class RandomizerItemEntity : IRandomizerItem
    {
        public int Id { get; set; }
    }
}
