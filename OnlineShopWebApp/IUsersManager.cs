using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IUsersManager
    {
        void Add(UserAccount user);
        List<UserAccount> GetAll();
        UserAccount TryGetByName(string name);
        void ChangePassword(string username, string newPassword);
    }
}