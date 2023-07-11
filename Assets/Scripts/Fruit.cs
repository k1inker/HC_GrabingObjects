using Zenject;
using UnityEngine;
using NTC.Global.Pool;
using System.Collections;
using UnityEngine.EventSystems;

public class Fruit : MonoBehaviour, IPoolItem, IPointerClickHandler
{
    public TypeFruit.FruitType fruitType;

    [Inject] private Conveyor _paramConveyor;
    [Inject] private TaskHandler _taskHandler;

    private float value = 0;
    private IEnumerator Move(float iteration)
    {
        while (value <= 1)
        {
            yield return new WaitForSeconds(iteration);
            value += iteration;
            gameObject.transform.position = Vector3.Lerp(_paramConveyor.FirstPoint.position, _paramConveyor.SecondPoint.position, value);
        }
        NightPool.Despawn(this);
    }
    public void PickUp()
    {
        Debug.Log(gameObject);
    }
    public void OnSpawn()
    {
        value = 0;
        StartCoroutine(Move(_paramConveyor.Speed / 1000f));
    }
    public void OnDespawn()
    {
        StopAllCoroutines();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_taskHandler.targetTypeFruit == fruitType)
        {
            _taskHandler.PickRightType();
            NightPool.Despawn(this);
        }
    }
}
