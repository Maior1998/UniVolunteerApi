using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public interface IUniRepository
    {
        #region UniEvents
        /// <summary>
        /// Получает список всех событий.
        /// </summary>
        /// <returns>Список всех событий в университете</returns>
        IEnumerable<UniEvent> GetAllEvents();

        /// <summary>
        /// Возвращает событие в университе по его Id.
        /// </summary>
        /// <param name="id">Id события.</param>
        /// <returns>Событие в университе или null, если событие не найдено.</returns>
        UniEvent GetEvent(Guid id);

        /// <summary>
        /// Создает новое событие.
        /// </summary>
        /// <param name="createUniEvent">Событие, которое необходимо создать.</param>
        /// <returns></returns>
        UniEvent CreateUniEvent(UniEvent createUniEvent);

        void UpdateUniEvent(UniEvent updatingUniEvent);
        void DeleteUniEvent(Guid id);
        Task EnsureUserEnrolledToEvent(Guid userId, Guid eventId);
        Task EnsureUserExitedFromEvent(Guid userId, Guid eventId);
        Task<IEnumerable<UniEvent>> GetUserParticipatedInEvents(Guid userId);
        #endregion

        #region UniUsers
        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserAsync(string login);
        Task<User> CreateUserAsync(User createUser);
        Task UpdateUserAsync(User updatingUser);
        Task DeleteUserAsync(Guid id);
        #endregion

        Task AddRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task UpdateRefreshTokenAsync(RefreshToken token);
        
    }
}
