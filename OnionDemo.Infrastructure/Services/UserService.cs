using OnionDemo.Application.Interfaces;
using OnionDemo.Domain.Entities;

namespace OnionDemo.Infrastructure.Services;

public class UserService : IUserService
{
    private static readonly List<User> _users = new();

    public IEnumerable<User> GetAll() => _users;

    public User Create(string name)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        _users.Add(user);
        return user;
    }
}
