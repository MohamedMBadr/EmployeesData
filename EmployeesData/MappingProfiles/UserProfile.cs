using AutoMapper;
using Demo.DAL.Models;
using EmployeesData.ViewModels;

namespace EmployeesData.MappingProfiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
                CreateMap<ApplicationUser ,UserViewModel>().ReverseMap();
        }
    }
}
