using NUnit.Framework;
using System;

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
                public void One_Question_Quizz_Reading(string input_file_name)
                {
                    Assert.Fail();
                }

                [Test]
                public void Multiple_Question_Quizz_Reading(string input_file_name)
                {
                    Assert.Fail();

                }

                [Test]
                public void Missing_Question_Content(string input_file_name)
                {

                    Assert.Fail();
                }
                
                [Test]
                public void Out_Of_Range_Answer(string input_file_name)
                {
                    Assert.Fail();

                }

                [Test]
                public void Missing_Option(string input_file_name)
                {
                    Assert.Fail();

                }

                [Test]
                [TestCase("_Qestione")]
                [TestCase("_tag")]
                [TestCase("")]
                [TestCase(null)]
                public void Unknown_Component(string component_tag)
                {
                    Assert.Fail();

                }
            }

        }
    }
}
