using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quizController : MonoBehaviour 
{
	

	[Header("UI References")]
	[SerializeField] private Text txt_question;
	[SerializeField] private Text txt_optionA;
	[SerializeField] private Text txt_optionB;
	[SerializeField] private Text txt_optionC;
	[SerializeField] private Text txt_optionD;
	
	[Header("Feedback prefab")]
	[SerializeField] private GameObject feedback;

	private QuizzMechanic quizzMechanic;

 	void Awake()
	{
		quizzMechanic = new QuizzMechanic();
		quizzMechanic.Read(Settings.quiz);
	}

    void Start () 
	{
		feedback.SetActive(false);
		quizzMechanic.NewGame();

		QuizzMechanic.Question current = quizzMechanic.CurrentQuestion;
		txt_question.text = current.question;
		txt_optionA.text = current.option1;
		txt_optionB.text = current.option2;
		txt_optionC.text = current.option3;
		txt_optionD.text = current.option4;
	}

	public void AnswerQuestion(int option) 
	{
		quizzMechanic.TryAnswer(option);
		NextQuestion();
	}
	private void NextQuestion()
	{
		if (quizzMechanic.NextQuestion(out QuizzMechanic.Question question)) 
		{
			txt_question.text = question.question;
			txt_optionA.text = string.IsNullOrEmpty(question.option1) ? "Nenhuma" : question.option1;
			txt_optionB.text = string.IsNullOrEmpty(question.option2) ? "Nenhuma" : question.option2;
			txt_optionC.text = string.IsNullOrEmpty(question.option3) ? "Nenhuma" : question.option3;
			txt_optionD.text = string.IsNullOrEmpty(question.option4) ? "Nenhuma" : question.option4;
		}
		else 
		{
			feedback.SetActive(true);
		}
	}

	public void SetFeelingRate(int stars)
	{
		quizzMechanic.SetFeelingRate(stars);

		JsonUtils jsonUtils = (new GameObject("jsonUtils")).AddComponent<JsonUtils>();
		jsonUtils.sendResponse(quizzMechanic.performance);
	}

}
