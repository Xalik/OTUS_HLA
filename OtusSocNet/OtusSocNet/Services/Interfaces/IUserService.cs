using OtusSocNet.Dtos;

namespace OtusSocNet.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="password">Пароль</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Токен пользователя</returns>
    Task<string> LoginUserAsync(Guid userId, string password, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <param name="parameters">Параметры</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор пользователя</returns>
    Task<Guid> RegisterUserAsync(RegisterParameters parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь</returns>
    Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}