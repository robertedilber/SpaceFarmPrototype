using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle
{
    public float Length { get; private set; }
    public float Completion { get; private set; }
    public Action<float> Action { get; set; }
    public Func<float> CompletionRate { get; set; }

    public Cycle(float cycleTime, Action<float> cycleAction)
    {
        Length = cycleTime;
        Action = cycleAction;
        CompletionRate = () => 1;
        Completion = 0;
    }

    public Cycle(float cycleTime, Func<float> cycleCompletionRate)
    {
        Length = cycleTime;
        Action = null;
        CompletionRate = cycleCompletionRate;
        Completion = 0;
    }

    public Cycle(float cycleTime, Action<float> cycleAction, Func<float> cycleCompletionRate)
    {
        Length = cycleTime;
        Action = cycleAction;
        CompletionRate = cycleCompletionRate;
        Completion = 0;
    }

    public void Reset() => Completion = 0;

    public bool Next()
    {
        Completion += CompletionRate();
        Completion = Math.Min(Completion, Length);
        Action?.Invoke(Completion / Length);
        return Completion < Length;
    }
}