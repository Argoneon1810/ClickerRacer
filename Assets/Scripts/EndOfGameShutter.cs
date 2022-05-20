using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndOfGameShutter : MonoBehaviour {
    [SerializeField] AnimationCurve shutdownCurve;
    [SerializeField] Canvas targetCanvas;
    [SerializeField] float shutdownTimeLength;
    RectTransform mRectTransform;
    Vector2 originalPosition;
    Vector2 readyPosition;

    private void Awake() {
        if(targetCanvas == null)
            targetCanvas = transform.GetComponentInParent<Canvas>();

        if(shutdownCurve.length < 1)
            shutdownCurve = AnimationCurve.EaseInOut(0,0,1,1);

        if(shutdownTimeLength <= 0)
            shutdownTimeLength = 2;
    }

    private void Start() {
        mRectTransform = transform as RectTransform;

        mRectTransform.sizeDelta = GetFullScreenVector(targetCanvas.GetComponent<CanvasScaler>());

        readyPosition = mRectTransform.anchoredPosition;
        originalPosition = readyPosition;
        readyPosition.y += mRectTransform.sizeDelta.y;
        mRectTransform.anchoredPosition = readyPosition;
    }

    public void RequestDropShutter() {
        StartCoroutine(DropShutter());
    }

    IEnumerator DropShutter() {
        float t = 0;
        while(true) {
            if(t > shutdownTimeLength) break;

            mRectTransform.anchoredPosition = Vector2.Lerp(readyPosition, originalPosition, shutdownCurve.Evaluate(t/shutdownTimeLength));
            t += Time.deltaTime;
            yield return null;
        }
    }

    private Vector2 GetFullScreenVector(CanvasScaler scaler) {
        float width = scaler.referenceResolution.x;
        float height = scaler.referenceResolution.y;
        float matchWidthOrHeight = scaler.matchWidthOrHeight;

        return new Vector2(
            Mathf.CeilToInt(Mathf.Lerp(
                width,
                height * Camera.main.aspect,
                matchWidthOrHeight
            )),
            Mathf.CeilToInt(Mathf.Lerp(
                width / Camera.main.aspect,
                height,
                matchWidthOrHeight
            ))
        );
    }
}
