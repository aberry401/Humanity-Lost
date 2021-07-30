using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopButton : MonoBehaviour
{
    private ChipInventory inv;
    private newController player;

    public int buttonNumber;
    public Shop shop;
    private Text text;
    private shopItem item;

    void earlyStart()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<newController>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<ChipInventory>();
        text = GetComponentInChildren<Text>();
        Debug.Log("button start");
    }

    public void OnNewShop()
    {
        earlyStart();
        Debug.Log("Button " + buttonNumber);

        if(shop.items[buttonNumber - 1] != null)
            item = shop.items[buttonNumber - 1];
        else
        {
            item = null;
            text.text = "";
        }

        if (item != null)
        {
            text.text =
            item.itemName + '\n'
            + "Quantity: " + item.quantity + '\n'
            + "Price: " + item.price;
        }
    }

    public void ItemBought()
    {
        if(item != null && item.quantity > 0 && item.price < player.scrap)
        {
            if(item.chip != null)
            {
                inv.Add(item.chip);
            }

            player.scrap -= item.price;
            item.quantity--;

            text.text =
            item.itemName + '\n'
            + "Quantity: " + item.quantity + '\n'
            + "Price: " + item.price;
        }

        else
        {
            Debug.Log("Purchase Impossible");
        }
    }
}
