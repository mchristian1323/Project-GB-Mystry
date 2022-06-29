using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tooltips
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string content;
        public string header;

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartCoroutine(DelayedShow());
        }

        private IEnumerator DelayedShow()
        {
            yield return new WaitForSeconds(.5f);
            TooltipSystem.Show(content, header);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            TooltipSystem.Hide();
        }
    }
}