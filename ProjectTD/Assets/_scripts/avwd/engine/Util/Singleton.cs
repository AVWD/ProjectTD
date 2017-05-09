using System;
namespace AVWD.Engine.Util
{
    public class Singleton<T> where T : new()
    {
        private static T instance;
        public static T Get()
        {
            if (Singleton<T>.instance == null)
            {
                Singleton<T>.instance = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
            }
            return Singleton<T>.instance;
        }
    }
}
