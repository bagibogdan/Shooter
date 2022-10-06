using Cinemachine;
using Configs;
using Enemy;
using UnityEngine;
using Zenject;
using Player;
using Level;
using Managers;
using UI;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Fighter playerFighter;
    [SerializeField] private LevelController levelController;
    [SerializeField] private EnemiesController enemiesController;
    [SerializeField] private UIObjectsController uiObjectsController;
    [SerializeField] private Pool damageViewPool;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private FirebaseManager firebaseManager;
    
    [Header("Prefabs:")]
    [SerializeField] private RedEnemyController redEnemy;
    [SerializeField] private WhiteEnemyController whiteEnemy;
    [SerializeField] private YellowEnemyController yellowEnemy;

    public override void InstallBindings()
    {
        Container.BindInstance(gameManager).AsSingle().NonLazy();
        Container.BindFactory<RedEnemyConfig, RedEnemyController, RedEnemyController.Factory>().FromComponentInNewPrefab(redEnemy);
        Container.BindFactory<WhiteEnemyConfig, WhiteEnemyController, WhiteEnemyController.Factory>().FromComponentInNewPrefab(whiteEnemy);
        Container.BindFactory<YellowEnemyConfig, YellowEnemyController, YellowEnemyController.Factory>().FromComponentInNewPrefab(yellowEnemy);
        Container.BindInstance(joystick);
        Container.BindInstances(playerInput, playerMovement, playerController, playerFighter);
        Container.BindInstance(levelController);
        Container.BindInstance(enemiesController);
        Container.BindInstance(damageViewPool);
        Container.BindInstance(virtualCamera);
        Container.BindInstance(uiObjectsController);
        Container.BindInstance(uiManager);
        Container.BindInstance(firebaseManager);
    }
}