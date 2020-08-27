using UnityEngine;
using System.Collections;

public class DictionaryItem
{
    public string original;
    public DictionaryWord word;



    public DictionaryItem(string original_word, int hieroglyph_number)
    {
        this.original = original_word;
        word = new DictionaryWord(hieroglyph_number);
    }
}
