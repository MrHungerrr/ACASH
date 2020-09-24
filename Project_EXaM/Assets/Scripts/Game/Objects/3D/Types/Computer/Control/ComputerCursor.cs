using UnityEngine;
using UnityEngine.UI;

namespace Computers
{
    public class ComputerCursor: MonoBehaviour
    {
        public enum types
        {
            Pointer,
            Finger
        }


        [SerializeField] private Image _pointer;
        [SerializeField] private Image _finger;


        public void SetLevel()
        {
            ChangeImage(types.Pointer);
        }


        public void Move(Vector3 movement)
        {
            transform.Translate(movement);

            if (Mathf.Abs(transform.localPosition.x - 720) > 720)
            {
                transform.localPosition = new Vector3(720 + Mathf.Sign(transform.localPosition.x - 720) * 720, transform.localPosition.y, transform.localPosition.z);
            }

            if (Mathf.Abs(transform.localPosition.y + 540) > 540)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -540 + Mathf.Sign(transform.localPosition.y + 540) * 540, transform.localPosition.z);
            }
        }

        public void ChangeImage(types type)
        {
            switch (type)
            {
                case types.Pointer:
                    {
                        _finger.enabled = false;
                        _pointer.enabled = true;
                        break;
                    }
                case types.Finger:
                    {
                        _finger.enabled = true;
                        _pointer.enabled = false;
                        break;
                    }
            }
        }
    }
}
