using AutoMapper;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, Options =>
                {
                    Options.MapFrom(src => src.SessionCategory.CategoryName);
                })
                .ForMember(dest => dest.TrainerName, Options =>
                {
                    Options.MapFrom(src => src.SessionTrainer.Name);
                })
                .ForMember(dest => dest.AvaliableSlots, Options =>
                {
                    Options.Ignore();
                });

            CreateMap<SessionViewModel, Session>();

            CreateMap<Session , UpdateSessionViewModel>();
        }
    }
}
