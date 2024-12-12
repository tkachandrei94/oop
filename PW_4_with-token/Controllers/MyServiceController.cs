using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize] 
[ApiController]
[Route("api/[controller]")]
public class MyServiceController : ControllerBase
{
    private static List<string> _items = new List<string> { "Item1", "Item2", "Item3" };

    // GET api/myservice - получить все элементы
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_items);
    }

    // GET api/myservice/{id} - получить конкретный элемент
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (id < 0 || id >= _items.Count) return NotFound();
        return Ok(_items[id]);
    }

    // POST api/myservice - добавить новый элемент
    [HttpPost]
    public IActionResult Add([FromBody] string newItem)
    {
        _items.Add(newItem);
        return CreatedAtAction(nameof(GetById), new { id = _items.Count - 1 }, newItem);
    }

    // PUT api/myservice/{id} - обновить существующий элемент
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] string updatedItem)
    {
        if (id < 0 || id >= _items.Count) return NotFound();
        _items[id] = updatedItem;
        return NoContent();
    }

    // DELETE api/myservice/{id} - удалить элемент
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id < 0 || id >= _items.Count) return NotFound();
        _items.RemoveAt(id);
        return NoContent();
    }
}
