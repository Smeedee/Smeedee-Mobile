using System;

namespace Smeedee
{
    public class AsyncResult<T>
    {
        public AsyncResult(T result)
        {
            Result = result;
        }
        
        public T Result { get; private set; }
    }
}
