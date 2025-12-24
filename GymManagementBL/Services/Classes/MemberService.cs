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


        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                // Check If Email Exists.
                var EmailExists = _memberRepository.GetAll(x => x.Email == createMember.Email).Any();

                // Check If Phone Exists
                var PhoneExists = _memberRepository.GetAll(x => x.Phone == createMember.Phone).Any();

                // If One Of Them Exists.
                if (EmailExists || PhoneExists)
                {
                    return false;
                }

                // If Not Add Member and Return True If Added.
                var Member = new Member()
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        Street = createMember.Street,
                        City = createMember.City,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthRecordViewModel.Height,
                        Weight = createMember.HealthRecordViewModel.Weight,
                        BloodType = createMember.HealthRecordViewModel.BloodType,
                        Notes = createMember.HealthRecordViewModel.Notes
                    },
                };

                // Add the new member.
                return _memberRepository.Add(Member) > 0;

            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}
