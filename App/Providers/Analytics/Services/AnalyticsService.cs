using System;
using System.Collections.Generic;

namespace App.Providers.Analytics.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public void TrackEvent(string eventName)
        {
            var properties = new Dictionary<string, string>();
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(eventName, properties);
        }
        public void TrackEvent(string eventName, Dictionary<string, string> properties)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(eventName, properties);
        }

        public void TrackView(string viewName)
        {
            var properties = new Dictionary<string, string>();
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(viewName, properties);
        }

        public void TrackError(Exception error, Dictionary<string, string> properties = null)
        {
            Microsoft.AppCenter.Crashes.Crashes.TrackError(error, properties);
        }
    }
}
