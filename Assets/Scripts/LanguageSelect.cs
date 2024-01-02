using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

namespace Assets.Scripts
{
    public class LanguageSelect : MonoBehaviour
    {
        [SerializeField]
        private ConfigurationController configurationController;

        [SerializeField]
        private MenuController menuController;

        [SerializeField]
        private GameObject buttonContentObject;

        [SerializeField]
        private GameObject buttonPrefab;

        private List<string> _languageIds = new List<string>();

        [SerializeField]
        private Page pageForButtons;

        public void InitializeButtons()
        {
            Debug.Log("testing kanguage init");
            foreach (var language in configurationController.GetContent().languageContents)
            {
                if (!this._languageIds.Contains(language.languageId))
                {
                    this._languageIds.Add(language.languageId);
                    GameObject buttonInstance = Instantiate(buttonPrefab);
                    buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = language.languageName;
                    buttonInstance.transform.SetParent(buttonContentObject.transform);
                    buttonInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.75f, Screen.height * 0.10f);
                    buttonInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate { LanguageButtonPressed(language.languageId); });
                }
                
            }
        }

        private void LanguageButtonPressed(string value)
        {
            configurationController.SetSelectedLanguageContent(value);
            menuController.PushPage(pageForButtons);
        }

    }
}
