using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MyServiceController : ControllerBase
{
    // Имитируем базу данных в памяти
    private static List<string> _items = new List<string> { "Item1", "Item2", "Item3" };

    // Basic Auth: простой пример (логин: admin, пароль: secret)
    private bool CheckAuth()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return false;

        var authHeader = Request.Headers["Authorization"].ToString(); 
        // Ожидаем Basic base64(login:password)
        // Пример: "Basic YWRtaW46c2VjcmV0" где YWRtaW46c2VjcmV0 = "admin:secret"
        if (authHeader.StartsWith("Basic "))
        {
            var encoded = authHeader.Substring("Basic ".Length).Trim();
            var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            var parts = decoded.Split(':');
            if (parts.Length == 2)
            {
                var login = parts[0];
                var pass = parts[1];
                if (login == "admin" && pass == "secret")
                    return true;
            }
        }
        return false;
    }

    // CRUD методы:
    // GET api/myservice - получить все элементы
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!CheckAuth()) return Unauthorized("No or invalid credentials");
        return Ok(_items);
    }

    // GET api/myservice/{id} - получить конкретный элемент
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (!CheckAuth()) return Unauthorized("No or invalid credentials");
        if (id < 0 || id >= _items.Count) return NotFound();
        return Ok(_items[id]);
    }

    // POST api/myservice - добавить новый элемент
    [HttpPost]
    public IActionResult Add([FromBody] string newItem)
    {
        if (!CheckAuth()) return Unauthorized("No or invalid credentials");
        _items.Add(newItem);
        return CreatedAtAction(nameof(GetById), new {id = _items.Count - 1}, newItem);
    }

    // PUT api/myservice/{id} - обновить существующий элемент
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] string updatedItem)
    {
        if (!CheckAuth()) return Unauthorized("No or invalid credentials");
        if (id < 0 || id >= _items.Count) return NotFound();
        _items[id] = updatedItem;
        return NoContent();
    }

    // DELETE api/myservice/{id} - удалить элемент
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!CheckAuth()) return Unauthorized("No or invalid credentials");
        if (id < 0 || id >= _items.Count) return NotFound();
        _items.RemoveAt(id);
        return NoContent();
    }
}
