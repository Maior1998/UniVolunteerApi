using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
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
                CreatedOn = source.CreatedOn,
                Name = source.Name,
                Place = source.Place,
                StartTime = source.StartTime
            };

        }

        public static User ConvertToUser(this CreateUserDto source)
        {
            if (source == null) return null;
            string salt = SaltHelper.GenerateSalt();
            string hash = SaltHelper.GetHash(source.Password, salt);
            return new()
            {
                FullName = source.FullName,
                Login = source.Login,
                PasswordHash = hash,
                Salt = salt,
                RoleId = source.RoleId,
            };
        }

        public static UserDto ConvertToUserDto(this User source)
        {
            if (source == null) return null;
            return new()
            {
                Id = source.Id,
                CreatedOn = source.CreatedOn,
                FullName = source.FullName,
                RoleId = source.RoleId
            };

        }
    }
}
