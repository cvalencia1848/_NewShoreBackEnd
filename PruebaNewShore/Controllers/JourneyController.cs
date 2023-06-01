using Microsoft.AspNetCore.Mvc;
using Models_.DTOs;
using Models_.Entitys;
using Services.Interfaces;

namespace PruebaNewShore.Controllers
{
    public class JourneyController : Controller
    {
        const string url = "https://recruiting-api.newshore.es/api/flights/2";
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
                var FlightsDtos = journeyService.GetFlightsApi(url);
                var Flight = journeyService.MappingFlights(FlightsDtos.Result);
                return Flight;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Problema 3
        [HttpPost("GetRoutes")]
        public Journey GetRoutes([FromBody] JourneyDTO journeyDTO)
        {
            try
            {
                var FlightsDtos = journeyService.GetFlightsApi(url);
                var Flight = journeyService.MappingFlights(FlightsDtos.Result);

                var routeFlights = journeyService.BuscarRuta(Flight, journeyDTO.Origin, journeyDTO.Destination);

                return journeyService.GetResponse(journeyDTO, routeFlights);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetCurrency")]
        public CurrencyDTo GetCurrency(string currency)
        {
            try
            {
                return journeyService.GetCurrency(currency);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
