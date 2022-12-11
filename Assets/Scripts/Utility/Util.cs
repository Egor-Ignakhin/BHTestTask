using System;
using System.Collections;
using UnityEngine;

public static class Util
{
    public static IEnumerator WaitWhile(Func<bool> check, Action onDone = null)
    {
        while (check.Invoke())
            yield return null;

        onDone?.Invoke();
    }

    public static IEnumerator Delay(float seconds, Action onDone)
    {
        yield return new WaitForSeconds(seconds);
        onDone?.Invoke();
    }
}