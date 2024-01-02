using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class Details : MonoBehaviour
{
    [SerializeField]
    private ConfigurationController _configurationController;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private MenuController _menuController;

    private List<Media> _media;

    private string _detailText;

    private AudioClip _detailAudio;

    private List<Sprite> _detailImages = new List<Sprite>();

    [SerializeField]
    private Slideshow _slideshow;

    [SerializeField]
    private TextMeshProUGUI _topicId;
    [SerializeField]
    private TextMeshProUGUI _topicName;

    public void OnViewActive()
    {
        Topic topic = _configurationController.GetContent()
            .GetLanguageById(_configurationController.GetSelectedLanguageContent())
            .GetTopicById(_configurationController.GetSelectedTopicContent());

        _topicId.text = topic.topicId;
        _topicName.text = topic.name;
    }

    public void LoadDetailContent()
    {
        this._media = _configurationController.GetContent()
            .GetLanguageById(_configurationController.GetSelectedLanguageContent())
            .GetTopicById(_configurationController.GetSelectedTopicContent()).media;

        string filepath = Application.persistentDataPath + "/" + _configurationController.GetSelectedLanguageContent() + "/"
            + _configurationController.GetSelectedTopicContent() + "/";

        foreach (Media media in _media)
        {
            switch (media.type)
            {
                case "photo":
                    StartCoroutine(GetPhotoFromStorage(filepath, media.name));
                    break;
                case "audio":
                    StartCoroutine(GetAudioFromStorage(filepath, media.name));
                    break;
                case "text":
                    GetTextFromStorage(filepath, media.name);
                    break;
            }
        }
    }

    private IEnumerator GetPhotoFromStorage(string filepath, string name)
    {
        string finalPath = filepath + name;
        if (File.Exists(finalPath))
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture("file://" + finalPath))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);
                    Sprite tempSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
                    this._detailImages.Add(tempSprite);
                    this._slideshow.LoadPhoto(tempSprite);
                }
            }
        }
    }

    private IEnumerator GetAudioFromStorage(string filepath, string name)
    {
        string finalPath = filepath + name;
        if (File.Exists(finalPath))
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + finalPath, AudioType.UNKNOWN))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    this._detailAudio = DownloadHandlerAudioClip.GetContent(www);
                }
            }
        }

        _audioSource.clip = this._detailAudio;

    }

    private void GetTextFromStorage(string filepath, string name)
    {
        string finalPath = filepath + name;
        if (File.Exists(finalPath))
        {
            byte[] bytes = File.ReadAllBytes(finalPath);
            this._detailText = Encoding.ASCII.GetString(bytes);
        }
        
    }
}
