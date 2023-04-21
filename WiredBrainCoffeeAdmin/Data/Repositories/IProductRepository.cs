using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Data.Repositories;

public interface IProductRepository
{
    public List<Product> GetAll();
    public Product GetById(int id);
    public void Create(Product product);
    public void Update(Product product);
    public void Delete(int id);
}