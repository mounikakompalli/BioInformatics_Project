using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfoTask
{
    public class ListOfRequiredAtomData
    {
        public List<Atom> listOfAtomsData { get; set; }

        public ListOfRequiredAtomData(List<Atom> abc)
        {


            listOfAtomsData = abc;

        }
    }
}
