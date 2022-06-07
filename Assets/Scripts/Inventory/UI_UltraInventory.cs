using UnityEngine;

namespace InventorySystem
{
    /// <summary>
    /// this script, placed on the inventory canvas, dispays visuals, needs work to properly display visuals
    /// </summary>
    public class UI_UltraInventory : MonoBehaviour
    {
        private UltraInventory inventory;
        [Header("From Children")]
        [SerializeField] Transform itemSlotContainer;
        [Header("From Prefab Folder")]
        [SerializeField] UI_ItemSlotContainer itemSlotTemplate;

        private void Awake()
        {
            //itemSlotContainer = transform.Find(UITags.itemSlotContainer);
            //took away set false because evidence does this for it
        }

        public void SetInventory(UltraInventory inventory)
        {
            this.inventory = inventory;

            inventory.InventoryUpdated += Inventory_InventoryUpdated;

            RefreshInventoryItems();
        }

        private void Inventory_InventoryUpdated(object sender, System.EventArgs e)
        {
            RefreshInventoryItems();
        }
            
        private void RefreshInventoryItems()//this leaves gaps when item is taken out
        {
            foreach (Transform child in itemSlotContainer) //this will bee here
            {
                Destroy(child.gameObject);
            }

            float itemSlotCellSize = 131f;
            for(int i = 0; i < inventory.GetInventorySize(); i++)
            {
                var thisSlot = Instantiate(itemSlotTemplate, itemSlotContainer);

                thisSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * itemSlotCellSize, 0 * itemSlotCellSize);

                thisSlot.Setup(inventory, i);

            }
        }
        //after this, the rest will be in that item when called to "setup"
        //instead of changing the inventory, maybe have each visual item have an index that matches wit the inventory
        //spaces, then have that look for the item based off sprite


        //if possible: make each slot its own script with data showing whats in the array. then when there is an update
        //each is destroyed and then rebuilt with new information
        //refreshinventory items
        //for each -> destory all
        //then for each -> instantiate and call their setup function
    }
}