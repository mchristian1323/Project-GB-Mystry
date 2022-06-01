using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    /// <summary>
    /// item data scriptable object, keeps all item information. needs more data
    /// </summary>
    [CreateAssetMenu(fileName = "Item", menuName = "ItemData", order = 1)]
    public class ItemData : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("Item Info")]
        [SerializeField] string itemID = null;
        public string itemName;

        [SerializeField] [TextArea] string description = null;

        [Header("Item Type")]
        [SerializeField] DropObjectType type;

        [Header("Prefab: If item is drop object")]
        public AdvancedItemWorld prefab;

        [Header("Inventory Data")]
        [SerializeField] Sprite itemImage = null;
        [SerializeField] bool stackable = false;
        [SerializeField] int value;

        [Header("-Edible Info")]
        [SerializeField] float healthValue; //TODO sets the health gain when eaten

        [Header("-Equipment Info")]
        [SerializeField] int placeholderEquipValue; //will need more stat info

        [Header("-Effect Info")]
        [SerializeField] GameObject effectItemPrefab;

        [Header("-Spawner Object")]
        [SerializeField] ItemData spawnedPlaceable;

        [Header("Npc Value")]
        [SerializeField] int placeHolderLikeValue; //need more stat info

        [Header("Prefab: If item is world object")]
        public GameObject worldPrefab;

        [Header("World Data")]
        [SerializeField] bool isObjectPlant;
        [SerializeField] bool doesObjectBockMovement;
        [SerializeField] float hitBoxX;
        [SerializeField] float hitBoxY;
        [SerializeField] float hitBoxVerticalOfset;

        [Header("Planting Data")]
        [SerializeField] bool isFinalStage;
        [SerializeField] int daysItTakestoGrow;
        [SerializeField] int waterCountDown;
        [SerializeField] int activeSeason;
        [SerializeField] ItemData nextStage;

        public enum DropObjectType
        {
            functionless,
            edible,
            equipment,
            placeable,
            spawner,
            effect,
        }

        //Item ID
        static Dictionary<string, ItemData> itemLookupCache;
      
        public static ItemData GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, ItemData>();
                var itemList = Resources.LoadAll<ItemData>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }

        //item info
        public bool IsStackable()
        {
            return stackable;
        }

        public Sprite GetSprite()
        {
            return itemImage;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetID()
        {
            return itemID;
        }

        //Inventory Use info
        public DropObjectType GetObjectType()
        {
            return type;
        }

        public int GetValue()
        {
            return value;
        }

        public float GetHealthValue()
        {
            return healthValue;
        }

        //plant info
        public string GetItemName()
        {
            return itemName;
        }    

        public bool GetPlantStatus()
        {
            return isObjectPlant;
        }

        public int GetWaterCountDown()
        {
            return waterCountDown;
        }

        public int GetActiveSeason()
        {
            return activeSeason;
        }

        public ItemData GetNextPlantStage()
        {
            return nextStage;
        }

        public ItemData GetSpawnedObject()
        {
            return spawnedPlaceable;
        }

        //World info
        public bool GetBlockableStatus()
        {
            return doesObjectBockMovement;
        }

        public int GetObjectPassThroughInfo() //make sure these layers match the proper layers
        {
            if (doesObjectBockMovement)
            {
                return 12;
            }
            else
            {
                return 19;
            }
        }

        //Hitbox info Getters
        public float GetHitboxX()
        {
            return hitBoxX;
        }

        public float GetHitboxY()
        {
            return hitBoxY;
        }

        public float GetHitboxOffset()
        {
            return hitBoxVerticalOfset;
        }

        //sets the item id
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if(string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //required by the Iserializationcallbackreciever but we don need anything with it.
        }
    }
}