namespace CrudOperationsInNetCore.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperationsInNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly BrandContext _dbContext;

    public BrandController(BrandContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/values
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
    {
        if (_dbContext.Brands == null)
        {
            return NotFound();
        }
            
        return await _dbContext.Brands.ToListAsync();
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrandById(int id)
    {
        if (_dbContext.Brands == null)
        {
            return NotFound();
        }

        var brand = await _dbContext.Brands.FindAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        return brand;
    }

    // POST api/values
    [HttpPost]
    public async Task<ActionResult<Brand>> PostBrand(Brand brand)
    {
        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBrandById), new { id = brand.ID }, brand);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task<ActionResult> PutBrand(int id, Brand brand)
    {
        if (id != brand.ID)
        {
            return BadRequest();
        }

        _dbContext.Entry(brand).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if (!BrandAvailable(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok();
    }

    private bool BrandAvailable(int id)
    {
        return (_dbContext.Brands?.Any(x => x.ID == id)).GetValueOrDefault();
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        if (_dbContext.Brands == null)
        {
            return NotFound();
        }

        var brand = await _dbContext.Brands.FindAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        _dbContext.Brands.Remove(brand);

        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
