using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;
using WiredBrainCoffeeAdmin.Data.Models;
using WiredBrainCoffeeAdmin.Data.Repositories;

namespace WiredBrainCoffeeAdmin.Pages.products;

public class AddProductModel : PageModel
{
    private readonly IProductRepository _repository;
    private readonly IWebHostEnvironment _webEnvironment;

    [BindProperty]
    public Product NewProduct { get; set; }

    public AddProductModel(
        IProductRepository _repository,
        IWebHostEnvironment webEnvironment)
    {
        this._repository = _repository;
        _webEnvironment = webEnvironment;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();
        
        await UploadImage(NewProduct.Upload);

        NewProduct.Created = DateTime.Now;

        _repository.Create(NewProduct);

        return RedirectToPage("ViewAllProducts");
    }

    private async Task UploadImage(IFormFile image)
    {
        if (image is not null)
        {
            NewProduct.ImageFileName = image.FileName;

            var filePath = Path
                .Combine(_webEnvironment.ContentRootPath, "wwwroot/images/menu", NewProduct.ImageFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await NewProduct.Upload.CopyToAsync(fileStream);
        }
    }
}
