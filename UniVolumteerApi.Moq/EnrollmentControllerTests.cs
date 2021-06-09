using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;
using UniVolunteerApi.Repositories;
using UniVolunteerApi.Controllers;
using UniVolunteerDbModel.Model;

namespace UniVolunteerApi.Moq
{
    public class EnrollmentControllerTests
    {
        private readonly EnrollmentController _enrollmentController;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();

        public EnrollmentControllerTests()
        {
            _enrollmentController = new EnrollmentController(_repository.Object);
        }

        [Fact]
        public async Task GetEnrollIntoEventTest()
        {
            //Arange
            Guid eventId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string name = "Event1";
            UniEvent @event = new UniEvent()
            {
                Id = eventId,
                Name = name,
                Place = "Place1",
                StartTime = new DateTime()
            };
            //_repository.Setup(x => x.GetEvent(guid)).Returns(@event);
            //_repository.Setup(x => x.EnsureUserEnrolledToEvent(userId, eventId)).Returns(() => @event);
            _repository.Setup(x => x.GetEvent(eventId)).Returns(() => @event);

            //Act
            var result = _enrollmentController.EnrollIntoEvent(eventId); // ошибка в currentUserId
            Assert.NotNull(result.Result);

        }
    }
}
