using InventoryLogic.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InventoryLogic.Facade
{
    public abstract class DomainModel<ModelType> : EqualityComparer<ModelType>, IHasUniqueObjectId where ModelType : IHasUniqueObjectId
    {
        public abstract int Id { get; set; }

        public override bool Equals([AllowNull] ModelType x, [AllowNull] ModelType y)
        {
            if (x == null || y == null || x.Id != y.Id) return false;
            return true;
        }
        public override int GetHashCode([DisallowNull] ModelType obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
