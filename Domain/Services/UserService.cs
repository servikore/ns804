using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{    
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly INotificationFactory notificationFactory;        

        public UserService(IUserRepository userRepository, INotificationFactory notificationFactory)
        {
            this.userRepository = userRepository;
            this.notificationFactory = notificationFactory;            
        }

        private void Validate(User user)
        {
            if (string.IsNullOrEmpty(user.Email))
                throw new DomainException("Email is required");

            if (string.IsNullOrEmpty(user.FirstName))
                throw new DomainException("FirstName is required");

            if (string.IsNullOrEmpty(user.LastName))
                throw new DomainException("LastName is required");
        }

        public async Task<User> Register(User user)
        {
            if (user.Id > 0)
                throw new DomainException("Invalid Id {user.Id}.");

            if (string.IsNullOrEmpty(user.Password))
                throw new DomainException("Password is required");

            this.Validate(user);
            
            user.IsBlocked = false;
            user.CreateOn = System.DateTime.UtcNow;

            await this.userRepository.Insert(user);
            await this.userRepository.Save();

            await this.notificationFactory.GetNotificationService(NotificationTypes.Email)
                .Notifify(new NotificationOption 
                {
                    To = user.Email,
                    Message = "Your user has been created."
                });

            return user;
        }

        public async Task Block(int userId)
        {
            var user = await this.userRepository.GetById(userId);

            if (user == null)
                throw new DomainException("User was not found: {userId}.");

            user.IsBlocked = true;
            await this.userRepository.Update(user);
        }

        public Task<User> FindById(int id)
        {
            return this.userRepository.GetById(id);
        }

        public Task<User> FindByEmail(string email)
        {
            return this.userRepository.GetByEmail(email);
        }

        public Task<List<User>> GetAll()
        {
            return this.userRepository.GetAll();
        }

        public async Task Modify(User user)
        {
            if (user.Id < 1)
                throw new DomainException($"Invalid User Id {user.Id}.");
            
            Validate(user);
            
            var usr = await this.userRepository.GetById(user.Id);
            
            if (usr == null)
                throw new DomainException("User was not found.");

            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            usr.Address = user.Address ?? usr.Address;

            await this.userRepository.Update(usr);
        }

        
    }
}
