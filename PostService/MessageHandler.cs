using EasyNetQ;
using SharedMessage;

namespace PostService
{
    public class MessageHandler : BackgroundService
    {
        private void HandleTimelineMessage(TimelineMessage message)
        {
            Console.WriteLine($"Got Post: {message.Message}!");
        }
        private void HandleUserMessage(UserMessage message)
        {
            Console.WriteLine($"Got User Message: {message.Message}!");
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Message handler is running..");

            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
            );

            messageClient.Listen<TimelineMessage>(HandleTimelineMessage, "timeline-message");
            messageClient.Listen<UserMessage>(HandleUserMessage, "user-message");

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);
            }
            Console.WriteLine("Message handler is stopping..");
        }
    }
}