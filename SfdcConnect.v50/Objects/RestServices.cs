/****************************************************************************
*
*   File name: DataObjects\RestServices.cs
*   Author: Sean Fife
*   Create date: 5/21/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes RestServices class, the api version available @ /services/data/
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfdcConnect.Rest
{
    public class RestServices
    {
        public string label;
        public string url;
        public string version;
    }

    public class AvailableResources : Dictionary<string, string>
    {
        //public string tooling { get; set; }
        //public string chatter { get; set; }
        //public string tabs { get; set; }
        //public string appMenu { get; set; }
        //public string quickActions { get; set; }
        //public string queryAll { get; set; }
        //public string wave { get; set; }
        //public string exchange_connect { get; set; }
        //public string analytics { get; set; }
        //public string search { get; set; }
        //public string identity { get; set; }
        //public string composite { get; set; }
        //public string theme { get; set; }
        //public string nouns { get; set; }
        //public string recent { get; set; }
        //public string connect { get; set; }
        //public string licensing { get; set; }
        //public string limits { get; set; }
        //public string process { get; set; }
        //public string async_queries { get; set; }
        //public string query { get; set; }
        //public string emailConnect { get; set; }
        //public string compactLayouts { get; set; }
        //public string flexiPage { get; set; }
        //public string sobjects { get; set; }
        //public string actions { get; set; }
        //public string support { get; set; }
    }

    public class ApiLimits
    {
        public Limit ConcurrentAsyncGetReportInstances { get; set; }
        public Limit ConcurrentSyncReportRuns { get; set; }
        public Limit DailyApiRequests { get; set; }
        public Limit DailyAsyncApexExecutions { get; set; }
        public Limit DailyBulkApiRequests { get; set; }
        public Limit DailyGenericStreamingApiEvents { get; set; }
        public Limit DailyGenericStreamingV2ApiEvents { get; set; }
        public Limit DailyStreamingApiEvents { get; set; }
        public Limit DailyWorkflowEmails { get; set; }
        public Limit DataStorageMB { get; set; }
        public Limit FileStorageMB { get; set; }
        public Limit HourlyAsyncReportRuns { get; set; }
        public Limit HourlyDashboardRefreshes { get; set; }
        public Limit HourlyDashboardResults { get; set; }
        public Limit HourlyDashboardStatuses { get; set; }
        public Limit HourlySyncReportRuns { get; set; }
        public Limit HourlyTimeBasedWorkflow { get; set; }
        public Limit MassEmail { get; set; }
        public Limit SingleEmail { get; set; }
        public Limit StreamingApiConcurrentClients { get; set; }
        public Limit StreamingV2ApiConcurrentClients { get; set; }
    }

    public class Limit
    {
        public int Max { get; set; }
        public int Remaining { get; set; }
        public int Used { get { return Max - Remaining; } }

        public override string ToString()
        {
            return string.Format("{0}/{1} used, {2} remain", Used, Max, Remaining);
        }
    }

}
