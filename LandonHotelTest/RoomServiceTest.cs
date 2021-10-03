using System;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;
using FluentAssertions;
using LandonHotel.Data;
using LandonHotel.Repositories;
using LandonHotel.Services;

namespace LandonHotelTest
{
    public class RoomServiceTest
    {
        private readonly IRoomsRepository roomRepo;
        private IRoomService roomService;
        public RoomServiceTest()
        {
            roomRepo = A.Fake<IRoomsRepository>();
            var rooms = new List<Room>
            {
                new Room
                {
                    ArePetsAllowed = true,
                    Bookings = new List<Booking>(),
                    Capacity = 17,
                    Id = 444,
                    Name = "Give Me Room plz",
                    Rate = int.MaxValue
                }
            };
            A.CallTo(() => roomRepo.GetRooms()).Returns(rooms);
            roomService = new RoomService(roomRepo);
        }

        [Fact]
        public void Should_GetRoomsFromRoomRepo_When_GetRoomsIsCalled()
        {
            var roomsRetrieved = roomService.GetAllRooms();

            A.CallTo(() => roomRepo.GetRooms()).MustHaveHappened();

            roomsRetrieved.Should().ContainSingle();
        }

        [Fact]
        public void Should_NotManipulateRoomCollection_When_GettingRooms()
        {
            roomService.GetAllRooms();
            A.CallTo(() => roomRepo.DeleteRooms()).MustNotHaveHappened();
        }
    }
}
