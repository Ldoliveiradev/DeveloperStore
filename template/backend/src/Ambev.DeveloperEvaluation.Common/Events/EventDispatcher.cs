namespace Ambev.DeveloperEvaluation.Common.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        public void Publish<T>(T eventMessage)
        {
            Console.WriteLine($"[EVENT] {eventMessage.GetType().Name}: {System.Text.Json.JsonSerializer.Serialize(eventMessage)}");
        }
    }
}
