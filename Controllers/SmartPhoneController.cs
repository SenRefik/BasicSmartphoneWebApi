using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWeekProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWeekProject.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class SmartphoneController : ControllerBase
    {
        private readonly SmartphoneContext _context;
        public SmartphoneController(SmartphoneContext context)
        {
            _context = context;

            if (_context.Smartphones.Count() == 0)
            {
                _context.Smartphones.Add(
                new Smartphone
                {
                    Id = 1,
                    BrandName = "Apple",
                    ModelName = "iPhone 13",
                    Price = 16299,
                    StockStatus = true
                });

                _context.Smartphones.Add(
                new Smartphone
                {
                    Id = 2,
                    BrandName = "Samsung",
                    ModelName = "S21Fe",
                    Price = 10132,
                    StockStatus = false
                });

                _context.Smartphones.Add(
                new Smartphone
                {
                    Id = 3,
                    BrandName = "Xiaomi",
                    ModelName = "Mi 11T",
                    Price = 10450,
                    StockStatus = true
                });

                _context.Smartphones.Add(
                new Smartphone
                {
                    Id = 4,
                    BrandName = "Huawei",
                    ModelName = "P40 Pro",
                    Price = 15035,
                    StockStatus = true
                });
                _context.SaveChanges();
            }
        }

       

        [HttpGet]
        public List<Smartphone> GetSmartphones()
        {
            return _context.Smartphones.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Smartphone> GetSmartphoneById(int id)
        {
            var smartPhone = _context.Smartphones.Find(id);

            if (smartPhone == null)
            {
                return NotFound();
            }

            return smartPhone;
        }

        [HttpGet("Search")]
        public  IQueryable<Smartphone> Search([FromQuery] SomeQuery search)
        {
            IQueryable<Smartphone> query = _context.Smartphones;

            if (!string.IsNullOrEmpty(search.BrandName))
            {
                query = query.Where(e => e.BrandName.ToLower().Contains(search.BrandName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.ModelName))
            {
                query = query.Where(e => e.ModelName.ToLower().Contains(search.ModelName.ToLower()));
            }

            if (search.StockStatusQuery != null)
            {
                query = query.Where(e => e.StockStatus == search.StockStatusQuery);
            }

            return query;
        }

        [HttpPut("{id}")]
        public  ActionResult PutSmartphone(int id, Smartphone smartphone)
        {
            if (id != smartphone.Id)
            {
                return BadRequest();
            }

            _context.Entry(smartphone).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartphoneExists(id))
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

        [HttpPost]
        public ActionResult<Smartphone> PostSmartphone(Smartphone smartphone)
        {
            _context.Smartphones.Add(smartphone);
            _context.SaveChanges();

            return CreatedAtAction("PostSmartphone", new { Id = smartphone.Id }, smartphone);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSmartphone(int id)
        {
            var smartphone = _context.Smartphones.Find(id);
            if (smartphone == null)
            {
                return NotFound();
            }

            _context.Smartphones.Remove(smartphone);
            _context.SaveChanges();

            return NoContent();
        }

        private bool SmartphoneExists(int id)
        {
            return _context.Smartphones.Any(e => e.Id == id);
        }
    }
}