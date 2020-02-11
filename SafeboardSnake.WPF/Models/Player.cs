using System;
using System.Collections.Generic;
using System.Text;
using SafeboardSnake.Core.Models;

namespace SafeboardSnake.WPF.Models
{
    public class Player
    {
        public string Name { get; set; }
        public bool IsSpawnProtected { get; set; }
        public ICollection<Cell> Snake { get; set; }
    }
}
