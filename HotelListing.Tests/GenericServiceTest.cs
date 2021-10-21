using HotelListing.Data;
using HotelListing.Data.DTOs;
using HotelListing.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Tests
{
    public class GenericServiceTest
    {
        private static DbContextOptions<ApplicationContext> _options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "HotelListingDBTest")
            .Options;

        ApplicationContext _context;
        UnitOfWork _unitOfWork;
        public RequestParams requestParams;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new ApplicationContext(_options);
            _context.Database.EnsureCreated();
            //SeedDataBase();

            _unitOfWork = new UnitOfWork(_context);
            requestParams = new()
            {
                PageNumber = 2,
                PageSize = 5
            };
        }

        [Test]
        public async Task GetCountries()
        {
            var result = await _unitOfWork.Countries.GetAll();

            Assert.That(result.Count, Is.EqualTo(10));
        }

        [Test]
        public async Task GetCountries_WithPaging()
        {
            var result = await _unitOfWork.Countries.GetAll(requestParams);

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.PageNumber == 2);
        }

        [Test]
        public async Task GetCountryById()
        {
            var result = await _unitOfWork.Countries.Get(c => c.Id == 3);

            Assert.That(result.Name, Is.EqualTo("Algeria"));
        }

        [Test]
        public async Task GetCountriesSortByNameDescending()
        {
            var result = await _unitOfWork.Countries.GetAll(null, q => q.OrderByDescending(c => c.Name) , null);

            Assert.That(result.Count, Is.EqualTo(10));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Austria"));
        }

        [Test]
        public async Task GetCountriesWithHotels()
        {
            var result = await _unitOfWork.Countries.GetAll(new RequestParams { PageNumber = 1, PageSize = 5 }, new List<string> { "Hotels" });

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result[4].Hotels.Count, Is.EqualTo(2));
        }

        [OneTimeTearDown]
        public void CleanUp() => _context.Database.EnsureDeleted();

        private void SeedDataBase()
        {
            var hasher = new PasswordHasher<User>();
            var users = new List<User>
            {
                new User
                {
                    Id = "C6E566D4-5044-4C2B-96E1-1B555BE5DCE1",
                    FirstName = "Test",
                    LastName = "One",
                    Email = "TestOne@mail.com",
                    UserName = "TestOne@mail.com",
                    PhoneNumber = "123456789",
                    PasswordHash = hasher.HashPassword(null, "Abc1234")
                },
                new User
                {
                    Id = "C4CD9C82-D012-4B49-A4BA-4036BDF5C0E5",
                    FirstName = "Test",
                    LastName = "Two",
                    Email = "TestTwo@mail.com",
                    UserName = "TestTwo@mail.com",
                    PhoneNumber = "123456789",
                    PasswordHash = hasher.HashPassword(null, "Abc1234")
                }
            };

            _context.Users.AddRange(users);

            var userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = "16ee2838-79a4-4e16-be22-cf5cf05accf0",
                    UserId = "C6E566D4-5044-4C2B-96E1-1B555BE5DCE1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "dd8c984c-2c2e-4a8a-b964-32ee3642382e",
                    UserId = "C4CD9C82-D012-4B49-A4BA-4036BDF5C0E5"
                }
            };

            _context.SaveChanges();
        }
    }
}