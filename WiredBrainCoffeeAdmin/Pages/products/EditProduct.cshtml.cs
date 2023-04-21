using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data.Models;
using WiredBrainCoffeeAdmin.Data.Repositories;

namespace WiredBrainCoffeeAdmin.Pages.products;

public class EditProductModel : PageModel
{
    private readonly IProductRepository _repository;
    private readonly IWebHostEnvironment _webEnvironment;

    public EditProductModel(
        IProductRepository repository,
        IWebHostEnvironment webEnvironment)
    {
        _repository = repository;
        _webEnvironment = webEnvironment;
    }

    [BindProperty]
    public Product EditProduct { get; set; }

    [FromRoute]
    public int Id { get; set; }

    public void OnGet()
    {
        EditProduct = _repository.GetById(Id);
    }

    public async Task<IActionResult> OnPostEdit()
    {
        if (!ModelState.IsValid) return Page();

        if (EditProduct.Upload != null)
        {
            await UploadImage(EditProduct.Upload);
        }

        _repository.Update(EditProduct);

        return RedirectToPage("ViewAllProducts");
    }

    public IActionResult OnPostDelete()
    {
        _repository.Delete(EditProduct.Id);
        return RedirectToPage("ViewAllProducts");
    }

    private async Task UploadImage(IFormFile image)
    {
        EditProduct.ImageFileName = image.FileName;

        var filePath = Path
            .Combine(_webEnvironment.ContentRootPath, "wwwroot/images/menu", EditProduct.ImageFileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await EditProduct.Upload.CopyToAsync(fileStream);
    }
}
