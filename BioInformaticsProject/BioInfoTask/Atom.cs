using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfoTask
{
    public class Atom
    {
        public String Name = "ATOM";
        public int SerialNum;
        public Decimal x;
        public Decimal y;
        public Decimal z;
        public String Line;

        public Atom(int num, Decimal a, Decimal b, Decimal c, String Ln)
        {
            this.SerialNum = num;
            this.x = a;
            this.y = b;
            this.z = c;
            this.Line = Ln;
        }
    }

}
