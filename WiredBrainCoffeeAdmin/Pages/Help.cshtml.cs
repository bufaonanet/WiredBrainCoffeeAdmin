using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data.Models;
using WiredBrainCoffeeAdmin.HttpServices;

namespace WiredBrainCoffeeAdmin.Pages;

public class HelpModel : PageModel
{
    [BindProperty]
    public HelpTicket NewTicket { get; set; }
    public List<HelpTicket> HelpTickets { get; set; }
    public string ResponseMessage { get; set; }

    private readonly ITicketService _ticketService;

    public HelpModel(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task<IActionResult> OnGet()
    {
        HelpTickets = await _ticketService.GetAll();

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        ResponseMessage = await _ticketService.Add(NewTicket);
        HelpTickets = await _ticketService.GetAll();
        return Page();
    }
}
