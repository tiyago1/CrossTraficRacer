using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	#region Fields

    public Text ScoreText;
    private int mScore;

    public GameOverPanel GameOverPanel;
    public ButtonManager StartButton;

    #endregion //Fields
	
    #region Unity Methods

    public void Start()
    {
        if(StartButton != null)
            StartButton.OnClickEvent += OnStartButtonClicked;
    }

    #endregion // Unity Methods

	#region Public Methods

    public void StartTimer()
    {
        StartCoroutine(TimeTicker());
    }

    public void OnStartButtonClicked()
    {
        StartCoroutine(SceneTranslate());
    }

    private IEnumerator SceneTranslate()
    {
        yield return new WaitForSeconds(0.2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnMainMenuButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameOver()
    {

        PlayerPrefs.SetInt("Score", mScore);
        int highScore = PlayerPrefs.GetInt("HighScore");

        if (mScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore",mScore);
        }

        GameOverPanel.ShowPanel();
    }

	#endregion // Public Methods
	
	#region Private Methods
	
    private IEnumerator TimeTicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            mScore++;
            ScoreText.text = mScore.ToString();
        }
    }

	#endregion //Private Methods
}
