using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempDataController : ControllerBase
    {
        private readonly TemperatureContext _context;

        public TempDataController(TemperatureContext context)
        {
            _context = context;
        }
        

        // GET: api/TempData
        [HttpGet("[action]")]
        public IEnumerable<TempData> GettempDatas()
        {
            if (_context.tempDatas.Count() == 0)
            {
                _context.tempDatas.Add(new TempData {Temperature="24",Humidity="55%" });
                _context.SaveChanges();
            }
            return _context.tempDatas;
        }

        //Get last data
        [HttpGet("[action]")]
        public TempData GetLast()
        {
            if (_context.tempDatas.Count() == 0)
            {
                _context.tempDatas.Add(new TempData { Temperature = "30", Humidity = "57%" });
                _context.SaveChanges();
            }
            var maximunData= _context.tempDatas.Max(t => t.Id);
            var tempData = _context.tempDatas.Where(t => t.Id == maximunData).FirstOrDefault();
            return (tempData);
        }

        // GET: api/TempData/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTempData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tempData = await _context.tempDatas.FindAsync(id);

            if (tempData == null)
            {
                return NotFound();
            }

            return Ok(tempData);
        }

        // PUT: api/TempData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTempData([FromRoute] int id, [FromBody] TempData tempData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tempData.Id)
            {
                return BadRequest();
            }

            _context.Entry(tempData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempDataExists(id))
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

        // POST: api/TempData
        [HttpPost]
        public async Task<IActionResult> PostTempData([FromBody] TempData tempData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.tempDatas.Add(tempData);
            await _context.SaveChangesAsync();

            if (_context.tempDatas.Count() > 50)
            {
                var maximunData = _context.tempDatas.Min(t => t.Id);
                var item = _context.tempDatas.Where(t => t.Id == maximunData).FirstOrDefault();
                _context.tempDatas.Remove(item);
                _context.SaveChanges();
            }


            //return CreatedAtAction("GetTempData", new { id = tempData.Id }, tempData);
            return Ok();
        }

        // DELETE: api/TempData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTempData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tempData = await _context.tempDatas.FindAsync(id);
            if (tempData == null)
            {
                return NotFound();
            }

            _context.tempDatas.Remove(tempData);
            await _context.SaveChangesAsync();

            return Ok(tempData);
        }

        private bool TempDataExists(int id)
        {
            return _context.tempDatas.Any(e => e.Id == id);
        }
    }
}