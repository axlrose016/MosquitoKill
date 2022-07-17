using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryManager : MonoBehaviour
{
    public Text uiMessage;
    public ItemSlot[] slot;

    public void LoadInventory()
    {
        //string test = $"10,40,{PlayerPrefs.GetString("DefaultThrowable")},1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0";
        string[] rawData = PlayerPrefs.GetString("RawData").Split(','); //test.Split(',');//

        Debug.Log($"Default: {PlayerPrefs.GetString("DefaultThrowable")}");
        RefreshInventory();
        for (int i = 3; i < rawData.Length; i++)
        {
            int hasItem = Convert.ToInt16(rawData[i]);
            if (hasItem == 1)
            {
                foreach (ItemSlot item in slot)
                {
                    if (!item.hasItem)
                    {
                        item.itemImg.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Items/item{i - 2}");
                        item.hasItem = true;
                        item.itemName = item.ObjectParent.GetComponent<InventoryItemProperties>().itemName = $"item{i - 2}";

                        Debug.Log($"item.itemName : {item.itemName} >> rawData[2]: {rawData[2].ToString()}");
                        if (item.itemName == rawData[2].ToString())
                        {
                            Debug.Log($"item.itemName : {item.itemName}");
                            item.isEquipped = true;
                            item.ObjectParent.GetComponent<InventoryItemProperties>().btnSetItem.DOScale(1, 0.25f);
                        }
                        //else
                        //    item.ObjectParent.GetComponent<InventoryItemProperties>().btnSetItem.DOScale(0, 0.25f);
                        break;
                    }
                }    
            }
            else
            {
                foreach (ItemSlot item in slot)
                {
                    if (!item.hasItem)
                    {
                        item.itemImg.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Items/lock");
                    }
                }
            }
        }
    }
    public void RefreshInventory()
    {
        foreach(ItemSlot item in slot)
        {
            if (item.hasItem)
            {
                item.itemImg.GetComponent<Image>().sprite = null;
                item.ObjectParent.GetComponent<InventoryItemProperties>().btnSetItem.DOScale(0, 0.25f);
                item.hasItem = false;
            }
        }
    }

    public void BtnSetActiveItem()
    {
        GameObject btnClickedObj = EventSystem.current.currentSelectedGameObject.gameObject;
        GameObject btnParent = btnClickedObj.GetComponent<InventoryItemProperties>().gameObject;
        string hasItem = btnParent.GetComponent<InventoryItemProperties>().itemName;
        if(!String.IsNullOrEmpty(hasItem))
            btnParent.GetComponent<InventoryItemProperties>().btnSetItem.DOScale(1, 0.25f);
    }

    public void SaveActiveItem()
    {
        GameObject btnClickedObj = EventSystem.current.currentSelectedGameObject.gameObject;
        string itemName = btnClickedObj.GetComponentInParent<InventoryItemProperties>().itemName;
        if(!String.IsNullOrEmpty(itemName))
        {
            Debug.Log(itemName);
            PlayerPrefs.SetString("DefaultThrowable", itemName);
            FindObjectOfType<GPGS>().OpenSave(true);
            LoadInventory();
        }
    }
}
