using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniVolunteerApi.Repositories;
using UniVolunteerApi.Services;
using Moq;
using UniVolunteerApi.Controllers;
using Xunit;

namespace UniVolunteerApi.Moq
{
    public class UniUserRolesControllerTests
    {
        private readonly UniUserRolesController _uniUserRolesController;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        private readonly Mock<IUniVolunteerSession> _session = new Mock<IUniVolunteerSession>();

        public UniUserRolesControllerTests()
        {
            _uniUserRolesController = new UniUserRolesController(_repository.Object, _session.Object);
        }

        [Fact]
        public void CreateUserRoleTest()
        {

        }
    }
}
