using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Task_Management.Data.Models;

namespace Task_Management.API.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskDto, Task>().ReverseMap();
        }
    }
}
