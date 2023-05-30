using AutoMapper;
using PruebaNewShore.DTOs;
using PruebaNewShore.Models;

namespace PruebaNewShore.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IMapper mapper;

        public JourneyService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<List<FlightDTO>> GetFlightsApi(string url)
        {
            var httpClient = new HttpClient();
            var result = await httpClient.GetFromJsonAsync<List<FlightDTO>>(url);

            return result;
        }

        public List<Flight> MappingFlights(List<FlightDTO> flightDTOs)
        {
            return mapper.Map<List<Flight>>(flightDTOs);
        }

        public List<Flight> BuscarRuta(List<Flight> flights, string origin, string destination)
        {
            List<Flight> route = new List<Flight>();
            HashSet<string> visited = new HashSet<string>();

            DFS(flights, origin, destination, visited, route);

            return route;

        }

        private bool DFS(List<Flight> flights, string origin, string destination, HashSet<string> visited, List<Flight> route)
        {
            visited.Add(origin);

            if (origin == destination)
                return true;

            foreach (var flight in flights)
            {
                if (flight.Origin == origin && !visited.Contains(flight.Destination))
                {
                    route.Add(flight);
                    if (DFS(flights, flight.Destination, destination, visited, route))
                        return true;
                    route.Remove(flight);
                }
            }

            return false;
        }

    }
}
