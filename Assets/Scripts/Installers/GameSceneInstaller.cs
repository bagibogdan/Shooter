using Player;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerInput playerInput;
    
    public override void InstallBindings()
    {
        Container.BindInstance(joystick);
        Container.BindInstance(playerInput);
    }
}