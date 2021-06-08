using System;
using Xunit;

using UniVolunteerApi.Repositories;
using UniVolunteerApi.Controllers;
using UniVolunteerDbModel.Model;
using UniVolunteerApi.DTOs.Responses;
using UniVolunteerApi.DTOs.Requests;

using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace UniVolunteerApi.Moq
{
    public class UniEventsControllerTests
    {
        private readonly UniEventsController _uniEvents;
        private readonly Mock<IUniRepository> _repository = new Mock<IUniRepository>();

        public UniEventsControllerTests()
        {
            _uniEvents = new UniEventsController(_repository.Object);
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

            //Assert
            Assert.NotNull(eventResult);
            Assert.Equal(guid, eventResult.Value.Id);
            Assert.Equal(name, eventResult.Value.Name);
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
        }

        [Fact]
        public void CreateUniEventTest()
        {
            //Arange
            string nameNewEvent = "";
            string placeNewEvent = "";
            DateTime startTimeNewEvent = new DateTime(2021, 7, 9);
            CreateUniEventDto eventDto = new CreateUniEventDto() { Name = nameNewEvent, Place = placeNewEvent, StartTime = startTimeNewEvent };
            //UniEvent newEvent = new UniEvent() { Name = nameNewEvent, Place = placeNewEvent, StartTime = startTimeNewEvent };
            
            _repository.Setup(x => x.CreateUniEvent(eventDto.ConvertToUniEvent())).Returns(eventDto.ConvertToUniEvent());


            //Act
            ActionResult<UniEventDto> buf = _uniEvents.CreateUniEvent(eventDto);

            //Assert
            //Assert.Equal(newEvent, buf.Result.Value);
            //Assert.NotNull(buf.Result.ActionName);

        }
    }
}
