using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.maui.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string? title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    bool isLoading;

    public bool IsNotLoading => !IsLoading;

}
