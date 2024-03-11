using System;
using System.Collections.Generic;
using System.Threading;
using ReactiveUI;

namespace Homework_6_WeatherApp.ViewModels
{
	public class DateViewModel : ReactiveObject
	{
        private string? _dateNow;
        public string? DateNow
        {
            get => _dateNow;
            set => this.RaiseAndSetIfChanged(ref _dateNow, value);
        }

        private Timer? _timer;

        public DateViewModel()
        {
            UpdateDate();
            SetupTimer();
        }

        private void UpdateDate()
        {
            DateNow = DateTime.Now.ToString("dddd d", new System.Globalization.CultureInfo("en-US"));
        }

        private void SetupTimer()
        {
            var now = DateTime.Now;
            var tomorrow = now.AddDays(1).Date;
            var timeToNextDay = tomorrow - now;
            _timer = new Timer(_ => UpdateDate(), null, timeToNextDay, TimeSpan.FromDays(1));
        }
    }
}