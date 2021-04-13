﻿using System;
using System.Collections.Generic;

public class QuizzMechanic : IMechanic
{
    [System.Serializable]
    public struct Question
    {
        public string question;
        public string option1;
        public string option2;
        public string option3;
        public string option4;
        public int answer;
    }
    public Question CurrentQuestion => questions[idQuestion];
    
    public List<Question> questions;
    public int idQuestion;
    public GameResult performance;

    private long _idGame;

    /// <summary>
    /// Reads game components and converts to quizz mechanic.
    /// Throws ArgumentException when game is not a Quizz mechanic and when unknown component is found.
    /// </summary>
    /// <param name="game"></param>
    public void Read(Game game)
    {
        if (!game.mechanic_name.Equals("Quizz"))
            throw new ArgumentException("Not a quizz game");

        questions = new List<Question>();
        foreach (Component component in game.components)
        {
            if (component.tag.Equals("_question"))
            {
                Question resultQuestion = FromComponentToQuestion(component);
            
                questions.Add(resultQuestion);
            }
            else
                throw new ArgumentException($"Non existent component {component.tag} in quizz mechanic was found in game {game.name}");
        }

        _idGame = game.id;
    }

    /// <summary>
    /// Setup for a new Game
    /// </summary>
    public void NewGame()
    {
        performance = new GameResult();
        performance.game = _idGame;
        performance.hits = 0;
        performance.fails = 0;
        idQuestion = 0;
    }


    /// <summary>
    /// Reads given component and converts it to struct Question.
    /// Throws ArgumentNullException when null, whitespace or empty string is passed.
    /// Throws ArgumentOutOfRangeException when answer is out of range [0,3] and when unknown role is received.
    /// </summary>
    /// <param name="component"></param>
    /// <returns>Converted Question</returns>
    private Question FromComponentToQuestion(Component component)
    {
        Question question = new Question();

        foreach (ComponentField field in component.fields)
        {
            if (string.IsNullOrEmpty(field.resource.content) || string.IsNullOrWhiteSpace(field.resource.content))
            {
                throw new ArgumentNullException($"Invalid content: {field.resource.content} with role {field.role}");
            }

            if (field.role.Equals("question"))
            {
                question.question = field.resource.content;
            }
            else if (field.role.Equals("answer"))
            {
                if (int.TryParse(field.resource.content, out int answer) && (answer >= 0 && answer < 4))
                {
                    question.answer = answer;
                }
                else throw new ArgumentOutOfRangeException($"Anwser {answer} is out of range");
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
            else
                throw new ArgumentOutOfRangeException($"Unknown role received {field.role}");
        }

        return question;
    }

    /// <summary>
    /// Measures performance and evaluates given option for current question
    /// </summary>
    /// <param name="option"></param>
    /// <returns>True if option is correct</returns>
    public bool TryAnswer(int option)
    {
        if (idQuestion >= questions.Count)
            return false;

        if (option == CurrentQuestion.answer)
        {
            performance.hits++;
            return true;
        }
        else
        {
            performance.fails++;
        }
        return false;
    }
    
    /// <summary>
    /// Advances to the next question
    /// </summary>
    /// <param name="question"></param>
    /// <returns>true if next question is available</returns>
    public bool NextQuestion(out Question question)
    {
        idQuestion++;
        if (idQuestion >= questions.Count)
        {
            question = new Question();
            return false;
        }

        question = CurrentQuestion;
        return true;
    }
    
    /// <summary>
    /// Sets performance score and feeling rate
    /// </summary>
    /// <param name="stars"></param>
    public void SetFeelingRate(int stars)
    {
        performance.feeling_rate = stars;
        performance.score = performance.hits;
    }
}