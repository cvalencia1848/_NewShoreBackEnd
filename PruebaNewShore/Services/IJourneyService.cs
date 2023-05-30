using PruebaNewShore.DTOs;
using PruebaNewShore.Models;

namespace PruebaNewShore.Services
{
    public interface IJourneyService
    {
        Task<List<FlightDTO>> GetFlightsApi(string url);
        List<Flight> MappingFlights(List<FlightDTO> flightDTOs);
        List<Flight> BuscarRuta(List<Flight> flights, string origin, string destination);

    }
}
