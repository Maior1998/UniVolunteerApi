using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.Model.DTOs.Requests;
using UniVolunteerApi.Model.DTOs.Responses;

using UniVolunteerDbModel.Model;

namespace UniVolunteerApi
{
    /// <summary>
    /// Класс для различного рода расширений над объектами системы и прочих полезных функций.
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Извлекает мероприятие из объекта создания.
        /// </summary>
        /// <param name="source">Объект, содержащий в себе данные, необходимые для создания мероприятия.</param>
        /// <returns>Извлеченное мероприятие</returns>
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

        /// <summary>
        /// Прозводит перевод указанного события в сущность переноса данных объекта (DTO)
        /// </summary>
        /// <param name="source">Исходный объект, который необходимо переконвертировать.</param>
        /// <returns>Объект переноса данных события вуза. (DTO)</returns>
        public static UniEventDto ConvertToUniEventDto(this UniEvent source)
        {
            if (source == null) return null;
            return new()
            {
                Id = source.Id,
                CreatedOn = source.CreatedOn,
                CreatedById = source.CreatedById,
                ModifiedOn = source.ModifiedOn,
                ModifiedById = source.ModifiedById,
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
                RegisteredOn = source.RegisteredOn,
                CreatedOn = source.RegisteredOn,
                FullName = source.FullName,
                RoleId = source.RoleId
            };

        }

        public static UserRoleDto ConvertToUserRoleDto(this UserRole source)
        {
            return new()
            {
                Id = source.Id,
                Access = source.Access,
                CreatedOn = source.CreatedOn,
                Name = source.Name
            };
        }

        public static UserRole ConvertToUserRole(this CreateUserRoleDto source)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Name = source.Name
            };
        }

        

        /// <summary>
        /// Выполняет проверку пароля пользователя.
        /// </summary>
        /// <param name="user">Пользователь, пароль которого необходимо проверить.</param>
        /// <param name="pass">Пароль, который необходимо проверить.</param>
        /// <returns>Результат проверки пароля.</returns>
        public static bool CheckPass(this User user, string pass)
        {
            string hash = SaltHelper.GetHash(pass, user.Salt);
            return hash == user.PasswordHash;
        }
    }
}
