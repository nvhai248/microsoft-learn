using GameStore.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonkeyController : ControllerBase
    {
        // In-memory monkey list
        private static readonly List<MonkeyDto> monkeys = new()
        {
            new MonkeyDto(1, "George", "Brown"),
            new MonkeyDto(2, "Kiki", "Black"),
            new MonkeyDto(3, "Lulu", "Golden"),
            new MonkeyDto(4, "Momo", "Gray"),
            new MonkeyDto(5, "Chico", "White")
        };

        // GET: api/monkey
        [HttpGet]
        public ActionResult<IEnumerable<MonkeyDto>> GetAll()
        {
            return Ok(monkeys);
        }

        // GET: api/monkey/2
        [HttpGet("{id}", Name = "GetMonkey")]
        public ActionResult<MonkeyDto> GetById(int id)
        {
            var monkey = monkeys.FirstOrDefault(m => m.Id == id);
            if (monkey is null)
                return NotFound();

            return Ok(monkey);
        }

        // POST: api/monkey
        [HttpPost]
        public ActionResult<MonkeyDto> Create(MonkeyDto newMonkey)
        {
            var monkey = new MonkeyDto(
                monkeys.Count + 1,
                newMonkey.Name,
                newMonkey.Color
            );

            monkeys.Add(monkey);

            return CreatedAtRoute("GetMonkey", new { id = monkey.Id }, monkey);
        }

        // PUT: api/monkey/3
        [HttpPut("{id}")]
        public IActionResult Update(int id, MonkeyDto updatedMonkey)
        {
            var index = monkeys.FindIndex(m => m.Id == id);
            if (index == -1)
                return NotFound();

            monkeys[index] = new MonkeyDto(id, updatedMonkey.Name, updatedMonkey.Color);

            return NoContent();
        }

        // DELETE: api/monkey/4
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var index = monkeys.FindIndex(m => m.Id == id);
            if (index == -1)
                return NotFound();

            monkeys.RemoveAt(index);

            return NoContent();
        }
    }
}
