using MyTask.Models;


namespace task1.Interface
{
    public interface ITaskInterface
    {
        List<MyTasks> GetAll();
    
        MyTasks GetById(int id);
    
        int Add(MyTasks newTask);
    
        bool Update(int id , MyTasks newTask);
    
        bool Delete(int id);
    } 
}
