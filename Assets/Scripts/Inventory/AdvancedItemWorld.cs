using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem //this is the pickup
{
    /// <summary>
    /// this script focuses on item management in the over game world. item spawning, droping and data retrieval are here, this on a prefab which then goes on the ItemManager in the world
    /// </summary>
    public class AdvancedItemWorld : MonoBehaviour
    {
        //private
        private ItemData item;
        private SpriteRenderer spriteRenderer;

        //Item Creation
        public static AdvancedItemWorld SpawnItemWorld(Vector3 position, ItemData item)
        {
            Transform transform = Instantiate(ItemManager.ManagerInstance.itemObjectPrefab, position, Quaternion.identity);

            AdvancedItemWorld itemWorld = transform.GetComponent<AdvancedItemWorld>();
            itemWorld.SetItem(item);

            return itemWorld;
        }

        //moves the item when spawned as a drop
        public static AdvancedItemWorld DropItem(Vector3 dropPosition, ItemData item)
        {
            Vector3 randomDir = InventoryUtils.GetRandomDir();
            AdvancedItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 1.5f, item);
            itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 10f, ForceMode2D.Impulse);
            return itemWorld;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        //Item Modification
        public void SetItem(ItemData item)
        {
            this.item = item;
            spriteRenderer.sprite = item.GetSprite();
        }

        public ItemData GetItem()
        {
            return item;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
