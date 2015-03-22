/*******************************************************************/
// Phil Graham   Z1690752
// Assignment 3
// Due: 10/16/14
// File: DictEdit.cs
// Description: Contains the code for the Dictionary Editor Form
/*******************************************************************/
using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using SemanticSocialAssister;
using SpellChecker;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictionaryEditor
{
    //*******************************************************************
    // Class: DictEdit
    // description: Contains the components and main routine for the application
    // Uses the SpellCheck Class
    //*******************************************************************
    public partial class DictEdit : Form
    {
        //Timer Object
        private System.Windows.Forms.Timer Clock;
        //Delegate Object for the Clock
        delegate void Deleg8(object sender, EventArgs e);
        //Components created from the Tool Box
        private Label label1;
        private Label label2;
        private ComboBox pOSBox;
        private TextBox headWordBox;
        private TextBox PronBox;
        private Button SpellCheckButt;
        private Button AddDefButt;
        private Label label4;
        private ListBox semanticBox;
        private Label label5;
        private ListBox socialBox;
        private Label label6;
        private TextBox crossBox;
        private Button addEntryButt;
        private Button searchEdButt;
        private Button clearButt;
        private Button seeAllButt;
        private Button XButt;
        private TextBox ClockBox;        
        private System.ComponentModel.IContainer components;
        private Button WordsOnOffButt;
        private Label label3;
        //create instance of SpellChecker class
        SpellCheck sc = new SpellCheck();

        //*******************************************************************
        // method: DictEdit()
        // description: Constructor
        // returns:Nothing
        //*******************************************************************
        public DictEdit()
        {
            InitializeComponent();//Turn on all the things
            CheckForIllegalCrossThreadCalls = false;//Squelch the InvalidOperationError

            //Call the methods for filling the list boxes
            semanticBox.DataSource = getSemantics();
            socialBox.DataSource = getSocials();
        }

        //*******************************************************************
        // method: Main Method
        // description: Displays the window, runs the application
        // returns:void
        //*******************************************************************
        public static void Main()
        {
            DictEdit f = new DictEdit();
            Deleg8 Del = f.ClockTick;
            f.Show();
            f.Text = "Dictionary Editor";
            f.Paint += new PaintEventHandler(f_Paint);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(f);
        }

        //*******************************************************************
        // method: f_Paint()
        // description: Used above in f.Paint
        // returns:void
        //*******************************************************************
        static void f_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
        }

        //*******************************************************************
        // method: InitializeComponent()
        // description: Initializes all the buttons and boxes
        // returns:void
        //*******************************************************************
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pOSBox = new System.Windows.Forms.ComboBox();
            this.headWordBox = new System.Windows.Forms.TextBox();
            this.PronBox = new System.Windows.Forms.TextBox();
            this.SpellCheckButt = new System.Windows.Forms.Button();
            this.AddDefButt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.semanticBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.socialBox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.crossBox = new System.Windows.Forms.TextBox();
            this.addEntryButt = new System.Windows.Forms.Button();
            this.searchEdButt = new System.Windows.Forms.Button();
            this.clearButt = new System.Windows.Forms.Button();
            this.seeAllButt = new System.Windows.Forms.Button();
            this.XButt = new System.Windows.Forms.Button();
            this.ClockBox = new System.Windows.Forms.TextBox();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            this.WordsOnOffButt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Headword";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "POS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pronounciation";
            // 
            // pOSBox
            // 
            this.pOSBox.FormattingEnabled = true;
            this.pOSBox.Items.AddRange(new object[] {
            "adj.",
            "adv.",
            "conj.",
            "ideo.",
            "prep.",
            "pro.",
            "n.",
            "v."});
            this.pOSBox.Location = new System.Drawing.Point(131, 114);
            this.pOSBox.Name = "pOSBox";
            this.pOSBox.Size = new System.Drawing.Size(160, 21);
            this.pOSBox.TabIndex = 3;
            // 
            // headWordBox
            // 
            this.headWordBox.Location = new System.Drawing.Point(131, 88);
            this.headWordBox.Name = "headWordBox";
            this.headWordBox.Size = new System.Drawing.Size(160, 20);
            this.headWordBox.TabIndex = 4;
            // 
            // PronBox
            // 
            this.PronBox.Location = new System.Drawing.Point(131, 143);
            this.PronBox.Name = "PronBox";
            this.PronBox.Size = new System.Drawing.Size(160, 20);
            this.PronBox.TabIndex = 5;
            // 
            // SpellCheckButt
            // 
            this.SpellCheckButt.Location = new System.Drawing.Point(325, 88);
            this.SpellCheckButt.Name = "SpellCheckButt";
            this.SpellCheckButt.Size = new System.Drawing.Size(105, 23);
            this.SpellCheckButt.TabIndex = 6;
            this.SpellCheckButt.Text = "Spell Check";
            this.SpellCheckButt.UseVisualStyleBackColor = true;
            this.SpellCheckButt.Click += new System.EventHandler(this.SpellCheckButt_Click);
            // 
            // AddDefButt
            // 
            this.AddDefButt.Location = new System.Drawing.Point(325, 115);
            this.AddDefButt.Name = "AddDefButt";
            this.AddDefButt.Size = new System.Drawing.Size(105, 23);
            this.AddDefButt.TabIndex = 7;
            this.AddDefButt.Text = "Add Definition";
            this.AddDefButt.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Semantic Fields";
            // 
            // semanticBox
            // 
            this.semanticBox.FormattingEnabled = true;
            this.semanticBox.Location = new System.Drawing.Point(138, 220);
            this.semanticBox.Name = "semanticBox";
            this.semanticBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.semanticBox.Size = new System.Drawing.Size(160, 95);
            this.semanticBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Social Usage";
            // 
            // socialBox
            // 
            this.socialBox.FormattingEnabled = true;
            this.socialBox.Location = new System.Drawing.Point(385, 221);
            this.socialBox.Name = "socialBox";
            this.socialBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.socialBox.Size = new System.Drawing.Size(175, 95);
            this.socialBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 347);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Cross References";
            // 
            // crossBox
            // 
            this.crossBox.Location = new System.Drawing.Point(138, 347);
            this.crossBox.Multiline = true;
            this.crossBox.Name = "crossBox";
            this.crossBox.Size = new System.Drawing.Size(442, 68);
            this.crossBox.TabIndex = 13;
            // 
            // addEntryButt
            // 
            this.addEntryButt.Location = new System.Drawing.Point(74, 452);
            this.addEntryButt.Name = "addEntryButt";
            this.addEntryButt.Size = new System.Drawing.Size(75, 23);
            this.addEntryButt.TabIndex = 14;
            this.addEntryButt.Text = "Add Entry";
            this.addEntryButt.UseVisualStyleBackColor = true;
            // 
            // searchEdButt
            // 
            this.searchEdButt.Location = new System.Drawing.Point(177, 452);
            this.searchEdButt.Name = "searchEdButt";
            this.searchEdButt.Size = new System.Drawing.Size(75, 23);
            this.searchEdButt.TabIndex = 15;
            this.searchEdButt.Text = "Search/Edit";
            this.searchEdButt.UseVisualStyleBackColor = true;
            // 
            // clearButt
            // 
            this.clearButt.Location = new System.Drawing.Point(294, 452);
            this.clearButt.Name = "clearButt";
            this.clearButt.Size = new System.Drawing.Size(75, 23);
            this.clearButt.TabIndex = 16;
            this.clearButt.Text = "Clear";
            this.clearButt.UseVisualStyleBackColor = true;
            // 
            // seeAllButt
            // 
            this.seeAllButt.Location = new System.Drawing.Point(403, 452);
            this.seeAllButt.Name = "seeAllButt";
            this.seeAllButt.Size = new System.Drawing.Size(82, 23);
            this.seeAllButt.TabIndex = 17;
            this.seeAllButt.Text = "See All Words";
            this.seeAllButt.UseVisualStyleBackColor = true;
            // 
            // XButt
            // 
            this.XButt.Location = new System.Drawing.Point(510, 452);
            this.XButt.Name = "XButt";
            this.XButt.Size = new System.Drawing.Size(75, 23);
            this.XButt.TabIndex = 18;
            this.XButt.Text = "Exit";
            this.XButt.UseVisualStyleBackColor = true;
            this.XButt.Click += new System.EventHandler(this.XButt_Click);
            // 
            // ClockBox
            // 
            this.ClockBox.Location = new System.Drawing.Point(559, 29);
            this.ClockBox.Name = "ClockBox";
            this.ClockBox.Size = new System.Drawing.Size(49, 20);
            this.ClockBox.TabIndex = 19;
            // 
            // Clock
            // 
            this.Clock.Enabled = true;
            this.Clock.Interval = 1000;
            this.Clock.Tick += new System.EventHandler(this.ClockTick);
            // 
            // WordsOnOffButt
            // 
            this.WordsOnOffButt.Location = new System.Drawing.Point(325, 139);
            this.WordsOnOffButt.Name = "WordsOnOffButt";
            this.WordsOnOffButt.Size = new System.Drawing.Size(105, 23);
            this.WordsOnOffButt.TabIndex = 20;
            this.WordsOnOffButt.Text = "Turn Words On";
            this.WordsOnOffButt.UseVisualStyleBackColor = true;
            this.WordsOnOffButt.Click += new System.EventHandler(this.WordsOnOffButt_Click);
            // 
            // DictEdit
            // 
            this.ClientSize = new System.Drawing.Size(661, 489);
            this.Controls.Add(this.WordsOnOffButt);
            this.Controls.Add(this.ClockBox);
            this.Controls.Add(this.XButt);
            this.Controls.Add(this.seeAllButt);
            this.Controls.Add(this.clearButt);
            this.Controls.Add(this.searchEdButt);
            this.Controls.Add(this.addEntryButt);
            this.Controls.Add(this.crossBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.socialBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.semanticBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AddDefButt);
            this.Controls.Add(this.SpellCheckButt);
            this.Controls.Add(this.PronBox);
            this.Controls.Add(this.headWordBox);
            this.Controls.Add(this.pOSBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DictEdit";
            this.Text = "Dictionary Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //*******************************************************************
        // method: OnPaint(PaintEventArgs e)
        // description: Overriden OnPaint method allows for the big blue tital
        // returns:void
        //*******************************************************************
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Font stringFont = new Font(FontFamily.GenericSansSerif, 20);
            SizeF length = g.MeasureString("Dictionary Editor", stringFont);
            //This line of code controls the content, style, size, color, and location of the title
            g.DrawString("Dictionary Editor", stringFont, Brushes.CornflowerBlue, ClientSize.Width - (ClientSize.Width / 2) - (length.Width / 2), 12);

            int BORDER_SIZE = 10;
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                  Color.Lavender, BORDER_SIZE, ButtonBorderStyle.Inset,
                                  Color.Lavender, BORDER_SIZE, ButtonBorderStyle.Inset,
                                  Color.CornflowerBlue, BORDER_SIZE, ButtonBorderStyle.Inset,
                                  Color.CornflowerBlue, BORDER_SIZE, ButtonBorderStyle.Inset);
        }

        //*******************************************************************
        // method: getSemantics()
        // description: returns an array of strings containing the words for SemanticsBox
        // returns:String[]
        //*******************************************************************
        private String[] getSemantics()
        {
            SemanticSocialAssister.SemanticSocial SemSoc = new SemanticSocialAssister.SemanticSocial();
            SemanticSocialAssister.ListBoxStrings LBS;
            int count = SemSoc.ListCount;
            String[] SemList = new String[count];
            for (int i = 0; i < count; i++)
            {
                LBS = SemSoc[i];
                SemList[i] = LBS.Semantic;
            }
            return SemList;
        }

        //*******************************************************************
        // method: getSocials()
        // description: returns an array of strings containing the words for SocialBox
        // returns:String[]
        //*******************************************************************
        private String[] getSocials()
        {
            SemanticSocialAssister.SemanticSocial SemSoc = new SemanticSocialAssister.SemanticSocial();
            SemanticSocialAssister.ListBoxStrings LBS;
            int count = SemSoc.ListCount;
            String[] SocList = new String[count];
            for (int i = 0; i < count; i++)
            {
                LBS = SemSoc[i];
                SocList[i] = LBS.Social;
            }
            return SocList;
        }

        //*******************************************************************
        // method: Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        // description: Custom Exception for DictEdit class
        //              kills the running process and throws up a message box
        // returns:void
        //*******************************************************************
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            DialogResult result;
            DateTime dt = new DateTime();
            result = MessageBox.Show("Unhandled Thread Exception", "An error has occurred causing the process to end.\n Would you like to restart Dictionary Editor?",
                MessageBoxButtons.YesNo);

            // Starts new process and kills old one when the user clicks Yes. 
            if (result == DialogResult.Yes)
            {
                Process.Start(Process.GetCurrentProcess().ProcessName);
                Process.GetCurrentProcess().Kill();
            }

            //Write exception data to file
            string executableLocation = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);//Get path to Debug folder
            string logPath = Path.Combine(executableLocation, "ErrorLog.txt");//Slap the filename on the path
            //Make the filestream object
            using (FileStream errorFile = new FileStream(logPath, FileMode.Append, FileAccess.Write))
            //Make the streamwriter
            using (StreamWriter sw = new StreamWriter(errorFile))
            {
                sw.WriteLine(e.ToString());
                sw.WriteLine("Date: " + dt.Date.ToString());
                sw.WriteLine("Time: " + dt.TimeOfDay.ToString());
                sw.WriteLine("----------------------------------------------------------");
            }
        }

        //*******************************************************************
        // method: ClockTick(object sender, EventArgs e)
        // description: Custom Event for the Clock, sets the text in HH:mm:ss format
        // returns:void
        //*******************************************************************
        public void ClockTick(object sender, EventArgs e)
        {
            ClockBox.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //*******************************************************************
        // method: SpellCheckButt_Click()
        // description: Calls the CheckSpelling() method of the SpellChecker class
        // returns:void
        //*******************************************************************
        private void SpellCheckButt_Click(object sender, EventArgs e)
        {
            String lilWord;
            String message;            
            //Get theWord from the text Box
            String theWord = headWordBox.Text;
            //Make it all lowercase
            lilWord = theWord.ToLower();
            try
            {
                //Check the Spelling
                message = sc.CheckSpelling(lilWord);
                //Message Box with output string
                MessageBox.Show(message);
            }
            catch(SpellChecker.SpellException SE)
            {
                String inMessage = (SE.InnerException != null)
                                    ? SE.InnerException.Message
                                    : "Spell Exception";
                MessageBox.Show(inMessage, SE.Message + SE.Sender);
            }
        }

        //*******************************************************************
        // method: XButt_Click()
        // description: Closes the application when "Exit" button is clicked
        // returns:void
        //*******************************************************************
        private void XButt_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //*******************************************************************
        // method: WordsOnOffButt_Click(object sender, EventArgs e)
        // description: Changes the text on the button and starts/stops 
        //                  the parade of words
        // returns:void
        //*******************************************************************
        private void WordsOnOffButt_Click(object sender, EventArgs e)
        {    
            if (WordsOnOffButt.Text == "Turn Words On")
            {
                WordsOnOffButt.Text = "Turn Words Off";
                sc.WordEvent += sc_WEA;
            }
            else
            if(WordsOnOffButt.Text == "Turn Words Off")
            {
                WordsOnOffButt.Text = "Turn Words On";
                sc.WordEvent -= sc_WEA;
            }
        }

        //*******************************************************************
        // method: sc_WEA(Object sender, WordEventArgs wea)
        // description: This message used to handle the WordEventArgs object
        //                  sent by the SpellChecker class
        // returns:void
        //*******************************************************************
        private void sc_WEA(Object sender, WordEventArgs wea)
        {
            //Flash the button red for 1 second
            WordsOnOffButt.BackColor = Color.Red;
            System.Threading.Thread.Sleep(1000);
            WordsOnOffButt.BackColor = default(Color);

            //Clear the selected items out of the big boxes
            semanticBox.SelectedIndex = -1;
            socialBox.SelectedIndex = -1;

            //These lists hold the Semantic and Social items to highlight
            List <string> semList;
            List <string> socList;
           
            //Set the easy boxes, easily
            headWordBox.Text = wea.Headword;
            pOSBox.Text = wea.Pos1;
            PronBox.Text = wea.Pron;
            crossBox.Text = wea.CR;

            //split strings by comma delimiters
            semList = new List<string>(wea.SemF.Split(',').ToList());
            socList = new List<string>(wea.SocU.Split(',').ToList());
            


            for (int i = 0; i < semanticBox.Items.Count; i++)//For every item in semanticBox
                for(int x = 0; x < semList.Count; x++)//Compare it to the items in semList
                    if (semanticBox.Items.Contains(semList[x]))//If there is a match
                        semanticBox.SelectedItem = semList[x];//then select that item

            for (int i = 0; i < socialBox.Items.Count; i++)//For every item in socialBox
                for (int x = 0; x < socList.Count; x++)//Compare it to the items in socList
                    if (socialBox.Items.Contains(socList[x]))//If there is a match
                        socialBox.SelectedItem = socList[x];//then select that item

            

        }
    }
}
