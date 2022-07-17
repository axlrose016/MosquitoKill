using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class ItemSlot
    {
        public bool hasItem;
        public GameObject itemImg;
        public bool isEquipped;
        public string itemName;
        public GameObject ObjectParent;
    }
}
