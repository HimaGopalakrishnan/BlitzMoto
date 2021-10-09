using System;
using System.Collections.Generic;

namespace App.Providers.Analytics.Services
{
    public class MockAnalyticsService : IAnalyticsService
    {
        public MockAnalyticsService()
        {
        }

        public void TrackError(Exception error, Dictionary<string, string> properties = null)
        {
            Console.WriteLine(error.Message);
        }

        public void TrackEvent(string eventName)
        {
            Console.WriteLine(eventName);
        }

        public void TrackEvent(string eventName, Dictionary<string, string> dictionary)
        {
            Console.WriteLine(eventName);
        }

        public void TrackView(string viewName)
        {
            Console.WriteLine(viewName);
        }
    }
}
