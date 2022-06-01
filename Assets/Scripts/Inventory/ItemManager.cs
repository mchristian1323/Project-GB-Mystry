using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    /// <summary>
    /// used to manage items. will be placed on an empty object in the world
    /// </summary>
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager ManagerInstance { get; private set; }

        public Transform itemObjectPrefab;

        private void Awake()
        {
            ManagerInstance = this;
        }

        //ad a system like item asset that stores the item and lets other make a list out of them. then when called, it grabs their id instead of the object
    }
}
