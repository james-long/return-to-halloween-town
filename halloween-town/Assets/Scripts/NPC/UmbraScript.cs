using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
    UmbraScript : GenericNPCScript
{

    // Use this for initialization
    void Start()
    {
        gameObject.transform.position = new Vector3(2880, -3264, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void repositionUmbra(int num)
    {
        if (num == 1)
            gameObject.transform.position = new Vector3(-448 - 64, -1408, 0);
        if (num == 2)
            gameObject.transform.position = new Vector3(960, 384, 0);
    }
}
