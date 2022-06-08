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

    public class Rock: Actor
    {

        private int scoreEffect = -1;

        private Point position=new Point(0,0);
        

        public Rock()
        {

        }
        
        public Point GetRockPosition()
        {
            position=GetPosition();
            return position;
        }

        public int GetRockScoreEffect()
        {
            return scoreEffect;
        }


        

    }
}
