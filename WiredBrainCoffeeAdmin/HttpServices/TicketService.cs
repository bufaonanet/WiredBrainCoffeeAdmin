using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.HttpServices;

public class TicketService : ITicketService
{
    private readonly HttpClient _httpClient;

    public TicketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Add(HelpTicket ticket)
    {
        var response = await _httpClient.PostAsJsonAsync("api/ticket", ticket);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<List<HelpTicket>> GetAll()
    {
        return await _httpClient.GetFromJsonAsync<List<HelpTicket>>("api/tickets");
    }
}