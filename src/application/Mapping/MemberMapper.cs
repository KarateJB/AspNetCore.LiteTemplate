using application.Features.Members.Contracts;
using domain.Entities;
using domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace application.Mapping;

[Mapper]
public partial class MemberMapper
{
    public Member MapForCreate(CreateMemberRequest request)
    {
        var member = MapCreateRequestToMember(request);
        member.Id = MemberId.New();
        member.RegisterOn = DateTimeOffset.UtcNow;
        return member;
    }

    public Member MapForUpdate(UpdateMemberRequest request)
    {
        return MapUpdateRequestToMember(request);
    }

    public MemberResponse MapToResponse(Member member)
    {
        return MapToResponseCore(member);
    }

    [MapperIgnoreTarget(nameof(Member.Id))]
    [MapperIgnoreTarget(nameof(Member.RegisterOn))]
    [MapProperty(nameof(CreateMemberRequest.Phone), nameof(Member.Phone), Use = nameof(MapPhone))]
    [MapProperty(nameof(CreateMemberRequest.Address), nameof(Member.Address), Use = nameof(MapAddress))]
    private partial Member MapCreateRequestToMember(CreateMemberRequest request);

    [MapperIgnoreTarget(nameof(Member.Id))]
    [MapperIgnoreTarget(nameof(Member.RegisterOn))]
    [MapProperty(nameof(UpdateMemberRequest.Phone), nameof(Member.Phone), Use = nameof(MapPhone))]
    [MapProperty(nameof(UpdateMemberRequest.Address), nameof(Member.Address), Use = nameof(MapAddress))]
    private partial Member MapUpdateRequestToMember(UpdateMemberRequest request);

    [MapProperty(nameof(Member.Id), nameof(MemberResponse.Id), Use = nameof(MapMemberId))]
    [MapProperty(nameof(Member.Birthday), nameof(MemberResponse.Birthday), Use = nameof(MapBirthday))]
    [MapProperty(nameof(Member.RegisterOn), nameof(MemberResponse.RegisterOn), Use = nameof(MapRegisterOn))]
    [MapProperty(nameof(Member.Phone), nameof(MemberResponse.Phone), Use = nameof(MapPhoneValue))]
    [MapProperty(nameof(Member.Address), nameof(MemberResponse.Address), Use = nameof(MapAddressValue))]
    private partial MemberResponse MapToResponseCore(Member member);

    private static Guid MapMemberId(MemberId? value) => value?.Value ?? Guid.Empty;

    private static string MapBirthday(DateOnly value) => value.ToString("yyyy-MM-dd");

    private static string MapRegisterOn(DateTimeOffset value) => value.ToString("yyyy-MM-dd HH:mm:ss");

    private static Phone MapPhone(string value) => new(value);

    private static Address MapAddress(string value) => new(value);

    private static string MapPhoneValue(Phone? value) => value?.Value ?? string.Empty;

    private static string MapAddressValue(Address? value) => value?.Value ?? string.Empty;
}
