using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ManagerUI : MonoBehaviour
{
    [Inject] private TaskHandler _taskHandler;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _textTask;
    [SerializeField] private TextMeshProUGUI _textFill;

    [Header("ResultPanels")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private float _timeToStartActionWining = 4f;

    [Header("Settings animation button")]
    [SerializeField] private float _timeAnimation;
    [SerializeField] private float _scaleButton;
    [SerializeField] private Transform _nextLevelButton;
    [SerializeField] private Transform _tryAgainButton;
    private void Awake()
    {
        _taskHandler.OnTargetCountChange += SetTaskText;

        _taskHandler.OnFillBasket += SetFillText;

        _taskHandler.OnResultGame += ActiveResultPanel;
    }
    private void SetTaskText(string text)
    {
        _textTask.text = text;
    }
    private void SetFillText(string text)
    {
        _textFill.text = text;
    }
    private void ActiveResultPanel(bool isWin)
    {
        if(isWin)
        {
            StartCoroutine(WinningAction());
        }
        else
        {
            _losePanel.SetActive(true);
            _textTask.gameObject.SetActive(false);
            _textFill.gameObject.SetActive(false);
            AnimationButton(1, _tryAgainButton);
        }
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator WinningAction()
    {
        yield return new WaitForSeconds(_timeToStartActionWining);
        _winPanel.SetActive(true);
        _textTask.gameObject.SetActive(false);
        _textFill.gameObject.SetActive(false);
        AnimationButton(1, _nextLevelButton);
    }
    private void AnimationButton(int direction, Transform buttonTransform)
    {
        float scaleX = buttonTransform.transform.localScale.x + (_scaleButton * direction);
        float scaleY = buttonTransform.transform.localScale.y + (_scaleButton * direction);

        DOTween.Sequence().
            Append(buttonTransform.DOScale(new Vector3(scaleX, scaleY), _timeAnimation))
            .OnComplete(() => AnimationButton(-direction, buttonTransform));
    }
}
