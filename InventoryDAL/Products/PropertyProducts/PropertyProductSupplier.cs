using System.Collections.Generic;
using System.Linq;
using InventoryDAL.Factories.Interfaces;
using InventoryDAL.Products.Converters.Interfaces;
using InventoryDAL.Products.ProductEntities;
using InventoryDAL.Products.ProductEntities.Interfaces;
using InventoryDAL.Products.PropertyProducts.Interfaces;
using InventoryLogic.Products;

namespace InventoryDAL.Products.PropertyProducts
{
    public class PropertyProductSupplier : IPropertyProductSupplier
    {
        private readonly IProductEntityDAO dao;
        private readonly IPropertyProductConverter converter;

        public PropertyProductSupplier(IProductEntityDAO dao,
                                  IPropertyProductConverter converter)
        {
            this.dao = dao;
            this.converter = converter;
        }

        public IList<PropertyProduct> GetAll()
        {
            List<ProductEntity> productEntities = dao.GetAll();
            return productEntities
                .Select(BuildProduct)
                .ToList();
        }

        public PropertyProduct Get(int id)
        {
            ProductEntity productEntity = dao.Get(id);
            return BuildProduct(productEntity);
        }

        private PropertyProduct BuildProduct(ProductEntity productEntity)
        {
            return converter.Convert(productEntity);
        }
    }
}
