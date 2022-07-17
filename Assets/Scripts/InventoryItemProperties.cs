using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryItemProperties : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public string itemDesc;
    [SerializeField] public int price;
    [SerializeField] public Transform btnSetItem;
}
