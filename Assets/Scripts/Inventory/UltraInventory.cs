using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace InventorySystem
{
    public class UltraInventory: MonoBehaviour
    {
        /// <summary>
        /// this script, placed on the player, will create the space and store items in each inventory. needs visual updating, backpack managment update, saving and loading
        /// </summary>
        
        //TODO
        //fix backpack list, fix visual

        //Ideas-
            //have stackable so's come with a stack prefab that works as a place holder for amounts and what not
            //
        //events
        public event EventHandler InventoryUpdated;
        public event EventHandler BackpackUpdated;

        //Serialized
        [SerializeField] int inventorySize;
        [SerializeField] int backpackSize;

        //private
        private InventorySlot[] itemList;
        private InventorySlot[] backpackList;

        public struct InventorySlot
        {
            public ItemData item;
            public int amount;
        }

        private int maxStack = 100;
        private int selectorNumber;
        
        private void Awake()
        {
            itemList = new InventorySlot[inventorySize];
            backpackList = new InventorySlot[backpackSize];
        }

        //item adding
        public void AddItemtoList(ItemData item, int number)
        {
            if(item.IsStackable()) //can it stack
            {
                int stackSlot = FindMatchingItem(item, number); //if yes then find a matching item

                if(stackSlot < 0)//if none insert as normal
                {
                    stackSlot = FindInventorySlot();

                    if(stackSlot < 0)//if no slot available, put in backpack
                    {
                        InsertItemInBackpack(item, number);
                    }
                    else//slot available
                    {
                        itemList[stackSlot].item = item;
                        itemList[stackSlot].amount = 1;
                        InventoryUpdated?.Invoke(this, EventArgs.Empty);
                    }
                }
                else//item found ++
                {
                    itemList[stackSlot].amount++;//if found increase the item slot
                    InventoryUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
            else//it can't stack
            {
                int slot = FindInventorySlot();//look for an empty space

                if(slot < 0)//if not slot available, put in backpack
                {
                    InsertItemInBackpack(item, number);
                }
                else//slot available
                {
                    itemList[slot].item = item;
                    InventoryUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void InsertItemInBackpack(ItemData item, int number)
        {
            if (item.IsStackable()) //can it stack
            {
                int stackSlot = FindMatchingBackpack(item, number); //if yes then find a matching item

                if (stackSlot < 0)//if none insert as normal
                {
                    stackSlot = FindBackpackSlot();

                    if (stackSlot < 0)//if no slot is available, drop the item
                    {
                        DropItem(item);
                    }
                    else
                    {
                        backpackList[stackSlot].item = item;
                        BackpackUpdated?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    backpackList[stackSlot].amount++;//if found increase the item slot
                    BackpackUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
            else//it can't stack
            {
                int slot = FindBackpackSlot();//look for an empty space

                if (slot < 0) //if no slot is available, drop the item
                {
                    DropItem(item);
                }
                else
                {
                    backpackList[slot].item = item;
                    BackpackUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        //slot search
        private int FindMatchingItem(ItemData item, int number)
        {
            for(int x = 0; x < itemList.Length; x++)
            {
                if(itemList[x].item != null)
                {
                    if (itemList[x].item.GetID() == item.GetID() && itemList[x].amount < maxStack)
                    {
                        return x;
                    }
                }
            }

            return -1;
        }

        private int FindMatchingBackpack(ItemData item, int number)
        {
            for (int x = 0; x < itemList.Length; x++)
            {
                if(backpackList[x].item != null)
                {
                    if (backpackList[x].item.GetID() == item.GetID() && backpackList[x].amount < maxStack)
                    {
                        return x;
                    }
                }
            }

            return -1;
        }

        private int FindInventorySlot()
        {
            for(int x = 0; x < itemList.Length; x++)
            {
                if(itemList[x].item == null) //if the slot is empty, return the slot
                {
                    return x;
                }
            }

            return -1; //if no space is found, return this as an indicators
        }

        private int FindBackpackSlot()
        {
            for (int x = 0; x < backpackList.Length; x++)
            {
                if (backpackList[x].item == null) //if the slot is empty, return the slot
                {
                    return x;
                }
            }

            return -1; //if no space is found, return this as an indicators
        }

        //Item Functionality/Removal
        /*
         * unneeded in current game
        public void UseItem(int selectNumber, Vector3 spaceScan, GridManager myGridManager) //TODO bounce back and play anim
        {
            ItemData targetItem = itemList[selectNumber].item;

            if(targetItem != null)
            {
                switch (targetItem.GetObjectType())
                {
                    case ItemData.DropObjectType.functionless:
                        //object does nothing
                        break;

                    case ItemData.DropObjectType.edible:
                        GetComponent<PlayerStats>().EdibleStatAdjuster(targetItem);
                        RemoveItemByNumber(selectNumber);
                        break;

                    case ItemData.DropObjectType.placeable:
                        if(myGridManager.CheckIfSpaceIsEmpty(spaceScan))
                        {
                            myGridManager.PlaceObject(spaceScan, targetItem);
                            RemoveItemByNumber(selectNumber);
                        }
                        break;

                    case ItemData.DropObjectType.spawner: //might need seed only switch for dirt checker
                        if(myGridManager.CheckIfGroundIsDirt(spaceScan) && myGridManager.CheckIfSpaceIsEmpty(spaceScan)) //looks for dirt and if there is space
                        {
                            myGridManager.PlaceObject(spaceScan, targetItem.GetSpawnedObject());
                            RemoveItemByNumber(selectNumber);
                        }
                        break;

                    case ItemData.DropObjectType.equipment:
                        //TODO equipment system in development
                        break;

                    default:
                        break;
                }
                //switch between enum and seach for that enum
                //if edible- get health and buff data, apply to the player, then destroy the item
                //if place- instantiat the object like the map builder
                //if unique effect- instantiate a prefab with its own ability and destoroy(?) the item
            }
        }

        public void DropItemByNumber(int selectNumber)
        {
            ItemData targetItem = itemList[selectNumber].item;
            if(targetItem != null)
            {
                if(targetItem.IsStackable())
                {
                    if(itemList[selectNumber].amount > 1)
                    {
                        itemList[selectNumber].amount--;
                        DropItem(targetItem);
                        InventoryUpdated?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        itemList[selectNumber].item = null;
                        DropItem(targetItem);
                        InventoryUpdated?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    itemList[selectNumber].item = null;
                    DropItem(targetItem);
                    InventoryUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        */

        public void RemoveItemByNumber(int selectNumber)
        {
            ItemData targetItem = itemList[selectNumber].item;
            if (targetItem != null)
            {
                if (targetItem.IsStackable())
                {
                    if (itemList[selectNumber].amount > 1)
                    {
                        itemList[selectNumber].amount--;
                        InventoryUpdated?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        itemList[selectNumber].item = null;
                        InventoryUpdated?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    itemList[selectNumber].item = null;
                    InventoryUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void DropItem(ItemData item)
        {
            //instantiate item, give it randam direction and give it velocity
            AdvancedItemWorld.DropItem(transform.position, item);
        }

        public void SetSelectorNumber(int number)
        {
            selectorNumber = number;
            InventoryUpdated?.Invoke(this, EventArgs.Empty);
        }

        //getters
        public int GetSelectorNumber()
        {
            return selectorNumber;
        }

        public ItemData GetBackpackSlotItem(int slot)
        {
            return backpackList[slot].item;
        }

        public int GetBackpackSlotAmount(int slot)
        {
            return backpackList[slot].amount;
        }

        public InventorySlot[] GetItemList()
        {
            return itemList;
        }

        public InventorySlot[] GetBackpackList()
        {
            return backpackList;
        }

        public ItemData GetSlotItem(int slot)
        {
            return itemList[slot].item;
        }

        public int GetSlotAmount(int slot)
        {
            return itemList[slot].amount;
        }

        public int GetInventorySize()
        {
            return inventorySize;
        }
    }
}