using UnityEngine;

public class TutorialPlayerEyes: MonoBehaviour
{


    private LayerMask layer_mask;

    [HideInInspector]
    public GameObject obj;


    private void Awake()
    {
        layer_mask = LayerMask.GetMask("Default", "Wall", "Selectable");
    }

    private void Update()
    {
        Looking();
    }

    private void Looking()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        //Рейкаст интерактивных объектов
        if (Physics.Raycast(ray, out hit, layer_mask))
        {
            obj = hit.collider.gameObject;
        }
        else
        {
            obj = null;
        }
    }
}
