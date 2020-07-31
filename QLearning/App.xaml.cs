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
            containerRegistry.Register<ICanvasHelper, CanvasHelper>();
            containerRegistry.Register<IStateHelper, StateHelper>();
            containerRegistry.Register<IColorHelper, ColorHelper>();
            containerRegistry.Register<IRewardHelper, RewardHelper>();
            containerRegistry.Register<IQLearningHandler, QLearningHandler>();
        }

        protected override Window CreateShell()
            => new MainWindow();
    }
}