using AutoMapper;
using domain.Models;
using webapi.Models;

namespace webapi.Mapping;

public class MemberApiProfile : Profile
{
    public MemberApiProfile()
    {
        CreateMap<CreateMemberRequest, MemberDataModel>()
            .ForMember(dest => dest.RegisterOn, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow));

        CreateMap<UpdateMemberRequest, MemberDataModel>()
            .ForMember(dest => dest.RegisterOn, opt => opt.Ignore());

        CreateMap<MemberDataModel, MemberResponse>();
    }
}
