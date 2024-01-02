using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class Page : MonoBehaviour
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private AudioSource _audioSource;

    [SerializeField]
    private float _animationSpeed = 1f;
    public bool exitOnNewPagePush = false;
    [SerializeField]
    private AudioClip _entryClip;
    [SerializeField]
    private AudioClip _exitClip;
    [SerializeField]
    private EntryMode _entryMode = EntryMode.NONE;
    [SerializeField]
    private Direction _entryDirection = Direction.LEFT;
    [SerializeField]
    private EntryMode _exitMode = EntryMode.NONE;
    [SerializeField]
    private Direction _exitDirection = Direction.LEFT;
    [SerializeField]
    private UnityEvent prePushAction;
    [SerializeField]
    private UnityEvent postPushAction;
    [SerializeField]
    private UnityEvent prePopAction;
    [SerializeField]
    private UnityEvent postPopAction;

    private Coroutine _animationCoroutine;
    private Coroutine _audioCoroutine;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.spatialBlend = 0;
        _audioSource.enabled = false;
    }

    public void Enter(bool playAudio)
    {
        prePushAction?.Invoke();

        switch (_entryMode)
        {
            case EntryMode.SLIDE:
                SlideIn(playAudio);
                break;

        }
    }

    public void Exit(bool playAudio)
    {
        prePushAction?.Invoke();

        switch (_entryMode)
        {
            case EntryMode.SLIDE:
                SlideOut(playAudio);
                break;
        }
    }

    private void SlideIn(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }
        _animationCoroutine = StartCoroutine(AnimationHelper.SlideIn(_rectTransform, _entryDirection, _animationSpeed, null));

        PlayEntryClip(playAudio);
    }

    private void SlideOut(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }
        _animationCoroutine = StartCoroutine(AnimationHelper.SlideOut(_rectTransform, _exitDirection, _animationSpeed, null));

        PlayExitClip(playAudio);
    }

    private void PlayEntryClip(bool playAudio)
    {
        if (playAudio && _entryClip != null && _audioSource != null)
        {
            if (_audioCoroutine != null)
            {
                StopCoroutine(_audioCoroutine);
            }

            _audioCoroutine = StartCoroutine(PlayClip(_entryClip));

        }
    }

    private void PlayExitClip(bool playAudio)
    {
        if (playAudio && _exitClip != null && _audioSource != null)
        {
            if (_audioCoroutine != null)
            {
                StopCoroutine(_audioCoroutine);
            }

            _audioCoroutine = StartCoroutine(PlayClip(_exitClip));

        }
    }

    private IEnumerator PlayClip(AudioClip clip)
    {
        _audioSource.enabled = true;
        WaitForSeconds wait = new WaitForSeconds(clip.length);
        _audioSource.PlayOneShot(clip);

        yield return wait;

        _audioSource.enabled = false;
    }
}
