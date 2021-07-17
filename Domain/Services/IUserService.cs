using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task Modify(User user);
        Task Block(int user);

        Task<List<User>> GetAll();
        Task<User> FindByEmail(string email);
        Task<User> FindById(int id);
    }
}
