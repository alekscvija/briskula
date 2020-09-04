using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitter : MonoBehaviour
{
    
        public void quitClick()
        {
            Debug.Log(("Quitting Application"));
            Application.Quit();
        }
    

}
