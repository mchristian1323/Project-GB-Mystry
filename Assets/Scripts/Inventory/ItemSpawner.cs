using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem //this is teh pickup spawner
{
    public class ItemSpawner : MonoBehaviour
    {
        /// <summary>
        /// used to spawn items. placed on a prefab
        /// </summary>
        [SerializeField] ItemData item;

        private void Start()
        {
            AdvancedItemWorld.SpawnItemWorld(transform.position, item);//changed from advanced
            Destroy(gameObject);
        }
    }
}
