 //using UnityEngine;
using System.Collections.Generic;
using MyTask.Models;
using task1.Interface;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
namespace UserService.Services{
// using System.Collections;
// using Microsoft.AspNetCore.Mvc;
// using System.Linq;
// using System.IO;
// using System;
// using System.Text.Json;
using User.Models;

    public  class UserService: IUserInterface
{
    private List<Users> Users;
    private string fileName = "User.json";
    public UserService()
    {
        this.fileName = Path.Combine("Data", fileName);
        using (var jsonFile = File.OpenText(fileName))
        {
            Users = JsonSerializer.Deserialize<List<Users>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(Users));
    }
    public  List<Users> GetAll() => Users;

    
    public  Users GetById(int id) 
    {
        return Users.FirstOrDefault(t => t.Id == id);
    }

    public  int Add(Users newUser)
    {
        if (Users.Count == 0)
            newUser.Id = 1;
        else
            newUser.Id =  Users.Max(t => t.Id) + 1;

        Users.Add(newUser);
        saveToFile();
        return newUser.Id;
    }
  
    public  bool Update(int id, Users newTask)
    {
        if (id != newTask.Id)
            return false;

        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = Users.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Users[index] = newTask;
        saveToFile();

        return true;
    }  

      
    public bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = Users.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Users.RemoveAt(index);
        saveToFile();

        return true;
    } 

    public int Count => Users.Count();

}


}
