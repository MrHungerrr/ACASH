using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DictionaryHieroglyphWord : MonoBehaviour
{


    private Image[] hieroglyphs = new Image[AlphabetInfo.max_length];




    public void SetDictionaryHieroglyph()
    {
        for(int i = 0; i < AlphabetInfo.max_length; i++)
        {
            hieroglyphs[i] = transform.Find("Hieroglyph_" + i).GetComponent<Image>();
        }

        Reset();
    }



    public void SetWord(string word)
    {
        for(int i = 0; i < AlphabetInfo.max_length; i++)
        {
            if((AlphabetInfo.max_length - i) <= word.Length)
            {

                hieroglyphs[i].enabled = true;
                hieroglyphs[i].sprite = AlphabetInfo.GetHieroglyph(word[word.Length - AlphabetInfo.max_length + i]);
                Debug.Log(" Символ под номером - " + i + ", равен иероглифу - " + (word.Length - AlphabetInfo.max_length + i));
            }
            else
            {
                hieroglyphs[i].enabled = false;
                Debug.Log(" Символ под номером - " + i + " пустой");
            }
        }
    }


    public void Reset()
    {
        SetWord("");
    }
}