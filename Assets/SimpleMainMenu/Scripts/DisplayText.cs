using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayText : MonoBehaviour
{
    public Text message;

    public void displayText()
    {
        message.text = "This game was created by Aleksandar Cvijanovic in 2020.";
    }
}
