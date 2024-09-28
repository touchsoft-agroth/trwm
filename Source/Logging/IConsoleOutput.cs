namespace trwm.Source.Logging
{
    public interface IConsoleOutput
    {
        void Write(string message);
        void Warn(string message);
    }
}