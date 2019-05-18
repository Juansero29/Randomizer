using System;

namespace Randomizer.Framework.Models.Contract
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
