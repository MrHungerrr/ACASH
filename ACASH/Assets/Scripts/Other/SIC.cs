using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public static class SIC// Search In Children by KEY
    {


        //Поиск компонентов
        private static Transform[] Components(Transform obj, string key)
        {
            List<Transform> list = new List<Transform>();

            if (obj.name == key)
            {
                list.Add(obj);
            }

            for (int i = 0; i < obj.childCount; i++)
            {
                list.AddRange(Components(obj.GetChild(i), key));
            }

            return list.ToArray();
        }





        //Фунуции для использования пользователем
        public static void Components(Transform obj, string key, out Transform[] array)
        {
            array = Components(obj.transform, key);
        }

        public static void Components(Transform obj, string key, out GameObject[] array)
        {
            Transform[] buf_array = Components(obj, key);
            List<GameObject> list = new List<GameObject>();

            foreach(Transform t in buf_array)
            {
                list.Add(t.gameObject);
            }

            array = list.ToArray();
        }



        //Перегрузки для GameObjects Вместо Transform
        public static void Components(GameObject obj, string key, out Transform[] array)
        {
            Components(obj.transform, key, out array);
        }

        public static void Components(GameObject obj, string key, out GameObject[] array)
        {
            Components(obj.transform, key, out array);
        }






        //Поиск компонента
        private static Transform Component(Transform obj, string key)
        {
            if (obj.name == key)
            {
                return obj;
            }

            for (int i = 0; i < obj.childCount; i++)
            {
                Transform buf = Component(obj.GetChild(i), key);
                if (buf != null)
                {
                    return buf;
                }
            }

            return null;
        }


        //Фунуции для использования пользователем
        public static void Component(Transform obj, string key, out Transform goal)
        {
            goal = Component(obj.transform, key);
        }

        public static void Component(Transform obj, string key, out GameObject goal)
        {
            goal = Component(obj.transform, key).gameObject;
        }




        //Перегрузки для GameObjects Вместо Transform
        public static void Component(GameObject obj, string key, out GameObject goal)
        {
            Component(obj.transform, key, out goal);
        }

        public static void Component(GameObject obj, string key, out Transform goal)
        {
            Component(obj.transform, key, out goal);
        }
    }
}
