using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tooltips
{
    /// <summary>
    /// need to add in controller suport for mouse position with stick selection
    /// </summary>
    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI headerField;
        [SerializeField] TextMeshProUGUI contentField;

        [SerializeField] LayoutElement layoutElement;

        [SerializeField] int characterWrapLimit;

        [SerializeField] Control.PlayerControl myPlayerControl; //replace this to get new input with character mouse control
        RectTransform myRectTransform;

        private void Awake()
        {
            myRectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if(Application.isEditor)
            {
                int headerLength = headerField.text.Length;
                int contentLength = contentField.text.Length;

                layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
            }

            Vector2 position = myPlayerControl.mousePos;

            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;

            myRectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = position;
        }

        public void SetText(string content, string header = "")
        {
            if(string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive(false);
            }
            else
            {
                headerField.gameObject.SetActive(true);
                headerField.text = header;
            }

            contentField.text = content;

            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
    }
}
