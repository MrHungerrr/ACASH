using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

namespace N_BH
{
    public class ComputerUIColliderManager : MonoBehaviour
    {

        [HideInInspector]
        private Dictionary<string, ComputerUICollider> colliders = new Dictionary<string, ComputerUICollider>();
        [HideInInspector]
        public RectTransform mouse;
        [SerializeField]
        private GameObject[] ui_colliders;



        private void Awake()
        {

            foreach (GameObject i in ui_colliders)
            {
                ComputerUICollider buf_collider = new ComputerUICollider(i);

                //buf_collider.DebugWrite();

                foreach (KeyValuePair<string, ComputerUICollider> pair in colliders)
                {

                }

                    colliders.Add(i.name, buf_collider);
            }
        }



        public string MouseCollision(Vector2 pos)
        {

            foreach (KeyValuePair<string, ComputerUICollider> pair in colliders)
            {
                if(pair.Value.MouseCollision(pos))
                {
                    return pair.Key;
                }
            }
            return null;
        }
    }
}
