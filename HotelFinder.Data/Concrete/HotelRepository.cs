using HotelFinder.Data.Abstract;
using HotelFinder.Data;
using HotelFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelFinder.Data.Concrete
{
    public class HotelRepository : IHotelRepository
    {
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            using (var hoteldbcontext = new HotelDbContext())
            {
                hoteldbcontext.Add(hotel);
                await hoteldbcontext.SaveChangesAsync();
                return hotel;
            }
        }

        public async Task DeleteHotel(int id)
        {
            using (HotelDbContext hoteldbcontext = new HotelDbContext())
            {
                var deletedHotel = await GetHotelById(id);
                hoteldbcontext.Hotels.Remove(deletedHotel);
                await hoteldbcontext.SaveChangesAsync();
            }
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            using (HotelDbContext hoteldbcontext = new HotelDbContext())
            {
                return await hoteldbcontext.Hotels.ToListAsync();
            }
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            using (HotelDbContext hoteldbcontext = new HotelDbContext())
            {
                return await hoteldbcontext.Hotels.FindAsync(id);
            }
        }

        public async Task<Hotel> GetHotelByName(string name)
        {
            using (HotelDbContext hoteldbcontext = new HotelDbContext())
            {
                return await hoteldbcontext.Hotels.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            }
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            using (var hoteldbcontext = new HotelDbContext())
            {
                hoteldbcontext.Hotels.Update(hotel);
                await hoteldbcontext.SaveChangesAsync();
                return hotel;
            }
        }

        
    }
}
