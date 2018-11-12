using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using TimerApp.Models;
using TimerApp.Services;
using Xamarin.Forms;

namespace TimerApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        Timer timer;

        AutoResetEvent autoEvent = new AutoResetEvent(false);

        bool started;

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

        int remainingTime;
        public int RemainingTime
        {
            get { return remainingTime; }
            set { SetProperty(ref remainingTime, value); }
        }
    
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            RunCommand = new Command(Run);
            StopCommand = new Command(Stop);
            PauseCommand = new Command(Pause);
            Status = StatusType.Stopped;
            RemainingTime = 10;

            TimerService.Instance.Status += Instance_Status;
            TimerService.Instance.Update += Instance_Update;
        }

        private void Instance_Update(int timerIndex, int timeRemaining)
        {
            RemainingTime = timeRemaining;
        }

        private void Instance_Status(StatusType obj)
        {
            Status = obj;
        }

        public ICommand RunCommand { get; private set; }

        public void Run()
        {
            if (Status == StatusType.Stopped)
                TimerService.Instance.Start(new List<int>() { Item.IntervalTime });
            else
                TimerService.Instance.Continue();            
        }

        public ICommand StopCommand { get; private set; }

        public void Stop()
        {
            TimerService.Instance.Stop();
            
            RemainingTime = 0;
        }

        public ICommand PauseCommand { get; private set; }

        public void Pause()
        {
            TimerService.Instance.Pause();
        }
    }
}
