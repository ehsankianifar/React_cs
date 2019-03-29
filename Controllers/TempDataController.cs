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
                _context.tempDatas.Add(new TempData {ReadingDateTime=DateTime.Now, AssetName="FOO", DeviceName="Fake",ReadingData=2019 });
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
                _context.tempDatas.Add(new TempData { ReadingDateTime = DateTime.Now, AssetName = "FOO", DeviceName = "Fake", ReadingData = 2020 });
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
        public async Task<IActionResult> PostTempData([FromBody] List<TempData> MyList)
        {


            List<NewAction2> _newActions = new List<NewAction2>();
            
            if (_context.NewActions.Count()==0)
            {
                _context.NewActions.Add(new NewAction() {ActionDateTime= "2019-03-27 16:40:00 PM", ActinoName="Off" });
                _context.SaveChanges();
            }
            var newAction = _context.NewActions.FirstOrDefault();
            if (newAction.ActinoName == "On")
                newAction.ActinoName = "Off";
            else
                newAction.ActinoName = "On";
            _context.SaveChanges();
            var newAction2 = new NewAction2() { ActionName = newAction.ActinoName, ActionDateTime = "2019-03-27 16:40:00 PM" };
            _newActions.Add(newAction2);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach(var tempData in MyList)
            {
                _context.tempDatas.Add(tempData);
                await _context.SaveChangesAsync();
            }
            

            while (_context.tempDatas.Count() > 50)
            {
                var maximunData = _context.tempDatas.Min(t => t.Id);
                var item = _context.tempDatas.Where(t => t.Id == maximunData).FirstOrDefault();
                _context.tempDatas.Remove(item);
                _context.SaveChanges();
            }


            //return CreatedAtAction("GetTempData", new { id = tempData.Id }, tempData);
            return Ok(_newActions);
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