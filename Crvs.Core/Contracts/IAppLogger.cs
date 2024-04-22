using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Core.Contracts
{
    public interface IAppLogger<T>
    {
        void LogError(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogInfo(string message, params object[] args);
    }
}
