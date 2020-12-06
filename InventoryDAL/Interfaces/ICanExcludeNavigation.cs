using System.Collections.Generic;

namespace InventoryDAL.Interfaces
{
    public interface ICanExcludeNavigation<T>
    {
        T Get(int id);
        List<T> GetAllExcludingNavigationProperties();
    }
}
