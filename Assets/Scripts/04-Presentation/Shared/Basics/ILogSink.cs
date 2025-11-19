namespace Infrastructure {
    public interface ILogSink {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogStatic(string message, string id, float delay);

    }
}