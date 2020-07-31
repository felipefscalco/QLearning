using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QLearning.Abstractions;
using QLearning.Messages;
using QLearning.Models;
using System;
using System.Collections.ObjectModel;
using System.Timers;

namespace QLearning.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator EventAggregator;
        private readonly ICanvasHelper CanvasHelper;
        private readonly IQLearningHandler QLearningHandler;
        private readonly Timer Timer;

        private ObservableCollection<QLine> _qTable;
        private ObservableCollection<Square> _squares;
        private int _stepsMade;
        private int _episodes;
        private int _iterationSpeed;
        private int _timeElapsed;
        private bool _hasStartedAlready;
        private bool _isRunning;
        private bool _bestPathFound;
        private string _timeElapsedString;

        public ObservableCollection<Square> Squares
        {
            get => _squares;
            set => SetProperty(ref _squares, value);
        }

        public ObservableCollection<QLine> QTable
        {
            get => _qTable;
            set => SetProperty(ref _qTable, value);
        }

        public int IterationSpeed
        {
            get => _iterationSpeed;
            set => SetProperty(ref _iterationSpeed, value);
        }

        public int StepsMade
        {
            get => _stepsMade;
            set => SetProperty(ref _stepsMade, value);
        }

        public int Episodes
        {
            get => _episodes;
            set => SetProperty(ref _episodes, value);
        }

        public string TimeElapsedString
        {
            get => _timeElapsedString;
            set => SetProperty(ref _timeElapsedString, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool BestPathFound
        {
            get => _bestPathFound;
            set => SetProperty(ref _bestPathFound, value);
        }

        public DelegateCommand StartCommand { get; private set; }
        public DelegateCommand StopCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }
        public DelegateCommand BestPathCommand { get; private set; }

        public MainWindowViewModel(IEventAggregator eventAggregator, ICanvasHelper canvasHelper, IQLearningHandler qLearningHandler)
        {
            EventAggregator = eventAggregator;
            CanvasHelper = canvasHelper;
            QLearningHandler = qLearningHandler;
            Timer = new Timer();

            InitializeProperties();

            EventAggregator.GetEvent<SetQTableMessage>().Subscribe((qTable) => UpdateQTable(qTable));
            EventAggregator.GetEvent<UpdateEpisodesMessage>().Subscribe(() => Episodes++);
            EventAggregator.GetEvent<BestPathFoundMessage>().Subscribe(() => StopTimerWhenBestPathFound());

            StartCommand = new DelegateCommand(() => StartQLearning());
            StopCommand = new DelegateCommand(() => StopCommandHandler());
            ResetCommand = new DelegateCommand(() => ResetCommandHandler());
            BestPathCommand = new DelegateCommand(() => BestPathCommandHandler());
        }

        private void InitializeProperties()
        {
            QTable = new ObservableCollection<QLine>();
            IterationSpeed = 200;
            Squares = CanvasHelper.GetSquares();
        }

        private void StartQLearning()
        {
            if (_hasStartedAlready)
            {          
                Restart();

                return;
            }

            QLearningHandler.InitializeQLearning(Squares);

            Timer.Interval = IterationSpeed;
            Timer.Elapsed -= TimerElapsed;
            Timer.Elapsed += TimerElapsed;
            Timer.Start();

            IsRunning = true;

            _hasStartedAlready = true;
        }

        private void StopCommandHandler()
        {
            Timer.Stop();
            IsRunning = false;
        }        

        private void ResetCommandHandler()
        {
            BestPathFound = false;
            StartQLearning();
        }

        private void BestPathCommandHandler()
        {
            EventAggregator.GetEvent<SetInitialStateMessage>().Publish();
            Timer.Interval = IterationSpeed;
            Timer.Start();
        }

        private void StopTimerWhenBestPathFound()
        {
            Timer.Stop();

            IsRunning = false;
            BestPathFound = true;

            _hasStartedAlready = false;
        }

        private void UpdateQTable(ObservableCollection<QLine> qTable)
        {
            QTable.Clear();
            QTable.AddRange(qTable);
        }

        private void Restart()
        {            
            Timer.Interval = IterationSpeed;
            Timer.Start();
            IsRunning = true;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _timeElapsed += IterationSpeed;
            var timeElapsedInSeconds = Math.Round((double)_timeElapsed / 1000, 2);
            TimeElapsedString = $"{timeElapsedInSeconds} s";

            QLearningHandler.PerformStep();

            StepsMade++;
        }
    }
}