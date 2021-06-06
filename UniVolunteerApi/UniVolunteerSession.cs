using System;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi
{
    public class UniVolunteerSession
    {
        public User CurrentSessionUser { get; set; }
    }
}
