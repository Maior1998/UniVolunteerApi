using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using UniVolunteerApi.Dtos;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Repositories
{
    public interface IUniVolunteerRepository
    {
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

    }
}
