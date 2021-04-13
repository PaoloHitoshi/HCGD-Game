using NUnit.Framework;

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
                    // Act
                    // Assert
                }

                [Test]
                public void Zero_Question_Quizz_Reading()
                {

                }

                [Test]
                public void One_Question_Quizz_Reading()
                {

                }

                [Test]
                public void Multiple_Question_Quizz_Reading()
                {

                }
            }

        }
    }
}
