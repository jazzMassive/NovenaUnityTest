using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListSelect : MonoBehaviour
{
    [SerializeField]
    private ConfigurationController configurationController;

    [SerializeField]
    private MenuController menuController;

    [SerializeField]
    private GameObject buttonContentObject;

    [SerializeField]
    private GameObject buttonPrefab;

    private List<string> _topicNames = new List<string>();

    [SerializeField]
    private Page detailsPage;


    private void Awake()
    {
        configurationController.onLanguageChange += OnLanguageChange;
    }

    public void InitializeButtons()
    {
        foreach (var topic in configurationController.GetContent().GetLanguageById(configurationController.GetSelectedLanguageContent()).topics)
        {
            if (!this._topicNames.Contains(topic.name))
            {
                this._topicNames.Add(topic.name);
                GameObject buttonInstance = Instantiate(buttonPrefab);
                buttonInstance.GetComponentInChildren<ListButton>().TopicName.text = topic.name;
                buttonInstance.GetComponentInChildren<ListButton>().TopicId.text = topic.topicId;
                buttonInstance.transform.SetParent(buttonContentObject.transform);
                buttonInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.75f, Screen.height * 0.10f);
                buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { TopicButtonPressed(topic); });
            }

        }
    }

    private void OnLanguageChange()
    {
        foreach (Transform child in buttonContentObject.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        _topicNames.Clear();
    }

    private void TopicButtonPressed(Topic topic)
    {
        configurationController.SetSelectedTopicContent(topic.topicId);
        switch (topic.type)
        {
            case "Gallery":
                menuController.PushPage(detailsPage);
                break;
            case "Extra":
                //menuController.PushPage(extraPage);
                break;
        }
    }
}
