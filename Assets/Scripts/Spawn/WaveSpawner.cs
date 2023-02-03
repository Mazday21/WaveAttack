using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : StickmanSpawner
{
    [SerializeField] private StickmanWave _wavePrefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Transform[] _spawnPoints;

    protected bool _coroutineAllowed = true;
    private readonly int _hashAnimRun = Animator.StringToHash("Run");
    public Queue<GameObject> Pool;

    private void Awake()
    {
        Pool = InitializePool(Pool);
        InitializePrefab(_wavePrefab.gameObject, Pool);
    }

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

    private void SpawnSet()
    {
        foreach(Transform t in _spawnPoints)
        {
            GetOrInstantiateGameObject(out GameObject waveStickman, Pool);
            SetStickman(waveStickman, t.position);
            Animator animator = waveStickman.GetComponent<Animator>();
            animator.Play(_hashAnimRun);
        }
    }

    public override void ReturnGameObject(GameObject gameObject, Queue<GameObject> pool)
    {
        base.ReturnGameObject(gameObject, pool);
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
