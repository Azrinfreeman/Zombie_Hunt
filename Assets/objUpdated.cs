using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objUpdated : MonoBehaviour
{
    public bool level2;
    public bool level3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && level2)
        {
            UIController.instance.objDesc.text = "-Defeat the infected wolf";
        }
        if (other.tag == "Player" && level3)
        {
            UIController.instance.objDesc.text = "-Read the note on the tree";
        }
    }
}
