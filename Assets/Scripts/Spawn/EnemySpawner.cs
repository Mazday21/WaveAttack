using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : StickmanSpawner
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] protected Level _level;
    
    private bool _coroutineAllowed = true;
    private readonly int _hashAnimRun = Animator.StringToHash("Run");
    private float _probability;
    private float _ratioToDecimalFraction = 10f;

    private void Update()
    {
        if (_coroutineAllowed)
        {
            StartCoroutine(DelayAppearance());
        }
    }

    protected IEnumerator DelayAppearance()
    {
        _coroutineAllowed = false;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);
        SpawnSet();
        yield return waitForSeconds;
        _coroutineAllowed = true;
    }

    protected virtual void SpawnSet()
    {
        foreach(Transform t in _spawnPoints)
        {
            if (Probability())
            {
            Pool.GetOrInstantiateGameObject(out GameObject waveStickman);
            SetStickman(waveStickman, t.position);
            Animator animator = waveStickman.GetComponent<Animator>();
            animator.Play(_hashAnimRun);
            }
        }
    }

    public override void ReturnGameObject(GameObject gameObject)
    {
        Pool.ReturnGameObject(gameObject);
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private bool Probability()
    {
        _probability = _level.Value / _ratioToDecimalFraction;
        if (Random.Range(0f, 1f) <= _probability) return true;
        else return false;
    }
}
