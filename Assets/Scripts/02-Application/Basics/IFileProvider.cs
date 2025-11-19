using System;

namespace Application {
    public interface IFileProvider {
        public void TryReadFileAsync(string path, Action<bool, string> OnFinished);
    }
}