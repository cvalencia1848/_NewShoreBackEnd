using Models_.DTOs;
using Models_.Entitys;

namespace Services.Interfaces
{
    public interface IJourneyService
    {
        Task<List<FlightDTO>> GetFlightsApi(string url);
        List<Flight> MappingFlights(List<FlightDTO> flightDTOs);
        List<Flight> BuscarRuta(List<Flight> flights, string origin, string destination);
        Journey GetResponse(JourneyDTO journeyDTO, List<Flight> journeys);
        CurrencyDTo GetCurrency(string currency);
    }
}
