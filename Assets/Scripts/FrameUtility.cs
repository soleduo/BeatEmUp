using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FrameUtility
{
    public static void WaitForFrame(int frameCount, System.Action callback)
    {
        if(frameCount <= 0)
        {
            callback.Invoke();
            return;
        }

        frameCount--;
        LeanTween.delayedCall(Time.deltaTime, () => WaitForFrame(frameCount, callback));
    }
}
