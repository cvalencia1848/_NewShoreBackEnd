using Microsoft.AspNetCore.Mvc;
using PruebaNewShore.DTOs;
using PruebaNewShore.Models;
using PruebaNewShore.Services;

namespace PruebaNewShore.Controllers
{
    public class JourneyController : Controller
    {
        private readonly IJourneyService journeyService;

        public JourneyController(IJourneyService journeyService)
        {
            this.journeyService = journeyService;
        }

        //Problema 2
        [HttpGet("GetFlights")]
        public List<Flight> GetFlights()
        {
            try
            {
                const string url = "https://recruiting-api.newshore.es/api/flights/2";
                var FlightsDtos = journeyService.GetFlightsApi(url);
                var Flight = journeyService.MappingFlights(FlightsDtos.Result);
                return Flight;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetRoutes")]
        public Journey GetRoutes(JourneyDTO journeyDTO)
        {
            try
            {
                const string url = "https://recruiting-api.newshore.es/api/flights/2";
                var FlightsDtos = journeyService.GetFlightsApi(url);
                var Flight = journeyService.MappingFlights(FlightsDtos.Result);

                //var routeFlights = Flight.Where(x => x.Origin == journeyDTO.Origin || x.Destination == journeyDTO.Destination).ToList();

                var routeFlights = journeyService.BuscarRuta(Flight, journeyDTO.Origin, journeyDTO.Destination);

                Journey journey = new Journey();
                journey.Origin = journeyDTO.Origin;
                journey.Destination = journeyDTO.Destination;
                journey.Price = routeFlights.Sum(x => x.Price);
                journey.Flights = routeFlights;
                return journey;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
