using Microsoft.EntityFrameworkCore;
using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly WiredContext _wiredContext;

    public ProductRepository(WiredContext wiredContext)
    {
        _wiredContext = wiredContext;
    }

    public List<Product> GetAll()
    {
        return _wiredContext.Products
            .AsNoTracking()
            .ToList();
    }

    public Product GetById(int id)
    {
        return _wiredContext.Products.FirstOrDefault(x => x.Id == id);
    }

    public void Create(Product product)
    {
        _wiredContext.Products.Add(product);
        _wiredContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var deleteItem = _wiredContext.Products.FirstOrDefault(x => x.Id == id);
        _wiredContext.Products.Remove(deleteItem);
        _wiredContext.SaveChanges();
    }   

    public void Update(Product product)
    {        
        _wiredContext.Entry(product).State = EntityState.Modified;
        _wiredContext.SaveChanges();
    }
}
