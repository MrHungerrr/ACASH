using UnityEngine;

namespace Single
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _get;

        private static System.Object _lock = new System.Object();

        public static T get
        {
            get
            {
                lock (_lock)
                {
                    if (_get == null)
                    {
                        _get = FindObjectOfType<T>();


                        if (FindObjectsOfType<T>().Length > 1)
                        {
                            Debug.LogError("Несколько Синглтонов '" + typeof(T).ToString() + "' найдено! ");
                        }


                        if (_get == null)
                        {
                            Debug.Log("На сцене отсутсвует " + typeof(T).ToString());
                            return null;
                        }
                    }

                    return _get;
                }
            }
        }
    }
}
