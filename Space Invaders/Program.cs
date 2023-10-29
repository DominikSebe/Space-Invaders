using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Space_Invaders
{
    internal class Program
    {
        static EnemyHandler enemyHandler;
        static Player player;
        static int points;
        static bool gameOver;
        static Timer gameOverTimer;

        static List<Invader> createInvaders(int rows, int columns)
        {
            List<Invader> invaders = new List<Invader>(rows * columns);
            for (int i = 0; i < rows * columns; i++)
            {
                invaders.Add(
                    new Invader(
                    ((i % rows) + 1) * 5,
                    ((i / rows) + 1) * 4,
                    new Character[6]
                    {
                        new Character('{', 0, 0),
                        new Character('@', 1, 0),
                        new Character('@', 2, 0),
                        new Character('}', 3, 0),
                        new Character('/', 0, 1),
                        new Character('\\', 3, 1)
                }));
            }

            return invaders;
        }
        static void playerAttack(object player)
        {
            Bullet bullet = new Bullet((player as Player).X + (player as Player).Width / 2, (player as Player).Y - 1, bulletUpdate);
            bullet.Start();
        }
        static void bulletUpdate(object bullet)
        {
            try
            {
                (bullet as Bullet).Cclear();
                (bullet as Bullet).Move(0, -1);

                Invader hitInvader = null;
                if (enemyHandler != null)
                    hitInvader = enemyHandler.Invaders.Find((invader) => invader.Collides(bullet));

                if (hitInvader != null)
                {
                    enemyHandler.Invaders.Remove(hitInvader);
                    hitInvader.CClear();
                    hitInvader.Dispose();
                    (bullet as Bullet).Stop();

                    points += 10;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Points: {0}", points);
                }
                (bullet as Bullet).Draw(ConsoleColor.Yellow);
            }
            catch (ArgumentOutOfRangeException) 
            {
                (bullet as Bullet).Stop();
                (bullet as Bullet).Cclear();
            }
        }
        static void checkGameOver(object player)
        {
            gameOver = enemyHandler.Invaders.Find(invader => invader.Collides(player)) != null;
            bool victory = enemyHandler.Invaders.Count == 0;

            if (gameOver || victory)
            {
                (player as Player).Dispose();
                enemyHandler.Stop();
                enemyHandler = null;
                gameOverTimer.Dispose();
                Console.Clear();

                string goText = victory ? "You Win" : "Game Over";
                string pointsText = String.Format("Points: {0}", points);
                Console.SetCursorPosition(Console.WindowWidth / 2 - goText.Length / 2, Console.WindowHeight);
                Console.WriteLine(goText);
                Console.SetCursorPosition(Console.WindowWidth / 2 - pointsText.Length / 2, Console.WindowHeight);
                Console.WriteLine(pointsText);
            }


        }

        static void Main(string[] args)
        {
            // Setup Console
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(80, 80);
            Console.SetWindowSize(80, 32);

            // Setup enemies
            enemyHandler = new EnemyHandler(createInvaders(4, 5));

            // Setup player
            player = new Player(
                Console.WindowWidth / 2,
                Console.WindowHeight * 4 / 5,
                playerAttack,
                new Character('┌', 0, 0),
                new Character('┴', 1, 0),
                new Character('┐', 2, 0),
                new Character('│', 0, 1),
                new Character('│', 2, 1)
                );

            // Set points to zero;
            points = 0;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Points: {0}", points);

            // Start timers
            gameOverTimer = new Timer(checkGameOver, player, 100, 100);
            enemyHandler.Start();

            player.Draw(ConsoleColor.Green);
            // Start Game
            while (!gameOver)
            {
                ConsoleKey pressed = Console.ReadKey().Key;
                if (!gameOver)
                {
                    player.CClear();
                    player.ProcessKey(pressed);
                    player.Draw(ConsoleColor.Green);
                }
            }

            Console.ReadKey();
        }
    }
}
