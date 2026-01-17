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
    public class MemberService : IMemberService
    {


        //private readonly IGenericRepository<Member> _memberRepository;

        //private readonly IGenericRepository<Membership> _memberShipRepository;
        //private readonly IPlanRepository _planRepository;

        //private readonly IGenericRepository<HealthRecord> _healthRecordRepository;
        //private readonly IGenericRepository<MemberSession> _memberSessionRepository;





        //public MemberService(IGenericRepository<Member> memberRepository 
        //                   , IGenericRepository<Membership> memberShipRepository
        //                   , IPlanRepository planRepository
        //                   , IGenericRepository<HealthRecord> healthRecordRepository
        //                   , IGenericRepository<MemberSession> memberSessionRepository)
        //{
        //    _memberRepository = memberRepository;
        //    _memberShipRepository = memberShipRepository;
        //    _planRepository = planRepository;
        //    _healthRecordRepository = healthRecordRepository;
        //    _memberSessionRepository = memberSessionRepository;
        //}



        // After Implementing Unit of work we don't need to inject all these repositories , onlu unit of work will handle.this.
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll();
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
                //// Check If Email Exists.
                //var EmailExists = _memberRepository.GetAll(x => x.Email == createMember.Email).Any();

                //// Check If Phone Exists
                //var PhoneExists = _memberRepository.GetAll(x => x.Phone == createMember.Phone).Any();

                // If One Of Them Exists.
                if (IsEmailExists(createMember.Email) || IsPhoneExists(createMember.Phone))
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
                 _unitOfWork.GetRepository<Member>().Add(Member) ; // Added Locally.
                return _unitOfWork.SaveChanges() > 0; //  Save TO Database.


            }
            catch (Exception )
            {
                return false;
            }
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member == null) return null;
            
            var ViewModel = new MemberViewModel() 
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.Phone,
                Gender = Member.Gender.ToString(),
                DateOfBirth = Member.DateOfBirth,
                Address = $"{Member.Address.BuildingNumber} - {Member.Address.Street} - {Member.Address.City}",
                Photo = Member.Photo,
            };

            // Get The Active MemberShip.
            var ActiveMemberShip = _unitOfWork.GetRepository<Membership>().GetAll(x => x.Id == MemberId && x.Status == "Active").FirstOrDefault();
            if(ActiveMemberShip is not null)
            {
                ViewModel.MemberShipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                ViewModel.MemberShipEndDate = ActiveMemberShip.EndDate.ToShortDateString();

                var Plan = _unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);

                ViewModel.PlanName = Plan?.Name;
            }

            return ViewModel;

            
        }

        public HealthRecordViewModel? GetMemberHealthDetails(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if(MemberHealthRecord == null) return null;

            var HealthViewModel = new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Height = MemberHealthRecord.Height,
                Notes = MemberHealthRecord.Notes,
                Weight = MemberHealthRecord.Weight


            };

            return HealthViewModel;
        }

        // This Service to Show The Data Only.
        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if(Member == null) return null;

            return new MemberToUpdateViewModel()
            {
                Name = Member.Name,
                Photo = Member.Photo,
                Email = Member.Email,
                Phone = Member.Phone,
                BuildingNumber = Member.Address.BuildingNumber,
                Street = Member.Address.Street,
                City = Member.Address.City  
            };

        }

        // This Service to update The Data.
        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                //var EmailExists = _memberRepository.GetAll(x=>x.Email == UpdatedMember.Email).Any();

                //var PhoneExists = _memberRepository.GetAll(x => x.Phone == UpdatedMember.Phone).Any();

                //if(IsEmailExists(UpdatedMember.Email) || IsPhoneExists(UpdatedMember.Phone)) return false;

                var EmailExists = _unitOfWork.GetRepository<Member>()
                                            .GetAll(x => x.Email == UpdatedMember.Email && x.Id == Id);

                var PhoneExists = _unitOfWork.GetRepository<Member>()
                                            .GetAll(x => x.Phone == UpdatedMember.Phone && x.Id == Id);

                if(EmailExists.Any() && PhoneExists.Any())return  false;

                var Member = _unitOfWork.GetRepository<Member>().GetById(Id);
                if(Member == null) return false;

                Member.Name = UpdatedMember.Name;
                Member.Photo = UpdatedMember.Photo;
                Member.Email = UpdatedMember.Email;
                Member.Phone = UpdatedMember.Phone;
                Member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                Member.Address.Street = UpdatedMember.Street;
                Member.Address.City = UpdatedMember.City;
                Member.UpdatedAt = DateTime.Now;

                 _unitOfWork.GetRepository<Member>().Update(Member) ; // Updated Locally.
                return _unitOfWork.SaveChanges() > 0; //  Updated To Database.


            }
            catch (Exception) 
            { 
                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if(Member == null) return false;

            var HasActiveMemberSessions = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(x=>x.MemberId == MemberId && x.Session.StartDate > DateTime.Now).Any();

            if(HasActiveMemberSessions) return false;

            var MemberShips = _unitOfWork.GetRepository<MemberSession>().GetAll(x => x.MemberId == MemberId);
            try
            {
                if (MemberShips.Any())
                {
                    foreach (var memberShip in MemberShips)
                    {
                        _unitOfWork.GetRepository<MemberSession>().Delete(memberShip);
                    }

                    
                }
                 _unitOfWork.GetRepository<Member>().Delete(Member); // Deleted Locally.
                return _unitOfWork.SaveChanges() > 0; // Deleted From Database.
            }
            catch
            {
                return false;
            }

        }




        #region Helper Method

        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x=>x.Email == email).Any();
        }


        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }

        
        #endregion



    }
}
