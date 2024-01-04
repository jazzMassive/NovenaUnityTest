using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListSelect : MonoBehaviour
{
    [SerializeField]
    private ConfigurationController _configurationController;

    [SerializeField]
    private MenuController _menuController;

    [SerializeField]
    private GameObject _buttonContentObject;

    [SerializeField]
    private GameObject _buttonPrefab;

    private List<string> _topicNames = new List<string>();

    [SerializeField]
    private Page _detailsPage;


    private void Awake()
    {
        _configurationController.OnLanguageChange += OnLanguageChange;
    }

    public void InitializeButtons()
    {
        foreach (var topic in _configurationController.GetContent().GetLanguageById(_configurationController.GetSelectedLanguageContent()).Topics)
        {
            if (!this._topicNames.Contains(topic.Name))
            {
                this._topicNames.Add(topic.Name);
                GameObject buttonInstance = Instantiate(_buttonPrefab);
                buttonInstance.GetComponentInChildren<ListButton>().TopicName.text = topic.Name;
                buttonInstance.GetComponentInChildren<ListButton>().TopicId.text = topic.TopicId;
                buttonInstance.transform.SetParent(_buttonContentObject.transform);
                buttonInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.75f, Screen.height * 0.10f);
                buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { TopicButtonPressed(topic); });
            }

        }
    }

    private void OnLanguageChange()
    {
        foreach (Transform child in _buttonContentObject.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        _topicNames.Clear();
    }

    private void TopicButtonPressed(Topic topic)
    {
        _configurationController.SetSelectedTopicContent(topic.TopicId);
        switch (topic.Type)
        {
            case "Gallery":
                _menuController.PushPage(_detailsPage);
                break;
            case "Extra":
                //menuController.PushPage(extraPage);
                break;
        }
    }
}
