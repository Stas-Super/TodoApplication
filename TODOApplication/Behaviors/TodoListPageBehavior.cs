using System;
using System.Collections.Generic;
using System.Text;
using TODOApplication.ViewModels;
using TODOApplication.Views;
using Xamarin.Forms;

namespace TODOApplication.Behaviors {
       
  class TodoListPageBehavior : Behavior<TodoListPage> {
    protected override async void OnAttachedTo(TodoListPage bindable) {
      base.OnAttachedTo(bindable);
      var model = new TodoViewModel(bindable.Navigation);
      await model.Fill();
      bindable.BindingContext = model;
    }
  }
}
