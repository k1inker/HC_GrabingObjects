using System.Collections;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Transform FirstPoint;
    public Transform SecondPoint;

    [Range(.1f,1f)] public float Speed;

    [SerializeField] private Material _material;
    private float value = 0;
    private void Start()
    {
        value = 0;
        StartCoroutine(PlusValue(Speed / 1000f));
    }
    private IEnumerator PlusValue(float iteration)
    {
        while(true)
        {
            yield return new WaitForSeconds(iteration);
            value += iteration;
            _material.SetTextureOffset("_MainTex", new Vector2(-value, 0));
        }
    }
}
