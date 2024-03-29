// using UnityEngine;



using System.Collections.Generic;
using MyTask.Models;
using task1.Interface;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using System.Runtime.CompilerServices;
namespace TaskService.Services{
// using System.Collections;
// using Microsoft.AspNetCore.Mvc;
// using System.Linq;
// using System.IO;
// using System;
// using System.Text.Json;


public  class TaskService:ITaskInterface
{
    private List<MyTasks> Tasks;
    private string fileName;
    public TaskService()
    {
        this.fileName = Path.Combine("Data", "Tasks.json");
        using (var jsonFile = File.OpenText(fileName))
        {
            Tasks = JsonSerializer.Deserialize<List<MyTasks>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(Tasks));
    }

   
    public  List<MyTasks> GetAll(int id)
    {
        return Tasks.Where(t=> t.UserId==id).ToList();
    }

    
    public  MyTasks GetById(int id) 
    {
        return Tasks.FirstOrDefault(t => t.Id == id);
    }

    public  int Add(MyTasks newTask)
    {
        if (Tasks.Count == 0)
            newTask.Id = 1;
        else
            newTask.Id = Tasks.Max(t => t.Id) + 1;
        Tasks.Add(newTask);
        saveToFile();
        return newTask.Id;
    }
  
    public bool Update(int id, MyTasks newTask)
    {
        if (id != newTask.Id)
            return false;
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        int index = Tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Tasks[index] = newTask;
        saveToFile();

        return true;
    }  

      
    public bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        int index = Tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Tasks.RemoveAt(index);
        saveToFile();

        return true;
    } 

    public int Count => Tasks.Count();

}


}