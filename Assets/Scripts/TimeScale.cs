using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    private float _time = 0;
    private float _fixedDeltaTime;

    private void Awake()
    {
        _fixedDeltaTime = Time.fixedDeltaTime;
    }

    public void DilationTime(float timeScale, float time)
    {
        if (Time.timeScale != 1)
            throw new System.Exception("Time has already setting");

        _time = time;
        Time.timeScale = timeScale;
        StartCoroutine(DelayNormalizeTimeScale(time));
        StartCoroutine(SetFixedDeltaTime());
    }

    private IEnumerator SetFixedDeltaTime()
    {
        while(_time != 0)
        {
            Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
            yield return null;
        }
        Time.fixedDeltaTime = _fixedDeltaTime;
    }

    private IEnumerator DelayNormalizeTimeScale(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        _time = 0;
    }
}
