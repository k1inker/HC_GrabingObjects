using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private CharacterHandler _unit;
    public override void InstallBindings()
    {
        Container.Bind<CharacterHandler>().FromInstance(_unit).AsSingle().NonLazy();
    }
}
