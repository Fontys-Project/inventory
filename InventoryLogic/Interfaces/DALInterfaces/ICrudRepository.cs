using System.Collections.Generic;

namespace InventoryLogic.Interfaces
{
    public interface ICrudRepository<DomainModel>
    {
        // used by facade
        DomainModel CreateNew();

        DomainModel Get(int id);
        List<DomainModel> GetAll();
        DomainModel Add(DomainModel obj);
        void Remove(int id);
        void Modify(DomainModel obj);
    }
}