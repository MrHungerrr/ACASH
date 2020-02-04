using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUICollider
{
    //x_pivot
    public int x_p;
    //x_corner
    public int x_c;


    //y_pivot
    public int y_p;
    //y_corner
    public int y_c;

    public GameObject obj;
    private ComputerSelect select;

    public ComputerUICollider(GameObject o)
    {
        obj = o;

        RectTransform rt = obj.GetComponentInParent<RectTransform>();
        rt.anchorMax = new Vector2(0, 1);
        rt.anchorMin = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);

        rt = obj.GetComponent<RectTransform>();
        rt.anchorMax = new Vector2(0, 1);
        rt.anchorMin = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);

        x_p = (int)rt.localPosition.x;
        y_p = (int)rt.localPosition.y;
        x_c = x_p + (int)rt.rect.width;
        y_c = y_p - (int)rt.rect.height;

        select = obj.GetComponent<ComputerSelect>();
    }

    public void DebugWrite()
    {
        Debug.Log("x pivot = " + x_p + "  x corner = " + x_c + "  y pivot = " + y_p + "  y corner = " + y_c);
        //Debug.Log("x pivot = " + x_p + "  x corner = " + x_c);
        //Debug.Log("y pivot = " + y_p + "  y corner = " + y_c);
    }


    public bool MouseCollision(Vector2 pos)
    {
        if (obj.activeInHierarchy)
            if (pos.x >= x_p && pos.x <= x_c)
                if (pos.y <= y_p && pos.y >= y_c)
                {
                    select.Select(true);
                    return true;
                }

        select.Select(false);
        return false;
    }

}
