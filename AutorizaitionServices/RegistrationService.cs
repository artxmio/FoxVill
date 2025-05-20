using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;

namespace FoxVill.AutorizaitionServices;

public static class RegistrationService
{
    /// <summary>
    /// Add new user in database.
    /// return true, where adding was successful, otherwise return false
    /// </summary>
    /// <param name="newUser"></param>
    /// <returns></returns>
    public static async Task<bool> AddUserToDatabase(User newUser, DatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(newUser);

        await context.Users.LoadAsync();

        var users = context.Users.Local.ToList();

        if (users.Any(u => u.Email == newUser.Email))
        {
            return false;
        }
        else
        {
            var cart = new Cart()
            {
                User = newUser,
                UserId = newUser.Id
            };
            await context.AddAsync(newUser);
            await context.AddAsync(cart);
            context.SaveChanges();

            return true;
        }
    }
}
