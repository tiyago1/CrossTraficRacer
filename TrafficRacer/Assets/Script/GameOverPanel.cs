using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour 
{
	#region Fields

    public Text Score;
    public Text HighScore;

    public Animator mAnimator;

	#endregion //Fields

	#region Public Methods

    public void ShowPanel()
    {
        Score.text = PlayerPrefs.GetInt("Score").ToString();
        HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();    
        this.gameObject.SetActive(true);
        mAnimator.SetTrigger("Show");
    }

	#endregion // Public Methods
}
