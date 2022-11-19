using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool isInRange = false;
    [SerializeField]
    bool autoCollect = false;
    Objet itemComponent;
    float basePos;
    float currentPos = 0;
    float moveFreq = .5f;
    float floatingRange = 0f;

    private void Start()
    {
        this.basePos = this.transform.position.y;
        itemComponent = gameObject.GetComponent<Objet>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, basePos + currentPos);
        currentPos += floatingRange * (Time.deltaTime / moveFreq);
        if(currentPos > floatingRange && floatingRange > 0 || currentPos < floatingRange && floatingRange < 0)
        {
            floatingRange *= -1;
        }
        if ( isInRange && Input.GetKeyDown(KeyCode.E) )
        {
            PlayerEquipements.instance.Equip(itemComponent);
        }
    }
}
