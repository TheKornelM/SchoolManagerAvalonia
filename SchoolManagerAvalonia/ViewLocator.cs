using Avalonia.Controls;
using Avalonia.Controls.Templates;
using SchoolManagerViewModel;
using System;
using System.Collections.Generic;


namespace SchoolManagerAvalonia;

public class ViewLocator : IDataTemplate
{
    private readonly Dictionary<Type, Func<Control?>> _locator = new();

    public ViewLocator()
    {
        RegisterViewFactory<FilterUsersViewModel, UsersView>();
        RegisterViewFactory<AdminClassesViewModel, AdminClassesView>();
        RegisterViewFactory<AddSubjectViewModel, AddSubjectView>();
    }

    public Control? Build(object? data)
    {
        if (data is null)
        {
            return new TextBlock { Text = "No VM provided" };
        }
        _locator.TryGetValue(data.GetType(), out var factory);

        return factory?.Invoke() ?? new TextBlock { Text = $"VM Not Registered: {data.GetType()}" };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }

    private void RegisterViewFactory<TViewModel, TView>()
      where TViewModel : class
      where TView : Control
      => _locator.Add(
          typeof(TViewModel), Activator.CreateInstance<TView>);
    /*Design.IsDesignMode
        ? Activator.CreateInstance<TView>
        : Ioc.Default.GetService<TView>);*/
}