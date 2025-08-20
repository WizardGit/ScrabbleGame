
namespace ScrabbleEngine
{
    public class Letter
    {
        public const char NoLetter = '-';
        public const char AnyLetter = '*';

        private char value;
        private int points;

        public Letter(char pCharValue)
        {
            Value = pCharValue;
        }

        public char Value
        {
            get { return this.value; }
            set
            {
                if ((char.IsLetter(value) == true) || (value == Letter.NoLetter) || (value == Letter.AnyLetter))
                    this.value = char.ToLower(value);
                else
                    throw new Exception("Value you are setting letter to is not a valid Letter!");

                switch (this.value)
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                    case 'l':
                    case 'n':
                    case 'r':
                    case 's':
                    case 't':
                        Points = 1;
                        break;
                    case 'd':
                    case 'g':
                        Points = 2;
                        break;
                    case 'b':
                    case 'c':
                    case 'm':
                    case 'p':
                        Points = 3;
                        break;
                    case 'f':
                    case 'h':
                    case 'v':
                    case 'w':
                    case 'y':
                        Points = 4;
                        break;
                    case 'k':
                        Points = 5;
                        break;
                    case 'j':
                    case 'x':
                        Points = 8;
                        break;
                    case 'q':
                    case 'z':
                        Points = 10;
                        break;
                    default:
                        Points = 0;
                        break;
                }
            }
        }

        public int Points
        {
            get { return points; }
            set { this.points = value; }
        }
    }
}
