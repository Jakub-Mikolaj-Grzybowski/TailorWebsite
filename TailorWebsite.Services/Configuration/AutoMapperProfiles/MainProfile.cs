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
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.OrderDate, o => o.MapFrom(src => src.OrderDate))
            .ForMember(d => d.Status, o => o.MapFrom(_ => OrderStatus.Pending))
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore())
            .ForMember(d => d.TotalPrice, o => o.MapFrom(src => src.TotalPrice ?? 0m));

        CreateMap<Order, OrderCreateViewModel>()
            .ForMember(d => d.TotalPrice, o => o.MapFrom(src => src.TotalPrice));
    }
}
