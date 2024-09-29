using Sources.Containers;
using UnityEngine;

namespace Sources.Installers
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void InstallBindings(DiContainer container);
    }
}