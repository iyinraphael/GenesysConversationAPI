using System.Diagnostics;
using System.Net.WebSockets;
using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Client;
using PureCloudPlatform.Client.V2.Extensions;
using GenesysSdkPoc;

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
        // var analyticsApi = new AnalyticsApi();
        // var conversationAnalyticsAggregate = new AnalyticsConversationsAggregates(analyticsApi);
        // conversationAnalyticsAggregate.PostAnalyticsConversationsAggregates();

        // Create Notfication channel
        // var notificationsApi = new NotificationsApi();
        // var notificationChannel = new NotficationChannel(notificationsApi);
        // var channelUri = notificationChannel.CreateNotficationChannel();

        // Connect to notification channel via websocket
        // var ws = new ClientWebSocket();
        // await ws.ConnectAsync(new Uri("wss://streaming.mypurecloud.com/channels/streaming-6-61mcndtm5p8nvupq0714ku413d"), CancellationToken.None);
        // Debug.WriteLine("Connected");

        var websocketClient = new WebSocketClient("wss://streaming.mypurecloud.com/channels/streaming-6-61mcndtm5p8nvupq0714ku413d");
        await websocketClient.ConnectToWebsocket();
    }
}