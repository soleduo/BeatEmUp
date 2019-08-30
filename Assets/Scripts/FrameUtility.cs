using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public static class FrameUtility
{
    public static float deltaTime = 15f;

    public static LTDescr WaitForFrame(int frameCount, System.Action callback)
    {
        LTDescr descr = LeanTween.delayedCall(Time.fixedDeltaTime * frameCount, () => callback.Invoke());
        return descr;
    }

    public static void DelayedCall(int frameCount, System.Action callback, CancellationToken cancellationToken)
    {
        Task.Delay(System.TimeSpan.FromMilliseconds(deltaTime * frameCount), cancellationToken).ContinueWith((Task) =>{ callback(); }, cancellationToken);
    }
}
