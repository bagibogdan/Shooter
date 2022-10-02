using Followers;
using Player;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerFollower playerFollower;

    public override void InstallBindings()
    {
        Container.BindInstance(joystick);
        Container.BindInstances(playerInput, playerMovement, playerController);
        Container.BindInstance(playerFollower);
    }
}