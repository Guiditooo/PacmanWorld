using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace PacManWorld
{
    public enum FadeType
    {
        Instantaneously,
        Delayed
    }
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private TMP_Text score;
        [SerializeField] private Image[] image;

        [SerializeField] private CanvasGroup pause;
        [SerializeField] private CanvasGroup gameOver;

        [SerializeField] private float fadeSpeed;
        private bool runningVisibilityCorroutine;

        private void Awake()
        {
            PauseSystem.OnPauseStateChange += ShowPausePanel;
        }
        private void OnDestroy()
        {
            PauseSystem.OnPauseStateChange -= ShowPausePanel;
        }
        private void Start()
        {
            runningVisibilityCorroutine = false;   
        }

        private void ShowPausePanel(PauseStates pauseState)
        {
            
            if(pauseState.Equals(PauseStates.Paused))
            {
                if (runningVisibilityCorroutine)
                {
                    StopCoroutine(MakeInvisible(pause));
                }
                ShowPanel(pause, FadeType.Delayed);
            }
            else
            {
                if (runningVisibilityCorroutine)
                {
                    StopCoroutine(MakeVisible(pause));
                }
                HidePanel(pause, FadeType.Delayed);
            }
        }

        public static void BackToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public static void RestartScene()
        {
            SceneManager.LoadScene("Gameplay");
        }

        public void ShowPanel(CanvasGroup panel, FadeType fadeType)
        {
            switch (fadeType)
            {
                case FadeType.Delayed:
                    StartCoroutine(MakeVisible(panel));
                    break;
                case FadeType.Instantaneously:
                default:
                    panel.alpha = 1;
                    panel.blocksRaycasts = true;
                    panel.interactable = true;
                    break;
            }
        }
        private IEnumerator MakeVisible(CanvasGroup panel)
        {
            float t = 0;
            runningVisibilityCorroutine = true;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime * fadeSpeed;
                panel.alpha = t;
                yield return null;
            }
            runningVisibilityCorroutine = false;
            panel.blocksRaycasts = true;
            panel.interactable = true;
        }

        public void HidePanel(CanvasGroup panel, FadeType fadeType)
        {
            switch (fadeType)
            {
                case FadeType.Delayed:
                    StartCoroutine(MakeInvisible(panel));
                    break;
                case FadeType.Instantaneously:
                default:
                    panel.alpha = 0;
                    panel.blocksRaycasts = false;
                    panel.interactable = false;
                    break;
            }
        }
        private IEnumerator MakeInvisible(CanvasGroup panel)
        {
            float t = 1;
            runningVisibilityCorroutine = true;
            while (t > 0)
            {
                t -= Time.unscaledDeltaTime * fadeSpeed;
                panel.alpha = t;
                yield return null;
            }
            runningVisibilityCorroutine = false;
            panel.blocksRaycasts = false;
            panel.interactable = false;
        }

    }

}