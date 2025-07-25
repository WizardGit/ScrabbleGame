
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
            this.value = pCharValue;
            
            switch(pCharValue)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                case 'l':
                case 'n':
                case 's':
                case 't':
                case 'r':
                    this.points = 1;
                    break;
                case 'd':
                case 'g':
                    this.points = 2;
                    break;
                case 'b':
                case 'c':
                case 'm':
                case 'p':
                    this.points = 3;
                    break;
                case 'f':
                case 'h':
                case 'v':
                case 'w':
                case 'y':
                    this.points = 4;
                    break;
                case 'k':
                    this.points = 5;
                    break;
                case 'j':
                case 'x':
                    this.points = 8;
                    break;
                case 'z':
                    this.points = 10;
                    break;
                default:
                    this.points = 0;
                    break;
            }
        }

        public char Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int Points
        {
            get { return points; }
            set { this.points = value; }
        }
    }
}
