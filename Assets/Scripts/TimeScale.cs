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

    private void Update()
    {
        if(_time != 0)
            Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
    }

    public void DilationTime(float slowdownTimes, float time)
    {
        if (Time.timeScale != 1)
            throw new System.Exception("Time has already slowed down");

        _time = time;
        Time.timeScale /= slowdownTimes;
        StartCoroutine(DelayNormalizeTimeScale(time));
    }

    private IEnumerator DelayNormalizeTimeScale(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        _time = 0;
        Time.fixedDeltaTime = _fixedDeltaTime;
    }
}
