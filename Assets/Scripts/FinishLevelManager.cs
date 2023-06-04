using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelManager : MonoBehaviour
{
    public RectTransform finishLevelNotification;
    public RectTransform finishLevelText;
    public RectTransform finishLevelDisplayPosition;
    public Image fadeOutPanel;
    [Header("Next Scene")]
    public INextScene nextScene;

    public IEnumerator OnLevelCompleted()
    {
        FindObjectOfType<PauseMenu>().canBePaused = false;
        float step = 0.5f * Time.fixedDeltaTime;

        for (float t = 0; t <= 1.0f; t += step) {
            finishLevelNotification.transform.position = Vector3.Lerp(finishLevelNotification.transform.position, finishLevelDisplayPosition.transform.position, t);
            yield return new WaitForEndOfFrame();
        }

        step = 1.5f * Time.fixedDeltaTime;
        Vector2 prevSize = new Vector2(5, 100);
        Vector2 newSize = new Vector2(400, 100);

        for (float t = 0; t <= 1.0f; t += step) {
            finishLevelNotification.sizeDelta = Vector2.Lerp(prevSize, newSize, t);
            finishLevelText.sizeDelta = Vector2.Lerp(prevSize, newSize, t);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3);

        step = 0.6f * Time.fixedDeltaTime;
        Color prevColor = new Color(0, 0, 0, 0);
        Color newColor = new Color(0, 0, 0, 1);

        for (float t = 0; t <= 1.0f; t += step) {
            Color transitionColor = Color.Lerp(prevColor, newColor, t);
            fadeOutPanel.color = transitionColor;
            yield return new WaitForEndOfFrame();
        }
        nextScene.moveToNextScene();
    }
}
