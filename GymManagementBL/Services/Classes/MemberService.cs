using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;

        public MemberService(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _memberRepository.GetAll();
            if(Members is null || !Members.Any()){
                return  Enumerable.Empty<MemberViewModel>();
            }

            #region Manual Mapping Way01
            //var MemberViewModel = new List<MemberViewModel>();
            //foreach (var Member in Members) 
            //{
            //    var memberviewmodel = new MemberViewModel() 
            //    {
            //        Id = Member.Id,
            //        Name = Member.Name,
            //        Photo = Member.Photo,
            //        Email = Member.Email,
            //        PhoneNumber = Member.Phone,
            //        Gender = Member.Gender.ToString(),

            //    };
            //    MemberViewModels.Add(memberviewmodel);

            //} 
            #endregion

            #region Manual Mapping Way02

            var MemberViewModels = Members.Select(x => new MemberViewModel 
            {   
                Id = x.Id,
                Name = x.Name,
                Photo = x.Photo,
                Phone = x.Phone,
                Gender = x.Gender.ToString(),

            });


            #endregion
            return MemberViewModels;
            
        }
    }
}
