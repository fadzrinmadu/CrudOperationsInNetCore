namespace CrudOperationsInNetCore.Models;

using System;
using CrudOperationsInNetCore.Controllers;
using Microsoft.EntityFrameworkCore;

public class BrandContext: DbContext
{
	public BrandContext(DbContextOptions<BrandContext> options): base(options)
	{
			
	}

	public DbSet<Brand> Brands { get; set; }

    public static implicit operator BrandContext(BrandController v)
    {
        throw new NotImplementedException();
    }
}

