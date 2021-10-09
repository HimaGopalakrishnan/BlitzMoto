using System;
using System.Collections.Generic;

namespace App.Providers.Analytics.Services
{
    public interface IAnalyticsService
    {
        void TrackEvent(string eventName);
        void TrackEvent(string eventName, Dictionary<string, string> dictionary);
        void TrackView(string viewName);
        void TrackError(Exception error, Dictionary<string, string> properties = null);
    }
}
