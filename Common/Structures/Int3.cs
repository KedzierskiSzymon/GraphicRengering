using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Int3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }


        public Int3(int x = 0, int y = 0, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
