using System;

namespace TimerApp.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public int IntervalTime  { get; set; }
    }
}