using AutoMapper;
using Clientes = JC_BookStation.Data.Models.Clientes;

namespace JC_BookStation.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Clientes, ClienteViewModel>();
        }
    }
}