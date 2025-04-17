using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;

namespace FoxVill.AutorizaitionServices;

public static class AuthorizationService
{
    /// <summary>
    /// Сhecks if the given user is in the database and returns the result
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static async Task<bool> AutorizateUser(User user, DatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(user);

        await context.Users.LoadAsync();

        var users = context.Users.Local.ToList();

        return users.Any(u => u.Email == user.Email && u.Password == user.Password);
    }
}
