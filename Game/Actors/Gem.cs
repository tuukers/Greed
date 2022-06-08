//color
//symbol
// velocity
// position x
// position y
// score effect


//methods
// change score
// fall

using System;
using Greed.Casting;

namespace Greed
{

    public class Gem: Actor
    {

        private int scoreEffect = 1;

        private Point position=new Point(0,0);
        

        public Gem()
        {

        }
        
        public Point GetGemPosition()
        {
            position=GetPosition();
            return position;
        }

        public int GetGemScoreEffect()
        {
            return scoreEffect;
        }


        

    }
}
