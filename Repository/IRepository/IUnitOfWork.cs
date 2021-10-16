﻿using System;
using System.Threading.Tasks;
using HotelListing.Data.Entities;

namespace Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        Task Save();
    }
}