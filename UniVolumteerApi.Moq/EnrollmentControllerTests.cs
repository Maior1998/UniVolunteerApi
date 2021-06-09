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
using UniVolunteerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using UniVolunteerApi.DTOs.Responses;

namespace UniVolunteerApi.Moq
{
    public class EnrollmentControllerTests
    {
        private readonly EnrollmentController _enrollmentController;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        private readonly Mock<IUniVolunteerSession> _session = new Mock<IUniVolunteerSession>();
        private readonly Guid sessionUserId = Guid.NewGuid();

        public EnrollmentControllerTests()
        {
            _enrollmentController = new EnrollmentController(_repository.Object, _session.Object);
            User currentSessionUser = new User() { Id = sessionUserId, FullName = "full name", Login = "login" };
            _session.Setup(x => x.CurrentSessionUser).Returns(currentSessionUser);
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

            _repository.Setup(x => x.GetEvent(eventId)).Returns(@event);
            //_repository.Setup(x=>x.)

            //Act
            ActionResult result = await _enrollmentController.EnrollIntoEvent(eventId); 

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);         
        }

        [Fact]
        public async Task GetEnrolledInEventsTest()
        {
            //Arange 
            Guid eventId = Guid.NewGuid();
            string nameEvent = "name event";
            IList<UniEvent> uniEvents = new List<UniEvent>{
                new UniEvent() {Id = eventId, Name=nameEvent},
                new UniEvent()
            };
            _repository.Setup(x => x.GetUserParticipatedInEvents(_session.Object.CurrentSessionUser.Id)).ReturnsAsync(uniEvents);

            //Act
            ActionResult<DTOs.Responses.UniEventDto[]> getEnrolledInEvents = await _enrollmentController.GetEnrolledInEvents();

            //Assert
            Assert.NotNull(getEnrolledInEvents);
            Assert.IsType<OkObjectResult>(getEnrolledInEvents.Result);
            Assert.Equal(nameEvent, ((IEnumerable)((OkObjectResult)getEnrolledInEvents.Result).Value).Cast<UniEventDto>().ToArray()[0].Name);
        }

        [Fact]
        public async Task ExitFromEventTest()
        {
            //Arange
            Guid eventId = Guid.NewGuid();
            UniEvent uniEvent = new UniEvent() { Id = eventId };

            _repository.Setup(x => x.GetEvent(eventId)).Returns(uniEvent);

            //Act
            ActionResult exitFromevent = await _enrollmentController.ExitFromEvent(eventId);

            //Assert
            Assert.NotNull(exitFromevent);
            Assert.IsType<NoContentResult>(exitFromevent);
        }
    }
}
