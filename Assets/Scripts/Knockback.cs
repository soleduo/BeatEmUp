using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback {

    int defaultFrameCount;

    public Knockback(GameObject go, Vector2 force, int frameCount)
    {
        defaultFrameCount = frameCount;
        DoKnockback(go, force, frameCount);
    }

	public void DoKnockback(GameObject go, Vector2 force, int frameCount)
    {
        go.transform.position = (Vector2)go.transform.position + force / defaultFrameCount;

        //Debug.Log(frameCount);
        if (frameCount > 0)
            LeanTween.delayedCall(Time.deltaTime, () => { DoKnockback(go, force, frameCount-1); });

    }
}
