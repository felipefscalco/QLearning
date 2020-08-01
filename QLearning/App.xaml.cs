using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using QLearning.Abstractions;
using QLearning.Handlers;
using QLearning.Helpers;
using QLearning.ViewModels;
using System.Windows;

namespace QLearning
{
    public partial class App : PrismApplication
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICanvasHelper, CanvasHelper>();
            containerRegistry.RegisterSingleton<IStateHelper, StateHelper>();
            containerRegistry.RegisterSingleton<IColorHelper, ColorHelper>();
            containerRegistry.RegisterSingleton<IRewardHelper, RewardHelper>();
            containerRegistry.RegisterSingleton<IQLearningHandler, QLearningHandler>();
        }

        protected override Window CreateShell()
            => new MainWindow();
    }
}