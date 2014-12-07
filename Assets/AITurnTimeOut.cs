using System;
using UnityEngine;
using System.Collections;

public class AITurnTimeOut : MonoBehaviour
{

    public float WaitTime = 1000f;
    public bool Running = false;

    private float _timeLeft = 1000f;
    private Canvas _canvas;

	void Start ()
	{
	    _canvas = GetComponent<Canvas>();
	}

    public void StartCountDown()
    {
        _timeLeft = WaitTime;
        _canvas.enabled = true;
        Running = true;
    }
	
	void Update ()
	{
        
	    if (!Running)
	        return;

	    if (_timeLeft <= 0)
	    {
	        Running = false;
	        _canvas.enabled = false;

            AITurnManager.Instance.RunTurn();
	    }
	    else
	    {
	        _timeLeft -= Time.deltaTime;
	    }
	}
}
