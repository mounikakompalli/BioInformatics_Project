using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfoTask
{
    public class Program
    {
        List<Helix> HelixList = new List<Helix>();
        List<Atom> AtomList = new List<Atom>();
        List<Sheet> SheetList = new List<Sheet>();

        List<Helix> SortedHelixList = new List<Helix>();
        List<Atom> SortedAtomList = new List<Atom>();
        List<Sheet> SortedSheetList = new List<Sheet>();

        List<Atom> OutputAtomList = new List<Atom>();
        List<Sheet> OutputSheetList = new List<Sheet>();

        List<Atom> ListOutputAtom = new List<Atom>();
        int counterVariable = 0;
        static List<ListOfRequiredAtomData> listOfAtomGroupsEqDiff = new List<ListOfRequiredAtomData>();
        List<Loop> loopList = new List<Loop>();

        List<string> SheetReferForCA1 = new List<string>();
        List<string> SheetReferForCA2 = new List<string>();
        List<Atom> ca1Atoms = new List<Atom>();
        List<Atom> ca2Atoms = new List<Atom>();
        static void Main(string[] args)
        {
            Program p = new Program();
            string[] listOfFiles;


            Console.WriteLine("Do you wish to align? If YES Please enter 'y', else enter 'n'");
            string align = Console.ReadLine();
            if (align.Equals("y"))
            {
                Console.WriteLine("How many input files do you wish to enter ?");
                int numberOfFiles = Convert.ToInt16(Console.ReadLine());
                listOfFiles = new string[numberOfFiles];

                for (int i = 0; i < numberOfFiles; i++)
                {
                    Console.WriteLine("Enter file name : " + (i + 1));
                    listOfFiles[i] = Console.ReadLine();
                }

                Console.WriteLine("Please enter the difference factor");
                int alignDiff = Convert.ToInt16(Console.ReadLine());


                foreach (string currFile in listOfFiles)
                {
                    Console.WriteLine("Processing " + currFile);
                    string[] lines = System.IO.File.ReadAllLines(currFile);
                    Loop loopOfCurrFile = new Loop();

                    foreach (string line in lines)
                    {
                        string select = "";
                        if (line.StartsWith("HELIX"))
                            select = "HELIX";
                        else if (line.StartsWith("SHEET"))
                            select = "SHEET";
                        else if (line.StartsWith("ATOM"))
                            select = "ATOM";

                        if (!select.Equals(""))
                            switch (select)
                            {
                                case "HELIX":
                                    Helix Helix_obj = new Helix(line.Substring(15, 3), Convert.ToInt32(line.Substring(21, 4)),
                                                          line.Substring(27, 3), Convert.ToInt32(line.Substring(33, 4)), line);
                                    loopOfCurrFile.HelixList.Add(Helix_obj);
                                    break;
                                case "ATOM":
                                    Atom Atom_obj = new Atom(Convert.ToInt32(line.Substring(22, 4)), Convert.ToDecimal(line.Substring(30, 8)),
                                                             Convert.ToDecimal(line.Substring(38, 8)), Convert.ToDecimal(line.Substring(46, 8)), line);
                                    loopOfCurrFile.AtomList.Add(Atom_obj);
                                    break;
                                case "SHEET":
                                    Sheet Sheet_obj = new Sheet(line.Substring(17, 3), Convert.ToInt32(line.Substring(22, 4)),
                                                          line.Substring(28, 3), Convert.ToInt32(line.Substring(33, 4)), line);
                                    loopOfCurrFile.SheetList.Add(Sheet_obj);
                                    break;
                            }
                    }
                    //p.loopList.Add(loopOfCurrFile);

                    loopOfCurrFile.SortedSheetList = loopOfCurrFile.SheetList.OrderBy(o => o.InitialRSeqNum).ToList();
                    loopOfCurrFile.SortedHelixList = loopOfCurrFile.HelixList.OrderBy(o => o.InitialRSeqNum).ToList();
                    loopOfCurrFile.SortedAtomList = loopOfCurrFile.AtomList.OrderBy(o => o.SerialNum).ToList();
                    int counter = 0;
                    int c = 0;

                    foreach (Sheet ob in loopOfCurrFile.SortedSheetList)
                    {

                        counter += 1;
                        if (counter < loopOfCurrFile.SortedSheetList.Count)
                        {

                            int ConseqDiff = Math.Abs(ob.TerminalRSeqNum - loopOfCurrFile.SortedSheetList[counter].InitialRSeqNum);
                            if (ConseqDiff - 1 == alignDiff)
                            {
                                c++;

                                p.SheetReferForCA1.Add(ob.Line);
                                p.SheetReferForCA2.Add(loopOfCurrFile.SortedSheetList[counter].Line);

                               // changed to previous logic
                                loopOfCurrFile.fetchOutputAtoms(ob.InitialRSeqNum, loopOfCurrFile.SortedSheetList[counter].TerminalRSeqNum);
                                
                                List<Atom> tempOutAtoms = new List<Atom>();

                                tempOutAtoms = loopOfCurrFile.OutputAtomList.ToList();

                                // make sure there is no repetition


                                ListOfRequiredAtomData obj007 = new ListOfRequiredAtomData(tempOutAtoms);
                                listOfAtomGroupsEqDiff.Insert(p.counterVariable, obj007);
                                p.counterVariable++;
                                loopOfCurrFile.OutputAtomList.Clear();
                                // check if it's now empty

                            }
                        }
                    }
                    p.loopList.Add(loopOfCurrFile);
                }


                p.rearrangeAtoms();

                Console.WriteLine("Done processing. Please verify 'TheOutput.pdb' file located inside bin/debug folder. Hit any key and press enter to exit the program ");
                Console.ReadLine();
            }


            else
            {

                Console.WriteLine("Please enter a file name");
                String InputFileName = Console.ReadLine();

                Console.WriteLine("Input File Name is " + InputFileName);
                string[] lines = System.IO.File.ReadAllLines(InputFileName);


                foreach (string line in lines)
                {
                    string select = "";

                    if (line.StartsWith("HELIX"))
                        select = "HELIX";
                    else if (line.StartsWith("SHEET"))
                        select = "SHEET";
                    else if (line.StartsWith("ATOM"))
                        select = "ATOM";

                    if (!select.Equals(""))
                        switch (select)
                        {
                            case "HELIX":
                                Helix Helix_obj = new Helix(line.Substring(15, 3), Convert.ToInt32(line.Substring(21, 4)),
                                                      line.Substring(27, 3), Convert.ToInt32(line.Substring(33, 4)), line);
                                p.HelixList.Add(Helix_obj);
                                break;
                            case "ATOM":
                                Atom Atom_obj = new Atom(Convert.ToInt32(line.Substring(22, 4)), Convert.ToDecimal(line.Substring(30, 8)),
                                                         Convert.ToDecimal(line.Substring(38, 8)), Convert.ToDecimal(line.Substring(46, 8)), line);
                                p.AtomList.Add(Atom_obj);
                                break;
                            case "SHEET":
                                Sheet Sheet_obj = new Sheet(line.Substring(17, 3), Convert.ToInt32(line.Substring(22, 4)),
                                                      line.Substring(28, 3), Convert.ToInt32(line.Substring(33, 4)), line);
                                p.SheetList.Add(Sheet_obj);
                                break;
                        }
                }

                p.SortedSheetList = p.SheetList.OrderBy(o => o.InitialRSeqNum).ToList();
                p.SortedHelixList = p.HelixList.OrderBy(o => o.InitialRSeqNum).ToList();
                p.SortedAtomList = p.AtomList.OrderBy(o => o.SerialNum).ToList();

                Console.WriteLine("Please enter the difference");
                int Difference = Convert.ToInt32(Console.ReadLine());
                int counter = 0;
                int c = 0;

                foreach (Sheet ob in p.SortedSheetList)
                {

                    counter += 1;
                    if (counter < p.SortedSheetList.Count)
                    {

                        int ConseqDiff = Math.Abs(ob.TerminalRSeqNum - p.SortedSheetList[counter].InitialRSeqNum);
                        if (ConseqDiff - 1 == Difference)
                        {
                            c++;
                            p.fetchOutputAtoms(ob.InitialRSeqNum, p.SortedSheetList[counter].TerminalRSeqNum);
                            //p.SheetInfo(ob.InitialRSeqNum, p.SortedSheetList[counter].TerminalRSeqNum);


                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(InputFileName.Substring(0, InputFileName.Length - 3) + '_' + p.OutputAtomList[0].Line.Substring(14, 4).Trim() + ob.InitialRSeqNum.ToString() + ".pdb"))
                            {

                                foreach (Sheet outSheet in p.SortedSheetList)
                                {
                                    file.WriteLine(outSheet.Line);
                                }
                                foreach (Atom outAtom in p.OutputAtomList)
                                {
                                    file.WriteLine(outAtom.Line);
                                }
                                p.OutputAtomList.Clear();
                                p.OutputSheetList.Clear();

                            }
                        }
                    }
                }
                if (c == 0)
                    Console.WriteLine("No match");
                if (c > 0)
                    Console.WriteLine("Match found check the Debug folder");

                Console.Read();
            }
        }

        public void rearrangeAtoms()
        {

            Console.WriteLine("Rearranging Atoms now.");
            Console.WriteLine(listOfAtomGroupsEqDiff.Count);

            for (int i = 0; i < this.SheetReferForCA1.Count; i++)
            {

                int terNum = Convert.ToInt32(SheetReferForCA1[i].Substring(33, 4));
                int intNum = Convert.ToInt32(SheetReferForCA2[i].Substring(22, 4));

                
                foreach (Atom getAtom in listOfAtomGroupsEqDiff[i].listOfAtomsData)
                {
                    if (getAtom.Line.Substring(13,2).Equals("CA") && (getAtom.SerialNum == terNum))
                    {
                        this.ca1Atoms.Add(getAtom);
                        break;
                    }
                }
                foreach (Atom getAtom in listOfAtomGroupsEqDiff[i].listOfAtomsData)
                {
                    if (getAtom.Line.Substring(13, 2).Equals("CA") && (getAtom.SerialNum == intNum))
                    {
                        this.ca2Atoms.Add(getAtom);
                        break;
                    }
                }

            }

            decimal baseCAx = ca1Atoms[0].x;
            decimal baseCAy = ca1Atoms[0].y;
            decimal baseCAz = ca1Atoms[0].z;

            for (int i = 1; i < listOfAtomGroupsEqDiff.Count; i++)
            {
                decimal xdist = baseCAx - ca1Atoms[i].x;
                decimal ydist = baseCAy - ca1Atoms[i].y;
                decimal zdist = baseCAz - ca1Atoms[i].z;

                for (int z = 0; z < listOfAtomGroupsEqDiff[i].listOfAtomsData.Count; z++)
                {
                    int terNum = Convert.ToInt32(SheetReferForCA1[i].Substring(33, 4));

                    if (listOfAtomGroupsEqDiff[i].listOfAtomsData[z].Line.Substring(13, 2).Equals("CA") && listOfAtomGroupsEqDiff[i].listOfAtomsData[z].SerialNum == terNum)
                    {
                        //listOfAtomGroupsEqDiff[i].listOfAtomsData[z].x = baseCAx;
                        //listOfAtomGroupsEqDiff[i].listOfAtomsData[z].y = baseCAy;
                        //listOfAtomGroupsEqDiff[i].listOfAtomsData[z].z = baseCAz;

                        string line = listOfAtomGroupsEqDiff[i].listOfAtomsData[z].Line;

                        

                       line =  line.Replace(listOfAtomGroupsEqDiff[i].listOfAtomsData[z].x.ToString(), baseCAx.ToString());
                       line =  line.Replace(listOfAtomGroupsEqDiff[i].listOfAtomsData[z].y.ToString(), baseCAy.ToString());
                       line =  line.Replace(listOfAtomGroupsEqDiff[i].listOfAtomsData[z].z.ToString(), baseCAz.ToString());

                        listOfAtomGroupsEqDiff[i].listOfAtomsData[z].Line = line;
                    }
                    else
                    {
                        //listOfAtomGroupsEqDiff[i].listOfAtomsData[z].x + xdist;
                        //listOfAtomGroupsEqDiff[i].listOfAtomsData[z].y + ydist;
                        //listOfAtomGroupsEqDiff[i].listOfAtomsData[z].z + zdist;


                        string line = listOfAtomGroupsEqDiff[i].listOfAtomsData[z].Line;

                       line =  line.Replace(listOfAtomGroupsEqDiff[i].listOfAtomsData[z].x.ToString(), (listOfAtomGroupsEqDiff[i].listOfAtomsData[z].x + xdist).ToString());
                       line =  line.Replace(listOfAtomGroupsEqDiff[i].listOfAtomsData[z].y.ToString(), (listOfAtomGroupsEqDiff[i].listOfAtomsData[z].y + ydist).ToString());
                       line =  line.Replace(listOfAtomGroupsEqDiff[i].listOfAtomsData[z].z.ToString(), (listOfAtomGroupsEqDiff[i].listOfAtomsData[z].z + zdist).ToString());

                        listOfAtomGroupsEqDiff[i].listOfAtomsData[z].Line = line;

                    }
                }
            }
            System.IO.File.WriteAllText("TheOutput.pdb", string.Empty);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("TheOutput.pdb"))
            {

                for (int i = 0; i < listOfAtomGroupsEqDiff.Count; i++)
                {
                    ListOfRequiredAtomData abc = listOfAtomGroupsEqDiff[i];

                    for (int x = 0; x < abc.listOfAtomsData.Count; x++)
                    {
                        file.WriteLine(abc.listOfAtomsData[x].Line);
                    }
                }



                for (int i = 0; i < listOfAtomGroupsEqDiff.Count; i++)
                {
                    ListOfRequiredAtomData abc = listOfAtomGroupsEqDiff[0];
                    for (int x = 0; x < abc.listOfAtomsData.Count; x++)
                    {
                        file.WriteLine(abc.listOfAtomsData[x].Line);
                    }
                }
            }
        }

        public void fetchOutputAtoms(int start, int end)
        {
            foreach (Atom obj in SortedAtomList)
            {
                if (obj.SerialNum >= start && obj.SerialNum <= end)
                {
                    OutputAtomList.Add(obj);
                }
            }
            OutputSheetList = SortedSheetList.ToList();
            foreach (Sheet obj in SortedSheetList)
            {
                if (obj.InitialRSeqNum >= start && obj.TerminalRSeqNum <= end)
                {
                    int conflict = 0;
                    foreach (Helix ob in SortedHelixList)
                    {
                        if (obj.InitialRSeqNum <= ob.InitialRSeqNum || obj.TerminalRSeqNum >= ob.TerminalRSeqNum)
                        {
                            conflict = 1;
                        }
                    }
                    if (conflict == 0)
                    {
                        OutputSheetList.Add(obj);
                    }
                }
            }
        }
      
    }
}
