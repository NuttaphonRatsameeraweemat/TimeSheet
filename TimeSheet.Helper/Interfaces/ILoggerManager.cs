using Microsoft.AspNetCore.Http;
using System;

namespace TimeSheet.Helper.Interfaces
{
    public interface ILoggerManager
    {
        void CreateNewSession(HttpContext context);
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(Exception ex, string message = null);
    }
}
