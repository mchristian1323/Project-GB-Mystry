using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheoryList
{
    [CreateAssetMenu(fileName = "New Theory", menuName = "Theory", order = 0)]
    public class Theory : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("Theory Info")]
        [SerializeField] string theory;
        [SerializeField] string description;
        [Header("Lookup ID")]
        [SerializeField] string theoryID;

        public enum Ending
        {
            Worst,
            Bad,
            Good,
            Just,
        }

        [SerializeField] Ending ending;

        static Dictionary<string, Theory> itemLookupCache;

        public static Theory GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, Theory>();
                var itemList = Resources.LoadAll<Theory>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.theoryID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.theoryID], item));
                        continue;
                    }

                    itemLookupCache[item.theoryID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }

        //getters
        public string GetTheory()
        {
            return theory;
        }    

        public string GetDescription()
        {
            return description;
        }

        public Ending GetEnding()
        {
            return ending;
        }

        public string GetTheoryID()
        {
            return theoryID;
        }

        //sets the item id
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(theoryID))
            {
                theoryID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //required by the Iserializationcallbackreciever but we don need anything with it.
        }
    }
}