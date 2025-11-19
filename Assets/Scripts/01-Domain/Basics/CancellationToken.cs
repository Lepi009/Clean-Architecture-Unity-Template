namespace Domain {
    public class CancellationToken {
        private bool _isCanceled;
        public bool IsCanceled => _isCanceled;

        internal void Cancel() => _isCanceled = true;
    }

    public class CancellationTokenSource {
        private readonly CancellationToken _token = new CancellationToken();
        public CancellationToken Token => _token;

        public void Cancel() => _token.Cancel();
    }

}