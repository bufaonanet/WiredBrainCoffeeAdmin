using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffeeAdmin.Data.Repositories;

namespace WiredBrainCoffeeAdmin.Components;

public class ProductListViewComponent : ViewComponent
{
    private readonly IProductRepository _repository;

    public ProductListViewComponent(IProductRepository repository)
    {
        _repository = repository;
    }

    public IViewComponentResult Invoke(int count)
    {
        var itens = count > 0 ?
            _repository.GetAll().Take(count).ToList() :
            _repository.GetAll();

        return View(itens);
    }
}