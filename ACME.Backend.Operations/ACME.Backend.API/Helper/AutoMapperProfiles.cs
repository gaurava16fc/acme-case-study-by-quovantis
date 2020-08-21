using AutoMapper;
using ACME.Backend.Core.Entities.Models;
using ACME.Backend.Core.DTO;


namespace ACME.Backend.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDetailsToReturnDTO>().ReverseMap();
            CreateMap<Employee, EmployeeForDetailedDTO>().ReverseMap();
            CreateMap<Customer, CustomerForDetailedDTO>().ReverseMap();
            CreateMap<SavingAccount, SavingAccountToReturnDTO>().ReverseMap();
        }
    }
}
