using System.Collections.Generic;
using Sources.Containers;
using Sources.Installers;
using UnityEngine;

namespace Sources.Contexts
{
    [DefaultExecutionOrder(-9000)]
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers;
        [SerializeField] private List<MonoBehaviour> _injectedObjects;
        
        private ProjectContext _projectContext;
        private DiContainer _container;
        
        private void Awake()
        {
            _projectContext = FindObjectOfType<ProjectContext>() ?? 
                              Instantiate(Resources.Load<ProjectContext>("ProjectContext"));
            _container = new DiContainer(_projectContext.Container);
            
            foreach (MonoInstaller installer in _installers)
                installer.InstallBindings(_container);

            foreach (MonoBehaviour injected in _injectedObjects)
                _container.Inject(injected);
        }
    }
}
