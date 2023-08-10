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
using System.Security.Permissions;
using System.Collections.ObjectModel;



// Assignment

// (0.5 pt) -- Into the 'team' Pane, add the pre-season record and the post-season record 
// (0.5 pt) -- Whenever we add a field, we have to change dataGridReset to make it not show.   Add a loop to make all the fields *not* visible, then make the ones we want to see visible and give a column index.
// (1 pt) -- Need More Stats!!!    We have Wins, Losses, Points Per Game.  Add Opponent Points Per Game.  And both of the above for wins and for losses.  (e.g. My points per game in a win)
// (2 pt)  -- Add labels, drop-downs, or buttons to the Game list datagridview that let you see:  all games, home games, away games;   all games, wins, losses; If you pick home / wins it should only show home games that were won.  

// (4 pt) -- We have to repeat all the logic for wins, losses, games, points per game, etc. for each set of games
//        -- e.g. reg season, post season, wins, losses, home gmes, away games, etc.
//        -- Really these are a property of a set of games.
//        -- Create a new TeamGameList class that holds a filtered list of games.
//        -- All the stats (e.g. wins,games,etc.) should be in the TeamGameList class instead
//        -- A Team would then have a number of sets of games:  AllGames, GamesWon, GamesLost, RegularSeasonGames, RegularSeasonGamesWon, etc.
//        -- Instead of calling myTeam.RegSeasonWins to get RegularSeasonWins, you would call myTeam.RegularSeasonGames.Wins

// Extensions
// (1 pt) -- Filtered Stats -- show the right stats when you filter the game list.
// (1 pt) -- really, the drop-down filters filter to one of your TeamGameLists -- connect the filters to get the games from a TeamGameList instead of building an all new filter (just for home/away and wins/losses, ignore opponent here)
// (2 pt) -- Advanced Filters!   Change the filters so you can get a list of games by your score (e.g. <100) or your opp's score (e.g. >100) -- of course, the stats should update
// (2 pt) -- Custom game lists -- Instead of having a *lot* of properties that return TeamGameLists, just have a single Games property that has a Dictionary of TeamGameLists.  So myTeam.Games["All"] would be all games, myTeam.Games["HomeWins"], etc.  
//        --  So you can add custom things to the dictionary with myTeam.Games.Add("HomeWins",myTeam.Games["Home"].findAll(x => x.IsWin))




namespace BasketballStats
{
    public partial class Form1 : Form
    {

        // Create our list of teams
        List<Team> teams = new List<Team>();
        Label Pre_Season_Record = new Label();
        Label Post_Season_Record = new Label();
        Label Opp_PPG = new Label();
        Label Pre_Season = new Label();
        Label Post_Season = new Label();
        ComboBox comboBox1 = new ComboBox();
        ComboBox comboBox2 = new ComboBox();
        ComboBox comboBox3 = new ComboBox();
        ComboBox comboBox4 = new ComboBox();    

        Label PPGWins = new Label();
        Label PPGLosses = new Label();
        Label OppPPGWins = new Label();
        Label OppPPGLosses = new Label();



        Team selectedTeam;

        public Form1()
        {



            InitializeComponent();

            PPGWins.AutoSize = true;
            PPGLosses.AutoSize = true;
            OppPPGLosses.AutoSize = true;
            OppPPGWins.AutoSize = true;

            teamTabPage.Controls.Add(PPGWins);
            teamTabPage.Controls.Add(PPGLosses);
            teamTabPage.Controls.Add(OppPPGLosses);
            teamTabPage.Controls.Add(OppPPGWins);

            PPGWins.Location = new Point(40, 300);
            PPGLosses.Location = new Point(40, 340);
            OppPPGWins.Location = new Point(220, 300);
            OppPPGLosses.Location = new Point(220, 340);


            Pre_Season_Record.Text = "Pre Season Record: ";
            Pre_Season_Record.AutoSize = true;
            Pre_Season_Record.Location = new Point(20, 130);
            teamTabPage.Controls.Add(Pre_Season_Record);

            Opp_PPG.Text = "Opponent Points Per Game: ";
            Opp_PPG.AutoSize = true;
            Opp_PPG.Location = new Point(310, 42);
            teamTabPage.Controls.Add(Opp_PPG);

            Post_Season_Record.Text = "Post Season Record: ";
            Post_Season_Record.AutoSize = true;
            Post_Season_Record.Location = new Point(20, 150);
            teamTabPage.Controls.Add(Post_Season_Record);

            Post_Season.Text = "0-0";
            Post_Season.AutoSize = true;
           

            Pre_Season.Text = "0-0";
            Pre_Season.AutoSize = true;

            Pre_Season_Record.Text += Pre_Season.Text;
            Post_Season_Record.Text += Post_Season.Text;

            loadTeamData();
            loadGameData();

            teams.Sort((t, u) => t.City.CompareTo(u.City));

            opponentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            opponentComboBox.Items.Add("All Opponents");
            foreach (Team t in teams)
            {
                opponentComboBox.Items.Add(t.City + " " + t.Nickname);
            }

            comboBox1.Items.Add("All");
            comboBox1.Items.Add("Wins");
            comboBox1.Items.Add("Losses");
            comboBox1.Location = new Point(170, 42);
            comboBox1.Size = new Size(100, 20);
            teamGamesTabPage.Controls.Add(comboBox1);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);

            comboBox2.Items.Add("All");
            comboBox2.Items.Add("Home");
            comboBox2.Items.Add("Away");
            comboBox2.Location = new Point(275, 42);
            comboBox2.Size = new Size(100, 20);
            teamGamesTabPage.Controls.Add(comboBox2);
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);

            comboBox3.Items.Add("points >= 100");
            comboBox3.Items.Add("points < 100");
            comboBox3.Location = new Point(380, 42);
            comboBox3.Size = new Size(100, 20);
            teamGamesTabPage.Controls.Add(comboBox3);
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.SelectedIndexChanged += new EventHandler(comboBox3_SelectedIndexChanged);

            comboBox4.Items.Add("opponent points >= 100");
            comboBox4.Items.Add("opponent points < 100");
            comboBox4.Location = new Point(490, 42);
            comboBox4.Size = new Size(140, 20);
            teamGamesTabPage.Controls.Add(comboBox4);
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.SelectedIndexChanged += new EventHandler(comboBox4_SelectedIndexChanged);


            // Hint D1 -- you'll have two more combo boxes to set up, but they will each have three explicit strings added.

            teams.Sort((u, t) => t.RegSeasonWins.CompareTo(u.RegSeasonWins));


            teamGridReset();

            teamDataGridView.SelectionChanged += new EventHandler(teamDataGridView_SelectionChanged);
            teamDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(teamDataGridView_ColumnHeaderMouseClick);

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
                Team awayTeam = teams.Find(x => x.TeamID == awayTeamGame.TeamID);
                homeTeamGame.Abbr = homeTeam.Abbr;
                homeTeamGame.OppAbbr = awayTeam.Abbr;
                homeTeam.AddGame(homeTeamGame);
                awayTeamGame.Abbr = awayTeam.Abbr;
                awayTeamGame.OppAbbr = homeTeam.Abbr;
                awayTeam.AddGame(awayTeamGame);
            }
        }

        private void teamGridReset()
        {
            teamDataGridView.DataSource = teams;

           /* teamDataGridView.Columns["IsHeader"].Visible = false;
            teamDataGridView.Columns["TeamID"].Visible = false;
            teamDataGridView.Columns["Arena"].Visible = false;
            teamDataGridView.Columns["Abbr"].Visible = false;
            teamDataGridView.Columns["Wins"].Visible = false;
            teamDataGridView.Columns["RegSeasonWins"].Visible = false;
            teamDataGridView.Columns["RegSeasonLosses"].Visible = false;
            teamDataGridView.Columns["RegSeasonGames"].Visible = false;
            teamDataGridView.Columns["Games"].Visible = false;
            teamDataGridView.Columns["PtsPerRegSeasonGame"].Visible = false;*/

            foreach (DataGridViewColumn column in teamDataGridView.Columns)
            {
                if (column.Name == "Nickname" || column.Name == "City")
                {
                    column.Visible = true;
                }
                else
                {
                    column.Visible = false;
                }
            }

            // DataGridHint:   Whenever you add a property, make sure to hide it above or you will see it


            // Hint B1a:  teamDataGridView.Columns will give you a list of all the columns
            // Loop through that and make them all invisible.

            // Hint B2a:  You turned off *everything* above -- you'll need to explicitly make the below columns visible
            teamDataGridView.Columns["City"].DisplayIndex = 0;
            teamDataGridView.Columns["NickName"].DisplayIndex = 1;
            teamDataGridView.Columns["RegSeasonRecord"].DisplayIndex = 2;
            teamDataGridView.Columns["ArenaCapacity"].DisplayIndex = 3;



            teamDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            teamDataGridView.MultiSelect = false;
        }

        private void gameGridReset(Team t)
        {
            gameGridView.DataSource = t.GameList;

            // Can do way better here -- clear all the columns except the ones we want to see.

           /* gameGridView.Columns["TeamId"].Visible = false;
            gameGridView.Columns["OppId"].Visible = false;
            gameGridView.Columns["GameId"].Visible = false;
            gameGridView.Columns["Abbr"].Visible = false;
            gameGridView.Columns["Points"].Visible = false;
            gameGridView.Columns["Assists"].Visible = false;
            gameGridView.Columns["Rebounds"].Visible = false;
            gameGridView.Columns["FG_Pct"].Visible = false;
            gameGridView.Columns["FT_Pct"].Visible = false;
            gameGridView.Columns["FG3_Pct"].Visible = false;
            gameGridView.Columns["Rebounds"].Visible = false;
            gameGridView.Columns["OppPoints"].Visible = false;
            gameGridView.Columns["OppAssists"].Visible = false;
            gameGridView.Columns["OppRebounds"].Visible = false;
            gameGridView.Columns["OppFG_Pct"].Visible = false;
            gameGridView.Columns["OppFT_Pct"].Visible = false;
            gameGridView.Columns["OppFG3_Pct"].Visible = false;
            gameGridView.Columns["OppRebounds"].Visible = false;
            gameGridView.Columns["IsPreSeason"].Visible = false;
            gameGridView.Columns["IsPostSeason"].Visible = false;*/

            foreach (DataGridViewColumn column in gameGridView.Columns)
            {
                if (column.Name == "OppAbbr" || column.Name == "IsWin" || column.Name == "Score" || column.Name == "Points" || column.Name == "OppPoints")
                {
                    column.Visible = true;
                }
                else
                {
                    column.Visible = false;
                }
            }


            // Hint B2a:  Do hint B1a here too!

            // Hint B2b:  You'd better make the columns you want visible here -- and set their display index if you care.

            gameGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gameGridView.MultiSelect = false;
        }

        private void teamDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // Get the set of selected rows.
            // Since multiselect is false, there will only be one row selected, so get the first one
            // DataBoundItem is the Team in that spot in the row.

            if (teamDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            selectedTeam = (Team) teamDataGridView.SelectedRows[0].DataBoundItem;

            teamName.Text = selectedTeam.Nickname;
            arenaName.Text = selectedTeam.Arena;

            // set the Label to the associated property
            regularSeasonRecordLabel.Text = selectedTeam.RegSeasonRecord;  // Hint A2:  you'll want to do this for PreSeasonRecord
            Pre_Season.Text = selectedTeam.PreSeasonRecord;
            Pre_Season_Record.Text = "Pre Season Resord: " + Pre_Season.Text;

            Post_Season.Text = selectedTeam.PostSeasonRecord;
            Post_Season_Record.Text = "Post Season Record: "+ Post_Season.Text;



            // Hint C2:  You'll need to add a Opponent's points per game label, 
            // and show the OppPtsPerRegSeasonGame Property for the selected team in that label below
            regularSeasonPPGLabel.Text = selectedTeam.PtsPerRegSeasonGame;
            Opp_PPG.Text = "Opposition Points Per Game: " + selectedTeam.OppPtsPerRegSeasonGame;
            OppPPGWins.Text = "Opposition Points Per Game in Wins: " + selectedTeam.oppgW;
            OppPPGLosses.Text = "Opposition Points Per Game in Losses: " + selectedTeam.oppgL;
            PPGWins.Text = "Points per Game in Wins: " + selectedTeam.ppgW;
            PPGLosses.Text = "Points Per Game in Losses: "+ selectedTeam.ppgL;

            // Hint C4:  You'll need to do the above for the other 4 stats


            gameGridReset(selectedTeam);

        }

        private void teamDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = teamDataGridView.Columns[e.ColumnIndex].Name;
            MessageBox.Show(columnName);
            teams.Sort((u, t) => t.Nickname.CompareTo(u.Nickname));
            teamGridReset();
            
        }

        private void opponentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Find the opponent name selected in the combo box
            String teamname = (String)opponentComboBox.SelectedItem;

            // Find the team selected in the teamDataGridView
            Team selectedTeam = (Team)teamDataGridView.SelectedRows[0].DataBoundItem;

            // If it is all opponents, then add the full game list.
            if (teamname == "All Opponents")
            {
                gameGridView.DataSource = selectedTeam.GameList;
                return;
            }

            // If we wan to see a specific team, then find the right team from the teamname
            Team selectedOpponent = teams.Find(t => teamname.Contains(t.Nickname));

            // Now, get the read-only list of all games from the selected team, make a mutable copy (ToList()), then filter so that the 
            // OppAbbr property in the game matches the Abbr of the selected opponent.
            gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.OppAbbr == selectedOpponent.Abbr);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            String Choice3 = (String)comboBox3.SelectedItem;
            if (Choice3 == "points >= 100")
            {
                gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.Points >= 100);
            }

            else
            {
                gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.Points < 100);

            }
           
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            String Choice4 = (String)comboBox4.SelectedItem;


            if (Choice4 == "opponent points >= 100")
            {
                gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.OppPoints >= 100);
            }
            else
            {
                gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.OppPoints < 100);

            }
        }

private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String Choice1 = (String)comboBox1.SelectedItem;
            String Choice2 = (String)comboBox2.SelectedItem;
         
            selectedTeam.GameList.ToList<TeamGame>();
            if(Choice2 == "Home")
            {
                if(Choice1 == "Wins")
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsHomeGame == true && g.IsWin == true);
                }

                else if(Choice1 == "Losses")
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsHomeGame == true && g.IsWin == false);
                }

                else
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsHomeGame == true);
                }

            }

            else if(Choice2 == "Away")
            {
                if (Choice1 == "Wins")
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsHomeGame == false && g.IsWin == true);
                }

                else if (Choice1 == "Losses")
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsHomeGame == false && g.IsWin == false);
                }

                else
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsHomeGame == false);
                }
            }
            else
            {
                if (Choice1 == "Wins")
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsWin == true);
                }

                else if (Choice1 == "Losses")
                {
                    gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.IsWin == false);
                }
            }

           
        }


        // Hint D2:  There will be a couple of SelectedIndexChanged events, one for each combo box
        // They will both call the same function
        // That function will get the selected item from both combo boxes.
        
        // Then it should get a list:   selectedTeam.GameList.ToList<TeamGame>();

        // If the home/away filter is set to all, do nothing
        // If the home/away filter is set to 'home', then filter the list to only home games
        // If the home/away filter is set to 'away', then filter the list to only away games

        // Then we will apply the win/loss filter to the above resulting list
        // If the win/loss filter is set to all, do nothing else
        // If the win/loss filter is set to win, filter the home/away filtered list to only wins
        // If the win/loss filter is set to loss, filter the home/away filtered list to only losses



    }

    class TeamGameList
    {
        public List<TeamGame> games;

        
        
        public TeamGameList(List<TeamGame> games)
        {
            this.games = games;
        }
        
        public TeamGameList()
        {
            games = new List<TeamGame>();
        }

        public void AddGame(TeamGame g)
        {
            games.Add(g);
        }

        public Int32 Wins
        {
            get { return games.Count(g => g.IsWin); }
        }

        public Int32 Losses
        {
            get { return games.Count(g => !g.IsWin); }
        }

        public Int32 Games
        {
            get { return games.Count; }
        }

        public String PPG
        {
            get
            {
                double PlayedGames = games.Count;
                double PointsTotal = 0;



                foreach(TeamGame g in games)
                {
                    PointsTotal += g.Points;
                }

                return (PointsTotal / PlayedGames).ToString("F");

            }
        }

        public String OppPPG
        {
            get
            {
                double PlayedGames = games.Count;
                double OppPointsTotal = 0;

                foreach(TeamGame g in games)
                {
                    OppPointsTotal += g.OppPoints;
                }

                return (OppPointsTotal/ PlayedGames).ToString("F");
            }
        }

        public String PPGw
        {
            get
            {
                double PlayedGames = games.Count;
                double totalPoints = 0;

                foreach(TeamGame g in games)
                {
                    if (g.IsWin)
                    {
                        totalPoints += g.Points;
                    }
                }
                return (totalPoints / PlayedGames).ToString("F");
            }
        }

        public String PPGl
        {
            get
            {
                double PlayedGames = games.Count;
                double totalPoints = 0;

                foreach (TeamGame g in games)
                {
                    if (!g.IsWin)
                    {
                        totalPoints += g.Points;
                    }
                }
                return (totalPoints / PlayedGames).ToString("F");
            }
        }

        public String OPPGw
        {
            get
            {
                double PlayedGames = games.Count;
                double totalPoints = 0;

                foreach (TeamGame g in games)
                {
                    if (g.IsWin)
                    {
                        totalPoints += g.OppPoints;
                    }
                }
                return (totalPoints / PlayedGames).ToString("F");
            }
        }

        public String OPPGl
        {
            get
            {
                double PlayedGames = games.Count;
                double totalPoints = 0;

                foreach (TeamGame g in games)
                {
                    if (!g.IsWin)
                    {
                        totalPoints += g.OppPoints;
                    }
                }
                return (totalPoints / PlayedGames).ToString("F");
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

        /*        List<TeamGame> games = new List<TeamGame>();
         *        
        */



        public TeamGameList games { get; set; }
        public TeamGameList RegularseasonGames { get; set; }
        public TeamGameList PostseasonGames { get; set; }
        public TeamGameList PreseasonGames { get; set; }

        public Team()
        {
            games = new TeamGameList();
            RegularseasonGames = new TeamGameList();
            PostseasonGames = new TeamGameList();
            PreseasonGames = new TeamGameList();

        }


        // Property which notes that this Team was loaded from a header line in the file

        public bool IsHeader
        {
            get
            {
                return TeamID == "TEAM_ID";
            }
        }

        public void AddGame (TeamGame g)
        {
            games.AddGame(g);
            TeamGame Rgames = new TeamGame();
            TeamGame Pregames = new TeamGame();
            TeamGame Postgames = new TeamGame();

            foreach(TeamGame game in games.games)
            {
                if (game.IsPreSeason)
                {
                    PreseasonGames.AddGame(game);
                }
            }

            foreach (TeamGame game in games.games)
            {
                if (game.IsPostSeason)
                {
                    PostseasonGames.AddGame(game);
                }
            }

            foreach (TeamGame game in games.games)
            {
                if (!game.IsPreSeason && !game.IsPostSeason)
                {
                    RegularseasonGames.AddGame(game);
                }
            }

        }

        public Int32 Wins
        {
            get
            {
                return games.Wins ; 
            }
        }

        public Int32 RegSeasonWins
        {
            get
            {
                return RegularseasonGames.Wins;
            }
        }
        public Int32 RegSeasonLosses
        {
            get
            {
                return RegularseasonGames.Losses;
            }
        }

        public Int32 PreSeasonWins
        {
            get
            {
                return PreseasonGames.Wins;
            }
        }

        public Int32 PreSeasonLosses
        {
            get
            {
                return PreseasonGames.Losses;
            }
        }

        public Int32 PostSeasonWins
        {
            get
            {
                return PostseasonGames.Wins;
            }
        }

        public Int32 PostSeasonLosses
        {
            get
            {
                return PostseasonGames.Losses;
            }
        }

        public Int32 Games
        {
            get
            {
                return games.games.Count;
            }
        }

        public Int32 RegSeasonGames
        {
            get
            {
                return RegularseasonGames.Games;
            }
        }

        public List<TeamGame> GameList
        {
            get
            {
                return this.games.games;

            }
        }

        public String PtsPerRegSeasonGame
        {
            get
            {
                /*// First, get a filtered lists of only regular season games
                List<TeamGame> regularSeasonGames = games.FindAll(x => !x.IsPreSeason && !x.IsPostSeason);

                // we'll need to know how many points were scored and how many total points scored 
                double gamesPlayed = regularSeasonGames.Count;
                double totalPoints = 0;

                // Loop through the list of regularSeasonGames, adding up the points
                foreach(TeamGame g in regularSeasonGames)
                {
                    totalPoints += g.Points;
                }

                // Return the average as a string formatted to display  a float.
                return (totalPoints / gamesPlayed).ToString("F");*/
                return RegularseasonGames.PPG;
            }
        }

        public String OppPtsPerRegSeasonGame
        {
            get
            {
                return RegularseasonGames.OppPPG;
            }
        }

        public String ppgW
        {
            get
            {
                return games.PPGw;
            }
        }

        public String ppgL
        {
            get
            {
                return games.PPGl;
            }
        }

        public String oppgW
        {
            get
            {
                return games.OPPGw;
            }
        }

        public String oppgL
        {
            get
            {
                return games.OPPGl;
            }
        }

        // Hint C1:   you'll want to add an OppPtsPerRegSeasonGame;  will look *very* much like the above

        // Hint C2:   you'll need 4 more properties -- but they will basically look like the above as well
        // The only difference is whether we are adding up Points or OppPoints, and then whether we are filtering the 
        // games to regular season games or regular season wins or regular season losses.

        public String RegSeasonRecord
        {
            get
            {
                return RegSeasonWins + "-" + RegSeasonLosses;
            }
        }

        public String PreSeasonRecord
        {
            get
            {
                return PreSeasonWins + "-" + PreSeasonLosses;
            }
        }

        public String PostSeasonRecord
        {
            get
            {
                return PostSeasonWins + "-" + PostSeasonLosses;
            }
        }
     

        // Hint A1 -- create a PreSeasonRecord property and a PostSeasonRecordProperty
        // Up to you if you want to create a PreSeasonWins and PreSeasonLosses properties as well or if you want to fold that logic into the PreSeasonRecord property


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
