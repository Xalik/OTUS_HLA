using Npgsql;
using OtusSocNet.DAL.Repositories.Interfaces;
using OtusSocNet.Dtos;

namespace OtusSocNet.DAL.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    private readonly NpgsqlConnection connection;
    
    public async Task<User?> GetAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            const string sql = @"select u.id,
       u.first_name,
       u.second_name,
       u.birth_date,
       u.is_male,
       u.biography,
       u.city,
       u.token,
       u.token_expires,
       u.password_hash,
       u.salt
from users u
where u.id = @userId;";
            await connection.OpenAsync(cancellationToken);
            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters = { new("userId", userId) }
            };
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (!await reader.ReadAsync(cancellationToken))
                return null;

            return new User
            {
                Id = userId,
                FirstName = reader.GetString(1),
                SecondName = reader.GetString(2),
                BirthDate = DateOnly.FromDateTime(reader.GetDateTime(3)),
                IsMale = reader.GetBoolean(4),
                Biography = reader.GetString(5),
                City = reader.GetString(6),
                Token = reader.GetString(7),
                TokenExpires = reader.GetDateTime(8),
                PasswordHash = reader.GetFieldValue<byte[]>(9),
                Salt = reader.GetFieldValue<byte[]>(10)
            };
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task UpdateTokenAsync(string userId, string userToken, int tokenLifetimeInDays, CancellationToken cancellationToken = default)
    {
        try
        {
            const string sql = @"update users
set token = @token, token_expires = @tokenExpires
where id = @userId;";
            await connection.OpenAsync(cancellationToken);
            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters =
                {
                    new("userId", userId),
                    new("token", userToken),
                    new("tokenExpires", DateTime.UtcNow.AddDays(tokenLifetimeInDays))
                }
            };
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            const string sql = @"insert into users (id, first_name, second_name, birth_date, is_male, biography, city, token, token_expires, password_hash, salt) 
values (@id, @firstName, @secondName, @birthDate, @isMale, @biography, @city, @token, @tokenExpires, @passwordHash, @salt);";
            await connection.OpenAsync(cancellationToken);
            await using var command = new NpgsqlCommand(sql, connection)
            {
                Parameters =
                {
                    new("id", user.Id),
                    new("firstName", user.FirstName),
                    new("secondName", user.SecondName),
                    new("birthDate", user.BirthDate),
                    new("isMale", user.IsMale),
                    new("biography", user.Biography),
                    new("city", user.City),
                    new("token", user.Token),
                    new("tokenExpires", user.TokenExpires),
                    new("passwordHash", user.PasswordHash),
                    new("salt", user.Salt),
                }
            };
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}