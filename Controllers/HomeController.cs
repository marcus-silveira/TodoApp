using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public List<TodoModel> Get([FromServices] AppDbContext appDbContext)
    {
        return appDbContext.Todos.ToList();
    }
    
    [HttpPost("/")]
    public List<TodoModel> Post([FromServices] AppDbContext appDbContext, [FromBody] TodoModel model)
    {
        var todo = new TodoModel()
        {
            Title = model.Title,
            Done = model.Done,
            CreatedAt = DateTime.Now
        };

        appDbContext.Todos.Add(todo);
        appDbContext.SaveChanges();
        return appDbContext.Todos.ToList();
    }
}