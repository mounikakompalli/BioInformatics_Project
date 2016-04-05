using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfoTask
{
    public class Loop
    {
        public List<Helix> HelixList = new List<Helix>();
        public List<Atom> AtomList = new List<Atom>();
        public List<Sheet> SheetList = new List<Sheet>();

        public List<Helix> SortedHelixList = new List<Helix>();
        public List<Atom> SortedAtomList = new List<Atom>();
        public List<Sheet> SortedSheetList = new List<Sheet>();

        public List<Atom> OutputAtomList = new List<Atom>();
        public List<Sheet> OutputSheetList = new List<Sheet>();



        public void fetchOutputAtoms(int start, int end)
        {
            foreach (Atom obj in SortedAtomList)
            {
                if ((obj.SerialNum >= start) && ( obj.SerialNum <= end)  && (obj.Line.Substring(13,2).Equals("CA")))
                {
                    OutputAtomList.Add(obj);
                }
            }

        }

    }
}
