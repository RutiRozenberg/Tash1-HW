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
using task1.Interface;
using User.Models;

namespace controller{

using TaskService.Services;

[ApiController]
[Route("[controller]")]
public class userController: ControllerBase
{
    IUserInterface UserService;
    public userController(IUserInterface UserService)
    {
        this.UserService=UserService;
    }
    [HttpGet]
    public ActionResult<List<Users>> Get()
    {
        return UserService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Users> Get(int id)
    {
        var UserId =UserService.GetById(id);
        if (UserId == null)
            return NotFound();
        return UserId;
    }
   

    [HttpPost]
    public ActionResult Post(Users newUser)
    {
        var newId =UserService.Add(newUser);
        return CreatedAtAction("Post",new {id =newId}, UserService.GetById(newId));
    }

    
    [HttpPut("{id}")]
    public ActionResult Put(int id,Users newUser)
    {
        var result = UserService.Update(id, newUser);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var result = UserService.Delete(id);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
}