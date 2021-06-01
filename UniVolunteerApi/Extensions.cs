using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using UniVolunteerApi.Dtos;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi
{
    public static class Extensions
    {
        /// <summary>
        /// Прозводит перевод указанного события в сущность переноса данных объекта (DTO)
        /// </summary>
        /// <param name="source">Исходный объект, который необходимо переконвертировать.</param>
        /// <returns>Объект переноса данных события вуза. (DTO)</returns>
        public static UniEvent ConvertToUniEvent(this CreateUniEventDto source)
        {
            if (source == null) return null;
            return new()
            {

                Name = source.Name,
                Place = source.Place,
                StartTime = source.StartTime
            };
        }

        public static UniEventDto ConvertToUniEventDto(this UniEvent source)
        {
            if (source == null) return null;
            return new()
            {
                Id = source.Id,
                CreatedAt = source.CreatedAt,
                Name = source.Name,
                Place = source.Place,
                StartTime = source.StartTime
            };

        }
    }
}
