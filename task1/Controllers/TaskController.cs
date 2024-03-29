using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

using task1.Interface;
using MyTask.Models;
namespace controller{
    using System.Runtime.CompilerServices;
    using TaskService.Services;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "User")]
public class TaskController : ControllerBase
{
    private ITaskInterface TaskService;
    private int UserId;
    private HttpContextAccessor httpContextAccessor;
    public TaskController(ITaskInterface TaskService,IHttpContextAccessor httpContextAccessor)
    {
        this.TaskService=TaskService;
        this.UserId=int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value);

    }


    [HttpGet]
    public ActionResult<List<MyTasks>> Get()
    {
        return TaskService.GetAll(this.UserId);
    }

    [HttpGet("{id}")]
    public ActionResult<MyTasks> Get(int id)
    {
        var TaskId =TaskService.GetById(id);
        if (TaskId == null)
            return NotFound();
        return TaskId;
    }
   

    [HttpPost]
    public ActionResult Post([FromBody] MyTasks newTask)
    {
        newTask.UserId=UserId;
        var newId =TaskService.Add(newTask);
        return CreatedAtAction("Post",new {id =newId}, TaskService.GetById(newId));
    }

    
    [HttpPut("{id}")]
    public ActionResult Put(int id,[FromBody] MyTasks newTask)
    {
        newTask.UserId=UserId;
        var result = TaskService.Update(id, newTask);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var result = TaskService.Delete(id);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
}