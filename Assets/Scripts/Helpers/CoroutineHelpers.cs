using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class CoroutineHelpers
{
    public static void StartCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine thread, IEnumerator coroutine)
    {
        if (thread != null)
            monoBehaviour.StopCoroutine(thread);
        thread = monoBehaviour.StartCoroutine(coroutine);
    }

    public static void StopCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine thread)
    {
        if (thread != null)
        {
            monoBehaviour.StopCoroutine(thread);
            thread = null;
        }
    }
}
