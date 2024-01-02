using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
    private Image _image;

    private List<Sprite> _photoList = new List<Sprite>();

    private int _currentPhoto = 0;

    [SerializeField]
    private float _fadeTime = 2f;

    private bool _fadeFinished = false;

    private float _deltaTime = 0f;

    private bool _isRunning = false;

    [SerializeField]
    private float _timerDuration = 5.0f;

    private float _timerRemaining = 5.0f;

    private bool _timerIsRunning = false;


    void Start()
    {
        _image = GetComponent<Image>();


        _timerIsRunning = true;
    }


    public void LoadPhoto(Sprite photo){
        this._photoList.Add(photo);
        this._isRunning = true;
        this._image.sprite = _photoList[0];
    }


    private void Update()
    {
        if (_isRunning)
        {
            _timerRemaining -= Time.deltaTime;

            if (_timerRemaining < 0)
            {
                _image.sprite = _photoList[_currentPhoto++];
                _timerRemaining = _timerDuration;
                if (_currentPhoto >= _photoList.Count)
                {
                    _currentPhoto = 0;
                }
            }


        }
    }
}
