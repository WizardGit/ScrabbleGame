
namespace ScrabbleEngine
{
    public class Square
    {
        public enum BonusType
        {
            nothing,
            doubleLetter,
            tripleLetter,
            doubleWord,
            tripleWord
        }

        private Letter letter;
        private List<Letter> validValues;
        private BonusType bonusType;
        private bool anyLetter;
        public char Value
        {
            get { return letter.Value; }
            set 
            {
                SetLetter(value);
            }
        }

        public BonusType Bonus
        {
            get { return this.bonusType; }
            set { this.bonusType = value; }
        }
        public List<Letter> ValidValues
        {
            get { return this.validValues; }
        }

        public bool Any
        {
            get { return this.anyLetter; }
            set { this.anyLetter = value; }
        }

        public Square(BonusType pBonusType = BonusType.nothing, char pstrValue = Letter.NoLetter) 
        {
            if (pBonusType != BonusType.nothing)
            {
                bonusType = pBonusType;
            }

            validValues = new List<Letter>();
            SetLetter(pstrValue);
        } 

        /// <summary>
        /// Sets the letter letter of our Square, if our letter is empty, it will reset the square letter and validValues list
        /// </summary>
        /// <param name="pCharLetter"></param>
        public void SetLetter(char pCharLetter, bool isAnyLetter = false)
        {
            if (isAnyLetter == true)
                Any = true;

            this.letter = new Letter(pCharLetter);
            validValues = new List<Letter>();            
            if (pCharLetter != Letter.NoLetter)
            {
                validValues.Add(this.letter);
            }
            else
            {
                validValues.Add(new Letter('a'));
                validValues.Add(new Letter('b'));
                validValues.Add(new Letter('c'));
                validValues.Add(new Letter('d'));
                validValues.Add(new Letter('e'));
                validValues.Add(new Letter('f'));
                validValues.Add(new Letter('g'));
                validValues.Add(new Letter('h'));
                validValues.Add(new Letter('i'));
                validValues.Add(new Letter('j'));
                validValues.Add(new Letter('k'));
                validValues.Add(new Letter('l'));
                validValues.Add(new Letter('m'));
                validValues.Add(new Letter('n'));
                validValues.Add(new Letter('o'));
                validValues.Add(new Letter('p'));
                validValues.Add(new Letter('q'));
                validValues.Add(new Letter('r'));
                validValues.Add(new Letter('s'));
                validValues.Add(new Letter('t'));
                validValues.Add(new Letter('u'));
                validValues.Add(new Letter('v'));
                validValues.Add(new Letter('w'));
                validValues.Add(new Letter('x'));
                validValues.Add(new Letter('y'));
                validValues.Add(new Letter('z'));
            }            
        }
        /// <summary>
        /// Returns if the pLetter is in our list of valid values
        /// </summary>
        /// <param name="pLetter">char letter</param>
        /// <returns></returns>
        public bool IsValid(char pLetter)
        {
            if (pLetter == Letter.NoLetter)
            {
                if (validValues.Count > 0)
                    return false;
                else
                    return true;
            }
            else if (pLetter == Letter.AnyLetter)
            {
                if (validValues.Count > 0)
                    return true;
                else
                    return false;
            }

            foreach (Letter l in this.validValues)
            {
                if (l.Value == pLetter)
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Removes the specified letter (value) from our valid vales list
        /// </summary>
        /// <param name="pLetter"></param>
        /// <returns></returns>
        public bool RemoveLetter(Letter pLetter)
        {
            Letter? match = validValues.Find(p => p.Value == pLetter.Value);
            if (match == null)
                return false;
            else
                return validValues.Remove(match);
        }      
        
        /// <summary>
        /// Returns total points on square + the parameter has a multiplication factor for the word that it is part of
        /// </summary>
        /// <param name="multiplicationFactor">The word multiplication factor (none/double/triple word score)</param>
        /// <returns>points for letter</returns>
        public int Points(ref int multiplicationFactor)
        {
            int score;

            if (letter.Value == Letter.NoLetter)
                throw new Exception("Can't calculate points on empty letter");
            else if (Any == true)
                score = 0;
            else
                score = letter.Points;

            switch (Bonus)
            {
                case BonusType.doubleLetter:
                    score *= 2;
                    break;
                case BonusType.tripleLetter:
                    score *= 3;
                    break;
                case BonusType.doubleWord:
                    multiplicationFactor *= 2;
                    break;
                case BonusType.tripleWord:
                    multiplicationFactor *= 3;
                    break;
            }

            return score;
        }
    }
}
