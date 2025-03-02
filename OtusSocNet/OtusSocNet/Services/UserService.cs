using Microsoft.Extensions.Options;
using OtusSocNet.DAL.Repositories.Interfaces;
using OtusSocNet.Dtos;
using OtusSocNet.Exceptions;
using OtusSocNet.Models.Settings;
using OtusSocNet.Services.Interfaces;
using System.Security.Cryptography;

namespace OtusSocNet.Services;

public class UserService : IUserService
{
    public UserService(IOptions<ServiceSettings> serviceSettings, IUserRepository userRepository)
    {
        this.serviceSettings = serviceSettings.Value;
        this.userRepository = userRepository;
    }

    private readonly ServiceSettings serviceSettings;
    private readonly IUserRepository userRepository;
    
    public async Task<string> LoginUserAsync(string userId, string password, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(userId, cancellationToken);
        if (user is null)
            throw new UserNotFoundException(userId);

        var passwordHash = GeneratePasswordHash(password, user.Salt);
        if (!ComparePasswords(user.PasswordHash, passwordHash))
            throw new BadAuthenticationException();

        var token = user.Token;
        if (user.TokenExpires < DateTime.UtcNow.AddDays(serviceSettings.TokenLifetimeInDays))
        {
            token = GenerateToken();
            await userRepository.UpdateTokenAsync(userId, token, serviceSettings.TokenLifetimeInDays * 2, cancellationToken);
        }

        return token;
    }

    public async Task<string> RegisterUserAsync(RegisterParameters parameters, CancellationToken cancellationToken = default)
    {
        var salt = GenerateSalt();
        var passwordHash = GeneratePasswordHash(parameters.Password, salt);
        var token = GenerateToken();
        var tokenExpires = DateTime.UtcNow.AddDays(serviceSettings.TokenLifetimeInDays * 2);
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = parameters.FirstName,
            SecondName = parameters.SecondName,
            BirthDate = parameters.BirthDate,
            IsMale = parameters.IsMale,
            Biography = parameters.Biography,
            City = parameters.City,
            Token = token,
            TokenExpires = tokenExpires,
            PasswordHash = passwordHash,
            Salt = salt
        };
        await userRepository.AddUserAsync(user, cancellationToken);
        
        return user.Id;
    }

    public async Task<User> GetUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(userId, cancellationToken);
        if (user is null)
            throw new UserNotFoundException(userId);

        return user;
    }
    
    private byte[] GeneratePasswordHash(string passwordText, byte[] salt)
    {
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(passwordText);
        var algorithm = SHA256.Create();
        var plainTextWithSaltBytes = 
            new byte[passwordText.Length + salt.Length];
        for (var i = 0; i < passwordText.Length; i++)
        {
            plainTextWithSaltBytes[i] = passwordBytes[i];
        }
        for (var i = 0; i < salt.Length; i++)
        {
            plainTextWithSaltBytes[passwordText.Length + i] = salt[i];
        }

        return algorithm.ComputeHash(plainTextWithSaltBytes);            
    }
    
    
    private bool ComparePasswords(byte[] actualPwd, byte[] generatedPwd)
    {
        if (actualPwd.Length != generatedPwd.Length)
        {
            return false;
        }

        for (var i = 0; i < actualPwd.Length; i++)
        {
            if (actualPwd[i] != generatedPwd[i])
            {
                return false;
            }
        }

        return true;
    }
    
    private byte[] GenerateSalt()
    {
        var salt = new byte[10];
        new Random().NextBytes(salt);
        return salt;
    }
    
    private string GenerateToken()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, serviceSettings.TokenLength)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}