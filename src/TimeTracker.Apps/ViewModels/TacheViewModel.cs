using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Apps.ViewModels
{
    internal class TacheViewModel : ViewModelBase
    {
        private TaskItem _tache;

        public TaskItem Tache
        {
            get => _tache;
            set => SetProperty(ref _tache, value);
        }

        public TacheViewModel(TaskItem tache)
        {
            Console.WriteLine(tache.Name);
            Tache = tache;
        }
    }
}
