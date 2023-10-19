using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Get([FromServices] AppDbContext appDbContext)
    {
        return Ok(appDbContext.Todos.ToList());
    }

    [HttpPost("/")]
    public IActionResult Post([FromServices] AppDbContext appDbContext, [FromBody] TodoModel model)
    {
        var todo = new TodoModel
        {
            Title = model.Title,
            Done = model.Done,
            CreatedAt = DateTime.Now
        };

        appDbContext.Todos.Add(todo);
        appDbContext.SaveChanges();
        return Ok(appDbContext.Todos.ToList());
    }

    [HttpPut("/{id:int}")]
    public IActionResult Put([FromServices] AppDbContext appDbContext, [FromBody] TodoModel model, [FromRoute] int id)
    {
        var todo = appDbContext.Todos.FirstOrDefault(todoModel => todoModel.Id == id);
        if (todo == null)
        {
            return BadRequest();
        }

        todo.Title = model.Title;
        todo.Done = model.Done;
        appDbContext.Todos.Update(todo);
        appDbContext.SaveChanges();
        
        return Ok(todo);
    }

    [HttpDelete("/{id:int}")]
    public IActionResult Delete([FromServices] AppDbContext appDbContext, [FromRoute] int id)
    {
        var todo = appDbContext.Todos.FirstOrDefault(model => model.Id == id);
        if (todo == null)
        {
            return BadRequest();
        }

        appDbContext.Todos.Remove(todo);
        appDbContext.SaveChanges();

        return Ok();
    }
}