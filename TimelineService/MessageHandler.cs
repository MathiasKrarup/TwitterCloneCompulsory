using EasyNetQ;
using SharedMessage;

namespace TimelineService
{
    public class MessageHandler : BackgroundService
    {
        private void HandlePostMessage(PostMessage message)
        {
            Console.WriteLine($"Got Post: {message.Message}!");
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Message handler is running..");

            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
            );

            messageClient.Listen<PostMessage>(HandlePostMessage, "post-message");

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);
            }
            Console.WriteLine("Message handler is stopping..");
        }
    }
}