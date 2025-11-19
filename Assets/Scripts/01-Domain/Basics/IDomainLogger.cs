using System;

namespace Domain {
    public interface IDomainLogger {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(Exception exception);
        void LogTODO(string message);
        void LogStatic(string message, string id, float delay = 3f);
    }
}