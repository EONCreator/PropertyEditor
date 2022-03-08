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
    public class CategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public CategoriesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _context.Category
                .Include(c => c.IntegerProperties)
                .Include(c => c.StringProperties);
        }

        /// <summary>
        /// Получение определенной категории
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <returns>Категория</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Категория с целочисленными и строковыми свойствами
            var category = await _context.Category
                .Include(c => c.IntegerProperties)
                .Include(c => c.StringProperties)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        /// <summary>
        /// Для редактирования категории
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <param name="category">Категория, переданная с body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }
            
            //Меняем целочисленные свойства
            foreach (var integerProperty in category.IntegerProperties)
            {
                if (integerProperty.Id == 0)
                    _context.IntegerProperties.Add(integerProperty); //Если добавлено новое свойство
                else
                    _context.Entry(integerProperty).State = EntityState.Modified; //Если свойство изменено
            }

            //Удаляем из БД те целочисленные свойства, которые удалили из объекта
            foreach (var integerProperty in _context
                .IntegerProperties.Where(c => c.CategoryId == category.Id))
            {
                if (category.IntegerProperties
                    .FirstOrDefault(c => c.Id == integerProperty.Id) == null)

                    _context.IntegerProperties.Remove(integerProperty);
            }

            //Меняем строковые свойства
            foreach (var stringProperty in category.StringProperties)
            {
                if (stringProperty.Id == 0)
                    _context.StringProperties.Add(stringProperty); //Если добавлено новое свойство
                else
                    _context.Entry(stringProperty).State = EntityState.Modified; //Если свойство изменено
            }

            //Удаляем из БД те строковые свойства, которые удалили из объекта
            foreach (var stringProperty in _context
                .StringProperties.Where(p => p.CategoryId == category.Id))
            {
                if (category.StringProperties
                    .FirstOrDefault(p => p.Id == stringProperty.Id) == null)

                    _context.StringProperties.Remove(stringProperty);
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(category); //Возвращаем измененную категорию
        }

        /// <summary>
        /// Для добавления категории
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        /// <summary>
        /// Для удаления категории
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Category
                .Include(c => c.IntegerProperties)
                .Include(c => c.StringProperties)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);

            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}