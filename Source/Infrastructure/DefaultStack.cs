using System.Collections.Generic;

namespace trwm.Source.Infrastructure
{
    public class DefaultStack<T>
    {
        private readonly Stack<T> _underlyingStack;
        private T _defaultValue;

        public DefaultStack()
        {
            _underlyingStack = new Stack<T>();
        }

        public void SetDefault(T defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public T Pop()
        {
            if (_underlyingStack.Count > 0)
            {
                return _underlyingStack.Pop();
            }

            return _defaultValue;
        }

        public T Peek()
        {
            if (_underlyingStack.Count > 0)
            {
                return _underlyingStack.Peek();
            }

            return _defaultValue;
        }

        public void Push(T element)
        {
            _underlyingStack.Push(element);
        }
    }
}