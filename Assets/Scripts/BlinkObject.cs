using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkObject : MonoBehaviour
{ 
    private Renderer render;
    private float baseDuration = .3f;
    private float minDuration = .05f;

    bool state;
    float startTime;
    float duration;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private void Start()
    {
        startTime = Time.time;
        duration = baseDuration;
    }

    private void Update()
    {
        //Debug.Log(Time.time - startTime < duration);
        if (Time.time - startTime < duration)
            return;

        state = !state;
        duration = (duration - .1f < minDuration) ? minDuration : duration - .1f;
        startTime = Time.time;

        render.enabled = state;
    }

    public void SetActive(bool isActive)
    {
        if (enabled == isActive)
            return;

        enabled = isActive;


        if (isActive)
        {
            state = true;
            startTime = Time.time;
            duration = baseDuration;
        }

        render.enabled = true;
    }
}
