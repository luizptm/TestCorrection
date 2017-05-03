using AutoMapper;
using TestCorrection.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Mappers
{
	//http://eduardopires.net.br/2013/08/asp-net-mvc-utilizando-automapper-com-view-model-pattern/
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
    }
}