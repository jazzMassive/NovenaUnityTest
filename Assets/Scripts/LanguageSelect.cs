using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LanguageSelect : MonoBehaviour
    {
        [SerializeField]
        private ConfigurationController _configurationController;

        [SerializeField]
        private MenuController _menuController;

        [SerializeField]
        private GameObject _buttonContentObject;

        [SerializeField]
        private GameObject _buttonPrefab;

        private List<string> _languageIds = new List<string>();

        [SerializeField]
        private Page _pageForButtons;

        public void InitializeButtons()
        {
            Debug.Log("testing kanguage init");
            foreach (var language in _configurationController.GetContent().LanguageContents)
            {
                if (!this._languageIds.Contains(language.LanguageId))
                {
                    this._languageIds.Add(language.LanguageId);
                    GameObject buttonInstance = Instantiate(_buttonPrefab);
                    buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = language.LanguageName;
                    buttonInstance.transform.SetParent(_buttonContentObject.transform);
                    buttonInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.75f, Screen.height * 0.10f);
                    buttonInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate { LanguageButtonPressed(language.LanguageId); });
                }
                
            }
        }

        private void LanguageButtonPressed(string value)
        {
            _configurationController.SetSelectedLanguageContent(value);
            _menuController.PushPage(_pageForButtons);
        }

    }
}
