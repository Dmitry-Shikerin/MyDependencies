using System.Collections.Generic;
using MyDependencies.Containers;
using MyDependencies.Installers;
using UnityEngine;

namespace MyDependencies.Contexts
{
    [DefaultExecutionOrder(-10000)]
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers;

        private ProjectContext _projectContext;
        private DiContainer _container;
        
        public DiContainer Container => _container;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            _container = new DiContainer();
            
            foreach (MonoInstaller installer in _installers)
                installer.InstallBindings(_container);
        }
    }
}
