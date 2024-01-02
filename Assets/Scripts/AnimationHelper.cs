using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHelper
{
    public static IEnumerator SlideIn(RectTransform Transform, Direction direction, float speed, UnityEvent OnEnd)
    {
        Vector2 startPosition;
        switch (direction)
        {
            case Direction.UP:
                startPosition = new Vector2(0, -Screen.height);
                break;
            case Direction.RIGHT:
                startPosition = new Vector2(-Screen.width, 0);
                break;
            case Direction.LEFT:
                startPosition = new Vector2(Screen.width, 0);
                break;
            case Direction.DOWN:
                startPosition = new Vector2(0, Screen.height);
                break;
            default:
                startPosition = new Vector2(0, -Screen.height);
                break;
        }

        float time = 0;
        while (time < 1)
        {
            Transform.anchoredPosition = Vector2.Lerp(startPosition, Vector2.zero, time);
            yield return null;
            time += Time.deltaTime * speed;
        }

        Transform.anchoredPosition = Vector2.zero;
        OnEnd?.Invoke();
    }

    public static IEnumerator SlideOut(RectTransform Transform, Direction direction, float speed, UnityEvent OnEnd)
    {
        Vector2 endPosition;
        switch (direction)
        {
            case Direction.UP:
                endPosition = new Vector2(0, Screen.height);
                break;
            case Direction.RIGHT:
                endPosition = new Vector2(Screen.width, 0);
                break;
            case Direction.LEFT:
                endPosition = new Vector2(-Screen.width, 0);
                break;
            case Direction.DOWN:
                endPosition = new Vector2(0, -Screen.height);
                break;
            default:
                endPosition = new Vector2(0, Screen.height);
                break;
        }

        
        float time = 0;
        while (time < 1)
        {
            Transform.anchoredPosition = Vector2.Lerp(Vector2.zero, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }

        Transform.anchoredPosition = endPosition;
        OnEnd?.Invoke();
    }
}