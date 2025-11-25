using AutoMapper;
using TailorWebsite.Model.DataModels;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Configuration.AutoMapperProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<SizeCreateViewModel, Size>().ForMember(d => d.CreatedAt, o => o.Ignore());
        CreateMap<Size, SizeCreateViewModel>();
        CreateMap<OrderCreateViewModel, Order>()
            .ForMember(d => d.OrderDate, o => o.MapFrom(src => src.OrderDate))
            .ForMember(d => d.Status, o => o.MapFrom(_ => OrderStatus.Pending))
            .ForMember(d => d.TotalPrice, o => o.MapFrom(src => src.TotalPrice ?? 0m));
        CreateMap<Order, OrderCreateViewModel>()
            .ForMember(d => d.TotalPrice, o => o.MapFrom(src => src.TotalPrice));
        CreateMap<OrderReviewViewModel, ServiceReview>();
        CreateMap<ServiceReview, OrderReviewViewModel>();

            CreateMap<Notification, NotificationViewModel>();
            CreateMap<NotificationViewModel, Notification>();
    }
}
