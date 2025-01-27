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
        var accessTokenInfo = Configuration.Default.ApiClient.PostToken("", "");
        Console.WriteLine("Access token=" + accessTokenInfo.AccessToken);

        // Setting purecloud org region 
        PureCloudRegionHosts region = PureCloudRegionHosts.us_east_1;
        Configuration.Default.ApiClient.setBasePath(region);

        // Using Analytics API to to get aggregate conversation details
        var analyticsApi = new AnalyticsApi();
        var conversationAnalyticsAggregate = new AnalyticsConversationsAggregates(analyticsApi);
        conversationAnalyticsAggregate.PostAnalyticsConversationsAggregates();

        // Create Notfication channel
        var notificationsApi = new NotificationsApi();
        var notificationChannel = new NotficationChannel(notificationsApi);
        var channelUri = notificationChannel.CreateNotficationChannel();

        // Subscribe ot converstiona notication for the queue
        var queueId = "";
        var channelId = "";
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
        var websocketClient = new WebSocketClient("");
        await websocketClient.ConnectToWebsocket();

        var queueObservationQuery = new AnalyticsQueryObservation(analyticsApi, queueId);
        queueObservationQuery.CreateAnalyticsQueuesObservationsQuery();
    }
}