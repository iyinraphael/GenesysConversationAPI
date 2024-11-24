using System;
using System.Diagnostics;
using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Client;
using PureCloudPlatform.Client.V2.Model;
using PureCloudPlatform.Client.V2.Extensions;

namespace GenesysSdkPoc
{
    public class AnalyticsConversationsDetails 
    {
        private ConversationsApi apiInstance;
        private List<string> id = new List<string>(){"e42f8684-e054-4561-8797-7262581cc66a"};

        public AnalyticsConversationsDetails(ConversationsApi apiInstance)
        {
            this.apiInstance = apiInstance;
        }

        public void GetAnalyticsConversationsDetails() 
        {
            try {
                AnalyticsConversationWithoutAttributesMultiGetResponse result = apiInstance.GetAnalyticsConversationsDetails(id);
                Debug.WriteLine(result.ToJson());
                Console.WriteLine(result);
            }
            catch (Exception e) 
            {
                Debug.Print("Exception when calling Conversations.GetAnalyticsConversationsDetails: " + e.Message );
            }
        }
    }
}