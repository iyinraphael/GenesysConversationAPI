using System.Diagnostics;
using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Client;
using PureCloudPlatform.Client.V2.Extensions;
using GenesysSdkPoc;

internal class Program
{
    private static void Main(string[] args)
    {
        var accessTokenInfo = Configuration.Default.ApiClient.PostToken("397af16f-8a2d-4565-9db2-98b3a021cb8e", "kfZIZUtYYMWfL0VCgehpS7UsFbZzptvo9gclpInYItU");
        Console.WriteLine("Access token=" + accessTokenInfo.AccessToken);

        PureCloudRegionHosts region = PureCloudRegionHosts.us_east_1;
        Configuration.Default.ApiClient.setBasePath(region);

        // var apiInstance = new ConversationsApi();
        var analyticsApi = new AnalyticsApi();

        // var conversationsDetails = new AnalyticsConversationsDetails(apiInstance);
        // conversationsDetails.GetAnalyticsConversationsDetails();

        var conversationAnalyticsAggregate = new AnalyticsConversationsAggregates(analyticsApi);
        conversationAnalyticsAggregate.PostAnalyticsConversationsAggregates();

        // var retryConfig = new ApiClient.RetryConfiguration
        // {
        //   MaxRetryTimeSec = 10,
        //   RetryMax = 5q
        // };

        // Configuration.Default.ApiClient.RetryConfig = retryConfig;

        // Instantiate instance of the Users API
    }

}