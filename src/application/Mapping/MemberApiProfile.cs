using application.Features.Members.Contracts;
using AutoMapper;
using domain.Entities;
using domain.ValueObjects;

namespace application.Mapping;

public class MemberApiProfile : Profile
{
    public MemberApiProfile()
    {
        CreateMap<CreateMemberRequest, Member>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => MemberId.New()))
            .ForMember(dest => dest.RegisterOn, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new Phone(src.Phone)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(src.Address)));

        CreateMap<UpdateMemberRequest, Member>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.RegisterOn, opt => opt.Ignore())
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new Phone(src.Phone)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(src.Address)));

        CreateMap<Member, MemberResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.RegisterOn, opt => opt.MapFrom(src => src.RegisterOn.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Value))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Value));
    }
}
