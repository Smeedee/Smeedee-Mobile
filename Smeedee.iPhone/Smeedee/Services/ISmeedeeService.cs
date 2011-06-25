using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
	public interface ISmeedeeService
	{
		void LoadTopCommiters(Action<AsyncResult<IEnumerable<Commiter>>> callback);
	}
	
	public class AsyncResult<T>
	{
		public AsyncResult(T result)
		{
			Result = result;
		}
		
		public T Result { get; private set; }
	}
}
