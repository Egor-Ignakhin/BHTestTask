using System;
using System.Collections;

public static class Util
{
    public static IEnumerator WaitWhile(Func<bool> check, Action onDone = null)
    {
        while (check.Invoke())
            yield return null;

        onDone?.Invoke();
    }
}
