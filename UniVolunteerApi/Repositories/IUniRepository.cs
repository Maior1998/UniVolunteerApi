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
        #endregion

        #region UniUsers
        IEnumerable<User> GetAllUsers();
        User GetUser(Guid id);
        Task<User> GetUserAsync(string login);
        Task<User> CreateUserAsync(User createUser);
        Task UpdateUser(User updatingUser);
        Task DeleteUser(Guid id);
        #endregion
    }
}
