using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigTournament
{
    


    public partial class Form1 : Form
    {

        // List of players
        Label roundRobin = new Label();
        TextBox roundRobinValue = new TextBox();
        List<IPigPlayer> playerList;
        int roundRobinGames = 10;
        // how many games to play

        public Form1()
        {
            InitializeComponent();
            roundRobin.Text = "round robin games: ";
            roundRobinValue.Text = 10.ToString();
            roundRobin.AutoSize = true;
            roundRobin.Location = new Point(1, 1);
            roundRobinValue.Location = new Point(1, 25);
            this.Controls.Add(roundRobinValue);
            this.Controls.Add(roundRobin);
            roundRobinValue.DoubleClick += new EventHandler(roundRobinValue_DoubleClick);

        }

        private void roundRobinValue_DoubleClick(object sender, EventArgs e)
        {
            roundRobinGames = int.Parse(roundRobinValue.Text);
        }

        private void StartTournamentButton_Click(object sender, EventArgs e)
        {

            Random rng = new Random();

            // Create four players, of three separate classes
            // We can create as many players of different classes and different settings
            // and put as many as we want in the playerList.
             
            PPAlwaysStick p1 = new PPAlwaysStick("S");
            PPRollNTimes p2 = new PPRollNTimes("Roll2", 2);
            PPRandomStick p3 = new PPRandomStick("R.33", rng, 0.33);
            PPRandomStick p4 = new PPRandomStick("R.50", rng, 0.50);


            // List of players, and a dictionary that will hold results.
            playerList = new List<IPigPlayer>() { p1, p2, p3, p4};
            /*            Dictionary<IPigPlayer, int> winList = new Dictionary<IPigPlayer, int>();
            */
            // Two-player tournament

            PigTournament p = new PigTournament(roundRobinGames, playerList, rng);
            p.startGame();
            tournamentReport.Text =  p.showReport();

            // Use the playerList to set up pairs of two-player games. 
           /* foreach (IPigPlayer p in playerList)
            {
                foreach (IPigPlayer q in playerList)
                {
                    // players don't play themselves.
                    if (p == q) continue;

                    // Run roundRobinGames number of games
                    for (int i = 0; i < roundRobinGames; i++)
                    {
                        // Create a new game
                        PigGame g = new PigGame(new List<IPigPlayer>() { p, q }, rng);

                        // PLay the game
                        g.playGame();

                        // get the winner
                        IPigPlayer w = g.getWinner();

                        // Store the winner's win in the winList
                        if (winList.ContainsKey(w))
                        {
                            winList[w] = winList[w] + 1;
                        }
                        else
                        {
                            winList.Add(w, 1);
                        }

                    }
                }
            }

            // Report the results of the tournament in the tournamentReport textbox.

            tournamentReport.Text = "";

            foreach (var kvp in winList)
            {
                IPigPlayer p = (IPigPlayer)kvp.Key;
                string s = p.getName() + " (" + p.getType() + ").               Wins: " + kvp.Value + "\r\n";
                tournamentReport.Text += s;

            }
*/
        }
    }

    class TournamentResult
    {
        int[] winLoose = new int[2];
        Dictionary<IPigPlayer, Dictionary<IPigPlayer, List<int[]>>> wl = new Dictionary<IPigPlayer, Dictionary<IPigPlayer, List<int[]>>>();

        IPigPlayer p1, p2;
        public TournamentResult(IPigPlayer p, IPigPlayer q)
        {
            p1 = p;
            p2 = q;
        }

        void arrResult(PigGame G)
        {
            IPigPlayer winner = G.getWinner();
            if(winner == p1)
            {
                if (wl.ContainsKey(winner))
                {
                    
                }
            }
        }

    }

    class PigTournament
    {
        

    List<IPigPlayer> pList;
        int rrGames = 10;
        Random R = new Random();
    Dictionary<IPigPlayer, int> winList = new Dictionary<IPigPlayer, int>();


    public PigTournament(int roundRobinGames, List<IPigPlayer> playerList, Random r)
            {
                pList = playerList; 
            rrGames = roundRobinGames;
            R = r;


            }

       public void startGame()
        {
            foreach (IPigPlayer p in pList)
            {
                foreach (IPigPlayer q in pList)
                {
                    // players don't play themselves.
                    if (p == q) continue;

                    // Run roundRobinGames number of games
                    for (int i = 0; i < rrGames; i++)
                    {
                        // Create a new game
                        PigGame g = new PigGame(new List<IPigPlayer>() { p, q }, R);

                        // PLay the game
                        g.playGame();

                        // get the winner
                        IPigPlayer w = g.getWinner();

                        // Store the winner's win in the winList
                        if (winList.ContainsKey(w))
                        {
                            winList[w] = winList[w] + 1;
                        }
                        else
                        {
                            winList.Add(w, 1);
                        }

                    }
                }
            }
        }

       public string showReport()
        {
            string sett = "";
            foreach (var kvp in winList)
            {
                IPigPlayer p = (IPigPlayer)kvp.Key;
                string s = p.getName() + " (" + p.getType() + ").               Wins: " + kvp.Value + "\r\n";
               sett += s;

            }
            return sett;
        }
    }


    // Class that represents a pig game
    class PigGame
    {
        // An array of players.
        PigGamePlayer[] players;

        // Count of players
        int playerCount;

        // current player
        int currentPlayer;

        // score for the currentplayer so far this turn
        int currentPlayerScore;

        // Random number generator -- don't want to create a new one for each game
        
        Random rng;

        public PigGame(List<IPigPlayer> playerList, Random r)
        {
            // Build our player array from the playerList

            this.playerCount = playerList.Count;
            this.players = new PigGamePlayer[this.playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new PigGamePlayer(playerList[i]);
            }

            rng = r;
        
        }

        public void playGame()
        {
            this.currentPlayer = -1;  // so that the first player will be 0
            this.currentPlayerScore = 0;

            // A game consists of playing Turns until the player that is about to 
            // play *already* is over 100.  That is, after someone gets to 100, everyone else gets one more turn

            while (this.players[this.nextPlayer()].getScore() < 100)
            {
                this.playTurn();
            }

            // At this point we have a winner
        }

        // The winner is the player with the highest score, not just the first player to get 100 or more
        public IPigPlayer getWinner()
        {
            int bestScore = 0;
            IPigPlayer winner = null;

            foreach (PigGamePlayer p in players)
            {
                if (p.getScore() > bestScore)
                {
                    bestScore = p.getScore();
                    winner = p.getPlayer();
                }
            }
            return winner;
        }

        // Helpful utility function to change the current player
        protected int nextPlayer()
        {
            currentPlayer += 1;

            if (currentPlayer == playerCount)
            {
                currentPlayer = 0;
            }
            return currentPlayer;
        }

        // Play a turn
        protected void playTurn()
        {
            // Score starts at 0
            currentPlayerScore = 0;

            // Keep going until we stop
            while (true)
            {
                // Roll a number
                int roll = rng.Next(6) + 1;

                // If it was  a 1, turn is over, and the player scores 0 for the entire turn.
                if (roll == 1)
                {
                    // current player scores 0, turn is over
                    players[currentPlayer].turnScore(0);
                    break;
                }

                // For any other roll, increment the current player Score
                currentPlayerScore += roll;

                // Need to build the current state of the game so players have lots 
                // of information on which to base their decisions
                PigGameState g = buildGameState();

                // Ask the player what to do
                bool rollAgain = players[currentPlayer].getPlayer().toRollOrNotToRoll(g);

                // Player is stopping, set their score
                if (!rollAgain)
                {
                    players[currentPlayer].turnScore(currentPlayerScore);
                    break;
                }
            }

        }

        // One way to build a game state.  It could include more history if we wanted it to.

        protected PigGameState buildGameState()
        {
            PigGameState g = new PigGameState();

            g.playerCount = playerCount;
            g.currentPlayerScoreThisTurn = currentPlayerScore;
            g.currentPlayerScore = players[currentPlayer].getScore();
            g.playerScores = new int[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                g.playerScores[i] = players[i].getScore();
            }

            return g;

        }

        // Utility function to get a string for an individual game score.
        public string prettyScores()
        {
            String s = "";

            foreach (PigGamePlayer p in players)
            {
                s += p.getPlayer().getName() + " (" + p.getPlayer().getType() + ") " + p.getScore() + "      ";
            }

            return s;
        }
       
    }

    // Represents the state of a player *during* a game (rather than their strategy)
    class PigGamePlayer
    {
        // Player strategy
        IPigPlayer player;

        // Score
        int score;

        // Score per turn
        List<int> turnScores;

        public PigGamePlayer(IPigPlayer p)
        {
            player = p;
            score = 0;
            turnScores = new List<int>();
        }

        // Add the score for their turn to their total and to the list of scores by turn.
        public void turnScore(int s)
        {
            score += s;
            turnScores.Add(s);
        }

        public int getScore()
        {
            return score;
        }

        public IPigPlayer getPlayer()
        {
            return player;
        }
    }

    class PigGameState
    {
        public int playerCount { get; set; }
        public int[] playerScores { get; set; }
        public int currentPlayerScore { get; set; }
        public int currentPlayerScoreThisTurn { get; set; }
    }

    // Player strategy interface.
    // need to be able to get a name, a type, and a decision on whether to roll or not.
    interface IPigPlayer
    {
        bool toRollOrNotToRoll(PigGameState s);
        string getName();
        string getType();
    }

    // abstract base class that handles name and type, but leaves toRollOrNotToRoll for the children
    abstract class BasePigPlayer : IPigPlayer
    {
        abstract public bool toRollOrNotToRoll(PigGameState s);

        protected string _name;
        protected string _type;

        public BasePigPlayer(string n)
        {
            _name = n;
            _type = "Base Player";
        }

        public string getName()
        {
            return _name;
        }

        public string getType()
        {
            return _type;
        }
        public override string ToString()
        {
            return $"{_name} ({_type})";
        }

    }

    // This player never rolls again.

    class PPAlwaysStick : BasePigPlayer
    {
        public PPAlwaysStick(string n) : base(n)
        {
            _type = "Always Stick";
        }

        // Ignore the game state, we *always* return false and never roll again.
        public override bool toRollOrNotToRoll(PigGameState s)
        {
            return false;
        }

    }

    // This player randomly rolls again based on the random number generator
    class PPRandomStick : BasePigPlayer
    {
        // Threshold to roll again
        protected Random rng;
        protected double threshold;

        // Set my variables
        public PPRandomStick(string n, Random r, double t) : base(n)
        {
            _type = "Random Stick";
            rng = r;
            threshold = t;
        }

        // If the random number is below threshold, roll, otherwise, don't
        public override bool toRollOrNotToRoll(PigGameState s)
        {
            if (rng.NextDouble() < threshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }


    // A player that tries to roll N times if they don't roll a 1 first.
    class PPRollNTimes : BasePigPlayer
    {
        // Need to know how many times I've rolled this turn, and also how many times I want to roll.
        protected int _desiredRolls;
        protected int rollsThisTurn = 0;
        protected int lastCurrentPlayerScoreThisTurn = -1;
        protected int lastCurrentPlayerScore = -1;

        public PPRollNTimes(string n, int j) : base(n)
        {
            _type = "Roll N Times";
            _desiredRolls = j;
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            // if currentPlayerScoreThisTurn is different than lastCurrentPLayerScoreThisTurn
            // and my currentScore has changed
            // then it is a new turn  
            // (almost -- possible that I rolled a 2, chose to roll again, failed, and on my *next* turn rolled a 2.
            // But that should only happen once every 36 turns on average, so close enough
            if (s.currentPlayerScore != lastCurrentPlayerScore && s.currentPlayerScoreThisTurn != lastCurrentPlayerScoreThisTurn)
            {
                rollsThisTurn = 1;
            } else
            {
                rollsThisTurn += 1;
            }

            // store the currentPlayer score
            lastCurrentPlayerScoreThisTurn = s.currentPlayerScoreThisTurn;
            lastCurrentPlayerScore = s.currentPlayerScore;

            // If I haven't rolled enough times yet, then roll
            if (rollsThisTurn < _desiredRolls)
            {
                return true;
            }

            // Otherwise, end the turn and take my score
            return false;
        }

    }

}
