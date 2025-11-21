using System;
using System.IO;
using Application;
using Domain;

namespace Infrastructure {
    public class ReadAllTextFileProvider : IFileProvider {

        //include all public methods here
        #region Public Methods

        public async void TryReadFileAsync(string path, Action<bool, string> OnFinished) {
            string fileContent = "";
            bool success = false;
            try {
                fileContent = await File.ReadAllTextAsync(path);
                success = true;
            }
            catch(Exception e) {
                ServiceLocator.Logger.LogError($"Cannot read file {path}");
                ServiceLocator.Logger.LogException(e);
            }
            OnFinished?.Invoke(success, fileContent);
        }

        #endregion


        //include all private methods here
        #region Private Methods

        #endregion

    }
}