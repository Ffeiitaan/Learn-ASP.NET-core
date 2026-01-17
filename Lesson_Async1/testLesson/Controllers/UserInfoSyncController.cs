namespace TestLesson.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TestLesson.Models;

[ApiController]
[Route("api/[controller]")]
public class UserInfoSyncController : ControllerBase
{
    private const string USERS_FILE_PATH = "Data/Users.json";
    private const string LOCATIONS_FILE_PATH = "Data/Location.json";
    private const string GAMES_FILE_PATH = "Data/Games.json";

    [HttpGet("user-info")]
    public ActionResult GetUserInfo()
    {
        var userId = GetRandomUserIdSync();

        var location = GetUserLocationSync(userId);

        var game = GetUserFavoriteGameSync(userId);

        return Ok(new { userId, location, game });
    }

    private int GetRandomUserIdSync()
    {
        Console.WriteLine("Отримання користувача");
        var userJson = System.IO.File.ReadAllText(USERS_FILE_PATH);
        Task.Delay(1000).Wait();

        Console.WriteLine("Користувачів отриманно");

        var userData = JsonSerializer.Deserialize<UserData>(userJson) 
            ?? throw new NullReferenceException();

        return userData.Users.First().Id;
    }
    private string GetUserLocationSync(int userId)
    {
        Console.WriteLine("отримання локації");
        var locationJson = System.IO.File.ReadAllText(LOCATIONS_FILE_PATH);
        Task.Delay(2000).Wait();

        Console.WriteLine("Локацію отриманно");

        var locationData = JsonSerializer.Deserialize<LocationData>(locationJson) 
            ?? throw new NullReferenceException();

        return locationData.Locations.First(l => l.UserId == userId).LocationName;
    }

    private string GetUserFavoriteGameSync(int userId)
    {
        Console.WriteLine("отримання гри");
        var gamesJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
        Task.Delay(2000).Wait();

        Console.WriteLine("Гру отриманно");

        var gameData = JsonSerializer.Deserialize<GamesData>(gamesJson) 
            ?? throw new NullReferenceException();

        return gameData.Games.First(g => g.UserId == userId).FavoriteGame;
    }
}