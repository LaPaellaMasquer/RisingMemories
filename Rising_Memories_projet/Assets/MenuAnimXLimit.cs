using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimXLimit : MonoBehaviour
{
    public float maxX, speed, baseX;
    public bool left;
    void Update()
    {
        if (left)
        {
            this.transform.position += new Vector3(-1,0,0) * Time.deltaTime * speed;
            if(maxX > this.transform.position.x)
            {
                this.transform.position = new Vector3(baseX, this.transform.position.y, this.transform.position.z);
            }
        }
        else
        {
            this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            if (maxX < this.transform.position.x)
            {
                this.transform.position = new Vector3(baseX, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
