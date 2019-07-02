using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canTrigger;
    public string[] itemsForSale = new string[40];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canTrigger && PlayerController.instance.isAllowedToMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            Shop.instance.itemsForSale = itemsForSale;
            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canTrigger = false;
        }
    }
}
