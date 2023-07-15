using System.Collections;
using UnityEngine;
using Zenject;

public class Conveyor : MonoBehaviour
{
    [Inject] TaskHandler _taskHandler;
    public Transform FirstPoint;
    public Transform SecondPoint;

    [Range(1f,5f)] public float Speed;

    [SerializeField] private Material _material;
    private float value = 0;
    private void Start()
    {
        value = 0;
        Speed /= 1000f;
        _taskHandler.OnResultGame += InactiveConveyor;
    }
    private void FixedUpdate()
    {
        value += Speed;
        _material.SetTextureOffset("_MainTex", new Vector2(-value, 0));
    }
    private void InactiveConveyor(bool isWin)
    {
        if(isWin)
        {
            gameObject.SetActive(false);
        }
    }
}
