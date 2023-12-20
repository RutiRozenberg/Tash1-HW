using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using MyTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Task.Controllers{

using TaskService.Services;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<MyTasks>> Get()
    {
        return TaskService.GetAll();
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
    public ActionResult Post(MyTasks newTask)
    {
        var newId =TaskService.Add(newTask);
        return CreatedAtAction("Post",new {id =newId}, TaskService.GetById(newId));
    }

    
    [HttpPut("{id}")]
    public ActionResult Put(int id,MyTasks newTask)
    {
        var result = TaskService.Update(id, newTask);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
}