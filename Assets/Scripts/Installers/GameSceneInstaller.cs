using Cinemachine;
using UnityEngine;
using Zenject;
using Player;
using Level;
using UI;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Fighter playerFighter;
    [SerializeField] private LevelController levelController;
    [SerializeField] private UIObjectsController uiObjectsController;
    [SerializeField] private Pool damageViewPool;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public override void InstallBindings()
    {
        Container.BindInstance(joystick);
        Container.BindInstances(playerInput, playerMovement, playerController, playerFighter);
        Container.BindInstance(levelController);
        Container.BindInstance(damageViewPool);
        Container.BindInstance(virtualCamera);
        Container.BindInstance(uiObjectsController);
    }
}