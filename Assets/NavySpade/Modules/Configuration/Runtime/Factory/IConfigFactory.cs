using System;
using NavySpade.Modules.Configuration.Runtime.SO;

namespace NavySpade.Modules.Configuration.Runtime.Factory
{
    public interface IConfigFactory
    {
        ObjectConfig CreateAndLoad(Type type);
    }
}