using CookBook.Maui.Models;

namespace CookBook.Maui.Services.Interfaces;

public interface IRoutingService
{
    IEnumerable<RouteModel> Routes { get; }
}
