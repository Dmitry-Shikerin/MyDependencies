using System.Collections.Generic;
using MyDependencies.Sources.Containers;
using MyDependencies.Sources.Containers.Extensions;
using MyDependencies.Sources.Installers;
using UnityEngine;

namespace MyDependencies.Sources.Contexts
{
    [DefaultExecutionOrder(-9000)]
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers;
        [SerializeField] private List<MonoBehaviour> _injectedObjects;
        
        private ProjectContext _projectContext;
        private DiContainer _container;
        
        public DiContainer Container => _container;
        
        private void Awake()
        {
            _projectContext = FindObjectOfType<ProjectContext>() ?? 
                              Instantiate(Resources.Load<ProjectContext>("ProjectContext"));
            _container = new DiContainer(_projectContext.Container);
            
            _container.Bind(_container);
            
            foreach (MonoInstaller installer in _installers)
                installer.InstallBindings(_container);

            foreach (MonoBehaviour injected in _injectedObjects)
                _container.Inject(injected);
        }
    }
}
