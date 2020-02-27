using UnityEngine;
using System.Collections;
using Searching;
using TMPro;

public class DictionaryController : MonoBehaviour
{


    [HideInInspector]
    public InputFieldDictionary input { get; private set; }
    private DictionaryHieroglyphWord word;
    private TextMeshProUGUI description;


    public void SetDictionary()
    {
        SIC<InputFieldDictionary> search = new SIC<InputFieldDictionary>();

        Transform dictionary = transform.Find("Dictionary");
        input = transform.GetComponentInChildren<InputFieldDictionary>();
        description = transform.Find("Description").Find("Text").GetComponent<TextMeshProUGUI>();
        word = transform.GetComponentInChildren<DictionaryHieroglyphWord>();
    }

}
