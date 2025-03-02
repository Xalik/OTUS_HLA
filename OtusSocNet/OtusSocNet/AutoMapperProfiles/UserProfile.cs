using AutoMapper;
using OtusSocNet.Dtos;
using OtusSocNet.Models.Requests;
using OtusSocNet.Models.Responses;

namespace OtusSocNet.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Response -> DTO
        CreateMap<RegisterRequest, RegisterParameters>();
        
        // DTO -> Request
        CreateMap<User, UserResponse>();
    }
}