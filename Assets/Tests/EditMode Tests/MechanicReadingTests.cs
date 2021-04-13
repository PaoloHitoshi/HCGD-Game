using NUnit.Framework;
using System;
using UnityEngine;

namespace Tests
{
    public class MechanicReadingTests
    {
        public class MechanicReaderTests
        {
            [Test]
            [TestCase("Quizz")]
            [TestCase("Collect")]
            [TestCase("Puzzle")]
            public void Existent_Mechanic_Available(string mechanic_name)
            {
                // Prepare
                Game game = new Game();
                game.mechanic_name = mechanic_name;

                //Act
                bool available = MechanicGameReader.IsMechanicAvailable(game);

                //Assert
                Assert.IsTrue(available, $"mechanic {mechanic_name} is not available in this project");
            }

            [Test]
            [TestCase("Quiz")]
            [TestCase("Platformer")]
            [TestCase("Visual Novel")]
            [TestCase("")]
            [TestCase(null)]
            public void NonExistent_Mechanic_Unavailable(string mechanic_name)
            {
                // Prepare
                Game game = new Game();
                game.mechanic_name = mechanic_name;

                //Act
                bool available = MechanicGameReader.IsMechanicAvailable(game);

                //Assert
                Assert.IsFalse(available, $"mechanic {mechanic_name} is not supposed to be available in this project");
            }
        }

        public class QuizzMechanicTests
        {
            public class Reading
            {
                private Game ReadGameFile(string fileName) => Resources.Load<GameTest>("Quizz/" + fileName).game;

                [Test]
                public void Non_Quizz_Game_Reading()
                {
                    // Prepare
                    Game game = new Game();
                    QuizzMechanic quizz = new QuizzMechanic();
                    
                    game.mechanic_name = "Anything";

                    // Act
                    TestDelegate testDelegate = () => quizz.Read(game);

                    // Assert
                    Assert.Catch<ArgumentException>(testDelegate);
                }

                [Test]
                public void Null_Components_Quizz_Reading()
                {
                    // Prepare
                    Game game = new Game();
                    QuizzMechanic quizz = new QuizzMechanic();

                    game.mechanic_name = "Quizz";

                    // Act
                    TestDelegate testDelegate = () => quizz.Read(game);

                    // Assert
                    Assert.Catch<ArgumentNullException>(testDelegate);
                }

                [Test]
                public void Zero_Question_Quizz_Reading()
                {
                    // Prepare
                    Game game = new Game();
                    QuizzMechanic quizz = new QuizzMechanic();

                    game.mechanic_name = "Quizz";
                    game.components = new Component[0];

                    // Act
                    TestDelegate testDelegate = () => quizz.Read(game);

                    // Assert
                    Assert.Catch<ArgumentOutOfRangeException>(testDelegate);
                }

                [Test]
                [TestCase("Quizz_One_Question")]
                public void One_Question_Quizz_Reading(string input_file_name)
                {
                    // Prepare
                    Game game = ReadGameFile(input_file_name);
                    QuizzMechanic quizzMechanic = new QuizzMechanic();

                    // Act
                    try
                    {
                        quizzMechanic.Read(game);
                    }
                    catch(Exception e)
                    {
                        Assert.Fail(e.Message);
                    }

                    // Assert
                    Assert.AreEqual(1, quizzMechanic.questions.Count);
                }

                [Test]
                [TestCase("Quizz_Multiple_Questions")]
                public void Multiple_Question_Quizz_Reading(string input_file_name)
                {
                    // Prepare
                    Game game = ReadGameFile(input_file_name);
                    QuizzMechanic quizzMechanic = new QuizzMechanic();

                    // Act
                    try
                    {
                        quizzMechanic.Read(game);
                    }
                    catch (Exception e)
                    {
                        Assert.Fail(e.Message);
                    }

                    // Assert
                    Assert.IsTrue(quizzMechanic.questions.Count > 1);
                }

                [Test]
                [TestCase("Quizz_Missing_Question_Content")]
                [TestCase("Quizz_Missing_option_Content")]
                [TestCase("Quizz_Missing_answer_Content")]
                public void Throws_Exception_At_Empty_Field_Content(string input_file_name)
                {
                    // Prepare
                    Game game = ReadGameFile(input_file_name);
                    QuizzMechanic quizzMechanic = new QuizzMechanic();

                    // Act
                    TestDelegate testDelegate = () => quizzMechanic.Read(game);

                    // Assert
                    Assert.Throws<ArgumentNullException>(testDelegate);
                }

                [Test]
                [TestCase("Quizz_Missing_Question_Field")]
                [TestCase("Quizz_Missing_option1_Field")]
                [TestCase("Quizz_Missing_option2_Field")]
                [TestCase("Quizz_Missing_option3_Field")]
                [TestCase("Quizz_Missing_option4_Field")]
                [TestCase("Quizz_Missing_answer_Field")]
                [TestCase("Quizz_Missing_All_Fields")]
                public void Throws_Exception_At_Missing_Field(string input_file_name)
                {

                    // Prepare
                    Game game = ReadGameFile(input_file_name);
                    QuizzMechanic quizzMechanic = new QuizzMechanic();

                    // Act
                    TestDelegate testDelegate = () => quizzMechanic.Read(game);

                    // Assert
                    Assert.Throws<IncompleteComponentException>(testDelegate);
                }

                [Test]
                [TestCase(-1)]
                [TestCase(4)]
                [TestCase(-10)]
                [TestCase(13)]
                public void Throws_Exception_At_OutOfRange_Answer(int range)
                {
                    // Prepare
                    Game game = ReadGameFile("Quizz_One_Question");
                    QuizzMechanic quizzMechanic = new QuizzMechanic();

                    Array.Find(game.components[0].fields, (x) => x.role == "answer").resource.content = range.ToString();

                    // Act
                    TestDelegate testDelegate = () => quizzMechanic.Read(game);

                    // Assert
                    Assert.Throws<ArgumentOutOfRangeException>(testDelegate);
                }

                [Test]
                [TestCase("_Qestione")]
                [TestCase("_tag")]
                [TestCase("")]
                public void Unknown_Component(string component_tag)
                {
                    // Prepare
                    Game game = ReadGameFile("Quizz_One_Question");
                    QuizzMechanic quizzMechanic = new QuizzMechanic();

                    game.components[0].tag = component_tag;

                    // Act
                    TestDelegate testDelegate = () => quizzMechanic.Read(game);

                    // Assert
                    Assert.Throws<ArgumentException>(testDelegate);
                }
            }

        }
    }
}
