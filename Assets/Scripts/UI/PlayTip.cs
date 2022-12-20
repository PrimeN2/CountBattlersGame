using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTip : MonoBehaviour
{
    private float _lerpDuration = 1.5f;
    private Vector3 _minimizedSize = new Vector3 (0.8f, 0.8f, 0.8f);
    private Vector3 _maximizedSize = Vector3.one;

    private void Start()
    {
        StartCoroutine(ChangeSize(_maximizedSize, _minimizedSize));
    }

    private IEnumerator ChangeSize(Vector3 startValue, Vector3 endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < _lerpDuration)
        {
            transform.localScale = Vector3.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localScale = _maximizedSize;
        StartCoroutine(ChangeSize(endValue, startValue));
    }
}
