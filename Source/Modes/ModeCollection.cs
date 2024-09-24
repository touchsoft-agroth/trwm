using System.Collections.Generic;

namespace trwm.Source.Modes
{
    public class ModeCollection
    {
        private readonly Dictionary<ModeType, Mode> _modes;

        public ModeCollection()
        {
            _modes = new Dictionary<ModeType, Mode>();
        }
        
        public void Add(ModeType type, Mode instance)
        {
            _modes.Add(type, instance);
        }

        public Mode? Get(ModeType modeType)
        {
            return _modes.GetValueOrDefault(modeType);
        }
    }
}