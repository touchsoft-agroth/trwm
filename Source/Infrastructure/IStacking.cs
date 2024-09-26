namespace trwm.Source.Infrastructure
{
    public interface IStacking<T>
    {
        void Push(T element);
        T Pop();
    }
}