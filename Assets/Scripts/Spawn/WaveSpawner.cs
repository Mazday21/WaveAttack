using System.Collections;
using UnityEngine;

public class WaveSpawner : StickmanSpawner
{
    [SerializeField] private StickmanWave _wavePrefab;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Transform[] _spawnPoints;

    protected bool _coroutineAllowed = true;
    private readonly int _hashAnimRun = Animator.StringToHash("Run");

    private void Start()
    {
        Initialize(_wavePrefab.gameObject);
    }

    private void Update()
    {
        if (_coroutineAllowed)
        {
            Debug.Log("small");
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
        GetOrInstantiateGameObject(out GameObject waveStickman);
        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
        SetStickman(waveStickman, _spawnPoints[spawnPointNumber].position);
        Animator animator = waveStickman.GetComponent<Animator>();
        animator.Play(_hashAnimRun);
    }

    public override void ReturnGameObject(GameObject gameObject)
    {
        base.ReturnGameObject(gameObject);
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
