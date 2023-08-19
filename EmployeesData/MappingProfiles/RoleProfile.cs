using AutoMapper;
using EmployeesData.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EmployeesData.MappingProfiles
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel , IdentityRole>()
                .ForMember(d=>d.Name , O=>O.MapFrom(s=>s.RoleName))
                .ReverseMap();


        }
    }
}
