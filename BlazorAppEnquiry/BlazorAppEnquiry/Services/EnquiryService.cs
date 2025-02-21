using Microsoft.EntityFrameworkCore;
using BlazorAppEnquiry.Models;
using BlazorAppEnquiry.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppEnquiry.Services
{
    public class EnquiryService
    {
        private readonly AppDbContext _context;
        public EnquiryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Enquiry>> GetEnquiriesAsync() => await _context.Enquiries.ToListAsync();

        [IgnoreAntiforgeryToken]
        public async Task AddEnquiryAsync(Enquiry enquiry)
        {
            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnquiryStatusAsync(Guid id, string status)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry != null)
            {
                enquiry.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEnquiryAsync(Guid id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry != null)
            {
                _context.Enquiries.Remove(enquiry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
