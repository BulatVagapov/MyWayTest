using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "Installers/ScriptableObjectsInstaller")]
public class ScriptableObjectsInstaller : ScriptableObjectInstaller<ScriptableObjectsInstaller>
{
    [SerializeField] private DefaultDataValues defaultDataValues;
    
    public override void InstallBindings()
    {
        Container.BindInstance(defaultDataValues).AsSingle();
    }
}