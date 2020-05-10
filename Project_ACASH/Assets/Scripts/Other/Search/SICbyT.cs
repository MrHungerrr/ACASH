using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public static class SIC<T> where T : MonoBehaviour // Search In Children By <T>
    { 

        //Поиск компонентов
        private static T[] Components(Transform obj)
        {
            List<T> list = new List<T>();
            T buf;

            if (obj.TryGetComponent<T>(out buf))
            {
                list.Add(buf);
            }

            for (int i = 0; i < obj.childCount; i++)
            {
                list.AddRange(Components(obj.GetChild(i)));
            }

            return list.ToArray();
        }


        // Функции для использования пользователем
        private static void Components(Transform obj, out GameObject[] array)
        {
            T[] buf_array = Components(obj.transform);
            List<GameObject> list = new List<GameObject>();

            foreach (T t in buf_array)
            {
                list.Add(t.gameObject);
            }

            array = list.ToArray();
        }

        private static void Components(Transform obj, out T[] array)
        {
            array = Components(obj);
        }




        //Перегрузки для GameObjects Вместо Transform
        public static void Components(GameObject obj, out T[] array)
        {
            Components(obj.transform, out array);
        }

        public static void Components(GameObject obj, out GameObject[] array)
        {
            Components(obj.transform, out array);
        }






        //Поиск компонента
        private static T Component(Transform obj)
        {
            T buf;

            if (obj.TryGetComponent<T>(out buf))
            {
                return buf;
            }

            for (int i = 0; i < obj.childCount; i++)
            {
                buf = Component(obj.GetChild(i));
                if (buf != default)
                {
                    return buf;
                }
            }

            return default;
        }



        // Функции для использования пользователем
        private static void Component(Transform obj, out GameObject goal)
        {
            goal = Component(obj.transform).gameObject;
        }

        private static void Component(Transform obj, out T goal)
        {
            goal = Component(obj.transform);
        }



        //Перегрузки для GameObjects Вместо Transform
        public static void Component(GameObject obj, out T goal)
        {
            Component(obj.transform, out goal);
        }

        public static void Component(GameObject obj, out GameObject goal)
        {
            Component(obj.transform, out goal);
        }


    }
}
