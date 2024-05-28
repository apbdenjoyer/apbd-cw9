using apbd_cw9.Data;
using apbd_cw9.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ApbdContext _context;

    public TripsController(ApbdContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int pageSize)
    {
        var result = new TripsDto()
        {
            PageNum = 1,
            PageSize = pageSize,
            AllPages = 20,
            Trips = await _context.Trips.Select(e => new TripDto()
            {
                Name = e.Name,
                Description = e.Description,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                MaxPeople = e.MaxPeople,
                Countries = e.IdCountries.Select(e => new CountryDto()
                {
                    Name = e.Name
                }).ToList(),
                Clients = e.ClientTrips.Select(e => new ClientDto()
                {
                    FirstName = e.IdClientNavigation.FirstName,
                    LastName = e.IdClientNavigation.LastName
                }).ToList()
            }).ToListAsync()
        };
        return Ok(result);
    }
}