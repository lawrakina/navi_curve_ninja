using System;
using System.IO;
using NavySpade.Modules.Configuration.Runtime.SO;
using NavySpade.Modules.Utils.Singletons.Runtime.Unity.Utils;

namespace NavySpade.Modules.Configuration.Runtime.Factory
{
    public class ObjectConfigFactory : ScriptableObjectFactory<ObjectConfig>, IConfigFactory
    {
        private const string Directory = "Configs/";

        public ObjectConfig CreateAndLoad(Type type)
        {
            var fullPath = Path.Combine(Directory, type.Name);
            return CreateAndLoad(type, fullPath);
        }
    }
}