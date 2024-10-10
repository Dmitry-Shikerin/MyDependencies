using MyDependencies.Containers;
using UnityEngine;

namespace MyDependencies.Installers
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void InstallBindings(DiContainer container);
    }
}