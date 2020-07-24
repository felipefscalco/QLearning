using Prism.Events;
using Prism.Mvvm;
using QLearning.Abstractions;
using QLearning.Models;
using System.Collections.ObjectModel;

namespace QLearning.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator EventAggregator;
        private readonly ICanvasHelper CanvasHelper;
        private readonly IQTableHelper QTableHelper;

        private ObservableCollection<Square> _squares;

        public ObservableCollection<Square> Squares
        {
            get => _squares;
            set => SetProperty(ref _squares, value);
        }

        public MainWindowViewModel(IEventAggregator eventAggregator, ICanvasHelper canvasHelper, IQTableHelper qTableHelper)
        {
            EventAggregator = eventAggregator;
            CanvasHelper = canvasHelper;
            QTableHelper = qTableHelper;

            InitializeProperties();

            Squares = CanvasHelper.GetSquares();
        }

        private void InitializeProperties()
        {
            Squares = new ObservableCollection<Square>();
        }
    }
}