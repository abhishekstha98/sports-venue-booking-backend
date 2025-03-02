using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/venue")]
public class VenuesController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenuesController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    // GET: /api/venues
    [HttpGet]
    public async Task<IActionResult> GetAllVenues()
    {
        var venues = await _venueService.GetAllVenuesAsync();
        return Ok(venues);
    }

    // GET: /api/venues/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVenueById(int id)
    {
        var venue = await _venueService.GetVenueByIdAsync(id);
        if (venue == null) return NotFound();
        return Ok(venue);
    }

    // POST: /api/venues
    [HttpPost]
    public async Task<IActionResult> CreateVenue([FromBody] Venue venue)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdVenue = await _venueService.CreateVenueAsync(venue);
        return CreatedAtAction(nameof(GetVenueById), new { id = createdVenue.Id }, createdVenue);
    }

    // PUT: /api/venues/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVenue(int id, [FromBody] Venue venue)
    {
        if (id != venue.Id) return BadRequest("Venue ID mismatch.");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedVenue = await _venueService.UpdateVenueAsync(id, venue);
        if (updatedVenue == null) return NotFound();

        return Ok(updatedVenue);
    }
}
