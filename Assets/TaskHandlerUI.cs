using TMPro;
using UnityEngine;
using Zenject;

public class TaskHandlerUI : MonoBehaviour
{
    [Inject] private TaskHandler _taskHandler;

    [SerializeField] private TextMeshProUGUI _textTask;
    private void Start()
    {
        _taskHandler.OnTextTaskChange += SetTaskText;
    }
    private void SetTaskText(string text)
    {
        _textTask.text = text;
    }
}
