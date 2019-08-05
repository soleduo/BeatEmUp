using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FrameUtility
{
    public static LTDescr WaitForFrame(int frameCount, System.Action callback)
    {
        LTDescr descr = LeanTween.delayedCall(Time.fixedDeltaTime * frameCount, () => callback.Invoke());
        return descr;
    }
}
