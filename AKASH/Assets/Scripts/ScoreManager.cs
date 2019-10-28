using System;
using UnityEngine;
using N_BH;

public class ScoreManager: Singleton<ScoreManager>
{
	
 
	private int score;
	
	private void Start()
	{
		score = 500;
	}
	
	private void Score(int value)
	{
			score+= value;
			
			if(score>1000)
			{
				score = 1000;
			}
			
			if(score<=0)
			{
				score = 0;
				//GameOver();
			}
	}
	
	
	
	public void BullScore(Scholar scholar, bool strong)
	{
		if(strong)
		{
			if(scholar.greeneyes)
			{
				Score(-50);
			}
			else
			{
				Score(-20);
			}
		}
		else
		{
			if(scholar.greeneyes)
			{
				Score(-25);
			}
			else
			{
				Score(-10);
			}
		}
	}
	
	public void ExecuteScore(string reason, Scholar scholar)
	{
		if(scholar.reason[reason])
		{
			Score(50);
		}
		else
		{
			Score(-100);
		}		
	}

    public void QuestionScore(string topic, bool answer)
    {
        if ((GameManager.get.banned[topic] || !answer) && (!GameManager.get.banned[topic] || answer))
            Score(10);
        else
            Score(-25);
    }
	
	public void SubjectScore(string subject)
	{
		if(GameManager.get.banned[subject])
            Score(10);
        else
            Score(-25);
	}

    
}
