using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class ContinueButton : MonoBehaviour
{
    Button m_button;

    void Start()
    {
        m_button = GetComponent<Button>();
        m_button.interactable = (CurrentLevelTracker.latestLevel > 0);
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(CurrentLevelTracker.latestLevel);
    }
}