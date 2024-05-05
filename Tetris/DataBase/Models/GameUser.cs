using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class GameUser
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Annonymous";
        public string Email { get; set; } = "None Mail";
        public string Password { get; set; }
        public int Points { get; set; }
        public GameLevel Level { get; set; }
        public string UsingProduct { get; set; } = "Regular";

    }
}
