using System;
using AutoMapper;
using TailorWebsite.Model.DataModels;
using TailorWebsite.ViewModels.VM;

namespace TailorWebsite.Services.Configuration.AutoMapperProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<SizeCreateViewModel, Size>();
        CreateMap<Size, SizeCreateViewModel>();
        CreateMap<OrderCreateViewModel, Order>()
            .ForMember(d => d.Status, o => o.MapFrom(_ => OrderStatus.Pending));

        CreateMap<Order, OrderCreateViewModel>();

        CreateMap<ReviewViewModel, ServiceReview>()
            .ForMember(
                d => d.ReviewDate,
                o => o.MapFrom(src => src.ReviewDate == default ? DateTime.Now : src.ReviewDate)
            );

        CreateMap<ServiceReview, ReviewViewModel>()
            .ForMember(d => d.ReviewDate, o => o.MapFrom(src => src.ReviewDate))
            .ForMember(
                d => d.UserFullName,
                o =>
                    o.MapFrom(src =>
                        src.User != null ? $"{src.User.Name} {src.User.Surname}" : null
                    )
            );
        CreateMap<Notification, NotificationViewModel>();
        CreateMap<NotificationViewModel, Notification>();
    }
}
