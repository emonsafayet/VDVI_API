using System;
using System.Collections.Generic;
using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;

namespace Framework.Core.Wrappers
{
    public static class TinyMapperWrapper
    {
        public static T Map<T>(object target)
        {
            if (target != null)
                return TinyMapper.Map<T>(target);

            return default;
        }

        public static void EnableMapping<T, TDb>(Action<IBindingConfig<T, TDb>> config = null)
        {
            if (config != null)
                TinyMapper.Bind<T, TDb>(config);
            else
                TinyMapper.Bind<T, TDb>();

            TinyMapper.Bind<List<T>, List<TDb>>();

            TinyMapper.Bind<TDb, T>();

            TinyMapper.Bind<List<TDb>, List<T>>();
        }
    }
}
