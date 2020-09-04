using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject difficulty1;

    public GameObject difficulty2;

    public GameObject difficulty3;
    // Start is called before the first frame update
    void Start()
    {
        difficulty1.SetActive(true);
        difficulty2.SetActive(true);
        difficulty3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
