using DitzelGames.FastIK;
using NTC.Global.Pool;
using System.Collections;
using TMPro.Examples;
using UnityEngine;
using Zenject;

public class CharacterHandler : MonoBehaviour
{
    [Inject] private TaskHandler _taskHandler;
    public BasketFillController BasketFillController { get; private set; }

    public bool isPickUpFruit;

    [Header("Transforms")]
    [SerializeField] private Transform targetTransformLeftHand;
    [SerializeField] private Transform transformPickHand;

    [SerializeField] private float duration = 1f;
    [SerializeField] private ParticleSystem _confetti;

    [SerializeField] private float _timeToStartActionWining = 2f;

    private GameObject _modelFruit;
    private Animator _animator;
    private FastIKFabric _ikFabric;
    private Fruit _pickFruit;
    private void Awake()
    {
        _ikFabric = GetComponentInChildren<FastIKFabric>();
        BasketFillController = GetComponentInChildren<BasketFillController>();

        _animator = GetComponent<Animator>();
        _taskHandler.OnResultGame += SetResultAnimation;
    }
    public void PickUpFruit(Transform transform, Fruit fruit)
    {
        //set flags
        isPickUpFruit = true;
        _ikFabric.enabled = true;
        // start moving hand to conveyor
        StartCoroutine(TransformTarget(transform, false));
        // get model and exmple class
        _pickFruit = fruit;
        _modelFruit = fruit.modelFruit;
    }
    public void PutingFruitOnBasket()
    {
        //spawning in basket model
        BasketFillController.FillBasket(_modelFruit);
        //destroy model in hand
        Destroy(_modelFruit);

        // stop moving hand by code
        _ikFabric.enabled = false;
        // enable picking new fruit
        isPickUpFruit = false;

        _animator.SetBool("isInteracting", false);
    }
    private void HoldHandFruit()
    {
        _animator.SetBool("isInteracting", true);
        // instatiate fruit in hand 
        _modelFruit = Instantiate(_modelFruit, transformPickHand);
        // despawn fruit in conveyor
        NightPool.Despawn(_pickFruit);
        // start moving hand to basket
        StartCoroutine(TransformTarget(BasketFillController.transform, true));
    }
    private IEnumerator TransformTarget(Transform transform, bool isPuting)
    {
        Vector3 startPosition = targetTransformLeftHand.position;
        Vector3 targetPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            float t = elapsedTime / duration;
            targetTransformLeftHand.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        targetTransformLeftHand.position = targetPosition;

        if (!isPuting)
        {
            HoldHandFruit();
        }
        else
        {
            PutingFruitOnBasket();
        }
    }
    private void SetResultAnimation(bool isWin)
    {
        if(isWin)
        {
            StartCoroutine(WinningAction());
        }
        else
        {
            _animator.SetTrigger("losing");
        }
    }
    private IEnumerator WinningAction()
    {

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
        float elapsedTime = 0f;
        float rotationDuration = _timeToStartActionWining;

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // finish rotate
        transform.rotation = targetRotation;

        _animator.SetTrigger("winning");
        _confetti.Play();
    }
}
