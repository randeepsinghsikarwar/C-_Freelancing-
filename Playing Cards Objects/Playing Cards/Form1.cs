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
using System.Runtime.InteropServices;
using Playing_Cards;

namespace Playing_Cards
{
    public partial class Form1 : Form
    {

       

        // Our form just got *really* simple
        // We basically create a new CardGame, reshuffle, then ask it to build its hand Displays
        // And the only other thing we can do right now is call the deal() function 
        // Everything else is buried in the CardGame class

        CardGame cardGame;
  


        public Form1()
        {
            InitializeComponent();


            Button Reshuffle = new Button();
            Reshuffle.Text = ("Reshuffle");
            Reshuffle.Location = new Point(702, 210);
            Reshuffle.Size = new Size(70, 20);
            this.Controls.Add(Reshuffle);
            Reshuffle.Click += new EventHandler(reshuffle_click);



                   Label FirstHandScore = new Label();
        Label SecondHandScore = new Label();
        FirstHandScore.Text = "First Hand Score";
            SecondHandScore.Text = "Second Hand Score";
            FirstHandScore.Location = new Point(670, 240);
            SecondHandScore.Location = new Point(670, 255);
            FirstHandScore.AutoSize = true;
            SecondHandScore.AutoSize = true;
            this.Controls.Add(FirstHandScore);
            this.Controls.Add(SecondHandScore);

            deckPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            deckPictureBox.Image = Image.FromFile("../../Images/deck.png");

            cardGame = new CardGame();

            // Hint A2:  Reshuffle logic
            // What is reshuffling?
            // Basically it is picking up all the cards, and then shuffling them
            // That is what we are doing here, so this is the logic of your reshuffle() function
            // Once you've written the reshuffle function, you should also call it here instead of 
            // these two functions separately. 

            cardGame.collectCards();
            cardGame.shuffle();
            cardGame.reshuffle();

            cardGame.buildHandDisplays(this);
        }

       

        private void dealButton_Click(object sender, EventArgs e)
        {
            cardGame.deal();

            
        }

        private void reshuffle_click(object sender, EventArgs e)
        {
            cardGame.reshuffle();
        }

        // Hint A1:  Reshuffle button
        // You will have a reshuffle button, and you will need a click handler
        // It shouldn't have to do anything except call cardGame.reshuffle()  
        // Once you build cardGame.reshuffle()
    }
}

class CardGame
{
    // This will hold the fullDeck of all cards, but then will never be changed
    protected Deck fullDeck;

    // This represents the current Deck of cards on the table, some of which might have been dealt
    protected Deck currentDeck;

    // Logic for building the fullDeck of cards
    protected String[] suits = { "hearts", "clubs", "diamonds", "spades" };
    protected String[] rankNames = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
    protected String[] suffix = { "", "", "", "", "", "", "", "", "", "", "", "2", "2", "2" };

    // How many hands are there?
    protected int handCount = 2;

    // How many cards in each hand?
    protected int handSize = 5;

    // an array of Hands
    protected Hand[] hands;

    // We want to know the topleft point of the displayArea and how big it is
    // This is necessary to determing how big the hand Displays should be.

    protected Point displayAreaTopLeft;
    protected Size displayAreaSize;

    // Default (and only) Constructor for a CardGame
    public CardGame()
    {
        // Right now, we set the topLeft point and the displayArea Size
        // We could have *another* constructor that lets us change this from the Form

        displayAreaTopLeft = new Point(30, 30);
        displayAreaSize = new Size(600, 350);

        // build a full deck of read-only cards
        buildFullDeck();
    }

    public void buildHandDisplays(Form f)
    {
        // create an array of hands
        hands = new Hand[handCount];

        // Now we need to create the individual hands
        for(int h= 0; h<handCount; h++) 
        {
            // Need to determine how the hands will display -- how much vertical space does each hand
            // get if there are 'handCount' of them?

            int handHeight = Convert.ToInt32((displayAreaSize.Height - handCount * 10) / handCount);

            // figure out the topleft co-ordinate and the size of the display given the above result
            Point handTopLeft = new Point(displayAreaTopLeft.X, displayAreaTopLeft.Y + handHeight * h);
            Size handDisplaySize = new Size(displayAreaSize.Width, handHeight);

            // create a new hand containing the right number of cards and the co-ordinates for drawing itself on the form
            hands[h] = new Hand(handSize, handTopLeft, handDisplaySize);

            // Each hand has a display panel
            // Add that display panel to the form
            
            f.Controls.Add(hands[h].display);
        }
    }


    // Build a full deck of read-only cards
    private void buildFullDeck()
    {
        // create an empty deck
        fullDeck = new Deck();

        // For every suit
        foreach (String suit in this.suits)
        {
            // for every rank
            for (int r = 1; r <= this.rankNames.Length; r++)
            {
                // build the filename of the image on disk
                String filename = "../../Images/"+this.rankNames[r - 1] + "_of_" + suit + this.suffix[r - 1] + ".png";

                // create a new card of this suit, rankname, rank value, and pass in the image filename.
                Card c = new Card(suit, this.rankNames[r - 1], r, filename);

                // Add the created card to the deck.
                fullDeck.addCard(c);
            }
        }
    }

    // Basically, make the currentDeck a copy of the fullDeck.
    public void collectCards()
    {
        this.currentDeck = new Deck();

        // uses the getCards which returns a readonly IEnumerable<Card> type 
        // this lets us page through without giving us the ability to modify the cards or the collection in the Deck

        foreach (Card c in fullDeck.getCards())
        {
            this.currentDeck.addCard(c);
        }

    }

    // Game shuffle is just a shuffle of the current Deck
    public void shuffle()
    {
        currentDeck.shuffle();
    }

    // Hint A3:   Reshuffle Location
    // We said we'd like a CardGame.reshuffle() function.
    // This is a great spot for it.   See Hint A2 for what the function should have inside it.
    public void reshuffle()
    {
        this.collectCards();
        this.shuffle();
    }


    // Deal out 'handSize' cards for every hand -- make sure there are no cards in the hand, 
    // then grab cards off the deck

    public void deal()
    {

        // Hint B1
        // How many cards do I need to deal each time?
        // There's a Class variable for that
        // this.handCount tells us how many hands there are
        // this.handSize tells us how many cards we need per hand.
        // currentDeck.cards.Count tells us how many cards there are in a queue


        // At this point, this nested loop is relatively self-documenting code.


        try
        {
            foreach (Hand h in hands)
            {
                h.clearHand();

                for (int c = 0; c < handSize; c++)
                {
                    h.AddCard(currentDeck.nextCard());
                }

                // Hint C3a
                // Once the hand is fully dealt, need to calculate the hand value
            }

            
        }

        catch(Exception e)
        {
            reshuffle();
            deal();
        }

        // Hint B2
        // Math is hard
        // Approach B1 requires math
        // Wrapping the foreach in a try{ } block is easy, 
        // just try to deal, and if we can't, we'll catch the exception
        // catch() {} block has to:   reshuffle() and call deal().

        // Hint C4
        // Once all the hands are dealt, they should all have a handValue
        // Now is a good time to show the value for each hand 
        // also to figure out the winner
        
    }
}



class Deck
{
    // A deck has a Queue of Cards and a random number generator
    protected Queue<Card> cards;
    protected Random rng;

    // How many riffle shuffles we do when we shuffle
    int riffleCount = 7;

    // Constructor builds a new random number generator and an empty queue of cards
    public Deck()
    {
        cards = new Queue<Card>();
        rng = new Random();
    
    }

    // Shuffle the queue
    public void shuffle()
    {
        // Create a 'left' and 'right' queue of cards
        // (we could actually create two new Decks if we really wanted)

        Queue<Card> left = new Queue<Card>();
        Queue<Card> right = new Queue<Card>();

        // do riffleCount riffles
        for (int i = 0; i < this.riffleCount; i++)
        {
            // For every card in the current deck
            while (this.cards.Count > 0)
            {
                // randomly place the card on the left or the right pile
                if (this.rng.Next(10) < 5)
                {
                    left.Enqueue(this.cards.Dequeue());
                }
                else
                {
                    right.Enqueue(this.cards.Dequeue());
                }
            }

            // put them back in the deck, left half then right half

            while (left.Count > 0)
            {
                this.cards.Enqueue(left.Dequeue());
            }
            while (right.Count > 0)
            {
                this.cards.Enqueue(right.Dequeue());
            }

        }
    }

    public Card nextCard()
    {
        // the next card is just the Dequeue operation -- it takes it off the deck
        return this.cards.Dequeue();
    }

    public void addCard(Card c)
    {
        // add a card to the bottom of the deck
        this.cards.Enqueue(c);
    }

    // Send back the cards, but not as a modifiable Queue<Card>, but rather as the
    // read-only IEnumerable<Card> which can be used in a foreach loop but does not
    // allow changing the collection of cards

    public IEnumerable<Card> getCards()
    {
        return this.cards;
    }
}

class Hand
{
    // A hand has a list of cards, an array of picture boxes and a display panel
    public List<Card> cards;
    protected PictureBox[] cardPictureBoxes;
    public Panel display;

    // We need to know the width of the cards and how many cards there are to display them
    protected int cardWidth = 100;
    protected int cardCount;

    // If they don't all fit, overlap them.   If they don't all fit overlapped, too bad.
     protected bool narrowDisplay;
    protected int firstHandValue = 0;
    protected int secondHandValue = 0;
    // Hint C1
    // If you are going to calculate handValue, need a place to store it
    // good idea to have a protected variable here
    // It should have an initial value of 0.


    // Complicated constructor
    public Hand(int cardCount, Point topLeft, Size panelSize)
    {

        // Now we know how many cards there are
        this.cardCount = cardCount;

        // Create a Panel with the topLeft co-ordinate and size that we are given
        display = new Panel()
        {
            Location = topLeft,
            Size = panelSize
        };

        // build the set of picture Boxes
        cardPictureBoxes = new PictureBox[cardCount];

        // build the list of cards
        cards = new List<Card>();

        // can we fit in the panel ?
        if (cardWidth*1.5 >= panelSize.Height)
        {
            // too big for default height, make cards smaller
            cardWidth = cardWidth / 2;
        }
            
        // Will the cards fit in the width of the panel?        
        if ((cardWidth+10)*cardCount >= panelSize.Width)
        {
            // too wide, toggle narrow
            this.narrowDisplay = true;
        }

        // Need to build the pictureBoxes
        for (int i = 0; i < this.cardPictureBoxes.Length; i++)
        {
            // Figure out how much space between them
            // 10 pixels between them normally, but an 80% overlap if narrowDisplay is true
            int spacing = cardWidth / (narrowDisplay ? 5 : 1) + 10;

            // Given the spacing and the card width, build the picture box's location, size
            // Also make sure that we stretch/shrink the card image to the size of the picture box
            PictureBox p = new PictureBox
            {
                Location = new Point(i * spacing,0),
                Size = new Size(cardWidth, Convert.ToInt32(cardWidth * 1.45)),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            };
            
            // put the new picturebox in the array, and also add to the panel
            this.cardPictureBoxes[i] = p;
            display.Controls.Add(p);
        }

    }

    public void AddCard(Card c)
    {
        int i = 0;
        while (i < 5)
        {
            firstHandValue += c.getNumber();
            i++;
            
        }
        while(i>=5 && i < 10)
        {
            secondHandValue += c.getNumber();
            i++;
        }

       

       
        // Add a card to the list
        cards.Add(c);
        
        // set the image of the corresponding pictureBox
        cardPictureBoxes[cards.Count - 1].Image = c.getImage();

        // bringToFront() so that if we are in narrowDisplay mode the hand looks right
        cardPictureBoxes[cards.Count - 1].BringToFront();

        // Hint C2b
        // If we are adding a card  we need to increase handValue 
        // (method b of calculating handValue)
        // We need to get the number of Card c somehow (check the Card functions)
        // We need to add that to the current value of handValue
    }

    public void clearHand()
    {
        // Take all the cards out of the list
        cards.Clear();

        // Clear the images in all the pictureBoxes
        foreach(PictureBox p in cardPictureBoxes)
        {
            p.Image = null;
        }

    }

    // Hint C2a
    // If we are only calculating handValue once all cards are dealt, 
    // we'll need a function here that does it

}


class Card
{
    protected string suit;
    protected string rank;
    protected int numberRank;
    protected Image image;

    public Card(string mySuit, string myRank, int myNumber, string imageFilename)
    {
        suit = mySuit;
        rank = myRank;
        numberRank = myNumber;

        this.setImage(imageFilename);
    }

    public void setImage(string filename)
    {
        try
        {
            this.image = Image.FromFile(filename);
        }
        catch (FileNotFoundException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public string getSuit() { return suit; }
    public string getRank() { return rank; }
    public int getNumber() { return numberRank; }
    public Image getImage() { return image; }

}
