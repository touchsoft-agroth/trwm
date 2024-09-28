using System;

namespace trwm.Source.Logging
{
    public class Logger
    {
        public static void BindOutput(IConsoleOutput output)
        {
            _output = output;
        }

        public static void UnbindOutput()
        {
            _output = null;
        }
        
        private static IConsoleOutput Output
        {
            get
            {
                if (_output == null)
                {
                    throw new InvalidOperationException("Output is not bound");
                }

                return _output;
            }
        }

        private static IConsoleOutput? _output;
        
        private readonly object? _source;

        public Logger(object? source)
        {
            _source = source;
        }

        public void Info(string message)
        {
            Output.Write(AppendSourceType(message));
        }

        public void Warning(string message)
        {
            Output.Warn(AppendSourceType(message));
        }

        private string AppendSourceType(string message)
        {
            var prefix = _source == null ? "[NULL] " : $"[{_source.GetType().Name}] ";
            return message.Insert(0, prefix);
        }
    }
}