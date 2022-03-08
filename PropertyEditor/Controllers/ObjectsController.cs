using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyEditor.Models;

namespace PropertyEditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectsController : ControllerBase
    {
        private readonly DBContext _context;

        public ObjectsController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выдает все объекты
        /// </summary>
        /// <returns>Объекты</returns>
        [HttpGet("GetObjects")]
        public IEnumerable<Models.Object> GetObjects()
        {
            return _context.Objects
                .Include(o => o.IntegerValues).ThenInclude(c => c.Property)
                .Include(o => o.StringValues).ThenInclude(c => c.Property);
        }

        /// <summary>
        /// Выдает объекты определенной категории
        /// </summary>
        /// <returns>Объекты</returns>
        [HttpGet("GetObjectsByCategory")]
        public async Task<IActionResult> GetObjectsByCategory([FromQuery] int categoryId)
        {
            var category = await _context.Category
                .Include(c => c.IntegerProperties)
                .Include(c => c.StringProperties)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            var objects = _context.Objects.Where(o => o.CategoryId == categoryId)
                .Include(o => o.IntegerValues).ThenInclude(c => c.Property)
                .Include(o => o.StringValues).ThenInclude(c => c.Property);

            var response = new
            {
                category,
                objects
            };
            return Ok(response);
        }

        /// <summary>
        /// Выдает конкретный объект
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>Объект</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @object = await _context.Objects.FindAsync(id);

            if (@object == null)
            {
                return NotFound();
            }

            return Ok(@object);
        }

        /// <summary>
        /// Добавляет объект
        /// </summary>
        /// <param name="object">Объект из body</param>
        /// <returns>Добавленный объект</returns>
        [HttpPost]
        public async Task<IActionResult> AddObject([FromBody] Models.Object @object)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Objects.Add(@object);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObject", new { id = @object.Id }, @object);
        }
        
        /// <summary>
        /// Редактирует объект
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <param name="object">Объект, переданный из body</param>
        /// <returns>Отредактированный объект</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditObject([FromRoute] int id, 
            [FromBody] Models.Object @object)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @object.Id)
            {
                return BadRequest();
            }

            _context.Entry(@object).State = EntityState.Modified;

            //Обновляем целочисленные свойства объекта
            foreach (var integerValue in @object.IntegerValues)
            {
                if (integerValue.Id == 0)
                    _context.IntegerValues.Add(integerValue);
                else
                    _context.Entry(integerValue).State = EntityState.Modified;
            }

            //Обновляем строковые свойства объекта
            foreach (var stringValue in @object.StringValues)
            {
                if (stringValue.Id == 0)
                    _context.StringValues.Add(stringValue);
                else
                    _context.Entry(stringValue).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @object = await _context.Objects
                .Include(o => o.IntegerValues)
                .Include(o => o.StringValues)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (@object == null)
            {
                return NotFound();
            }

            _context.Objects.Remove(@object);
            await _context.SaveChangesAsync();

            return Ok(@object);
        }

        private bool ObjectExists(int id)
        {
            return _context.Objects.Any(e => e.Id == id);
        }
    }
}