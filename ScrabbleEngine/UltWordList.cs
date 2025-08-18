namespace ScrabbleEngine
{
    internal class UltWordList
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

        public UltWordList(List<List<Word>> wordList)
        {
            this.wordList = wordList;
        }

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

        public UltWordList()
        {
            wordList = new List<List<Word>>();
        }

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
