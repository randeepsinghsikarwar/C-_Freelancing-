using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace BasketballStats
{
    public partial class Form1 : Form
    {

        // Create our list of teams
        List<Team> teams = new List<Team>();
        List<TeamGame> games = new List<TeamGame>();
       

        public Form1()
        {
            InitializeComponent();

            
            loadTeamData();
            loadGameData();

            comboBoxSetup();          

        }

        private void loadTeamData()
        {
            // Read in the file.  Each line is a team (except the header row)

            foreach (string line in File.ReadLines("../../data/teams.csv"))
            {

                // Team has a static function that will create a team from a line of this data.
                Team t = Team.readTeamData(line);

                // If this isn't the header line
                if (!t.IsHeader)
                {
                    teams.Add(t);
                }
            }

        }

        private void loadGameData()
        {
            bool headerRow = true;
            foreach (string line in File.ReadLines("../../data/games2122.csv"))
            {
                // Skip the first line, it is a header
                if (headerRow)
                {
                    headerRow = false;
                    continue;
                }

                TeamGame homeTeamGame = TeamGame.readGameData(true, line);
                TeamGame awayTeamGame = TeamGame.readGameData(false, line);

                Team homeTeam = teams.Find(x => x.TeamID == homeTeamGame.TeamID);
                Team awayTeam = teams.Find(x => x.TeamID == homeTeamGame.OppID);

                homeTeamGame.Abbr = homeTeam.Abbr;
                homeTeamGame.OppAbbr = awayTeam.Abbr;
                games.Add(homeTeamGame);

                awayTeamGame.Abbr = awayTeam.Abbr;
                awayTeamGame.OppAbbr = homeTeam.Abbr;
                games.Add(awayTeamGame);
            }
        }

        private void updateGUI<T> (List<T> myList)
        {
            gameGridView.DataSource = myList;
            listCountLabel.Text = myList.Count().ToString();


        }

        private void comboBoxSetup()
        {
            opComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            
            opComboBox.Items.Add(new myOp { name = "All Teams", method = this.allTeams });
            opComboBox.Items.Add(new myGameOp { name = "All Games", method = this.allGames });
            opComboBox.Items.Add(new myGameOp { name = "PreSeason Games Xt", method = this.preseasonGamesXt });
            opComboBox.Items.Add(new myGameOp { name = "PreSeason Games", method = this.preseasonGames });


            // We can also define the method anonymously using a lambda expression instead of building a function
            opComboBox.Items.Add(new myGameOp { name = "PostSeason Games", method = () => (from g in games where g.IsPostSeason && g.IsHomeGame select g).ToList()});

            opComboBox.Items.Add(new myGameOp { name = "RegularSeason Games", method = this.regularSeasonGames });

            // Order By using Extension method
            opComboBox.Items.Add(new myGameOp { name = "RegularSeason Best Home Defense", method = () => this.regularSeasonGames().OrderBy(g => g.OppPoints).ToList() });

            // Order By descending using Query Expression
            opComboBox.Items.Add(new myGameOp { name = "RegularSeason Best Home Offense", method = () => (from g in this.regularSeasonGames() orderby g.Points descending select g ).ToList() });

            // Order By descending using Query Expression
            opComboBox.Items.Add(new myGameOp { name = "RegularSeason Best Home Offense 2", method = () => (from g in games
             
                                                                                                            where !g.IsPostSeason && !g.IsPreSeason && !g.IsHomeGame
                                                                                                            orderby g.Points descending select g).ToList() });

            opComboBox.Items.Add(new myGameOp { name = "sorted regular season games", method = () => (from g in this.regularSeasonGames() orderby (g.Rebounds) select g).ToList() });

            opComboBox.Items.Add(new myGameOp { name = "custom team data", method = () => (from g in games where g.TeamID == textBox1.Text orderby g.Assists  descending select g).ToList() });

            opComboBox.Items.Add(new myGameOp { name = "rebounds greater than n", method = () => (from g in games where g.Rebounds >= int.Parse(textBox1.Text) select g).ToList() });

            opComboBox.Items.Add(new myGameOp { name = "top 20 assist games of the season", method = () => (from g in games orderby g.Assists descending select g).Take(20).ToList() });

            opComboBox.Items.Add(new myGameOp { name = "Regular season games when opponent score is <100", method = () => (from g in this.regularSeasonGames() where g.OppPoints < 100 select g).ToList() });
            opComboBox.Items.Add(new myOp { name = "Teams with Arena Capacity > 17500", method = () => (from t in teams where t.ArenaCapacity > 17500 select t).ToList() });
            opComboBox.Items.Add(new myGameOp { name = "RegularSeason Raptors Games", method = () => (from g in games where !g.IsPostSeason && !g.IsPreSeason && g.Abbr == "TOR"  select g).ToList() });
            opComboBox.Items.Add(new myGameOp1 { name = "RegularSeason Games Abbr {TextBox1}", method = this.regularSeasonTeamGames });
            opComboBox.Items.Add(new myGameOp { name = "RegularSeason Games Abbr {TextBox1} Sorted by Score", method = () => (from g in this.regularSeasonTeamGames(textBox1.Text) orderby (g.Points+g.OppPoints) select g).ToList() });
            opComboBox.Items.Add("RegularSeason New Object");
            opComboBox.Items.Add(new myGameOp1 { name = "RegularSeason Home Games Abbr {TextBox1}", method = this.regularSeasonTeamHomeGames });
            opComboBox.Items.Add(new myGameOp1 { name = "RegularSeason Away Games Abbr {TextBox1}", method = this.regularSeasonTeamAwayGames });
            opComboBox.Items.Add("Wins By Team");
            opComboBox.Items.Add("High Score By Team");
            opComboBox.Items.Add("Total Score By Team");
            opComboBox.Items.Add("Points Per Game By Team");
            opComboBox.Items.Add("Win and Loss Stats");


            opComboBox.Items.Add("Average Assist");

        }

        private List<TeamGame> nullList()
        {
            return null;
        }

        private List<Team> allTeams()
        {
            return teams;
        }

        private List<TeamGame> allGames()
        {
            // Old-style predicate
            return games.FindAll( x => x.IsHomeGame);
        }

        // Preseason games with .Where extension method
        private List<TeamGame> preseasonGamesXt()
        {
            return games.Where(g => g.IsPreSeason && g.IsHomeGame).ToList();
        }

        // Preseason games with Query Expression
        private List<TeamGame> preseasonGames()
        {
            return (
                from g in allGames()
                where g.IsPreSeason && g.IsHomeGame
                select g
                ).ToList();
        }

        private List<TeamGame> regularSeasonGames()
        {
            return games.Where(g => !g.IsPreSeason && !g.IsPostSeason && g.IsHomeGame).ToList();
        }


        private List<TeamGame> regularSeasonTeamGames(String abbr)
        {
            return (
                from g in games
                where !g.IsPreSeason && !g.IsPostSeason && g.Abbr == abbr
                select g
                ).ToList();
        }

        private List<TeamGame> regularSeasonTeamHomeGames(String abbr)
        {
            return (
                from g in regularSeasonTeamGames(abbr)
                where g.IsHomeGame
                select g
                ).ToList();
        }
        private List<TeamGame> regularSeasonTeamAwayGames(String abbr)
        {
            return (
                from g in regularSeasonTeamGames(abbr)
                where !g.IsHomeGame
                select g
                ).ToList();
        }

      


        // We can't pass this as a FUNC because this creates a new, anonymous object and FUNC is strongly typed.
        private void regSeasonNewObject()
        {
            var myList = (
               from g in this.regularSeasonTeamGames(textBox1.Text)
               orderby (g.Points + g.OppPoints)
               select new
               {
                   Abbr = g.Abbr,
                   OppAbbr = g.OppAbbr,
                   GameDate = g.GameDate,
                   Points = g.Points,
                   OppPoints = g.OppPoints,
                   TotalPoints = g.Points + g.OppPoints
               }
               ).ToList();
            updateGUI(myList);
        }

        private void winsByTeam()
        {
            var myList = (
                from g in games
                where !g.IsPreSeason && !g.IsPostSeason && g.Points > g.OppPoints
                group g by g.Abbr into teamGroup
                orderby teamGroup.Count() descending
                select new {team = teamGroup.Key, wins = teamGroup.Count()}
                ).ToList();

            updateGUI(myList);
        }

        private void highScoreByTeam()
        {
            var myList = (
                from g in games
                where !g.IsPreSeason && !g.IsPostSeason
                group g by g.Abbr into teamGroup
                orderby teamGroup.Max(g => g.Points) descending
                select new { team = teamGroup.Key, highScore = teamGroup.Max(g=>g.Points) }
                ).ToList();

            updateGUI(myList);
        }

        private void totalScoreByTeam()
        {
            // this creates the list first, then uses that list as input to a query that sorts using the new variable
            var myList = (
                from t in (

                from g in games
                where !g.IsPreSeason && !g.IsPostSeason
                group g by g.Abbr into teamGroup
                select new { team = teamGroup.Key, totalScore = teamGroup.Sum(g => g.Points) }

                ) orderby t.totalScore descending
                select t


                ).ToList();

            updateGUI(myList);
        }

        private void averageAssist()
        {
            var myList = (
                from g in games
                where !g.IsPreSeason && !g.IsPostSeason
                group g by g.Abbr into teamGroup
                orderby teamGroup.Average(g => g.Assists) descending
                select new { team = teamGroup.Key, assist = teamGroup.Average(g => g.Assists) }
                ).ToList();

            updateGUI(myList);
        }


        private void PointsPerGameByTeam()
        {
            // This repeats the 'average' expression in the orderby and the select portion
            var myList = (
                from g in games
                where !g.IsPreSeason && !g.IsPostSeason
                group g by g.Abbr into teamGroup
                orderby teamGroup.Average(g=>g.Points) descending
                select new { team = teamGroup.Key, ppg = teamGroup.Average(g=>g.Points) }
                ).ToList();

            updateGUI(myList);
        }

        private void WinAndLossStats()
        {
            // This repeats the 'average' expression in the orderby and the select portion
            var myList = (
                from g in games
                where !g.IsPreSeason && !g.IsPostSeason
                group g by g.Abbr into teamGroup
                orderby teamGroup.Count(g=>g.Points>g.OppPoints) descending
                select new { team = teamGroup.Key, 
                    wins = teamGroup.Count(g=>g.Points>g.OppPoints), losses = teamGroup.Count(g=>g.Points<g.OppPoints),
                    winPPG = teamGroup.Sum(g=>(g.Points>g.OppPoints?g.Points:0)) / teamGroup.Count(g=>g.Points>g.OppPoints),
                    lossPPG = teamGroup.Sum(g => (g.Points < g.OppPoints ? g.Points : 0)) / teamGroup.Count(g => g.Points < g.OppPoints)

                }
                ).ToList();


            updateGUI(myList);
        }


        // Our handler here can do some interesting things.
        // Rather than have a giant switch statement by name, we can actually 
        // store the method to call in a property of an object, and call that.

        private void opComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            var op = opComboBox.SelectedItem;

            //Special cases too complicated for delegate functions
            if (op is String && ((String)op) == "RegularSeason New Object")
            {
                this.regSeasonNewObject();
                return;
            }

            if (op is String && ((String)op) == "Wins By Team")
            {
                this.winsByTeam();
                return;
            }
            if (op is String && ((String)op) == "High Score By Team")
            {
                this.highScoreByTeam();
                return;
            }
            if (op is String && ((String)op) == "Total Score By Team")
            {
                this.totalScoreByTeam();
                return;
            }
            if (op is String && ((String)op) == "Points Per Game By Team")
            {
                this.PointsPerGameByTeam();
                return;
            }
            if (op is String && ((String)op) == "Win and Loss Stats")
            {
                this.WinAndLossStats();
                return;
            }

            if(op is String && ((String)op) == "Average Assist")
            {
                this.averageAssist();
                return;
            }

            if (op is myOp)
            {
                myOp op1 = (myOp)op;
                updateGUI(op1.method());
            } else if (op is myGameOp)
            {
                myGameOp op2 = (myGameOp)op;
                updateGUI(op2.method());
            }

            else if (op is myGameOp1)
            {
                myGameOp1 op3 = (myGameOp1)op;
                updateGUI(op3.method(textBox1.Text));
            }

        }


        // If we want to / need to, a method or function can be stored in a variable
        // The *type* of a stored method is Action (if it is a return void function with no parameters)
        // Otherwise it is an instantiation of a generic 'Func' object

        // In the myOp class, we store a function that takes 0 parameters and with a return type of List<Team>
        class myOp
        {
            public string name { get; set; }
            public Func<List<Team>> method { get; set; }

            public override String ToString()
            {
                return name;
            }
        }

        // In the myGameOp class, we store a function that takes 0 parameters and with a return type of List<TeamGame>
        class myGameOp
        {
            public string name { get; set; }
            public Func<List<TeamGame>> method { get; set; }

            public override String ToString()
            {
                return name;
            }
        }

        // In the myGameOp1 class, we store a function that takes a single string parameter, and returns a list of TeamGames
        class myGameOp1
        {
            public string name { get; set; }
            public Func<String,List<TeamGame>> method { get; set; }

            public override String ToString()
            {
                return name;
            }
        }


    }


    class Team
    {
        public String TeamID {get;  private set;}
        public String Abbr { get; private set; }
        public String Nickname { get; private set; }
        public String City { get; private set; }
        public String Arena { get; private set; }
        public Int32 ArenaCapacity { get; private set; }

        public bool IsHeader
        {
            get
            {
                return TeamID == "TEAM_ID";
            }
        }

        public override string ToString()
        {
            return City + " " + Nickname;
        }

        public static Team readTeamData(string teamLine)
        {
            Team t = new Team();

            // Reading from a .csv with the following data
            // LEAGUE_ID,TEAM_ID,MIN_YEAR,MAX_YEAR,ABBREVIATION,NICKNAME,YEARFOUNDED,CITY,ARENA,ARENACAPACITY,OWNER,GENERALMANAGER,HEADCOACH,DLEAGUEAFFILIATION
            string[] data = teamLine.Split(',');

            // private Properties are accessible within the class
            // even when you aren't inside the object!

            t.TeamID = data[1];
            t.Abbr = data[4];
            t.Nickname = data[5];
            t.City = data[7];
            t.Arena = data[8];
            int ac;
            if (Int32.TryParse(data[9], out ac))
            {
                t.ArenaCapacity = ac;
            }

            return t;
        }

    }


    class TeamGame
    {
        public String TeamID { get; private set; }
        public String OppID { get; private set; }
        public String GameID { get; private set; }

        public String OppAbbr { get; set; }
        public String Abbr { get; set; }

        public bool IsHomeGame { get; private set; }

        public DateTime GameDate { get; private set; }

        public Int32 Points { get; private set; }
        public Int32 Assists { get; private set; }
        public Int32 Rebounds { get; private set; }

        public Double FG_Pct { get; private set; }
        public Double FT_Pct { get; private set; }
        public Double FG3_Pct { get; private set; }

        public Int32 OppPoints { get; private set; }
        public Int32 OppAssists { get; private set; }
        public Int32 OppRebounds { get; private set; }

        public Double OppFG_Pct { get; private set; }
        public Double OppFT_Pct { get; private set; }
        public Double OppFG3_Pct { get; private set; }

        // Derived property.  We won the game if we scored more points.
        public bool IsWin
        {
            get
            {
                return Points > OppPoints;
            }
        }

        public bool IsPreSeason
        {
            get
            {
                return GameDate < new DateTime(2021,10,19);
            }
        }

        public bool IsPostSeason
        {
            get
            {
                return GameDate > new DateTime(2022, 4, 11);
            }
        }

        public string Score
        {
            get
            {
                return Points + "-" + OppPoints;
            }
        }


        public static TeamGame readGameData(bool homegame, string gameLine)
        {
            TeamGame tg = new TeamGame();

            // home game offsets
            int myOffset = 6;
            int oppOffset = 13;

            // reading from a .csv with the following data
            // GAME_DATE_EST,GAME_ID,GAME_STATUS_TEXT,HOME_TEAM_ID,VISITOR_TEAM_ID,SEASON,TEAM_ID_home,PTS_home,FG_PCT_home,FT_PCT_home,FG3_PCT_home,AST_home,REB_home,TEAM_ID_away,PTS_away,FG_PCT_away,FT_PCT_away,FG3_PCT_away,AST_away,REB_away,HOME_TEAM_WINS

            string[] data = gameLine.Split(',');

            tg.GameDate = DateTime.Parse(data[0]);
            tg.GameID = data[1];

            tg.IsHomeGame = homegame;

            if (!homegame)
            {
                myOffset = 13;
                oppOffset = 6;
            }

            tg.TeamID = data[myOffset + 0];
            tg.OppID = data[oppOffset + 0];

            tg.Points = (Int32) Double.Parse(data[myOffset + 1], CultureInfo.InvariantCulture);
            tg.OppPoints = (Int32)Double.Parse(data[oppOffset + 1], CultureInfo.InvariantCulture);

            tg.Assists = (Int32)Double.Parse(data[myOffset + 5], CultureInfo.InvariantCulture);
            tg.OppAssists = (Int32)Double.Parse(data[oppOffset + 5], CultureInfo.InvariantCulture);

            tg.Rebounds = (Int32)Double.Parse(data[myOffset + 6], CultureInfo.InvariantCulture);
            tg.OppRebounds = (Int32)Double.Parse(data[oppOffset + 6], CultureInfo.InvariantCulture);

            // Culture info is required to properly parse doubles on my wife's computer set to French 
            tg.FG_Pct = Double.Parse(data[myOffset + 2],CultureInfo.InvariantCulture);
            tg.OppFG_Pct = Double.Parse(data[oppOffset + 2], CultureInfo.InvariantCulture);

            tg.FT_Pct = Double.Parse(data[myOffset + 3], CultureInfo.InvariantCulture);
            tg.OppFT_Pct = Double.Parse(data[oppOffset + 3], CultureInfo.InvariantCulture);

            tg.FG3_Pct = Double.Parse(data[myOffset + 4], CultureInfo.InvariantCulture);
            tg.OppFG3_Pct = Double.Parse(data[oppOffset + 4], CultureInfo.InvariantCulture);


            return tg;
        }



    }

}
