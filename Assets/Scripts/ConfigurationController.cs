using Assets.Scripts.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ConfigurationController : MonoBehaviour
{
    //public static ConfigurationController instance = null;

    [SerializeField]
    private TextAsset jsonFile;

    private AppContent _appContent;
    private string _selectedLanguageContent = null;
    private string _selectedTopicContent = null;

    public Action onLanguageChange;
    public Action onTopicChange;

    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //} else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}

        Debug.Log(Application.persistentDataPath);
        LoadJsonData();
        GetRequriedAssets();

    }

    public AppContent GetContent()
    {
        return _appContent;
    }

    public string GetSelectedLanguageContent()
    {
        return _selectedLanguageContent;
    }

    public void SetSelectedLanguageContent(string value)
    {
        _selectedLanguageContent = value;
        onLanguageChange?.Invoke();
    }

    public string GetSelectedTopicContent()
    {
        return _selectedTopicContent;
    }

    public void SetSelectedTopicContent(string value)
    {
        _selectedTopicContent = value;
        onTopicChange?.Invoke();
    }

    private void GetRequriedAssets()
    {
        foreach( var language in _appContent.GetAllLanguages())
        {
            foreach (var topic in language.topics )
            {
                CreateTopicDirectory(topic.topicId, language.languageId);
                foreach(var m in topic.media)
                {
                    string savePath = language.languageId + "/" + topic.topicId + "/" + m.name;
                    if (!File.Exists(Application.persistentDataPath + "/" + savePath))
                    {
                        switch (m.type)
                        {
                            case "photo":
                                StartCoroutine(DownloadImage(m.value, savePath));
                                break;
                            case "audio":
                                StartCoroutine(DownloadAudio(m.value, savePath));
                                break;
                            case "text":
                                StartCoroutine(DownloadText(m.value, savePath));
                                break;

                        }
                    }
                    
                    
                }
            }
        }
    }

    private void LoadJsonData()
    {
        _appContent = JsonConvert.DeserializeObject<AppContent>(jsonFile.text);
    }

    private void CreateTopicDirectory(string topicName, string languageName)
    {
        string path = Path.Combine(Application.persistentDataPath, languageName + "/" + topicName);
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public IEnumerator DownloadImage(string filepath, string name)
    {
        //Download
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(filepath))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error + " " + name);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                string savePath = Application.persistentDataPath + "/" + name;
                Debug.Log("Savepath: " + savePath);
                System.IO.File.WriteAllBytesAsync(savePath, texture.EncodeToPNG());
            }
        }

    }

    public IEnumerator DownloadAudio(string filepath, string name)
    {
        //Download
        using (UnityWebRequest www = UnityWebRequest.Get(filepath))
        {
            //www.downloadHandler = new DownloadHandlerFile(Path.Combine(Application.persistentDataPath, name));
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error + " " + name);
            }
            else
            {
                byte[] clip = www.downloadHandler.data;
                string savePath = Application.persistentDataPath + "/" + name;
                Debug.Log(savePath);
                System.IO.File.WriteAllBytesAsync(savePath, clip);
            }
        }

    }

    public IEnumerator DownloadText(string filepath, string name)
    {
        //Download
        using (UnityWebRequest www = UnityWebRequest.Get(filepath))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error + " " + name);
            }
            else
            {
                byte[] text = www.downloadHandler.data;
                string savePath = Application.persistentDataPath + "/" + name;
                Debug.Log(savePath);
                System.IO.File.WriteAllBytesAsync(savePath, text);
            }
        }

    }
}

