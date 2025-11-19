using System.Collections;

namespace Domain {
    public interface ICoroutineRunner {
        public void StartCoroutine(IEnumerator coroutine);
    }
}