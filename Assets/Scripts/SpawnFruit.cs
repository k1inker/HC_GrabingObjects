using System.Collections;
using UnityEngine;
using Zenject;
using NTC;
using NTC.Global.Pool;
using static UnityEditorInternal.ReorderableList;

public class SpawnFruit : MonoBehaviour
{
    [SerializeField] private float _fruitPerSeconds;

    [SerializeField] private GameObject[] _fruitPref;

    [Inject]private DiContainer diContainer;
    private void Start()
    {
        StartCoroutine(SpawnObject());
    }
    private IEnumerator SpawnObject()
    {
        while(true)
        {
            yield return new WaitForSeconds(_fruitPerSeconds);
            GameObject selectObject = _fruitPref[Random.Range(0, _fruitPref.Length)];
            NightPool.Spawn(selectObject, diContainer,default, selectObject.transform.rotation);
        }
    }
}
