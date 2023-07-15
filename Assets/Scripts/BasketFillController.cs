using UnityEngine;
public class BasketFillController : MonoBehaviour
{
    public int countFill { get { return spawnPoints.Length; } }

    [SerializeField] private Transform[] spawnPoints;

    private int idFillFruit = 0;
    public void FillBasket(GameObject gameObject)
    {
        if (idFillFruit > spawnPoints.Length - 1)
            return;

        Instantiate(gameObject, spawnPoints[idFillFruit],false);

        idFillFruit++;
    }
}
