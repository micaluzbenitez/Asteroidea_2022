using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        [Header("Menu panel")]
        [SerializeField] private CanvasGroup startingPanel = null;
        [SerializeField] private TMP_Text versionText = null;
        [SerializeField] private GameObject playButton = null;

        [Header("Game scene")]
        [SerializeField] private string gameSceneName = default;
        [SerializeField] private string tutorialSceneName = default;

        [Header("Skins")]
        [SerializeField] private Image player = null;
        [SerializeField] private Skin[] skins = null;

        private CanvasGroup actualPanel = null;

        private string seenTutorialKey = "tutorial";
        private const int TRUE_VALUE = 1;
        private const int FALSE_VALUE = 0;

        private void Awake()
        {
            SetProjectVersion();

            actualPanel = startingPanel;

            foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            actualPanel.alpha = 1;
            actualPanel.blocksRaycasts = true;
            actualPanel.interactable = true;

            EnablePlay();
            SetPlayerSkin();
        }

        private void Start()
        {
            Time.timeScale = 1;
        }

        private void SetProjectVersion()
        {
            versionText.text = $"v{Application.version}";
        }

        private void SetPlayerSkin()
        {
            for (int i = 0; i < skins.Length; i++)
            {
                if (PlayerPrefs.GetInt("Skin") == skins[i].ID)
                {
                    player.sprite = skins[i].starUI;
                }
            }
        }

        public void StartPanel(CanvasGroup newPanel)
        {
            StartCoroutine(PanelChange(newPanel));
        }

        IEnumerator MakeItVisible(CanvasGroup panel)
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
        IEnumerator MakeItInvisible(CanvasGroup panel)
        {
            panel.blocksRaycasts = false;
            panel.interactable = false;
            float t = 1;
            while (t > 0)
            {
                t -= Time.deltaTime;
                panel.alpha = t;
                yield return null;
            }
        }

        IEnumerator PanelChange(CanvasGroup panel)
        {
            yield return StartCoroutine(MakeItInvisible(actualPanel));
            StartCoroutine(MakeItVisible(panel));
            actualPanel = panel;
        }

        private void EnablePlay()
        {
            if (PlayerPrefs.HasKey(seenTutorialKey)) return;

            if (PlayerPrefs.GetInt(seenTutorialKey) == FALSE_VALUE) return;

            playButton.GetComponent<Image>().color = new Color();
            playButton.GetComponent<Button>().interactable = true;
        }


        public void CloseGame()
        {
            Application.Quit();
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }
        public void LoadTutorial()
        {
            SceneManager.LoadScene(tutorialSceneName);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}