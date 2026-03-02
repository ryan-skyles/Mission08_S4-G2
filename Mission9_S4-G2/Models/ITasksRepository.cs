namespace Mission8_S4_G2.Models
{
    public interface ITasksRepository
    {
        IEnumerable<Task> GetAllTasks();
        Task? GetTaskById(int id);
        void AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(Task task);
        IEnumerable<Category> GetCategories();
    }
}
