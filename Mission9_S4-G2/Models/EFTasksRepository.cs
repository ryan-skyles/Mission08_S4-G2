using Microsoft.EntityFrameworkCore;

namespace Mission8_S4_G2.Models
{
    public class EFTasksRepository : ITasksRepository
    {
        private QuadrantsContext _context;

        public EFTasksRepository(QuadrantsContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _context.Tasks.Include(t => t.Category).ToList();
        }

        public Task? GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.TaskId == id);
        }

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.CategoryName).ToList();
        }
    }
}
