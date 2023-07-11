using UnityEngine;
using Zenject;

public class TaskManagerInstaller : MonoInstaller
{
    [SerializeField] private TaskHandler _unit;
    public override void InstallBindings()
    {
        Container.Bind<TaskHandler>().FromInstance(_unit).AsSingle().NonLazy();
    }
}
