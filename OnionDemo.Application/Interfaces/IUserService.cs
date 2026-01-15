using OnionDemo.Domain.Entities;

namespace OnionDemo.Application.Interfaces;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User Create(string name);
}
