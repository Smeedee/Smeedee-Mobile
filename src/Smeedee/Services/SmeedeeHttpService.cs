using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public class SmeedeeHttpService : ISmeedeeService
    {
        public SmeedeeHttpService()
        {
        }
        
        public void LoadTopCommiters(Action<AsyncResult<IEnumerable<Committer>>> callback)
        {
            throw new NotImplementedException();
        }
    }
}
