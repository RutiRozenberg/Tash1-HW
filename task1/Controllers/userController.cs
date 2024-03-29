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
using Microsoft.AspNetCore.Http;
using TaskService.Services;


namespace controller{
    

[ApiController]
[Route("[controller]")]
public class userController: ControllerBase
{
    IUserInterface UserService;
    private int Id;
    public userController(IUserInterface UserService,IHttpContextAccessor httpContextAccessor)
    {
        this.UserService=UserService;
        Id=int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value);

    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<Users>> GetAll()
    {
        return UserService.GetAll();
    }

    
    [HttpGet]
    [Route("[action]")]
    [Authorize(Policy = "User")]
    public ActionResult<Users> Get()
    {
        var MyUser =UserService.GetById(Id);
        if (MyUser == null)
            return NotFound();
        return MyUser;
    }
   

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post([FromBody] Users newUser)
    {
        var newId =UserService.Add(newUser);
        return CreatedAtAction("Post",new {id =newId}, UserService.GetById(newId));
    }

    
    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Put(int id,[FromBody] Users newUser)
    {
        var result = UserService.Update(id, newUser);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

    
    [HttpPut]
    [Route("[action]")]
    [Authorize(Policy = "User")]
    public ActionResult PutThisUser([FromBody] Users newUser)
    {
        newUser.Id =Id;
        var result = UserService.Update(Id, newUser);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
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