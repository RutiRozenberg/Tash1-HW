using User.Models;


namespace task1.Interface
{
    public interface IUserInterface
    {
        List<Users> GetAll();
    
        Users GetById(int id);
    
        int Add(Users newTask);
    
        bool Update(int id , Users newTask);
    
        bool Delete(int id);
    } 
}
