using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TODOApplication.Models;
using TODOApplication.Models.Rpositories;
using TODOApplication.Views;
using Xamarin.Forms;

namespace TODOApplication.ViewModels {
  public class TodoViewModel : BaseViewModel {
    public ICommand EditCommand { protected set; get; }
    public ICommand AddCommand { protected set; get; }
    public ICommand LoadMoreCommand { protected set; get; }
    public ICommand RemoveCommand { protected set; get; }
    
    public ObservableCollection<Todo> TodoList { get; set; }
    private bool IsBusy;
    private readonly INavigation Navigation;

    public TodoViewModel(INavigation navigation) {
      Navigation = navigation;
      EditCommand = new Command(Edit);
      LoadMoreCommand = new Command(LoadMoreResult, () => !IsBusy);
      RemoveCommand = new Command(Remove);
      AddCommand = new Command(Add);
      Subscribe();
    }

    private void Subscribe() {
      MessagingCenter.Subscribe<TodoItemViewModel, Todo>(
          this,
          "Changed",   
          Changed
          );
      MessagingCenter.Subscribe<TodoItemViewModel, Todo>(
          this,
          "Deleted",
          Deleted
          );

    }
    private void Changed(TodoItemViewModel vm, Todo todo) {
      var existing = TodoList.FirstOrDefault(x => x.Id == todo.Id);
      if(existing == null) { //new
        TodoList.Add(todo);
      }
      else { //changed
        TodoList[TodoList.IndexOf(existing)] = todo;
      }
    }

    private void Deleted(TodoItemViewModel vm, Todo todo) {
      var existing = TodoList.FirstOrDefault(x => x.Id == todo.Id);
      if(existing != null) { 
        TodoList.Remove(existing);
      }
    }

    

    public async Task Fill() {
      TaskRepository todo = new TaskRepository();
      List<Todo> list = await todo.GetAll(0, 10);
      TodoList = new ObservableCollection<Todo>(list);
    }
    

    private async void LoadMoreResult() {
      IsBusy = true;
      TaskRepository repo = new TaskRepository();
      var count = await repo.Count();
      if (TodoList.Count < count) {
        var list = await repo.GetAll(TodoList.Count, 10);
        foreach(var item in list) {
          TodoList.Add(item);
        }
      }
      IsBusy = false;
    }

    private async void Add(object obj) {
      await Navigation.PushAsync(new TodoItemPage {
        BindingContext = new TodoItemViewModel(new Todo(), Navigation)
      });
    }

    async void Edit(object obj) {
      var todo = (Todo)obj;
      await Navigation.PushAsync(new TodoItemPage {
        BindingContext = new TodoItemViewModel(todo, Navigation)
      });
    }

    private async void Remove(object obj) {
      var todo = (Todo)obj;
      TaskRepository repo = new TaskRepository();
      if (await repo.Delete(todo)) {
        TodoList.Remove(todo);
      }
      
    }

  }
}
