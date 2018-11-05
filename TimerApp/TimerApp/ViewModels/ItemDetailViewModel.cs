using System;
using System.Windows.Input;
using TimerApp.Models;
using Xamarin.Forms;

namespace TimerApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private enum StatusType
        {
            Running,
            Paused,
            Stopped
        };

        StatusType status = StatusType.Stopped;
        private StatusType Status
        {
            get { return status; }
            set
            {
                switch (value)
                {
                    case StatusType.Running:
                        IsRunEnabled = false;
                        IsPauseEnabled = true;
                        IsStopEnabled = true;
                        break;
                    case StatusType.Paused:
                        IsRunEnabled = true;
                        IsPauseEnabled = false;
                        IsStopEnabled = true;
                        break;
                    case StatusType.Stopped:
                        IsRunEnabled = true;
                        IsPauseEnabled = false;
                        IsStopEnabled = false;
                        break;
                }
                SetProperty(ref status, value);
            }
        }

        public Item Item { get; set; }

        bool isRunEnabled = false;
        public bool IsRunEnabled
        {
            get { return isRunEnabled; }
            set { SetProperty(ref isRunEnabled, value); }
        }

        bool isPauseEnabled = false;
        public bool IsPauseEnabled
        {
            get { return isPauseEnabled; }
            set { SetProperty(ref isPauseEnabled, value); }
        }

        bool isStopEnabled = false;
        public bool IsStopEnabled
        {
            get { return isStopEnabled; }
            set { SetProperty(ref isStopEnabled, value); }
        }

        public int RemainingTime { get; set; }

        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            RunCommand = new Command(Run);
            StopCommand = new Command(Stop);
            PauseCommand = new Command(Pause);
            Status = StatusType.Stopped;
        }

        public ICommand RunCommand { get; private set; }

        public void Run()
        {
             Status = StatusType.Running;
        }

        public ICommand StopCommand { get; private set; }

        public void Stop()
        {
             Status = StatusType.Stopped;
        }

        public ICommand PauseCommand { get; private set; }

        public void Pause()
        {
            Status = StatusType.Paused;
        }
    }
}
