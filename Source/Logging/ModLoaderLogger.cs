using MelonLoader;

namespace trwm.Source.Logging
{
    public class ModLoaderLogger : IConsoleOutput
    {
        private readonly MelonLogger.Instance _loggerInstance;
        
        public ModLoaderLogger(MelonLogger.Instance loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }

        public void Write(string message)
        {
            _loggerInstance.Msg(message);
        }
    }
}