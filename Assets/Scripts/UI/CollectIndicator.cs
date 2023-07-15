using DG.Tweening;
using UnityEngine;
using Zenject;

public class CollectIndicator : MonoBehaviour
{
    [Inject] private TaskHandler _taskHandler;
    [Inject] private CharacterHandler _characterHandler;

    [SerializeField] private Transform _indicator;

    [SerializeField] private float heightUpIndicator = 3f;

    private Vector3 _defaultPosition;
    private void Start()
    {
        _taskHandler.OnRightFruitPick += SpawnIndicator;
        _defaultPosition = _characterHandler.BasketFillController.transform.TransformPoint(Vector3.zero);
        transform.position = _defaultPosition;
    }
    private void SpawnIndicator()
    {
        _indicator.gameObject.SetActive(true);

        DOTween.Sequence()
            .Append(transform.DOMoveY(_indicator.position.y + heightUpIndicator, 2f))
            .Join(transform.DOScale(new Vector3(1,1,1), 2f))
            .AppendCallback(() => SetDefaultValue());
    }
    private void SetDefaultValue()
    {
        transform.localScale = new Vector3(0, 0, 0);    
        transform.position = _defaultPosition;
        _indicator.gameObject.SetActive(false);
    }
}
