/*******************************************************************/
// Phil Graham   Z1690752
// Assignment 3
// Due: 10/16/14
// File: SpellCheck.cs
// Description: Contains the code for the SpellCheck class
/*******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;

namespace SpellChecker
{
    //*******************************************************************
    // Class: SpellCheck
    // description: Contains data members, constructor, and methods of the
    // SpellCheck Class
    // Uses WordList.txt
    //*******************************************************************
    public class SpellCheck
    {
        //Contains the list of words drawn from the input File
        private List<string> wordList;
        private List<WordEventArgs> weaList;
        public delegate void WEAHandler(object sender, WordEventArgs w);
        public event WEAHandler WordEvent;
        
        //*******************************************************************
        // method: SpellCheck()
        // description: Constructor
        // returns:Nothing
        //*******************************************************************
        public SpellCheck()
            : base()
        {
            //Write exception data to file
            string executableLocation = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);//Get path to Debug folder
            string listPath = Path.Combine(executableLocation, "WordList.txt");//Slap the filename on the path
            //direct file variable to WordList.txt
            var file = File.ReadAllLines(listPath);
            wordList = new List<string>(file);
            ReadWords();
            AutoResetEvent autoEvent = new AutoResetEvent(true);
            TimerCallback tcb = new TimerCallback(OnWordEventArgs);
            //The timer should raise the event after 10 seconds, then every 10 seconds
            Timer timer = new Timer(tcb, autoEvent, 10000, 10000);

        }

        //*******************************************************************
        // method: CheckSpelling()
        // description: Compares the user entered word to a list of existing words
        // returns: A string, depending on whether the input was valid
        //*******************************************************************
        public string CheckSpelling(string checkWord)
        {
            //These objects get the assembly information for the success string
            Assembly assembly = typeof(SpellCheck).Assembly;
            AssemblyName assName = assembly.GetName();

            if (checkWord == "aaa")
            {
                throw new SpellException("Word was aaa");
            }
            if (checkWord == "bbb")
            {
                int divError = 0;
                divError = 3 / divError;
            }
            if (checkWord == "ccc")
            {
                try
                {
                    int divError = 0;
                    divError = 3 / divError;
                }
                catch(DivideByZeroException div)
                {
                    throw new SpellException(this.ToString(), "Word was ccc", div);
                }
            }
            //For the length of the list
            for (int i=0; i < wordList.Count; i++)
            {
                //Compare the input against each word in the list
                if (wordList[i] == checkWord)
                {
                    //if there is a match, return this message
                    string checkOut = "Word " + checkWord + " is spelled correctly. \r\n" 
                        + assembly.FullName + "\r\n" 
                        + assName.Version.Major + "." + assName.Version.Minor;
                    return checkOut;
                }
            }

            //if there was no match return this
            return "Invalid Spelling";
        }

        //*******************************************************************
        // method: ReadWords()
        // description: Reads the dictionary information for each word from WordDefinitions.txt
        // returns: void
        //*******************************************************************
        public void ReadWords()
        {
            weaList = new List<WordEventArgs>();
            WordEventArgs wea;
            List<string> chunkList = new List<string>();
            List<string> lineList = new List<string>();

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//Get path to Debug folder
            string listPath = Path.Combine(executableLocation, "WordDefinitions.txt");//Slap the filename on the path
            //direct file variable to WordDefinitions.txt
            var file = File.ReadAllLines(listPath);
            lineList = new List<string>(file);

            for (int i = 0; i < lineList.Count; i++)
            {
                wea = new WordEventArgs();
                chunkList = (lineList[i].Split('*').ToList());//split string by asterisk delimiter

                wea.Headword = chunkList[0];
                wea.Pos1 = chunkList[1];
                wea.Pron = chunkList[2];
                wea.SemF = chunkList[3];
                wea.SocU = chunkList[4];
                wea.CR = chunkList[5];
                wea.DefNum = chunkList[6];
                wea.Pos2 = chunkList[7];
                wea.SDef = chunkList[8];

                weaList.Add(wea);
            }
        }

        //*******************************************************************
        // method: OnWordEventArgs(WordEventArgs wea)
        // description: Returns a randomly chosen WordEventArgs to DictEdit
        // returns: void
        //*******************************************************************
        protected virtual void OnWordEventArgs(Object stateInfo)
        {
            Random RNG = new Random();
            int which = RNG.Next(0, weaList.Count);
            if (WordEvent != null)
                WordEvent(this, weaList[which]);
        }
    }

    //*******************************************************************
    // Class: SpellException
    // description: Custom Exception for reporting spelling errors to the
    //                  Dictionary Editor
    //*******************************************************************
    public class SpellException : ApplicationException
    {
        public string Sender
        {
            get
            {
                return Sender;
            }
            set
            {
                Sender = value;
            }
        }

        public SpellException()
            : base()
        {
        }

        public SpellException(String s)
            : base(s)
        {
        }

        public SpellException(String s, Exception e)
            : base(s, e)
        {
        }
        
        protected SpellException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public SpellException(String sender, String s, Exception e)
            : base(s, e)
        {
            Sender = sender;
        }
    }

    //*******************************************************************
    // Class: WordEventArgs
    // description: Custom data type for relaying dictionary information
    //                  from WordDefinitions.txt to the Dictionary Editor
    //*******************************************************************
    public class WordEventArgs : System.EventArgs
    {
        public string Headword { get; set; }        
        
        public string Pos1 { get; set; }

        public string Pron { get; set; }

        public string SemF { get; set; }

        public string SocU { get; set; }

        public string DefNum { get; set; }

        public string CR { get; set; }

        public string Pos2 { get; set; }

        public string SDef { get; set; }

        public WordEventArgs() 
            : base()
        {
        }
    }
}