using System.Collections;
using UnityEngine;

namespace Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        
        const string defaultSaveFile = "save";

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        }

        private void Update()
        {
            /*
            if (Input.GetKeyDown(saveKey))
            {
                
            }
            if (Input.GetKeyDown(loadKey))
            {
                
            }
            if (Input.GetKeyDown(deleteKey))
            {
                
            }
            */
        }

        private void OnSave()
        {
            Save();
        }

        private void OnLoad()
        {
            Load();
        }

        private void OnDelete()
        {
            Delete();
        }

        public void Load()
        {
            StartCoroutine(GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile));
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}
