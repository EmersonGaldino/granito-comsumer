

using AutoMapper;
using granito.consumer.api.Models.ModelView;
using granito.consumer.api.Models.ViewModel;
using granito.consumer.domain.Entity;

public class MappingProfilesModelView : Profile
{
    public MappingProfilesModelView()
    {

        CreateMap<JurosEntity, JurosViewModel>().ReverseMap();
        CreateMap<JurosEntity, JurosModelView>().ReverseMap();
        CreateMap<JurosResponse,JurosModelView>().ReverseMap();
    }
}