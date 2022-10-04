using UnityEngine;
using Zenject;
using Player;
using Level;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Fighter playerFighter;
    [SerializeField] private LevelController levelController;

    public override void InstallBindings()
    {
        Container.BindInstance(joystick);
        Container.BindInstances(playerInput, playerMovement, playerController, playerFighter);
        Container.BindInstance(levelController);
    }
}