using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data.Models;
using WiredBrainCoffeeAdmin.Data.Repositories;

namespace WiredBrainCoffeeAdmin.Pages.products;

public class ViewAllProductsModel : PageModel
{
    private readonly IProductRepository _repository;
    public List<Product> Products { get; set; }

    public ViewAllProductsModel(IProductRepository repository)
    {
        _repository = repository;
    }

    public void OnGet()
    {
        Products = _repository.GetAll();
    }
}