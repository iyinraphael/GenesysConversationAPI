using System.Diagnostics;
using System.Net.WebSockets;
using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Client;
using PureCloudPlatform.Client.V2.Extensions;
using GenesysSdkPoc;
using System.Threading.Channels;
using PureCloudPlatform.Client.V2.Model;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Authenticaiton using Client Credential Oauth token created on Genesys Cloud 
        var accessTokenInfo = Configuration.Default.ApiClient.PostToken("397af16f-8a2d-4565-9db2-98b3a021cb8e", "kfZIZUtYYMWfL0VCgehpS7UsFbZzptvo9gclpInYItU");
        Console.WriteLine("Access token=" + accessTokenInfo.AccessToken);

        // Setting purecloud org region 
        PureCloudRegionHosts region = PureCloudRegionHosts.us_east_1;
        Configuration.Default.ApiClient.setBasePath(region);

        // Using Analytics API to to get aggregate conversation details
        var analyticsApi = new AnalyticsApi();
        // var conversationAnalyticsAggregate = new AnalyticsConversationsAggregates(analyticsApi);
        // conversationAnalyticsAggregate.PostAnalyticsConversationsAggregates();

        // Create Notfication channel
        var notificationsApi = new NotificationsApi();
        // var notificationChannel = new NotficationChannel(notificationsApi);
        // var channelUri = notificationChannel.CreateNotficationChannel();

        // Subscribe ot converstiona notication for the queue
        var queueId = "dd5d18cc-a538-4925-ab47-a1cb9b29dd80";
        var channelId = "streaming-6-61mcndtm5p8nvupq0714ku413d";
        var channelTopic = new ChannelTopic 
        {
            Id = $"v2.routing.queues.{queueId}.conversations"
        };
        var topics = new List<ChannelTopic>(){ channelTopic };

        try
        {
            ChannelTopicEntityListing result = notificationsApi.PutNotificationsChannelSubscriptions(channelId, topics, true);
        }
        catch (Exception e)
        {
            Debug.Print("Exception when calling NotificationsApi.PutNotificationsChannelSubscriptions: " + e.Message );
        }

        
        // connect the web socket for live data streaming
        var websocketClient = new WebSocketClient("wss://streaming.mypurecloud.com/channels/streaming-6-61mcndtm5p8nvupq0714ku413d");
        await websocketClient.ConnectToWebsocket();

        var queueObservationQuery = new AnalyticsQueryObservation(analyticsApi, queueId);
        queueObservationQuery.CreateAnalyticsQueuesObservationsQuery();
    }
}