using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{

    public Text text_price;
    private int price;
    private bool isTriggered;
    private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        double percent;
        RandomItem rnd = gameObject.transform.GetComponentInChildren<RandomItem>();
        obj = rnd.randItem();
        Destroy(rnd.gameObject);
        price = obj.GetComponent<Objet>().getPrice();
        percent = price * 0.20;
        price += Random.Range(0, (int)percent);
        text_price.text = price.ToString();

        obj.GetComponent<Collider2D>().enabled=false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject body = collision.gameObject;
        if (body.name == "Player")
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject body = collision.gameObject;
        if (body.name == "Player")
        {
            isTriggered = false;
        }
    }

    private void Update()
    {
        if(isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerEquipements.instance.getSold() >= price)
            {
                PlayerEquipements.instance.Pay(price);
                PlayerEquipements.instance.Equip(obj.GetComponent<Objet>());
                this.GetComponent<Collider2D>().enabled = false;
                isTriggered = false;
                text_price.text = "SOLD";
            }
        }
    }
}
