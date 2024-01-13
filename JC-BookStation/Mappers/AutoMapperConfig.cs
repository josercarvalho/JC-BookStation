using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using JC_BookStation.Data.Models;

namespace JC_BookStation.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}