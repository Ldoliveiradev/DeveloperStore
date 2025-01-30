namespace Ambev.DeveloperEvaluation.Common.Events
{
    public interface IEventDispatcher
    {
        void Publish<T>(T eventMessage);
    }
}
