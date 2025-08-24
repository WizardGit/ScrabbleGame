namespace ScrabbleEngine
{
    public class UltWordList
    {
        private List<List<Word>> wordList;

        public int Length
        {
            get { return wordList.Count; }
        }

        public List<List<Word>> GetWordList()
        {
            return wordList;
        }

        /// <summary>
        /// Sets internal wordList to pWordList
        /// If spread == true, then it will only take the first list from the list of list and spread each element in its own list
        /// </summary>
        /// <param name="pWordList"></param>
        /// <param name="pblnSpread"></param>
        public UltWordList(List<List<Word>> pWordList, bool pblnSpread)
        {
            if (pblnSpread == true)
            {
                this.wordList = new List<List<Word>>();
                List<Word> wordList = pWordList[0];

                foreach (Word word in wordList)
                {
                    List<Word> words = new List<Word>();
                    words.Add(word);
                    this.wordList.Add(words);
                }
            }
            else
            {
                this.wordList = pWordList;
            }
        }

        /// <summary>
        /// Sets internal wordList to pWordList
        /// If spread == true, then it will take each object in the list and create a new list just for that word.
        /// Otherwise, it just puts the entire list as the first list of our internallist
        /// </summary>
        /// <param name="wordList"></param>
        /// <param name="pblnSpread"></param>
        public UltWordList(List<Word> wordList, bool pblnSpread)
        {
            this.wordList = new List<List<Word>>();

            if (pblnSpread == true)
            {
                foreach (Word word in wordList)
                {
                    List<Word> words = new List<Word>();
                    words.Add(word);
                    this.wordList.Add(words);
                }
            }
            else
            {
                this.wordList.Add(wordList);
            }
        }

        /// <summary>
        /// If nothing is specified, just set the internal list to a new list of list
        /// </summary>
        public UltWordList()
        {
            wordList = new List<List<Word>>();
        }

        /// <summary>
        /// Prints the words in the list at the specified index
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pblnPoints"></param>
        /// <param name="pblnIndex"></param>
        /// <returns></returns>
        public string PrintWordListAt(int pIndex, bool pblnPoints, bool pblnIndex)
        {
            string strResult = "";

            List<Word> theWordList = wordList[pIndex];

            for (int i = 0; i < theWordList.Count; i++)
            {
                Word word = theWordList[i];
                strResult += word.PrintWord(pblnPoints, pblnIndex);
                if (i < (theWordList.Count - 1))
                {
                    strResult += " & ";
                }
            }

            return strResult;
        }

        public int PointsAt(int pIndex, Board pBoard)
        {
            int intResult = 0;

            foreach (Word word in wordList[pIndex])
            {
                intResult += pBoard.CalculateWordPoints(word);
            }

            return intResult;
        }

        public int PointsAt(int pIndex)
        {
            int intResult = 0;

            foreach (Word word in wordList[pIndex])
            {
                word.CalculatePoints();
                intResult += word.Points;
            }

            return intResult;
        }

        // I don't understand what this is doing...
        /// <summary>
        /// This is for sorting points for a line
        /// </summary>
        /// <param name="pAscending"></param>
        public void SortPoints(bool pAscending)
        {
            // Precompute index + points for each word
            var indexedPoints = wordList
                .Select((word, index) => new
                {
                    Word = word,
                    Index = index,
                    Points = PointsAt(index)
                })
                .ToList();

            // Sort using the precomputed points
            indexedPoints.Sort((a, b) =>
            {
                return pAscending
                    ? a.Points.CompareTo(b.Points)   // Ascending
                    : b.Points.CompareTo(a.Points);  // Descending
            });

            // Rebuild the sorted pWordList
            wordList = indexedPoints.Select(x => x.Word).ToList();
        }

        // I don't understand what this is doing...
        public void SortPoints(bool pAscending, Board pBoard)
        {
            // Precompute index + points for each word
            var indexedPoints = wordList
                .Select((word, index) => new
                {
                    Word = word,
                    Index = index,
                    Points = PointsAt(index, pBoard)
                })
                .ToList();

            // Sort using the precomputed points
            indexedPoints.Sort((a, b) =>
            {
                return pAscending
                    ? a.Points.CompareTo(b.Points)   // Ascending
                    : b.Points.CompareTo(a.Points);  // Descending
            });

            // Rebuild the sorted pWordList
            wordList = indexedPoints.Select(x => x.Word).ToList();
        }
    }
}
