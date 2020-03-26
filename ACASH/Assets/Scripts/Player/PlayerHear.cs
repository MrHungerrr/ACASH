using UnityEngine;
using System.Collections;

public class PlayerHear
{
    private const float hear_distance = 15f;
    private static LayerMask visible_layerMask = LayerMask.GetMask("Wall", "Selectable", "Default");

    public bool GetOcclusion(GameObject obj)
    {
        RaycastHit hit;
        Vector3 direction = BaseGeometry.GetDirection(Player.get.Camera.transform.position, obj.transform.position);

        Debug.DrawRay(Player.get.Camera.transform.position, direction, Color.red);

        if (Physics.Raycast(Player.get.Camera.transform.position, direction, out hit, visible_layerMask))
        {
            Debug.Log(hit.collider.tag);

            switch(hit.collider.tag)
            {
                case "Wall":
                case "Door":
                case "Elevator":
                    {
                        return true;
                    }
                default:
                    {
                        if (hit.collider.gameObject == obj)
                            return false;
                        else
                            return true;
                    }
            }
        }
        else
        {
            return false;
        }
    }

}
