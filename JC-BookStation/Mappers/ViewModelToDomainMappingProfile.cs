using AutoMapper;
using Clientes = JC_BookStation.Data.Models.Clientes;

namespace JC_BookStation.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ClienteViewModel, Clientes>();
        }
    }
}