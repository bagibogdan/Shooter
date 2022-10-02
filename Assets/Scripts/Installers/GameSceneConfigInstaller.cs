using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneConfigInstaller", menuName = "Installers/GameSceneConfigInstaller")]
public class GameSceneConfigInstaller : ScriptableObjectInstaller<GameSceneConfigInstaller>
{
    public override void InstallBindings()
    {
    }
}