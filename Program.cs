using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Greed.Casting;
using Greed;
using Greed.Services;


namespace Greed
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed";
        private static Color WHITE = new Color(255, 255, 255);
        private static int Default_Gems = 15;
        private static int Default_Rocks = 15;
        

        static void Main(string[] args)
        {
                        Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.SetText("");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the robot
            Actor robot = new Actor();
            robot.SetText("#");
            robot.SetFontSize(FONT_SIZE);
            robot.SetColor(WHITE);
            robot.SetPosition(new Point(MAX_X / 2, MAX_Y - 15));
            cast.AddActor("robot", robot);


            //Create Gems
            Random random = new Random();
            for (int i = 0; i < Default_Gems; i++)
            {
                string text = "*";
                Point fallVelocity= new Point(0,5);

                int x = random.Next(1, MAX_X);
                int y = 5*random.Next(1, 4);
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Gem gem = new Gem();
                gem.SetText(text);
                gem.SetFontSize(FONT_SIZE);
                gem.SetColor(color);
                gem.SetPosition(position);
                cast.AddActor("gem", gem);

                gem.SetVelocity(fallVelocity);
            }

            //Create Rocks
            for (int i = 0; i < Default_Rocks; i++)
            {
                string text = "O";
                Point fallVelocity= new Point(0,5);


                int x = random.Next(1, MAX_X);
                int y = 5*random.Next(1,4);
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Rock rock = new Rock();
                rock.SetText(text);
                rock.SetFontSize(FONT_SIZE);
                rock.SetColor(color);
                rock.SetPosition(position);
                cast.AddActor("rock", rock);

                rock.SetVelocity(fallVelocity);
            }
            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
        

    }
}