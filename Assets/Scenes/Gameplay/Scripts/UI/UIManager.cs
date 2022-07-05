using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
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
        [SerializeField] private List<Image> livesList;

        [SerializeField] private CanvasGroup pause;
        [SerializeField] private CanvasGroup gameOver;
        [SerializeField] private TMP_Text gameOverText;
        [SerializeField] private TMP_Text gameOverScoreText;

        [SerializeField] private float fadeSpeed;
        private bool runningVisibilityCorroutine;

        private void Awake()
        {
            GameManager.OnLifeLost += ChangeLifes;
            GameManager.OnGameOver += ShowGameOverPanel;
            GameManager.OnScoreChange += ChangeScoreText;
            PauseSystem.OnPauseStateChange += ShowPausePanel;
        }
        private void OnDestroy()
        {
            GameManager.OnLifeLost -= ChangeLifes;
            GameManager.OnGameOver -= ShowGameOverPanel;
            GameManager.OnScoreChange -= ChangeScoreText;
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

        private void ShowGameOverPanel(string text)
        {
            gameOverText.text = text;
            gameOverScoreText.text = score.text + " Points";
            ShowPanel(gameOver, FadeType.Delayed);
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
            panel.GetComponent<Image>().color = UnityEngine.Random.ColorHSV (0, 1, 0.8f, 1, 0.85f, 1, 0.7f, 0.85f);
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

        private void ChangeScoreText(int newScore)
        {
            string newText = "";
            if(newScore <1000)
            {
                newText += "0";
            }
            if(newScore <100)
            {
                newText += "0";
            }
            newText += newScore.ToString();

            score.text = newText;

        }

        private void ChangeLifes()
        {
            GameObject imageToDelete = livesList[livesList.Count - 1].gameObject;
            livesList.RemoveAt(livesList.Count - 1);
            Destroy(imageToDelete);
        }

    }

}