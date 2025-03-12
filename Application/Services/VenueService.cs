using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interface;
using Domain.Interfaces;

namespace Application.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IGenericUnitOfWork _uow;

        public VenueService(IVenueRepository venueRepository, IGenericUnitOfWork uow)
        {
            _uow = uow;
            _venueRepository = venueRepository;
        }

        public async Task<List<Venue>> GetAllVenuesAsync()
        {
            return await _venueRepository.GetAllAsync();
        }

        public async Task<List<object>> GetVenueByIdAsync(int id)
        {
            var obj = new List<object>();
            //var venue = await _venueRepository.GetAllAsync();
            var venue = await _uow.AsyncRepository<Venue>().GetSingleBySpecAsync(x => x.Id == id);
            var amenities = await _uow.AsyncRepository<Amenity>().ListBySpecAsync(x => x.VenueId == venue.Id);
            obj.Add(venue);
            obj.Add(amenities);
            return obj;
        }

        public async Task<Venue> CreateVenueAsync(Venue venue)
        {
            await _venueRepository.AddAsync(venue);
            return venue;
        }

        public async Task<Venue> UpdateVenueAsync(int id, Venue venue)
        {
            var existingVenue = await _venueRepository.GetByIdAsync(id);
            if (existingVenue == null) return null;

            existingVenue.VenueAddress = venue.VenueAddress;
            existingVenue.OpenHoursFrom = venue.OpenHoursFrom;
            existingVenue.OpenHoursTo = venue.OpenHoursTo;
            existingVenue.Sport = venue.Sport;
            existingVenue.VenueType = venue.VenueType;
            existingVenue.ParkingAvailable = venue.ParkingAvailable;
            existingVenue.PricePerHour = venue.PricePerHour;
            existingVenue.Description = venue.Description;
            existingVenue.Amenities = venue.Amenities;
            existingVenue.Images = venue.Images;

            await _venueRepository.UpdateAsync(existingVenue);
            return existingVenue;
        }
    }
}
