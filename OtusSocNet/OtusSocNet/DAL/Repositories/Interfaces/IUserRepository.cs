using OtusSocNet.Dtos;

namespace OtusSocNet.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь</returns>
    Task<User?> GetAsync(string userId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Обновить токен пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="userToken">Токен пользовтеля</param>
    /// <param name="tokenLifetimeInDays">Время жизни токена в днях</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UpdateTokenAsync(string userId, string userToken, int tokenLifetimeInDays, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
}