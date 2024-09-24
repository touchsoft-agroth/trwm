namespace trwm.Source.Game
{
    public readonly struct WindowHandle
    {
        public string Identifier { get; }

        public WindowHandle(string identifier)
        {
            Identifier = identifier;
        }
    }
}