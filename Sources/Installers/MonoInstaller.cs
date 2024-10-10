using MyDependencies.Sources.Containers;
using UnityEngine;

namespace MyDependencies.Sources.Installers
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void InstallBindings(DiContainer container);
    }
}