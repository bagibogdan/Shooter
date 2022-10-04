using UnityEngine;
using Zenject;
using Configs;

public class EnemyController : MonoBehaviour
{
    private EnemyConfig _enemyConfig;
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(SignalBus signalBus, EnemyConfig enemyConfig)
    {
        _enemyConfig = enemyConfig;
        _signalBus = signalBus;
    }
    
    private void Awake()
    {
        GetComponent<Health>().Initialize(_enemyConfig.MaxHealth);
    }
}
