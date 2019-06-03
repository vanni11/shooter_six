using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	PlayerHealth playerHealth;

	public int score;
	public Text scoreText;

	public Image gameoverImage;
	public Text gameoverText;
	public Text resultScoreText;
	public Button retryButton;
	public Button exitGameButton;

	private void Awake()
	{
		playerHealth = FindObjectOfType<PlayerHealth>();
	}

	public void SetScoreText()
	{
		scoreText.text = score.ToString();
	}

	float timer = 0;

	public IEnumerator Gameover()
	{
		yield return new WaitForSeconds(1f);
		gameoverImage.gameObject.SetActive(true);
		Color myColor = gameoverImage.color;
		while(gameoverImage.color.a < 1f)
		{
			timer += Time.deltaTime / 2f;
			myColor.a = Mathf.Lerp(0f, 1f, timer);
			gameoverImage.color = myColor;
			yield return null;
		}

		resultScoreText.text = score.ToString();
		gameoverText.gameObject.SetActive(true);
		resultScoreText.gameObject.SetActive(true);
		retryButton.gameObject.SetActive(true);
		exitGameButton.gameObject.SetActive(true);
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}