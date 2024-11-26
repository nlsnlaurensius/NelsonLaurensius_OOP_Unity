using UnityEngine;
using UnityEngine.UIElements;

public class ChooseWeaponController : MonoBehaviour
{
    private VisualElement container;
    private bool hasTapped = false;
    private UIDocument uiDocument;

    public static bool isGameActive = false;

    private float fadeDuration = 1f;
    private float fadeProgress = 0f;
    private bool isFading = false;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        container = root.Q<VisualElement>("Container");
        if (container == null)
        {
            return;
        }

        container.RegisterCallback<ClickEvent>(evt =>
        {
            if (!hasTapped)
            {
                hasTapped = true;
                StartFadeOut();
            }
        });

        isGameActive = false;
    }

    private void StartFadeOut()
    {
        container.style.opacity = 1;
        isFading = true;
    }

    void Update()
    {
        if (isFading)
        {
            fadeProgress += Time.deltaTime / fadeDuration;
            container.style.opacity = Mathf.Lerp(1, 0, fadeProgress);

            if (fadeProgress >= 1f)
            {
                isFading = false;
                fadeProgress = 0f;
                FinishFadeOut();
            }
        }
    }

    private void FinishFadeOut()
    {
        container.RemoveFromHierarchy();
        isGameActive = true;
    }
}
