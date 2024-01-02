using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public class MenuController : MonoBehaviour
{
    public static MenuController instance = null;

    [SerializeField]
    private Page initialPage;
    private Canvas _rootCanvas;
    private Stack<Page> _pageStack = new Stack<Page>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        _rootCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        if (initialPage != null)
        {
            PushPage(initialPage);
        }
    }

    private void OnCancel()
    {
        if (_rootCanvas.enabled && _rootCanvas.gameObject.activeInHierarchy)
        {
            if (_pageStack.Count != 0)
            {
                PopPage();
            }
        }
    }

    public bool IsPageInStack(Page page)
    {
        return _pageStack.Contains(page);
    }

    public bool IsPageOnTopOfStack(Page page)
    {
        return _pageStack.Count > 0 && page == _pageStack.Peek();
    }

    public void PushPage(Page page)
    {
        page.Enter(true);

        if (_pageStack.Count > 0)
        {
            Page currentPage = _pageStack.Peek();

            if (currentPage.exitOnNewPagePush)
            {
                currentPage.Exit(false);
            }
        }

        _pageStack.Push(page);
    }

    public void PopPage()
    {
        if (_pageStack.Count > 1)
        {
            Page page = _pageStack.Pop();
            page.Exit(true);

            Page newCurrentPage = _pageStack.Peek();
            if (newCurrentPage.exitOnNewPagePush)
            {
                newCurrentPage.Enter(false);
            }
        }
        else
        {
            Debug.LogWarning("No pages left in stack");
        }
    }

    public void PopAllPages()
    {
        for (int i = 1; i < _pageStack.Count; i++)
        {
            PopPage();
        }
    }
}
