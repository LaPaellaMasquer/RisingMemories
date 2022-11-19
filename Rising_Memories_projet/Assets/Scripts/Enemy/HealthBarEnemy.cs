using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private RectTransform healthBarRect;

    private void Start()
    {
        image.GetComponent<Image>().enabled=false;
    }

    public void SetHealth(float cur,float max)
    {
        float value = cur / max;
        image.GetComponent<Image>().enabled = true;

        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);
    }
}
