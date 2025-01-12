using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static int width = 20, height = 10;
    static List<(int x, int y)> snake = new List<(int, int)> { (5, 5) };
    static (int x, int y) direction = (1, 0);
    static (int x, int y) food = (new Random().Next(1, width + 1), new Random().Next(1, height + 1));
    static bool gameOver = false;

    static void ClearScreen()
    {
        Console.Clear();
    }

    static void Draw()
    {
        ClearScreen();
        for (int y = 1; y <= height; y++)
        {
            for (int x = 1; x <= width; x++)
            {
                if (snake.Contains((x, y)))
                    Console.Write("O");
                else if (food == (x, y))
                    Console.Write("X");
                else
                    Console.Write(".");
            }
            Console.WriteLine();
        }
    }

    static void Update()
    {
        var newHead = (snake[0].x + direction.x, snake[0].y + direction.y);

        if (newHead.x < 1 || newHead.x > width || newHead.y < 1 || newHead.y > height || snake.Contains(newHead))
        {
            gameOver = true;
            return;
        }

        snake.Insert(0, newHead);

        if (newHead == food)
        {
            food = (new Random().Next(1, width + 1), new Random().Next(1, height + 1));
        }
        else
        {
            snake.RemoveAt(snake.Count - 1);
        }
    }

    static void ChangeDirection((int x, int y) newDirection)
    {
        if (newDirection.x == -direction.x && newDirection.y == -direction.y) return;
        direction = newDirection;
    }

    static void Main()
    {
        while (!gameOver)
        {
            Draw();
            Update();

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.W: ChangeDirection((0, -1)); break;
                    case ConsoleKey.S: ChangeDirection((0, 1)); break;
                    case ConsoleKey.A: ChangeDirection((-1, 0)); break;
                    case ConsoleKey.D: ChangeDirection((1, 0)); break;
                }
            }

            Thread.Sleep(100);
        }

        Console.WriteLine("Game Over!");
    }
}
