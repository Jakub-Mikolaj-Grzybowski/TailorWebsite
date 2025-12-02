using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TailorWebsite.DAL.EF;
using TailorWebsite.Model.DataModels;
using TailorWebsite.Services.Interface;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.ConcreteServices
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddReviewAsync(ReviewViewModel reviewViewModel)
        {
            var review = _mapper.Map<ServiceReview>(reviewViewModel);
            _context.ServiceReviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReviewViewModel>> GetReviewsForOrderAsync(int orderId)
        {
            var reviews = await _context
                .ServiceReviews.Where(r => r.OrderId == orderId)
                .ToListAsync();
            return _mapper.Map<List<ReviewViewModel>>(reviews);
        }

        public async Task<List<ReviewViewModel>> GetReviewsForUserAsync(int userId)
        {
            var reviews = await _context
                .ServiceReviews.Where(r => r.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<ReviewViewModel>>(reviews);
        }
    }
}
