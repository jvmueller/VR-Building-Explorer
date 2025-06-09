using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour {
    [Header("Fade Settings")] [SerializeField]
    private float fadeInDuration = 0.5f;

    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("UI Components")] [SerializeField]
    private Image fadeImage;

    [SerializeField] private Color fadeColor = Color.black;

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Coroutine _currentFadeCoroutine;

    void Awake() {
        // Get or create required components
        _canvas = GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

        if (_canvasGroup == null) {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Setup fade image if not assigned
        if (fadeImage == null) {
            SetupFadeImage();
        }

        // Initialize canvas as invisible
        SetCanvasVisibility(false);
    }

    void Start() {
        // Ensure the fade image covers the entire screen
        if (fadeImage != null) {
            fadeImage.color = fadeColor;
        }
    }

    /// <summary>
    /// Quickly fades the screen to black/solid color
    /// </summary>
    public void QuickFadeIn() {
        if (_currentFadeCoroutine != null) {
            StopCoroutine(_currentFadeCoroutine);
        }

        _currentFadeCoroutine = StartCoroutine(FadeCoroutine(true, fadeInDuration));
    }

    /// <summary>
    /// Quickly fades the screen back to transparent/clear
    /// </summary>
    public void QuickFadeOut() {
        if (_currentFadeCoroutine != null) {
            StopCoroutine(_currentFadeCoroutine);
        }

        _currentFadeCoroutine = StartCoroutine(FadeCoroutine(false, fadeOutDuration));
    }

    /// <summary>
    /// Core fade coroutine that handles the actual fading
    /// </summary>
    /// <param name="fadeIn">True to fade to solid color, false to fade to transparent</param>
    /// <param name="duration">Duration of the fade</param>
    private IEnumerator FadeCoroutine(bool fadeIn, float duration) {
        // Enable canvas for fading
        SetCanvasVisibility(true);

        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;

        // Set initial alpha
        _canvasGroup.alpha = startAlpha;

        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / duration;

            // Apply animation curve for smooth transition
            float curveValue = fadeCurve.Evaluate(normalizedTime);
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, curveValue);

            _canvasGroup.alpha = currentAlpha;

            yield return null;
        }

        // Ensure final alpha is set
        _canvasGroup.alpha = endAlpha;

        // Disable canvas if fully transparent to avoid blocking interactions
        if (!fadeIn) {
            SetCanvasVisibility(false);
        }

        _currentFadeCoroutine = null;
    }

    /// <summary>
    /// Sets the visibility and interaction state of the canvas
    /// </summary>
    /// <param name="visible">Whether the canvas should be visible and active</param>
    private void SetCanvasVisibility(bool visible) {
        _canvas.enabled = visible;
        _canvasGroup.blocksRaycasts = visible;
        _canvasGroup.interactable = visible;

        if (!visible) {
            _canvasGroup.alpha = 0f;
        }
    }

    /// <summary>
    /// Creates and configures the fade image if one doesn't exist
    /// </summary>
    private void SetupFadeImage() {
        // Create a full-screen image child
        GameObject imageObject = new GameObject("FadeImage");
        imageObject.transform.SetParent(transform, false);

        fadeImage = imageObject.AddComponent<Image>();
        fadeImage.color = fadeColor;

        // Make the image fill the entire canvas
        RectTransform rectTransform = fadeImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// Immediately sets the fade to a specific alpha value
    /// </summary>
    /// <param name="alpha">Alpha value (0 = transparent, 1 = opaque)</param>
    public void SetFadeAlpha(float alpha) {
        if (_currentFadeCoroutine != null) {
            StopCoroutine(_currentFadeCoroutine);
            _currentFadeCoroutine = null;
        }

        SetCanvasVisibility(alpha > 0f);
        _canvasGroup.alpha = Mathf.Clamp01(alpha);
    }

    /// <summary>
    /// Check if a fade is currently in progress
    /// </summary>
    public bool IsFading => _currentFadeCoroutine != null;

    /// <summary>
    /// Get the current fade alpha value
    /// </summary>
    public float CurrentAlpha => _canvasGroup != null ? _canvasGroup.alpha : 0f;
}