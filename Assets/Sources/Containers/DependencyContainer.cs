using System.Collections.Generic;

namespace Sources.Containers
{
    public class DependencyContainer
    {
        public object Dependency { get; set; }
        public List<object> Dependencies { get; set; } = new List<object>();
    }
}