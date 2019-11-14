using System;
using System.Collections;
using UnityEngine;

public static class CoroutineTemplates
{
    public static void ExecuteWhen(ref Coroutine coroutine, MonoBehaviour caller, Func<bool> condition, Action action)
    => caller.StartCoroutine(ref coroutine, ExecuteWhenCoroutine(condition, action));

    private static IEnumerator ExecuteWhenCoroutine(Func<bool> condition, Action action)
    {
        yield return new WaitUntil(condition);
        action();
    }

    public static void ExecuteWhile(ref Coroutine coroutine, MonoBehaviour caller, Func<bool> condition, Action action, YieldInstruction yieldInstruction, Action endAction = null)
        => caller.StartCoroutine(ref coroutine, ExecuteWhileCoroutine(condition, action, yieldInstruction, endAction));

    private static IEnumerator ExecuteWhileCoroutine(Func<bool> condition, Action action, YieldInstruction yieldInstruction, Action endAction = null)
    {
        while (condition())
        {
            action();
            yield return yieldInstruction;
        }

        endAction?.Invoke();
    }

    public static void ExecuteCycle(ref Coroutine coroutine, MonoBehaviour caller, Cycle cycle, Action endAction = null)
    => caller.StartCoroutine(ref coroutine, ExecuteCycleCoroutine(cycle, endAction));

    private static IEnumerator ExecuteCycleCoroutine(Cycle cycle, Action endAction = null)
    {
        while (cycle.Next())
            yield return null;

        endAction?.Invoke();
    }

    public static void ExecuteAfter(ref Coroutine coroutine, MonoBehaviour caller, Action action, float timeInSeconds)
        => caller.StartCoroutine(ref coroutine, ExecuteAfterCoroutine(action, timeInSeconds));

    private static IEnumerator ExecuteAfterCoroutine(Action action, float timeInSeconds, bool realtime = false)
    {
        if (realtime)
            yield return new WaitForSecondsRealtime(timeInSeconds);
        else
            yield return new WaitForSeconds(timeInSeconds);

        action.Invoke();
    }
}