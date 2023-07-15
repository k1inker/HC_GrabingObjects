using System.Collections;
using UnityEngine;
using Zenject;
using NTC.Global.Pool;

public class SpawnFruit : MonoBehaviour
{
    [SerializeField] private float _fruitPerSeconds;
    [SerializeField] private float _timeAmplitude;

    [SerializeField] private GameObject[] _fruitPref;

    [Inject] private DiContainer _diContainer;
    [Inject] private TaskHandler _taskHandler;
    private void Start()
    {
        StartCoroutine(SpawnObject());
        _taskHandler.OnResultGame += StopSpawn;
    }
    private IEnumerator SpawnObject()
    {
        while(true)
        {
            float time = Random.Range(_fruitPerSeconds - _timeAmplitude, _fruitPerSeconds + _timeAmplitude);
            yield return new WaitForSeconds(_fruitPerSeconds);
            GameObject selectObject = _fruitPref[Random.Range(0, _fruitPref.Length)];
            NightPool.Spawn(selectObject, _diContainer,transform, selectObject.transform.rotation);
            
        }
    }
    private void StopSpawn(bool isWin)
    {
        gameObject.SetActive(false);
    }
}
