using Prism.Events;
using QLearning.Models;
using System.Collections.ObjectModel;

namespace QLearning.Messages
{
    public class SetQTableMessage : PubSubEvent<ObservableCollection<QLine>>
    {
    }
}