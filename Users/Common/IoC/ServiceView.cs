using Avalonia.Controls;
using System;
using System.Collections.Generic;

namespace Users.Common.IoC
{
    public class ServiceView(IContainer container) : IServiceView
    {
        IContainer _containerDi = container;
        Dictionary<object, (Type, Type)> containerView = new();
        Dictionary<Type, IView> registryView = new();
        Window window;

        public void RegisterTypeView<ViewModel, View>()
        {
            if (typeof(Window).IsAssignableFrom(typeof(View)))
                containerView.Add(typeof(ViewModel), (typeof(View), null));
        }

        public IServiceView Window<ViewModel>(string[] identifier = null, params object[] args) where ViewModel : class
        {
            if (containerView.TryGetValue(typeof(ViewModel), out var view) && view.Item1 != null)
            {
                if (view.Item1 == null) throw new Exception($"{view.Item1} = null");

                var window = _containerDi.Resolve((Type)view.Item1, identifier, args) as Window;
                var viewModel = _containerDi.Resolve(typeof(ViewModel), identifier, args);
                window.DataContext = viewModel;
                this.window = window;
                registryView.Add(viewModel.GetType(), (IView)window);
            }
            else throw new Exception($"Модель представления: -- {typeof(ViewModel).Name} -- не зарегистрирована!");

            return this;
        }

        public void NonModal()
        {
            if (window != null) window.Show();
        }
        public void Close<ViewModel>()
        {
            if (registryView.TryGetValue(typeof(ViewModel), out var view))
            {
                view.Exit();
                registryView.Remove(typeof(ViewModel));
            }
        }
    }
}