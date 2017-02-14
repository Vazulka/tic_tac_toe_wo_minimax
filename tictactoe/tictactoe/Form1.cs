using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe
{

    public partial class Form1 : Form
    {

        static bool endOfTheGame = false;
        static string xcollection = "";
        static string ocollection = "";
        static bool mmGateopen = false;
        static bool decidgate = false;
        static bool manual = false;
        static string[] win = new string[]
{
                "123","456","789","147","258","369","159","357"
};
        public void decider(string code)
        {
            for (int i = 0; i < code.Length; i++)
            {

            }
 
        }
        public void  brain(string a, string b)
        {
            int solutionChange = 0;
            string wincheck = "";
            string blockcheck = "";
            string makefork = "";
            string deffork = "";
            string forkcode = "";
            int code = 0;

            for (int i = 0; i < win.Length; i++)
            {
                int dba = 0;
                int db = 0;
                string dubwin = "";
                string dubdef = "";
                for (int j = 0; j < 3; j++)
                {
                    if (a.Contains(win[i][j]))
                    {
                        dba++;
                    }
                    else
                        if (!b.Contains(win[i][j]))
                    {
                        dubwin = dubwin + win[i][j];
                    }

                    if (dba == 2)
                    {
                        wincheck = wincheck + dubwin;
                    }
                    if (dubwin.Length == 2 && dba == 1)
                    {
                        makefork = makefork + dubwin;
                    }
                    if (b.Contains(win[i][j]))
                    {
                        db++;
                    }
                    else
                        dubdef = dubdef + win[i][j];
                    if (db == 2)
                    {
                        blockcheck = blockcheck + dubdef;
                    }
                    if (dubdef.Length == 2 && db == 1)
                    {
                        deffork = deffork + dubdef;
                    }
                }
            }
            for (int i = 0; i < wincheck.Length; i++)
            {
                if (!b.Contains(wincheck[i]))
                {
                    endOfTheGame = true;
                    code = wincheck[i] - 48; 
                    solutionChange++;
                }
            }
            if (solutionChange == 0)
            {
                for (int i = 0; i < blockcheck.Length; i++)
                {
                    if (!a.Contains(blockcheck[i]))
                    {
                        code = blockcheck[i] - 48;
                        solutionChange++; 
                    }
                }
            }
            if (solutionChange == 0)
            {
                for (int i = 0; i < makefork.Length; i++)
                {
                    code = makefork[i] - 48;
                    forkcode += makefork[i];
                }
                if (code != 0)
                    if ((makefork.GroupBy(x => x).OrderByDescending
                        (x => x.Count()).ElementAt(0).Count() > 1))
                    {
                        code = (makefork.GroupBy(x => x).OrderByDescending
                            (x => x.Count()).ElementAt(0).Key) - 48;
                        solutionChange++;
                    }

            }
            if (solutionChange == 0)
            {

                for (int i = 0; i < deffork.Length; i++)
                {
                    if (!a.Contains(deffork[i]))
                    {
                        code = deffork[i] - 48;
                        forkcode += deffork[i];
                    }
                }
                if (code != 0)
                    code = (forkcode.GroupBy(x => x).OrderByDescending
                        (x => x.Count()).ElementAt(0).Key) - 48;
            }
            if (mmGateopen == false)
                taken(code);
            else
                decider(forkcode);
        
        }
        public void taken(int code)
        {
            string txt;
            if (counter % 2 != 0)
                txt = "O";
            else
                txt = "X";
            int x = 0;
            int y = 0;
            if (code == 0)
                code = r.Next(1, 10);
            if (code < 4)
            {
                x = 0;
                y = code - 1;
            }
            if (code > 3 && code < 7)
            {
                x = 1;
                y = code - 4;
            }
            if (code > 6)
            {
                x = 2;
                y = code - 7;
            }
            buts[x, y].Text = txt;
            buts[x, y].Enabled = false;
            if (txt == "X")
            {
                xcollection = xcollection + buts[x, y].Name;
                if (manual == true && win.Any(String.Concat(xcollection.OrderBy(c => c)).Contains))
                {
                    endOfTheGame = true;
                }

            }
            else
            {
                ocollection = ocollection + buts[x, y].Name;
                if (manual == true && win.Any(String.Concat(ocollection.OrderBy(c => c)).Contains))
                {
                    endOfTheGame = true;
                }
            }
            if(endOfTheGame==true)
            {
                MessageBox.Show("Gratula az " + txt + "játékosnak");
                start.Enabled = false;
            }
            counter++;
            if(counter==9 && endOfTheGame==false)
            {
                MessageBox.Show("Döntetlen");
                start.Enabled = false;
            }
        }
        static Random r = new Random();
        static int counter = 0;
        static int pieces = 3;
        static int size = 70;
        public Form1()
        {
            InitializeComponent();
        }
        static Button[,] buts = new Button[pieces, pieces];
        static Button start = new Button();
        private void Form1_Load(object sender, EventArgs e)
        {  
            int namer = 1;
            for (int i = 0; i < pieces; i++)
            {
                for (int j = 0; j < pieces; j++)
                {
                    Button b = new Button();
                    b.Size = new Size(size, size);
                    b.Location = new Point(i * size, j * size);
                    b.Click += button_click;
                    b.Name = namer.ToString();
                    b.Font = new Font("Arial", 30);
                    namer++;
                    this.Controls.Add(b);

                    buts[i, j] = b;
                }
            }
            this.Size = new Size(pieces * size + 15, pieces * size + 140);
            start.Size = new Size(45, 23);
            start.Location = new Point(80, 210);
            start.Click += start_click;
            start.Text = "Start";
            this.Controls.Add(start);
            Button clear = new Button();
            clear.Size = new Size(45, 23);
            clear.Location = new Point(80, 250);
            clear.Click += clear_click;
            clear.Text = "Clear";
            this.Controls.Add(clear);
        }
        void start_click(object sender, EventArgs e)
        {
            manual = false;
            if(counter%2!=0)
                brain(ocollection, xcollection);            
            else
                brain(xcollection, ocollection);        
        }
        void clear_click(object sender, EventArgs e)
        {
            for (int i = 0; i < pieces; i++)
            {
                for (int j = 0; j <pieces; j++)
                {
                    buts[i, j].Enabled = true;
                    buts[i, j].Text = "";
                }
            }
            counter = 0;
            ocollection = xcollection= "";
            start.Enabled = true;
            endOfTheGame = false;
        }
        void button_click(object sender, EventArgs e)
        {
            manual = true;
            Button b = sender as Button;
            taken(Convert.ToInt32(b.Name));
   
        }

    }
}
