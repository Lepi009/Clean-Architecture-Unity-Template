using System;
using System.Collections;
using Domain;
using UnityEngine.Networking;

namespace Infrastructure {
    public class WebRequestFileProvider : IFileProvider {

        //include all public methods here
        #region Public Methods

        public void TryReadFileAsync(string path, Action<bool, string> OnFinished) {
            ServiceLocator.CoroutineRunner.StartCoroutine(WebRequest(path, OnFinished));
        }

        #endregion


        //include all private methods here
        #region Private Methods

        private IEnumerator WebRequest(string path, Action<bool, string> OnFinished) {
            // Special case to access StreamingAsset content on Android and Web
            using var request = UnityWebRequest.Get(path);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if(request.responseCode >= 300) {
                ServiceLocator.Logger.LogError($"{request.error}: {request.downloadHandler.text}");
                OnFinished?.Invoke(false, "");
            }
            else {
                var fileContent = request.downloadHandler.text;
                OnFinished?.Invoke(true, fileContent);
            }
        }

        #endregion

    }
}