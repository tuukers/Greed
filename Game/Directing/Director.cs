using System.Collections.Generic;
using Greed.Casting;
using Greed.Services;


namespace Greed
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private int score = 0;
        private static int COLS = 60;
        private static int ROWS = 40;
        private Random random = new Random();
        

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirectionPlayer();
            robot.SetVelocity(velocity);  
            List<Actor> gems = cast.GetActors("gem");
            List<Actor> rocks = cast.GetActors("rock");


            foreach (Gem gem in gems)
            {
                Point fallVelocity= new Point(0,5);

                int maxX = videoService.GetWidth();
                int maxY = videoService.GetHeight();
                gem.SetVelocity(fallVelocity);
                gem.MoveNext(maxX, maxY);

            } 

            foreach (Rock rock in rocks)
            {
                Point fallVelocity= new Point(0,5);

                int maxX = videoService.GetWidth();
                int maxY = videoService.GetHeight();
                rock.SetVelocity(fallVelocity);
                rock.MoveNext(maxX, maxY);
            }

        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> gems = cast.GetActors("gem");
            List<Actor> rocks = cast.GetActors("rock");

            banner.SetText($"Your Score is: {score}");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Gem gem in gems)
            {
                if (robot.GetPosition().Equals(gem.GetGemPosition()))
                {
                    score+=1;
                    int x = 15*random.Next(1, maxX/15);
                    int y = 5*random.Next(1, 4);

                    Point newposition = new Point(x,y);

                    gem.SetPosition(newposition);
                }
            } 

            foreach (Rock rock in rocks)
            {
                if (robot.GetPosition().Equals(rock.GetRockPosition()))
                {
                    score-=1;
                    int x = 15*random.Next(1, maxX/15);
                    int y = 5*random.Next(1, 4);

                    Point newposition = new Point(x,y);
                   
                    rock.SetPosition(newposition);
                }
            }
            banner.SetText($"Your Score is: {score}");  
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }
    }
}