using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PacManWorld
{
    public enum FadeType
    {
        Instantaneously,
        Delayed
    }
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup pause;
        [SerializeField] private CanvasGroup gameOver;

        
        public static void ShowPanel(CanvasGroup panel, FadeType fadeType)
        {
            switch (fadeType)
            {
                case FadeType.Delayed:
                    MakeVisible(panel);
                    break;
                case FadeType.Instantaneously:
                default:
                    panel.alpha = 1;
                    panel.blocksRaycasts = true;
                    panel.interactable = true;
                    break;
            }
        }

        private static IEnumerator MakeVisible(CanvasGroup panel)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                panel.alpha = t;
                yield return null;
            }
            panel.blocksRaycasts = true;
            panel.interactable = true;
        }

    }

}