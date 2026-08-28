using Azure.Communication;
using Azure.Communication.Chat;
using Azure.Communication.Identity;
using System.Net;

try
{
    var connectionString = "YOUR_CONNECTION_STRING";
    var userIdentifier = "YOUR_USER_IDENTIFIER";
    var acsEndpoint = "YOUR_ACS_ENDPOINT";

    var user = new CommunicationUserIdentifier(userIdentifier);
    var tokenClient = new CommunicationIdentityClient(connectionString);
    var token = await tokenClient.GetTokenAsync(user, new[] { CommunicationTokenScope.Chat }, TimeSpan.FromMinutes(60));

    ChatClient chatClient = new ChatClient(new Uri(acsEndpoint), new CommunicationTokenCredential(token.Value.Token));


    var participants = new[]
    {
        new ChatParticipant(user) { }
    };

    CreateChatThreadResult createChatThreadResult = await chatClient.CreateChatThreadAsync(topic: DateTime.UtcNow.ToString(), participants: participants);

    Console.WriteLine($"Chat thread created with ID: {createChatThreadResult.ChatThread.Id}");
    Console.ReadKey();


}
catch(Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}