using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AudioControls : MonoBehaviour
{
    private float _audioLength = 0f;

    [SerializeField] 
    private AudioSource _audioSource;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private GameObject _playButton;

    [SerializeField]
    private GameObject _pauseButton;

    private void Start()
    {
        _slider.maxValue = 1f;
        _slider.onValueChanged.AddListener(OnSliderValueChange);
        _pauseButton.SetActive(false);
    }

    void Update()
    {
        _slider.value = _audioSource.time;
    }

    private void OnSliderValueChange(float f) => _audioSource.time = f;

    public void PlayPause()
    {
        if (_audioLength == 0f)
        {
            _audioLength = _audioSource.clip.length;
            _slider.maxValue = _audioSource.clip.length;
        }

        if (_audioSource.isPlaying)
        {
            _audioSource.Pause();
            _pauseButton.SetActive(false);
            _playButton.SetActive(true);
        } else
        {
            _audioSource.Play();
            _pauseButton.SetActive(true);
            _playButton.SetActive(false);
        }
    }

    public void OnViewExit()
    {
        _audioSource.Stop();
        _pauseButton.SetActive(false);
        _playButton.SetActive(true);
    }
}
