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
            .ForMember(d => d.OrderDate, o => o.Ignore())
            .ForMember(d => d.Status, o => o.Ignore())
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());

        CreateMap<Order, OrderCreateViewModel>();
    }
}
