using MyTask.Models;


namespace task1.Interface
{
    public interface ITaskInterface
    {
        List<MyTasks> GetAll(int id);
    
        MyTasks GetById(int id);
    
        int Add(MyTasks newTask);
    
        bool Update(int id , MyTasks newTask);
    
        bool Delete(int id);
    } 
}
