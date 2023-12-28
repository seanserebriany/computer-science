using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Models
{
    public class Level
    {
        public int Id { get; set; }
        public int LevelNumber { get; set; }
        public double BallSpeed { get; set; }
        public int BarWidth { get; set; }
        public int CountJellyGreen { get; set; }
        public int CountJellyYellow { get; set; }
        public int CountJellyPurple { get; set; }
    }
}
