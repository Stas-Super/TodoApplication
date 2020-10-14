using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TODOApplication.Models.Repositories;
using TODOApplication.Services;

namespace TODOApplication.Models.Rpositories {
  public class TaskRepository : IRepository<Todo, int> {
    public async Task<int> Count() {
      return await TodoItemDatabase.Database.Table<Todo>().CountAsync();
    }

    public Task<List<Todo>> GetAll() {
      return TodoItemDatabase.Database.Table<Todo>().ToListAsync();
    }
    public async Task<List<Todo>> GetAll(int skip, int take) {
        return await TodoItemDatabase.Database.Table<Todo>().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<bool> Delete(Todo item) {
      var deleted = await TodoItemDatabase.Database.DeleteAsync(item);
      return deleted > 0;
    }
    public async Task<Todo> Get(int Id) {
      return await TodoItemDatabase.Database.Table<Todo>().Where(t => t.Id == Id).FirstOrDefaultAsync();
    }
    
    public async Task<bool> Save(Todo item) {
      var updated = 0;
      if(item.Id == 0) {
        updated = await TodoItemDatabase.Database.InsertAsync(item);
      }
      else {
        updated = await TodoItemDatabase.Database.UpdateAsync(item);
      }
      return updated > 0;
    }
  }
}
