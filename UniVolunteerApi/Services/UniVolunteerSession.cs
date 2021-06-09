using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using UniVolunteerDbModel.Model;
using System.Security.Claims;
using UniVolunteerDbModel;
using Microsoft.EntityFrameworkCore;

namespace UniVolunteerApi.Services
{
    public class UniVolunteerSession : IUniVolunteerSession
    {
        private IHttpContextAccessor accessor;
        private UniVolunteerContext context;
        public UniVolunteerSession(
            IHttpContextAccessor accessor,
            UniVolunteerContext context
            )
        {
            this.accessor = accessor;
            this.context = context;
        }
        private User currentSessionUser;
        public User CurrentSessionUser
        {
            get
            {
                Claim claim = accessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "Id");
                if (claim == null)
                {
                    currentSessionUser = null;
                    return null;
                }
                Guid userId = Guid.Parse(claim.Value);
                if(currentSessionUser != null && currentSessionUser.Id == userId)
                {
                    return currentSessionUser;
                }
                currentSessionUser = context.Users
                    .Include(x => x.Role)
                    .SingleOrDefault(x => x.Id == userId);
                return currentSessionUser;
            }
        }
    }
}
