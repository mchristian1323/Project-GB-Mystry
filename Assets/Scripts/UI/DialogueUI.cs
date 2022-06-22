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
        [SerializeField] Image portrait;
        AudioSource speechPlayer;

        // Start is called before the first frame update
        void Start()
        {
            myPlayerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            myPlayerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => myPlayerConversant.Next());
            quitButton.onClick.AddListener(() => myPlayerConversant.Quit());
            speechPlayer = GetComponent<AudioSource>();

            UpdateUI();
        }

        private void UpdateUI()
        {
            if(dialogueBox.IsActive() != false)
            {
                StartCoroutine(FadeOut());
            }
            StopAllCoroutines();

            gameObject.SetActive(myPlayerConversant.IsActive());
            if(!myPlayerConversant.IsActive())
            {
                return;
            }

            //conversantName.text = myPlayerConversant.GetCurrentConversantName(); //origina code
            conversantName.text = myPlayerConversant.GetName(); //my code
            aiResponse.SetActive(!myPlayerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(myPlayerConversant.IsChoosing());
            portrait.sprite = myPlayerConversant.GetCurrentPortrait();
            speechPlayer.clip = myPlayerConversant.CurrentSound();

            if(portrait.sprite != null)
            {
                Image image = portrait.gameObject.GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 1f;
                image.color = tempColor;
            }
            else
            {
                Image image = portrait.gameObject.GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 0f;
                image.color = tempColor;
            }

            if(myPlayerConversant.HasSkip())
            {
                quitButton.gameObject.SetActive(false);
            }
            else
            {
                quitButton.gameObject.SetActive(true);
            }

            if(myPlayerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                StartCoroutine(ScrollingText(myPlayerConversant.GetText()));
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

        private IEnumerator ScrollingText(string currentText)
        {
            dialogueBox.text = "";
            speechPlayer.volume = 1;
            speechPlayer.Play();

            
            string originalText = currentText;
            string displayedText = "";
            int alphaIndex = 0;

            foreach (char c in currentText.ToCharArray())
            {
                alphaIndex++;

                dialogueBox.text = originalText;
                displayedText = dialogueBox.text.Insert(alphaIndex, "<color=#00000000>");
                dialogueBox.text = displayedText;

                yield return new WaitForSeconds(0.01f);
            }

            /*
            for (int i = 0; i < currentText.Length + 1; i++)
            {
                dialogueBox.text = currentText.Substring(0, i);
                yield return new WaitForSeconds(0.01f);
            }
            */
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            float audioVolume = speechPlayer.volume;

            while(speechPlayer.volume > 0)
            {
                audioVolume -= .8f;
                speechPlayer.volume = audioVolume;
                yield return new WaitForSeconds(0.1f);
            }

            speechPlayer.Stop();
        }
    }
}
