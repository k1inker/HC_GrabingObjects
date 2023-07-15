using Zenject;
using UnityEngine;
using NTC.Global.Pool;
using System.Collections;
using UnityEngine.EventSystems;

public class Fruit : MonoBehaviour, IPoolItem, IPointerClickHandler
{
    public TypeFruit.FruitType fruitType;
    public GameObject modelFruit;

    [Inject] private Conveyor _paramConveyor;
    [Inject] private TaskHandler _taskHandler;
    [Inject] private CharacterHandler _characterHandler;

    private float value = 0;
    private void FixedUpdate()
    {
        value += _paramConveyor.Speed;
        gameObject.transform.position = Vector3.Lerp(_paramConveyor.FirstPoint.position, _paramConveyor.SecondPoint.position, value);

        if (value > 1)
            NightPool.Despawn(this);
    }
    public void OnSpawn()
    {
        value = 0;
    }
    public void OnDespawn()
    {
        StopAllCoroutines();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_characterHandler.isPickUpFruit)
            return;

        _characterHandler.PickUpFruit(transform, this);
        _taskHandler.PickFruit(fruitType);
    }
}
