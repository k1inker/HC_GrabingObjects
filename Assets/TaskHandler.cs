using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaskHandler : MonoBehaviour
{
    [Header("FruitSetup")]
    [SerializeField] private TypeFruit.FruitType[] randomSampling;
    public TypeFruit.FruitType targetTypeFruit { get; private set; }

    [Header("CountSetup")]
    [SerializeField] private int targetCount;
    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = 5;

    public Action<string> OnTextTaskChange;
    private void Start()
    {
        targetTypeFruit = randomSampling[Random.Range(0, randomSampling.Length)];

        targetCount = Random.Range(minCount, maxCount);
        UpdateText(targetCount);
    }
    private void UpdateText(int targetCount)
    {
        string taskText = $"Collect {targetCount} {TypeFruit.TypeToString(targetTypeFruit)}";
        OnTextTaskChange?.Invoke(taskText);
    }
    public void PickRightType()
    {
        targetCount--;
        UpdateText(targetCount);
        if (targetCount <= 0)
            Debug.Log("Victory");
    }
}
