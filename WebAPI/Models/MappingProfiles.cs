using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<User, UserDto>();			
			CreateMap<UserAddress, AddressDto>();
		}
	}
}