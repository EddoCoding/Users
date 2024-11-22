using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace Users.ViewModels
{
    public class UserVM : ReactiveObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
       [Reactive] public string SurName { get; set; } = string.Empty;
       [Reactive] public string Name { get; set; } = string.Empty;
       [Reactive] public string Login { get; set; } = string.Empty;
       [Reactive] public string Password { get; set; } = string.Empty;
       [Reactive] public string Mail { get; set; } = string.Empty;
       [Reactive] public string AccessLevel { get; set; } = string.Empty;
       [Reactive] public string Notes { get; set; } = string.Empty;
    }
}