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
        Transform dictionary = transform.Find("Dictionary");
        input = dictionary.GetComponentInChildren<InputFieldDictionary>();
        description = dictionary.Find("Description").Find("Text").GetComponent<TextMeshProUGUI>();
        word = dictionary.GetComponentInChildren<DictionaryHieroglyphWord>();
    }

}
