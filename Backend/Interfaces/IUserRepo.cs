using Core;
namespace Backend.Repository.Interfaces;

public interface IUserRepo
{
    public User Login(string username);
}