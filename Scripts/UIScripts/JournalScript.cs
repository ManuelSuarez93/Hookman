using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class JournalScript : MonoBehaviour
{
    public Dropdown dropdown;
    public Text content;
    public TextAsset[] textAssets;
    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
        if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            Load();
        }
        
        content.text = Encoding.UTF8.GetString(textAssets[int.Parse(dropdown.options[dropdown.value].text)].bytes);
    }

    void DropdownValueChanged(Dropdown change)
    {
        string txt = Encoding.UTF8.GetString(textAssets[int.Parse(change.options[change.value].text)].bytes);
        content.text = txt;
        PlayerPrefs.SetInt(textAssets[change.value].ToString(), 1);
    }

    void Load()
    {
        List<string> value = new List<string>();
        int j = 0;
        foreach (TextAsset i in textAssets)
        {
            
            if (PlayerPrefs.GetInt(i.ToString()) == 1)
            {
                value.Add(j.ToString());
            }
            j++;
                
        }

        dropdown.AddOptions(value);

    }
}
