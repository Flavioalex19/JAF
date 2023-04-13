using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _lineText;

    public void Dialogue(List<string> list, int index, bool isPanelOn)
    {
        if (isPanelOn)
        {
            if (index < list.Count)
            {
                StartCoroutine(TypeSentence(list[index], _lineText));
                print(index);
                //index++;
            }
            else isPanelOn = false;
        }

        
    }


    //function for dialogue - create the effect of typing the sentence
    IEnumerator TypeSentence(string sentence, TextMeshProUGUI GUItext)
    {
        GUItext.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            GUItext.text += letter;
            yield return new WaitForSeconds(.01f);
        }
    }
}
