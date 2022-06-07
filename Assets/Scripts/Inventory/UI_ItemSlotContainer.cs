using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InventorySystem
{
    public class UI_ItemSlotContainer : MonoBehaviour
    {
        int index;
        UltraInventory inventory;

        RectTransform myRectTransform;

        ItemData currentItem;
        int amount = 0;

        public void Setup(UltraInventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            
            myRectTransform = GetComponent<RectTransform>();

            UpdateImage(index);
        }

        private void UpdateImage(int index)
        {
            //gets image from the array number and number of objects there and sets the image and text
            Image image = myRectTransform.Find(UITags.image).GetComponent<Image>(); //MAKE SURE TO NAME THE OBJCECT IN QUESTION THE SAME IN THE SCRIPT HELPER
            TextMeshProUGUI uiText = myRectTransform.Find(UITags.amountText).GetComponent<TextMeshProUGUI>();
            image.gameObject.SetActive(true);

            currentItem = inventory.GetSlotItem(index);
            amount = inventory.GetSlotAmount(index);

            if(currentItem != null)
            {
                image.sprite = currentItem.GetSprite();

                 
                uiText.gameObject.SetActive(true);
                if (amount > 1)
                {
                    uiText.SetText(amount.ToString());
                }
                else
                {
                    uiText.SetText("");
                }

                if (inventory.GetSelectorNumber() == index)
                {
                    myRectTransform.Find(UITags.selector).gameObject.SetActive(true);
                }
                else
                {
                    myRectTransform.Find(UITags.selector).gameObject.SetActive(false);
                }
            }
            else
            {
                image.gameObject.SetActive(false);
                uiText.gameObject.SetActive(false);
            }
        }
    }
}
