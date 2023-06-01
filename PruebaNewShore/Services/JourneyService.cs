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

            var flightsList = FilterRoute(flights, origin, destination);
            DFS(flightsList, origin, destination, visited, route);

            return route;

        }

        private List<Flight> FilterRoute(List<Flight> flights, string origin, string destination)
        {
            return flights.Where(x => x.Origin == origin || x.Destination == destination).ToList();
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

        public Journey GetResponse(JourneyDTO journeyDTO, List<Flight> journeys)
        {
            Journey journey = new Journey();
            journey.Origin = journeyDTO.Origin;
            journey.Destination = journeyDTO.Destination;
            journey.Price = journeys.Sum(x => x.Price);
            journey.Flights = journeys;

            return journey;
        }

        public Currency GetCurrency(string currency)
        {
            var currencyStr = CreateCurrency();
            return currencyStr.Where(c => c.Name.Equals(currency)).First();
        }


        private List<Currency> CreateCurrency()
        {
            List<Currency> currencyList = new List<Currency>();
            currencyList.Add(new Currency { Name = "EUR", Value = "1.4" });
            currencyList.Add(new Currency { Name = "JPY", Value = "0.8" });
            currencyList.Add(new Currency { Name = "GBP", Value = "1.8" });

            return currencyList;

        }

    }
}
