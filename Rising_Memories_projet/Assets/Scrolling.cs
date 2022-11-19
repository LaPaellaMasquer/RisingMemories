using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+.002f);
        if(this.transform.position.y > 17)
        {
            this.transform.position = new Vector2(this.transform.position.x, -60.29f);
        }
    }
}
