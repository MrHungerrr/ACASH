using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public class SIC// Search In Children by KEY
    {
        string key;

        public SIC(string search_key)
        {
            this.key = search_key;
        }

        //Смена ключа поиска
        public void Key(string search_key)
        {
            this.key = search_key;
        }


        //Поиск компонентов
        private Transform[] Components(Transform obj)
        {
            List<Transform> list = new List<Transform>();

            if (obj.name == key)
            {
                list.Add(obj);
            }

            for (int i = 0; i < obj.childCount; i++)
            {
                list.AddRange(Components(obj.GetChild(i)));
            }

            return list.ToArray();
        }





        //Фунуции для использования пользователем
        public void Components(Transform obj, out Transform[] array)
        {
            array = Components(obj.transform);
        }

        public void Components(Transform obj, out GameObject[] array)
        {
            Transform[] buf_array = Components(obj);
            List<GameObject> list = new List<GameObject>();

            foreach(Transform t in buf_array)
            {
                list.Add(t.gameObject);
            }

            array = list.ToArray();
        }



        //Перегрузки для GameObjects Вместо Transform
        public void Components(GameObject obj, out Transform[] array)
        {
            Components(obj.transform, out array);
        }

        public void Components(GameObject obj, out GameObject[] array)
        {
            Components(obj.transform, out array);
        }






        //Поиск компонента
        private Transform Component(Transform obj)
        {
            if (obj.name == key)
            {
                return obj;
            }

            for (int i = 0; i < obj.childCount; i++)
            {
                Transform buf = Component(obj.GetChild(i));
                if (buf != null)
                {
                    return buf;
                }
            }

            return null;
        }


        //Фунуции для использования пользователем
        public void Component(Transform obj, out Transform goal)
        {
            goal = Component(obj.transform);
        }

        public void Component(Transform obj, out GameObject goal)
        {
            goal = Component(obj.transform).gameObject;
        }




        //Перегрузки для GameObjects Вместо Transform
        public void Component(GameObject obj, out GameObject goal)
        {
            Component(obj.transform, out goal);
        }

        public void Component(GameObject obj, out Transform goal)
        {
            Component(obj.transform, out goal);
        }
    }
}
