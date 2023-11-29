using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, PeoductToReturnDto>()
                   .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
                   .ForMember(D => D.Category, O => O.MapFrom(S => S.Category.Name))
                   .ForMember(D=>D.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(D => D.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(D => D.Product.ProductName))
                .ForMember(D => D.ProductUrl, O => O.MapFrom(D => D.Product.ProductUrl));


            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.ShortName, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.Cost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<Address, AddressDto>();
        }

        

    }
}
