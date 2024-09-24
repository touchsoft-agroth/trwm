using System;
using System.Collections.Generic;

namespace trwm.Source.Infrastructure
{
    public class ChangeDetector<T>
    {
        private readonly Func<T> _resolverFunc;
        private T _lastValue;
        private bool _isInitialized = false;

        public ChangeDetector(Func<T> resolverFunc)
        {
            _resolverFunc = resolverFunc;
        }

        public bool HasChanged(out T currentValue)
        {
            currentValue = _resolverFunc();

            if (!_isInitialized)
            {
                _lastValue = currentValue;
                _isInitialized = true;
                return true;
            }

            if (!EqualityComparer<T>.Default.Equals(_lastValue, currentValue))
            {
                _lastValue = currentValue;
                return true;
            }

            return false;
        }
    }
}