using UnityEngine;
using UnityEngine.UI;

public class CreditScroll : MonoBehaviour {

    public float endY;

    private RectTransform rectTransform;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update() {
        Vector2 tmp = rectTransform.anchoredPosition;
        if (tmp.y < endY) {
            tmp.y += 50.0f * Time.deltaTime;
            if (tmp.y > endY) {
                tmp.y = endY;
            }
            rectTransform.anchoredPosition = tmp;
        }
    }
}
