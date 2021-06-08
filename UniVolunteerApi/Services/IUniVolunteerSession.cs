using System;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Services
{
    public interface IUniVolunteerSession
    {
        public User CurrentSessionUser { get; }
    }
}
