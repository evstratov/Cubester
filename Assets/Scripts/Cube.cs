using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	
	private bool isSteelMoving = false;
	private const float moveTime = 0.125f;

	private GameObject Target;
	public Animation anim;
	public GameObject gameOverPanel;

	private int scores = 0;
	private float time = 3f;

	public Text scoresText;
	public Text timeText;

	// Start is called before the first frame update
	void Start()
    {
		Utils.GameOver = false;

		Target = GameObject.FindWithTag("Target");

		StartCoroutine("TimeDecrementCoroutine", 0);

		ShowScores();
    }

    // Update is called once per frame
    void Update()
    {
		#if UNITY_EDITOR
			SwipeKeys();
		#elif ANDROID
			SwipeTouch();
		#endif
	}

 
	public void SwipeTouch()
	{
	     if(Input.touches.Length > 0)
	     {
	         Touch t = Input.GetTouch(0);
	         if(t.phase == TouchPhase.Began)
	         {
	              //save began touch 2d point
	             firstPressPos = new Vector2(t.position.x,t.position.y);
	         }
	         if(t.phase == TouchPhase.Ended)
	         {
	              //save ended touch 2d point
	             secondPressPos = new Vector2(t.position.x,t.position.y);
	                           
	              //create vector from the two points
	             currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
	               
	             //normalize the 2d vector
	             currentSwipe.Normalize();
	 
	             //swipe upwards
	             if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
	             {
					 if (Utils.FirstPhase)
						return;
					 if (Utils.FirstPlay)
						Utils.SecondPhase = false;
					StartCoroutine(MoveRoutine(new Vector3(0, 0, 1), "UpSwipe"));
				 }
	             //swipe down
	             if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
	             {
					 if (Utils.FirstPhase)
						return;
					 if (Utils.FirstPlay)
						Utils.SecondPhase = false;
					StartCoroutine(MoveRoutine(new Vector3(0, 0, -1), "DownSwipe"));
				 }
	             //swipe left
	             if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	             {
					 if (Utils.SecondPhase)
						return;
					 if (Utils.FirstPlay)
						Utils.FirstPhase = false;
					StartCoroutine(MoveRoutine(new Vector3(-1, 0, 0), "LeftSwipe"));
				 }
	             //swipe right
	             if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	             {
					 if (Utils.SecondPhase)
						return;
					 if (Utils.FirstPlay)
						Utils.FirstPhase = false;
					StartCoroutine(MoveRoutine(new Vector3(1, 0, 0), "RightSwipe"));
				 }
	         }
	     }
	}
	public void SwipeKeys()
	{
		if (isSteelMoving)
			return;
		
        //swipe upwards
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
			if (Utils.FirstPhase)
				return;
			if (Utils.FirstPlay)
				Utils.SecondPhase = false;
			StartCoroutine(MoveRoutine(new Vector3(0,0,1), "UpSwipe"));
        }
        //swipe down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
			if (Utils.FirstPhase)
				return;
			if (Utils.FirstPlay)
				Utils.SecondPhase = false;
			StartCoroutine(MoveRoutine(new Vector3(0,0,-1), "DownSwipe"));
        }
        //swipe left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
			if (Utils.SecondPhase)
				return;
			if (Utils.FirstPlay)
				Utils.FirstPhase = false;
	        StartCoroutine(MoveRoutine(new Vector3(-1,0,0), "LeftSwipe"));
        }
        //swipe right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
			if (Utils.SecondPhase)
				return;
			if (Utils.FirstPlay)
				Utils.FirstPhase = false;
			StartCoroutine(MoveRoutine(new Vector3(1,0,0), "RightSwipe"));
        }
	}
	
	IEnumerator MoveRoutine(Vector3 direction, string animName)
	{
		if (CanMove(direction))
		{
			double progress = 0;
			// за 100  по 0.01 до 1 итераций 
			double step = 0.1f;
			// время задержки между итерациями, чтобы всё прошло за 0.25с
			float delayTime = moveTime / (1f / (float) step);
			Vector3 toPosition = transform.position + direction;
			toPosition = new Vector3(Mathf.Round(toPosition.x), toPosition.y, Mathf.Round(toPosition.z));

			isSteelMoving = true;

			

			anim[animName].speed = 8f;
			anim.Play(animName);

			// PlayJumpIfTarget(direction);

			while (progress <= 1)
			{
				progress += step;
				transform.position = Vector3.Lerp(transform.position, toPosition, (float) progress);

				if (progress >= 1)
				{
					transform.position = toPosition;
					isSteelMoving = false;
					break;
				}

				yield return new WaitForSeconds(delayTime);
			}
		}
	}

	private void PlayJumpIfTarget(Vector3 direction)
	{
		if (transform.position + direction == Target.transform.position)
		{
			// TODO: Jump
		}
	}
	
	private bool CanMove(Vector3 direction)
	{
		Vector3 newPos = transform.position + direction;
	    RaycastHit hit;
        Ray ray = new Ray(newPos, -Vector3.up);
        Physics.Raycast(ray, out hit);

        if (hit.collider != null && hit.collider.gameObject.tag == "Level" &&
			!Utils.TapToContinueButtonShowing && !Utils.GameOver)
        {
            return true;
        }
        else 
			return false;
	}

	public void AddScore(int score)
	{
		scores += score;
		time = 5f;
		StartCoroutine("TimeDecrementCoroutine", 0);
		ShowScores();
	}

	private void ShowScores()
	{
		scoresText.text = $"{LocalizationManager.GetTranslate(LocalizationManager.SCORE_KEY)} {scores}";
	}

	private IEnumerator TimeDecrementCoroutine()
	{
		while (time > 0)
		{
			timeText.text = String.Format("{0:0.00}", time);
			if (!Utils.IsGamePause())
				time -= 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
		timeText.text = "Game Over";
		GameOver();
	}

	private void GameOver()
    {
		gameOverPanel.SetActive(true);
		Utils.GameOver = true;
	}
}
