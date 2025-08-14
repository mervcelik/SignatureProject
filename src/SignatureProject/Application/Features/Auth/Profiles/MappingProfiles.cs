using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Core.Security.Entities.RefreshToken, RefreshToken>().ReverseMap();
        CreateMap<Core.Security.Entities.OperationClaim, OperationClaim>().ReverseMap();
        CreateMap<Core.Security.Entities.User, User>().ReverseMap();
        CreateMap<User, Core.Security.Entities.User>().ReverseMap();
    }
}