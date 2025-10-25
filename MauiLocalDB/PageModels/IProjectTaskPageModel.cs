using CommunityToolkit.Mvvm.Input;
using MauiLocalDB.Models;

namespace MauiLocalDB.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}