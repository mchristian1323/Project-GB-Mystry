using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant myPlayerConversant;
        [SerializeField] TextMeshProUGUI dialogueBox; //AI text?
        [SerializeField] Button nextButton;
        [SerializeField] GameObject aiResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] Button quitButton;
        [SerializeField] TextMeshProUGUI conversantName;

        // Start is called before the first frame update
        void Start()
        {
            myPlayerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            myPlayerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => myPlayerConversant.Next());
            quitButton.onClick.AddListener(() => myPlayerConversant.Quit());

            UpdateUI();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(myPlayerConversant.IsActive());
            if(!myPlayerConversant.IsActive())
            {
                return;
            }
            conversantName.text = myPlayerConversant.GetCurrentConversantName();
            aiResponse.SetActive(!myPlayerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(myPlayerConversant.IsChoosing());
            if(myPlayerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                dialogueBox.text = myPlayerConversant.GetText();
                nextButton.gameObject.SetActive(myPlayerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (DialogueNode choiceText in myPlayerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choiceText.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    myPlayerConversant.SelectChoice(choiceText);
                });
            }
        }
    }
}
