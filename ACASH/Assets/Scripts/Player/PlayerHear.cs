using UnityEngine;
using System.Collections;

public class PlayerHear
{
    private const float hear_distance = 15f;
    private static LayerMask visible_layerMask = LayerMask.GetMask("Wall", "Selectable", "Default");

    public bool GetOcclusion(GameObject obj)
    {
        return GetOcclusion(obj, string.Empty);
    }

    public bool GetOcclusion(GameObject obj, string ignore_tag)
    {
        return GetOcclusion(obj, new string[]{ignore_tag});
    }


    public bool GetOcclusion(GameObject obj, string[] ignore_tags)
    {
        RaycastHit hit;
        Vector3 direction = BaseGeometry.GetDirection(Player.get.Camera.transform.position, obj.transform.position);


        Debug.DrawRay(Player.get.Camera.transform.position, direction, Color.green);

        if (Physics.Raycast(Player.get.Camera.transform.position, direction, out hit, visible_layerMask))
        {
            Debug.Log(obj.name + " sound collides with - " + hit.collider.tag);

            switch (hit.collider.tag)
            {
                case "Wall":
                case "Door":
                case "Elevator":
                    {
                        if(IsEqual(hit.collider.tag, ignore_tags))
                            return false;
                        else
                            return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
        else
        {
            return false;
        }
    }





    private bool IsEqual(string word, string[] words)
    {
        if(words == null || string.IsNullOrEmpty(word))
            return false;

        foreach (string w in words)
        {
            if (word == w)
                return true;
        }

        return false;
    }

}
