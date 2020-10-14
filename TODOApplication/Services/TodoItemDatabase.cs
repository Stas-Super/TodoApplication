using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TODOApplication.Models;
using TODOApplication.Models.Rpositories;

namespace TODOApplication.Services {
  public static class TodoItemDatabase {

    private static SQLiteAsyncConnection database;
    public static SQLiteAsyncConnection Database { 
      get { 
        if (database == null) {
          database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
          var init = InitializeAsync().Result;
        }
        return database;
      } 
    }
    static bool initialized = false;
    public static async Task Fill() {
      TaskRepository repo = new TaskRepository();
      HttpClient client = new HttpClient();
      var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
      var result = await response.Content.ReadAsStringAsync();
      var res = JsonConvert.DeserializeObject<List<Todo>>(result);
      foreach (var item in res) {
        await repo.Save(item);
      }
    }

    public static async Task<bool> InitializeAsync() {
      if (!initialized) {
        if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Todo).Name)) {
          await Database.CreateTablesAsync(CreateFlags.None, typeof(Todo)).ConfigureAwait(false);
          try {
            await Fill();
          }
          catch (Exception e) {
            Console.WriteLine(e.Message);
          }
        }
        initialized = true;
      }
      return true;
    }


  }
}
