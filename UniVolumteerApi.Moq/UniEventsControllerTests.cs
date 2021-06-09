using System;
using Xunit;

using UniVolunteerApi.Repositories;
using UniVolunteerApi.Controllers;
using UniVolunteerDbModel.Model;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.DTOs.Requests;
using UniVolunteerApi.Services;

using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace UniVolunteerApi.Moq
{
    public class UniEventsControllerTests
    {
        private readonly UniEventsController _uniEvents;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();
        private readonly Mock<IUniVolunteerSession> _session = new Mock<IUniVolunteerSession>();
        private readonly Guid sessionUserId = Guid.NewGuid();

        public UniEventsControllerTests()
        {
            _uniEvents = new UniEventsController(_repository.Object, _session.Object);
            User currentSessionUser = new User() { Id = sessionUserId, FullName = "full name", Login = "login" };
            _session.Setup(x => x.CurrentSessionUser).Returns(currentSessionUser);
        }


        [Fact]
        public void GetEventByIdTest()
        {
            //Arange
            Guid guid = Guid.NewGuid();
            string name = "Event1";
            UniEvent @event = new UniEvent()
            {
                Id = guid,
                Name = name,
                Place = "Place1", 
                StartTime = new DateTime() 
            };
            _repository.Setup(x => x.GetEvent(guid)).Returns(@event);

            //Act
            ActionResult<UniEventDto> eventResult = _uniEvents.GetUniEvent(guid);
            object buf = ((OkObjectResult)eventResult.Result).Value;

            //Assert
            Assert.NotNull(eventResult);
            Assert.Equal(guid, ((UniEventDto)((OkObjectResult)eventResult.Result).Value).Id);
            Assert.Equal(name, ((UniEventDto)((OkObjectResult)eventResult.Result).Value).Name);
        }
        [Fact]
        public void GetEventByIdTest_InvalidEvent()
        {
            //Arange
           
            _repository.Setup(x => x.GetEvent(It.IsAny<Guid>())).Returns(()=>null);

            //Act
            ActionResult<UniEventDto> eventResult = _uniEvents.GetUniEvent(Guid.NewGuid());

            //Assert
            Assert.Null(eventResult.Value);
            Assert.Equal(404, ((NotFoundResult)eventResult.Result).StatusCode);
        }

        [Fact]
        public void GetAllEventsTest()
        {
            //Arange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            IList<UniEvent> events = new List<UniEvent>
            {
                new UniEvent(){Id=guid1, Name="Name1", Place="Place1", StartTime=new DateTime() },
                new UniEvent(){Id=guid2, Name="Name2", Place="Place2", StartTime=new DateTime()}
            };
            _repository.Setup(x => x.GetAllEvents()).Returns(events);

            //Act
            ActionResult<IEnumerable<UniEventDto>> eventResult = _uniEvents.GetUniEvents();

            //Assert
            Assert.NotNull(((OkObjectResult)eventResult.Result).Value);
            Assert.Equal(guid1, ((IEnumerable)(((OkObjectResult)eventResult.Result).Value)).Cast<UniEventDto>().ToArray()[0].Id);
            Assert.Equal(guid2, ((IEnumerable)(((OkObjectResult)eventResult.Result).Value)).Cast<UniEventDto>().ToArray()[1].Id);
        }

        [Fact]
        public void CreateUniEventTest()
        {
            //Arange
            string nameNewEvent = "";
            string placeNewEvent = "";
            DateTime startTimeNewEvent = new DateTime(2021, 7, 9);
            Guid eventId = Guid.NewGuid();

            //UniEvent newEvent = new UniEvent() {Id=eventId, Name = nameNewEvent, Place = placeNewEvent, StartTime = startTimeNewEvent};
            CreateUniEventDto createUniEventDto = new CreateUniEventDto() { Name = nameNewEvent, Place = placeNewEvent, StartTime = startTimeNewEvent };
            
            _repository.Setup(x => x.CreateUniEvent(It.IsAny<UniEvent>())).Returns(createUniEventDto.ConvertToUniEvent());


            //Act
            ActionResult<UniEventDto> createEvent = _uniEvents.CreateUniEvent(createUniEventDto);

            //Assert
            //Assert.Equal(newEvent, ((OkObjectResult)createEvent.Result).Value);

        }

        [Fact]
        public void UpdateUniEventTest()
        {
            //Arange
            string nameNewEvent = "new event";
            string placeNewEvent = "new place";
            DateTime startTimeNewEvent = new DateTime(2021, 7, 9);
            Guid eventId = Guid.NewGuid();

            UniEvent uniEvent = new UniEvent() { Id = eventId, Name="old event" };
            _repository.Setup(x => x.GetEvent(eventId)).Returns(uniEvent);


            UpdateUniEventDto updateUniEventDto = new UpdateUniEventDto() { Name = nameNewEvent, Place = placeNewEvent, StartTime = startTimeNewEvent };

            //Act
            ActionResult<UniEventDto> updateEvent = _uniEvents.UpdateUniEvent(eventId, updateUniEventDto);

            //Assert
            Assert.Equal(204, ((NoContentResult)updateEvent.Result).StatusCode);
        }

        [Fact]
        public void DeleteUniEventTest()
        {
            //Arange
            string nameNewEvent = "event";
            string placeNewEvent = "place";
            DateTime startTimeNewEvent = new DateTime(2021, 7, 9);
            Guid eventId = Guid.NewGuid();

            UniEvent uniEvent = new UniEvent() { Id = eventId, Name = nameNewEvent, Place = placeNewEvent, StartTime = startTimeNewEvent, CreatedById = sessionUserId };
            _repository.Setup(x => x.GetEvent(eventId)).Returns(uniEvent);


            //Act
            ActionResult deleteEvent = _uniEvents.DeleteUniEvent(eventId);

            //Assert
            Assert.Equal(204, ((NoContentResult)deleteEvent).StatusCode);
        }
    }
}
