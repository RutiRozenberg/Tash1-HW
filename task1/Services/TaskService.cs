// using UnityEngine;
using System.Collections.Generic;
using MyTask.Models;
namespace TaskService.Services{
using System.Collections;
using System.Linq;


public static class TaskService
{
    private static List<MyTasks> Tasks;

    static TaskService()
    {
        Tasks = new List<MyTasks>
        {
            new MyTasks {Name = "Homework", Id = 2,  isDone=false},
            new MyTasks {Name = "Shopping", Id = 1,  isDone=false},
            new MyTasks {Name = "Cooking", Id = 4,  isDone=false},
            
          
        };
    }

    public static List<MyTasks> GetAll() => Tasks;

    
    public static MyTasks GetById(int id) 
    {
        return Tasks.FirstOrDefault(t => t.Id == id);
    }

    public static int Add(MyTasks newTask)
    {
        if (Tasks.Count == 0)

            {
                newTask.Id = 1;
            }
            else
            {
        newTask.Id =  Tasks.Max(t => t.Id) + 1;

            }

        Tasks.Add(newTask);

        return newTask.Id;
    }
  
    public static bool Update(int id, MyTasks newTask)
    {
        if (id != newTask.Id)
            return false;

        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = Tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Tasks[index] = newTask;

        return true;
    }  

      
    public static bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = Tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        Tasks.RemoveAt(index);
        return true;
    }  



}
}