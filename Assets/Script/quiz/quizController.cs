using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quizController : MonoBehaviour 
{
	[System.Serializable]
	private struct Question
    {
		public string question;
		public string option1;
		public string option2;
		public string option3;
		public string option4;
		public int answer;
    }

	[Header("UI References")]
	[SerializeField] private Text txt_question;
	[SerializeField] private Text txt_optionA;
	[SerializeField] private Text txt_optionB;
	[SerializeField] private Text txt_optionC;
	[SerializeField] private Text txt_optionD;
	
	[Header("Feedback prefab")]
	[SerializeField] private GameObject feedback;


	private List<Question> _questions;
	private int _idQuestion;
	private GameResult _performance;

 	void Awake(){
		_questions = new List<Question>();
		
		Component[] questions = Settings.quiz.components;

		foreach(Component questionComponent in questions){
			Debug.Log(questionComponent.name);
			Debug.Log(questionComponent.fields.Length);

			Question questionStruct = FromComponentToQuestion(questionComponent);
			_questions.Add(questionStruct);
		}
	}

    private Question FromComponentToQuestion(Component component)
    {
		Question question = new Question();

		foreach(ComponentField field in component.fields)
        {
            if (field.role.Equals("question"))
            {
				question.question = field.resource.content;
            }
			else if (field.role.Equals("answer"))
            {
				question.answer = int.Parse(field.resource.content);
			}
			else if (field.role.Equals("option1"))
			{
				question.option1 = field.resource.content;
			}
			else if (field.role.Equals("option2"))
			{
				question.option2 = field.resource.content;
			}
			else if (field.role.Equals("option3"))
			{
				question.option3 = field.resource.content;
			}
			else if (field.role.Equals("option4"))
			{
				question.option4 = field.resource.content;
			}
		}

		return question;
    }

    void Start () {
		feedback.SetActive(false);
		_performance = new GameResult();
		_performance.game = Settings.quiz.id;
		_performance.hits = 0;
		_performance.fails = 0;
		_idQuestion = 0;


		txt_question.text = _questions[_idQuestion].question;
		txt_optionA.text = _questions[_idQuestion].option1;
		txt_optionB.text = _questions[_idQuestion].option2;
		txt_optionC.text = _questions[_idQuestion].option3;
		txt_optionD.text = _questions[_idQuestion].option4;
	}

	public void answer(int option) 
	{
		if (_idQuestion >= _questions.Count)
			return;
		
		if (option == _questions[_idQuestion].answer) 
		{
			Debug.Log("Certo");
			_performance.hits++;
		} 
		else 
		{
			Debug.Log("Errado");
			_performance.fails++;
		}
			
		nextQuestion();
	}
	private void nextQuestion(){
		_idQuestion++;
		Debug.Log("Next" + _idQuestion);
		if (_idQuestion >= _questions.Count) {
			feedback.SetActive(true);
		}
		else {
			Question question = _questions[_idQuestion];
			txt_question.text = question.question;
			txt_optionA.text = string.IsNullOrEmpty(question.option1) ? "Nenhuma" : question.option1;
			txt_optionB.text = string.IsNullOrEmpty(question.option2) ? "Nenhuma" : question.option2;
			txt_optionC.text = string.IsNullOrEmpty(question.option3) ? "Nenhuma" : question.option3;
			txt_optionD.text = string.IsNullOrEmpty(question.option4) ? "Nenhuma" : question.option4;
		}
	}

	public void setFeelingRate(int stars){
		_performance.feeling_rate = stars;
		_performance.score = _performance.hits;
		JsonUtils jsonUtils = (new GameObject("jsonUtils")).AddComponent<JsonUtils>();
		jsonUtils.sendResponse(_performance);
	}

}
