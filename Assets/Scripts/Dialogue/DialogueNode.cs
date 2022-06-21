using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool dialogueChoiceNode = false; //make an enum for multiple speakers
        [SerializeField] string conversantName;
        [SerializeField] string text;
        [SerializeField] Sprite portrait;
        [SerializeField] AudioClip characterSound;
        [SerializeField] bool unSkipable;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(0, 0, 200, 100);
        [SerializeField] string onEnterAction;
        [SerializeField] string onExitAction; //array to trigger multipe actions on exiting a node

        public Rect GetRect()
        {
            return rect;
        }

        public string GetConversantName()
        {
            return conversantName;
        }

        public string GetText()
        {
            return text;
        }

        public Sprite GetPortrait()
        {
            return portrait;
        }

        public AudioClip GetConversantAudio()
        {
            return characterSound;
        }    

        public List<string> GetChildren()
        {
            return children;
        }

        public bool IsPlayerSpeaking()
        {
            return dialogueChoiceNode;
        }

        public bool IsTextUnskippable()
        {
            return unSkipable;
        }

        public string GetOnEnterActions()
        {
            return onEnterAction;
        }
        
        public string GetOnExitActions()
        {
            return onExitAction;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);

            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);

        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);

        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            dialogueChoiceNode = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
