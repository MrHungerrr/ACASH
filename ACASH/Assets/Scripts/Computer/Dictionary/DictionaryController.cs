using UnityEngine;
using System.Collections;
using Searching;
using TMPro;

public class DictionaryController : MonoBehaviour
{


    [HideInInspector]
    public InputFieldSingle input { get; private set; }
    private DictionaryHieroglyphWord word;
    private TextMeshProUGUI description;


    public void SetDictionary()
    {
        SIC<InputFieldSingle> search = new SIC<InputFieldSingle>();

        Transform dictionary = transform.Find("Dictionary");
        input = transform.GetComponentInChildren<InputFieldSingle>();
        description = transform.Find("Description").Find("Text").GetComponent<TextMeshProUGUI>();
        word = transform.GetComponentInChildren<DictionaryHieroglyphWord>();
    }

}
