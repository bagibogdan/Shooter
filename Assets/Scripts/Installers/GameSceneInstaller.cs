using UnityEngine;
using Zenject;
using Player;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerController playerController;

    public override void InstallBindings()
    {
        Container.BindInstance(joystick);
        Container.BindInstances(playerInput, playerMovement, playerController);
    }
}