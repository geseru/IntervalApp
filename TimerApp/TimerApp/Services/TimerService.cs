using System;
using System.Collections.Generic;
using System.Timers;
using TimerApp.Models;

namespace TimerApp.Services
{
    public class TimerService
    {
        private Timer timer = new Timer();
        private static TimerService instance = null;
        private List<int> _timesInSec;
        private int timerIndex;
        private int actualTime;

        public event Action<int, int> Update;
        public event Action<StatusType> Status; 

        private TimerService()
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }

        public static TimerService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TimerService();
                }
                return instance;
            }
        }

        public void Start(List<int> timesInSec)
        {
            _timesInSec = timesInSec;
            timerIndex = 0;
            actualTime = _timesInSec[timerIndex];
            timer.Stop();
            timer.Start();
            Status?.Invoke(StatusType.Running);
        }

        public void Pause()
        {
            timer.Stop();
            Status?.Invoke(StatusType.Paused);
        }

        public void Continue()
        {
            timer.Start();
            Status?.Invoke(StatusType.Running);
        }

        public void Stop()
        {
            timer.Stop();
            Status?.Invoke(StatusType.Stopped);
            Update?.Invoke(timerIndex, 0);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            actualTime--;
            if (actualTime == 0)
            {
                timerIndex++;
                if (timerIndex < _timesInSec.Count)
                    actualTime = _timesInSec[timerIndex];
                else
                    Stop();
            }
                
            Update?.Invoke(timerIndex, actualTime);
        }
    }
}
