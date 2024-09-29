using System.Collections.Generic;
using Sources.Containers;
using Sources.Installers;
using UnityEngine;

namespace Sources.Contexts
{
    [DefaultExecutionOrder(-10000)]
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers;

        private ProjectContext _projectContext;
        private DiContainer _container;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            _container = new DiContainer();
            
            foreach (MonoInstaller installer in _installers)
                installer.InstallBindings(_container);
        }
    }
}
