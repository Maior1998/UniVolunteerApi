using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniVolunteerApi.Repositories;
using UniVolunteerApi.Services;
using Moq;

namespace UniVolunteerApi.Moq
{
    class UniUserRolesControllerTests
    {
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        private readonly IUniVolunteerSession _session;
    }
}
