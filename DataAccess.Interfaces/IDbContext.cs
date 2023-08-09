using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Interfaces
{
    public interface IDbContext
    {
        DbSet<Order> Orders { get;  }
        DbSet<Product> Products { get; }
        DbSet<OrderItem> OrderItems { get;  }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}