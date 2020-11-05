using InventoryDAL.Tags;

namespace InventoryDAL.Interfaces
{
    public interface IConverterFactory
    {
        TagConverter TagConverter { get; }
        ProductConverter ProductConverter { get; }
    }
}