using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.HttpServices;

public interface ITicketService
{
    Task<List<HelpTicket>> GetAll();

    Task<string> Add(HelpTicket ticket);
}