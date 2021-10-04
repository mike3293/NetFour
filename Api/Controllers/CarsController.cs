using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly Data _context;


        public CarsController(Data context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] int? providerId = null)
        {
            var baseQuery = _context.Cars.Include(c => c.Provider);

            if (providerId is null)
            {
                return await baseQuery.ToListAsync();
            }

            return await baseQuery.Where(c => c.ProviderId == providerId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.Include(c => c.Provider).FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!_context.Cars.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                if (_context.Cars.Any(e => e.RegNumber == car.RegNumber))
                {
                    ModelState.TryAddModelError(nameof(Car.RegNumber), "Registration number is not unique.");

                    return BadRequest(ModelState);
                }
            }
            catch
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_context.Cars.Any(e => e.RegNumber == car.RegNumber))
                {
                    ModelState.TryAddModelError(nameof(Car.RegNumber), "Registration number is not unique.");

                    return BadRequest(ModelState);
                }
                else
                {
                    throw;
                }
            }

            await _context.Entry(car).Reference(c => c.Provider).LoadAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
