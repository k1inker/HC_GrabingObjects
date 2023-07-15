using System;
using TMPro.Examples;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class TaskHandler : MonoBehaviour
{
    public TypeFruit.FruitType targetTypeFruit { get; private set; }

    [Inject] private CharacterHandler _character;
    [Header("FruitSetup")]
    [SerializeField] private TypeFruit.FruitType[] randomSampling;

    [Header("CountSetup")]
    public int targetCount;
    public int maxBasketAmount;
    private int fruitCountInBasket = 0;

    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = 5;

    public Action OnRightFruitPick;
    public Action<string> OnTargetCountChange;
    public Action<string> OnFillBasket;
    public Action<bool> OnResultGame;
    private void Start()
    {
        targetTypeFruit = randomSampling[Random.Range(0, randomSampling.Length)];

        targetCount = Random.Range(minCount, maxCount);

        maxBasketAmount = Random.Range(targetCount, _character.BasketFillController.countFill);

        UpdateTaskText();
        UpdateFillBasketText();
    }
    private void UpdateTaskText()
    {
        string taskText = $"Collect {targetCount} {TypeFruit.TypeToString(targetTypeFruit)}";
        OnTargetCountChange?.Invoke(taskText);
    }
    private void UpdateFillBasketText()
    {
        string fillText = $"{fruitCountInBasket}/{maxBasketAmount}";
        OnFillBasket?.Invoke(fillText);
    }
    public void PickFruit(TypeFruit.FruitType fruitType)
    {
        if (fruitType == targetTypeFruit)
        {
            targetCount--;
            UpdateTaskText();
            OnRightFruitPick?.Invoke();
        }
        fruitCountInBasket++;
        UpdateFillBasketText();

        if (targetCount <= 0)
        {
            Camera.main.GetComponent<CameraController>().enabled = true;
            OnResultGame?.Invoke(true);
        }
        else if(fruitCountInBasket >= maxBasketAmount)
        {
            OnResultGame?.Invoke(false);
        }
    }
}
