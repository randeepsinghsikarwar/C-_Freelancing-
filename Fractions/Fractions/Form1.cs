using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// Required for serialization

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Fractions
{
    public partial class Form1 : Form
    {

        Fraction a, b;
        Point p;

        public Form1()
        {
            InitializeComponent();
            fractionCountLabel.Text = Fraction.fractionCount.ToString();
        }

        // Graphical user interface code to show various operations on Fractions
        private void button1_Click(object sender, EventArgs e)
        {
            int aNum = 1;
            int aDen = 2;
            int bNum = 1;
            int bDen = 3;

            int px = 3;
            int py = 4;


            if (Int32.TryParse(textBox1.Text, out aNum) && Int32.TryParse(textBox2.Text, out aDen))
            {
                a = new Fraction(aNum, aDen);
            }
            else
            {
                textBox1.Text = "1";
                textBox2.Text = "2";
                a = new Fraction(1, 2);
            }

            if (Int32.TryParse(textBox3.Text, out bNum) && Int32.TryParse(textBox4.Text, out bDen))
            {
                b = new Fraction(bNum, bDen);
            }
            else
            {
                textBox3.Text = "1";
                textBox4.Text = "3";
                b = new Fraction(1, 3);
            }

            if (Int32.TryParse(textBox5.Text, out px) && Int32.TryParse(textBox6.Text, out py))
            {
                p = new Point(px, py);
            }
            else
            {
                textBox5.Text = "3";
                textBox6.Text = "4";
                p = new Point(3, 4);
            }

            AToStringLabel.Text = a.ToString();
            BToStringLabel.Text = b.ToString();
            PToString.Text = p.ToString();

            doubleALabel.Text = ((double)a).ToString("F5");
            doubleBLabel.Text = ((double)b).ToString("F3");
            fractionPLabel.Text = ((Fraction)p).ToString();


            minusALabel.Text = (-a).ToString();
            minusBLabel.Text = (-b).ToString();

            AplusBLabel.Text = (a + b).ToString();
            AminusBLabel.Text = (a - b).ToString();
            AtimesBLabel.Text = (a * b).ToString();
            AdivBLabel.Text = (a / b).ToString();

            minusARLabel.Text = (-a).reduce().ToString();
            minusBRLabel.Text = (-b).reduce().ToString();

            AplusBRLabel.Text = (a + b).reduce().ToString();
            AminusBRLabel.Text = (a - b).reduce().ToString();
            AtimesBRLabel.Text = (a * b).reduce().ToString();
            AdivBRLabel.Text = (a / b).reduce().ToString();

            fractionCountLabel.Text = Fraction.fractionCount.ToString();

            MessageBox.Show("About to serialize and deserialize.  Watch the fraction Count!");

            // Write fraction a to disk

            using (FileStream fs = new FileStream("../../save.dat", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, a);
                 fs.Close();

            }

            // Read fraction c from disk

            using (FileStream fs = new FileStream("../../save.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Fraction c = (Fraction)bf.Deserialize(fs);
   
                fs.Close();
                CToStringLabel.Text = c.ToString();

            }

            fractionCountLabel.Text = Fraction.fractionCount.ToString();

        }
    }

    // Lab 0.25  Add implicit conversion for integers to become Fractions (hint -- denominator is 1)
    // Lab 0.5  Implement IFormattable on Fraction -- Hint:  ignore provider.  "G" -> default -> num/den.  "M" -> mixed number -- e.g. 15/12 -> 1 3/12.  Hint -- use integer / and % (mod)
    // Lab 0.25  Implement IComparable on Fraction -- Hint:  compare them as doubles
    // Lab 0.25  Add an extension to invert a fraction
    // Lab 0.50  Fraction is Serializable. Add a save and load button
    // Lab 0.5 Operator overloading -- %
    // Lab 0.25  Operator overloading -- Fraction and int

    [Serializable]
    sealed public class Fraction : ISerializable
    {
        // Don't want to change once set -- fractions can't be changed.

        // store the numerator and the denominator -- that is sufficient to define a fraction.

        public readonly int num;
        public readonly int den;

        // have a static variable to store the number of fractions that are floating about in the program.
        public static int fractionCount;

        // We create a fraction by storing the numerator and the denominator.
        // The denominator cannot be zero.
        // Every time we use the constructor, increment the fractionCount
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
            }
            num = numerator;
            den = denominator;
            fractionCount++;
        }

        // Unary plus -- this doesn't get used a lot....
        public static Fraction operator +(Fraction a) => a;

        public static Fraction operator +(Fraction a, int i)
            => new Fraction(a.num + i * a.den, a.den);

        // Unary minus -- negative of the numerator, same denominator
        public static Fraction operator -(Fraction a) => new Fraction(-a.num, a.den);

        // Add two fractions by getting a common denominator (the product), adjusting each numerator appropriately, then adding them.
        public static Fraction operator +(Fraction a, Fraction b)
            => new Fraction(a.num * b.den + b.num * a.den, a.den * b.den);

        // Hint C:  overload + again, but this time with +(Fraction a, int b).   Remember, an integer is just a fraction with a denomiator of 1.

        // Subtraction is just adding the negative of b
        public static Fraction operator -(Fraction a, Fraction b)
            => a + (-b);

        // To multiply fractions, just multiply the tops and multiply the bottoms
        public static Fraction operator *(Fraction a, Fraction b)
            => new Fraction(a.num * b.num, a.den * b.den);

        // To divide fractions, make sure that b isn't 0, then it is a * b flipped on its head
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.num == 0)
            {
                throw new DivideByZeroException();
            }
            return new Fraction(a.num * b.den, a.den * b.num);
        }

        public static Fraction operator %(Fraction a, Fraction b)
        {
            Fraction c = b;
            while (c > b)
            {
                a = a - b;
                c = a;
            }
            return c;
            
        }


        // implicit conversion: turn a fraction into a double (through division)
        public static implicit operator double (Fraction f) => (double) f.num / (double) f.den;

        // Hint A:  you can implicitly turn an int into a Fraction.  The denominator is always 1.

        public static implicit operator int(Fraction f) => f.num / 1;

        // explicit converison:  turn a point into a fraction.
        // This has to be explicit, because if Y = 0, then the fraction creation will throw an exception.
        public static explicit operator Fraction (Point p) => new Fraction(p.X,p.Y);


        // For custom serialization, we specify the name, value, and type of key data that is required to reconstitute the object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("numerator", num, typeof(int));
            info.AddValue("denominator", den, typeof(int));
        }

        // For custom deserialization we need a constructor with exactly this signature
        // which can get the key data back out of the SerializationInfo object.
        public Fraction(SerializationInfo info, StreamingContext context)
        {
            num = (int)info.GetValue("numerator", typeof(int));
            den = (int)info.GetValue("denominator", typeof(int));
            fractionCount++;
        }


        // We just show the numerator and the denominator
        public override string ToString() => $"{num} / {den}";
    }


    // Nice Extension Method to turn any fraction into its reduced fraction

    public static class ExtensionExamples
    {

        // Hint B -- you can add an invert extension here, which will flip the numerator and denominator
        public static Fraction invert(this Fraction f)
        {
            int num = f.den;
            int den = f.num;

            return new Fraction                                  (num, den);
        }
        

        public static Fraction reduce(this Fraction f)
        {
            int num = f.num;
            int den = f.den;

            // reduced fractions have positive denominators
            if (f.den < 0)
            {
                num *= -1;
                den *= -1;
            }

            // we want to remove common factors from the numerator and the denominator
            // There are better ways to do this, but this one should work.
            // Start with the smaller number and go down to 2
            // See if each number is a common factor of both num and den
            // If so, divide each by the number (integer division) and start again.
            // If you get to the end without finding a common factor, you are done.

            bool notdone = false;

            do
            {
                int smaller = num > den ? num : den;
                notdone = false;

                for (int i = smaller; i >= 2; i--)
                {
                    if (num % i == 0 && den % i == 0)
                    {
                        num /= i;
                        den /= i;
                        notdone = true;
                        break;
                    }
                }
            } while (notdone);

            return new Fraction(num, den);
        }
    }


}
public class Fraction : IComparable<Fraction>
{
    public int Numerator { get; set; }
    public int Denominator { get; set; }

    public Fraction(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public double ToDouble()
    {
        return (double)Numerator / Denominator;
    }

    public int CompareTo(Fraction other)
    {
        if (other == null)
        {
            return 1;
        }

        double thisDouble = ToDouble();
        double otherDouble = other.ToDouble();

        if (thisDouble > otherDouble)
        {
            return 1;
        }
        else if (thisDouble < otherDouble)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}



