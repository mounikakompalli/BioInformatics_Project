using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BioInfoTask
{
    public class Helix
    {
        public String name = "HELIX";
        public String InitialResidueName;
        public int InitialRSeqNum;
        public String TerminalResidueName;
        public int TerminalRSeqNum;
        public String Line;

        public Helix(string IRName, int IRSNum, string TRName, int TRSNum, String Ln)
        {

            this.InitialResidueName = IRName;
            this.InitialRSeqNum = IRSNum;
            this.TerminalResidueName = TRName;
            this.TerminalRSeqNum = TRSNum;
            this.Line = Ln;



        }
    }
}
