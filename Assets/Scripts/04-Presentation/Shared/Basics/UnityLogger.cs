using Domain;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure {
    public class UnityLogger : IDomainLogger {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        private readonly List<ILogSink> _sinks = new();

        #endregion

        //include all events here
        #region Events

        #endregion

        //include all constructors here
        #region Constructors

        public UnityLogger() {
            UnityEngine.Application.logMessageReceived += Application_LogMessageReceived;
        }


        #endregion

        //include all public methods here
        #region Public Methods

        public void AddSink(ILogSink sink) {
            if(sink == null) return;
            _sinks.Add(sink);
        }

        public void Log(string message) {
            Debug.Log($"<color=white>[LOG] {message}</color>");
            //_sinks.ForEach(_sinks => _sinks.Log(message));
        }
        public void LogWarning(string message) {
            Debug.Log($"<color=yellow>[WARN] {message}</color>");
            //_sinks.ForEach(_sinks => _sinks.LogWarning(message));
        }
        public void LogError(string message) {
            Debug.LogError($"<color=red>[ERROR] {message}</color>");
            //_sinks.ForEach(_sinks => _sinks.LogError(message));
        }

        public void LogException(Exception exception) {
            Debug.LogException(exception);
        }

        public void LogTODO(string message) {
            Debug.Log($"<color=orange>[TODO] {message}</color>");
        }

        public void LogStatic(string message, string id, float delay = 3) {
            _sinks.ForEach(_sinks => _sinks.LogStatic(message, id, delay));
        }

        #endregion

        //include all private methods here
        #region Private Methods

        private void Application_LogMessageReceived(string condition, string stackTrace, LogType type) {
            switch(type) {
                case LogType.Error:
                case LogType.Exception:
                    _sinks.ForEach(sink => sink.LogError(condition));
                    break;
                case LogType.Warning:
                    _sinks.ForEach(sink => sink.LogWarning(condition));
                    break;
                case LogType.Log:
                    _sinks.ForEach(sink => sink.Log(condition));
                    break;
            }
        }

        #endregion


    }
}