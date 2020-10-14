using System;
using System.IO;
using System.Windows.Input;
using TODOApplication.Models;
using TODOApplication.Models.Rpositories;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TODOApplication.ViewModels {
  public class TodoItemViewModel : BaseViewModel {
    private readonly INavigation Navigation;
    public ICommand PikeFileCommand { protected set; get; }
    public ICommand SaveCommand { protected set; get; }
    public ICommand DeleteCommand { protected set; get; }
    public ICommand CancelCommand { protected set; get; }
    public string FileName {
      get {
        return Todo.FileName;
      }
      set {
        Todo.FileName = value;
      }
    }
    public string Title {
      get {
        return Todo.Title;
      }
      set {
        Todo.Title = value;
      }
    }
    public DateTime? Date {
      get {
        return Todo.Date;
      }
      set {
        Todo.Date = value;
      }
    }

    private readonly Todo Todo;
    public TodoItemViewModel(Todo todo, INavigation navigation) {
      Todo = todo;
      Navigation = navigation;
      PikeFileCommand = new Command(PikeFile);
      SaveCommand = new Command(Save);
      DeleteCommand = new Command(Delete);
      CancelCommand = new Command(Cancel);
    }



    private async void PikeFile() {
      try {
        var result = await FilePicker.PickAsync();
        if (result != null) {
          FileName = $"File Name: {result.FileName}";
          var stream = await result.OpenReadAsync();
          using (MemoryStream ms = new MemoryStream()) {
            stream.CopyTo(ms);
            Todo.FileContent = ms.ToArray();
          }
          OnPropertyChanged("FileName");
        }
      }
      catch (Exception ex) {
        // The user canceled or something went wrong
      }
    }

    private async void Save() {
      var repo = new TaskRepository();
      await repo.Save(Todo);
      MessagingCenter.Send<TodoItemViewModel, Todo>(this, "Changed", Todo);
      await Navigation.PopAsync();
    }

    async void Delete() {
      var repo = new TaskRepository();
      await repo.Delete(Todo);
      MessagingCenter.Send<TodoItemViewModel, Todo>(this, "Deleted", Todo);
      await Navigation.PopAsync();
    }

    async void Cancel() {
      await Navigation.PopAsync();
    }
  }
}
