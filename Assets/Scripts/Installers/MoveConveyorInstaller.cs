using Zenject;
using UnityEngine;

public class MoveConveyorInstaller : MonoInstaller
{
    [SerializeField] private Conveyor _unit;
    public override void InstallBindings()
    {
        Container.Bind<Conveyor>().FromInstance(_unit).AsSingle().NonLazy();
    }
}
