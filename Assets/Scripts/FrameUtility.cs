using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FrameUtility
{
    public static LTDescr WaitForFrame(int frameCount, System.Action callback)
    {
        if(frameCount <= 0)
        {
            callback.Invoke();
            return null;
        }

        frameCount--;
        return LeanTween.delayedCall(Time.deltaTime, () => WaitForFrame(frameCount, callback));
    }
}
