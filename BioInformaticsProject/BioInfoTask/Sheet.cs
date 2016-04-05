using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfoTask
{
    public class Sheet
    {
        public string name = "SHEET";
        public String InitialResidueName;
        public int InitialRSeqNum;
        public String TerminalResidueName;
        public int TerminalRSeqNum;
        public String Line;

        public Sheet(string IRName, int IRSNum, string TRName, int TRSNum, String Ln)
        {
            try
            {

                this.InitialRSeqNum = IRSNum;
                this.InitialResidueName = IRName;
                this.TerminalResidueName = TRName;
                this.TerminalRSeqNum = TRSNum;
                this.Line = Ln;
            }
            catch (Exception e)
            {
            }

        }

    }
}
