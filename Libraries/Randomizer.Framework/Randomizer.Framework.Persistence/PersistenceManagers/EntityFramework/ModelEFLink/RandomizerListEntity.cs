using Randomizer.Framework.Models.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Randomizer.Framework.Persistence.PersistenceManagers.EntityFramework.ModelEFLink
{
    public class RandomizerListEntity : IRandomizerList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<RandomizerItemEntity> Items { get; set; }

        IEnumerable<IRandomizerItem> IRandomizerList.Items
        {
            get { return Items; }
            set
            {
                Items = value as List<RandomizerItemEntity>;
            }
        }
    }
}
