using GymManagementBLL.Services.Interfcaes;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            if (Trainers == null || !Trainers.Any())
                return [];

            var TrainerViewModels = Trainers.Select(x=>new TrainerViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialization = x.Specialties.ToString()
            });
            return TrainerViewModels;
        }

        public TrainerViewModel? GetTrainerDetails(int TrainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);

            if (Trainer == null) return null!;

            var GetTrainerDetailsViewModel = new TrainerViewModel()
            {
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                DateofBirth = Trainer.DateOfBirth.ToShortDateString(),
                Address = $"{Trainer.Address.BuildingNumber} - {Trainer.Address.Street} - {Trainer.Address.City}"
            };

            return GetTrainerDetailsViewModel;
        }


        public bool CreateTrainer(CreateTrainerViewModel CreateTrainer)
        {
            try
            {
                if (IsEmailExists(CreateTrainer.Email) || IsPhoneExists(CreateTrainer.Phone))
                    return false;

                var Trainer = new Trainer()
                {
                    Name = CreateTrainer.Name,
                    Email = CreateTrainer.Email,
                    Phone = CreateTrainer.Phone,
                    DateOfBirth = CreateTrainer.DateOfBirth,
                    Gender = CreateTrainer.Gender,
                    Address = new Address()
                    {
                        BuildingNumber = CreateTrainer.BuildingNumber,
                        City = CreateTrainer.City,
                        Street = CreateTrainer.Street,
                    },

                };

                _unitOfWork.GetRepository<Trainer>().Add(Trainer); // Added Locally.

                return _unitOfWork.SaveChanges() > 0; // Added To Database.
            }
            catch
            {
                return false;
            }


            
        }



        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var UpdateTrainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);

            if (UpdateTrainer == null) return null;

            return new TrainerToUpdateViewModel()
            {
                 Name = UpdateTrainer.Name,
                 Email = UpdateTrainer.Email,
                 Phone = UpdateTrainer.Phone,
                 BuildingNumber = UpdateTrainer.Address.BuildingNumber,
                 Street = UpdateTrainer.Address.Street,
                 City = UpdateTrainer.Address.City,

            };


        }


        public bool UpdateTrainerDetails(int Id, TrainerToUpdateViewModel UpdateTrainer)
        {
            try
            {
                if (IsEmailExists(UpdateTrainer.Email) || IsPhoneExists(UpdateTrainer.Phone))
                {
                    return false;
                }

                var OldMember = _unitOfWork.GetRepository<Trainer>().GetById(Id);

                if (OldMember == null) return false;

                OldMember.Name = UpdateTrainer.Name;
                OldMember.Email = UpdateTrainer.Email;
                OldMember.Phone = UpdateTrainer.Phone;
                OldMember.Address.BuildingNumber = UpdateTrainer.BuildingNumber;
                OldMember.Address.Street = UpdateTrainer.Street;
                OldMember.Address.City = UpdateTrainer.City;
                OldMember.Specialties = UpdateTrainer.Specialties;
                OldMember.UpdatedAt = DateTime.Now;

                _unitOfWork.GetRepository<Trainer>().Update(OldMember); // Updated Locally.

                return _unitOfWork.SaveChanges() > 0; // Updated to Database.
            }
            catch
            {
                return false; 
            }


        }


        public bool RemoveTrainer(int TrainerId)
        {
            var Member = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Member == null) return false;

            var HasTrainerSessions = _unitOfWork.GetRepository<Session>()
                .GetAll(x=>x.TrainerId == TrainerId && x.StartDate > DateTime.Now).Any();
            if(Member == null || HasTrainerSessions) return false;

            _unitOfWork.GetRepository<Trainer>().Delete(Member);

            return _unitOfWork.SaveChanges() > 0;

        }







        #region Helper Methods
        private bool IsEmailExists(string Email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == Email).Any();
        }

        private bool IsPhoneExists(string Phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x=>x.Phone == Phone).Any();
        }

        





        #endregion
    }
}
