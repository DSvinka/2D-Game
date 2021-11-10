using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Utils
{
    internal static class DataUtils
    {
        public static T GetData<T>(string path, T obj) where T : Object
        {
            if (obj == null)
            {
                obj = AssetPath.Load<T>(path);
            }

            return obj;
        }
        
        public static T[] GetDataList<T>(string[] paths, T[] objs) where T : Object
        {
            var objsNull = false;
            if (objs == null || objs.Length == 0)
            {
                objs = new T[paths.Length];
                objsNull = true;
            }

            if (objsNull)
            {
                for (var i = 0; i < paths.Length; i++)
                {
                    var path = paths[i];
                    var obj = AssetPath.Load<T>(path);
                    objs[i] = obj;
                }
            }

            return objs;
        }

        private static T[] LoadAll<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);
    }
}