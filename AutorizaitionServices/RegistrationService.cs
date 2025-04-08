using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace FoxVill.AutorizaitionServices;

public static class RegistrationService
{
    /// <summary>
    /// Add new user in database.
    /// return true, where adding was successful, otherwise return false
    /// </summary>
    /// <param name="newUser"></param>
    /// <returns></returns>
    public static async Task<bool> AddUserToDatabase(User newUser)
    {
        ArgumentNullException.ThrowIfNull(newUser);

        using DatabaseContext context = new();

        context.Users.Load();

        var users = context.Users.Local.ToList();

        if (users.Any(u => u.Email == newUser.Email))
        {
            MessageBox.Show("Такой пользователь уже существует!", "Внимание!");
            return false;
        }
        else
        {
            await context.AddAsync(newUser);
            context.SaveChanges();

            MessageBox.Show("Регистрация прошла успешно!", "Внимание!");

            return true;
        }
    }
}
