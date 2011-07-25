namespace Smeedee.Services
{
    public class AsyncResult<T>
    {
        public T Result { get; private set; }

        public AsyncResult(T result)
        {
            Result = result;
        }
    }
}