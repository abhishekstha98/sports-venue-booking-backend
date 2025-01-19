using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVenueService
    {
        Task<List<Venue>> GetAllVenuesAsync();
        Task<Venue> GetVenueByIdAsync(int id);
        Task<Venue> CreateVenueAsync(Venue venue);
        Task<Venue> UpdateVenueAsync(int id, Venue venue);
    }
}
