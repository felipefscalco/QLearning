using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using QLearning.Abstractions;
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
            containerRegistry.Register<IQTableHelper, QTableHelper>();
        }

        protected override Window CreateShell()
            => new MainWindow();
    }
}