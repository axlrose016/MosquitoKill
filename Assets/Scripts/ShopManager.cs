using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void btnBuy()
    {
        int playerKoinz = PlayerPrefs.GetInt("Koinz");
        GameObject btnObj = EventSystem.current.currentSelectedGameObject.gameObject;
        int price = btnObj.GetComponent<InventoryItemProperties>().price;
        string itemName = btnObj.GetComponent<InventoryItemProperties>().itemName;
        string itemDesc = btnObj.GetComponent<InventoryItemProperties>().itemDesc;
        
        int hasItem = PlayerPrefs.GetInt(itemName);
        if (hasItem == 1)
            FindObjectOfType<UIManager>().ShowDialogBox("You already purchased this Item, Please select other item");
        else
        {
            if (playerKoinz >= price)
            {
                int remKoinz = playerKoinz - price;
                PlayerPrefs.SetInt("Koinz", remKoinz);
                PlayerPrefs.SetInt(itemName, 1);
                FindObjectOfType<GameplayManager>().txtKoinz.text = FindObjectOfType<GameplayManager>().txtStoreKoinz.text = PlayerPrefs.GetInt("Koinz").ToString();
                FindObjectOfType<GPGS>().OpenSave(true);
                FindObjectOfType<UIManager>().ShowDialogBox($"Thank you for purchasing {itemDesc}");
                GPGS.IncrementalAchievement(GPGSIds.achievement_collector);
                FindObjectOfType<InventoryManager>().LoadInventory();
            }
            else
                FindObjectOfType<UIManager>().ShowDialogBox($"Sorry not enough koinz");
        }
    }
}
