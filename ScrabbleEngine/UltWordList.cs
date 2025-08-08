namespace ScrabbleEngine
{
    internal class UltWordList
    {
        private List<List<Word>> wordList;     

        public List<List<Word>> GetWordList()
        {
            return wordList;
        }

        public UltWordList(List<List<Word>> wordList)
        {
            this.wordList = wordList;
        }

        public UltWordList(List<Word> wordList)
        {
            this.wordList = new List<List<Word>>();
            foreach (Word word in wordList)
            {
                List<Word> words = new List<Word>();
                words.Add(word);
                this.wordList.Add(words);
            }
        }

        public UltWordList()
        {
            wordList = new List<List<Word>>();
        }

        public List<string> ConvertToStringList()
        {
            List<string> resList = new List<string>();            

            for (int i = 0; i < wordList.Count(); i++)
            {
                resList.Add(PrintWordListAt(i) + " " + PointsAt(i));
            }

            return resList;
        }


        public string PrintWordListAt(int pIndex)
        {
            string strResult = "";

            foreach (Word word in wordList[pIndex])
            {
                strResult += word.PrintWordIndexPoints() + " ";
            }

            return strResult;
        }

        public int PointsAt(int pIndex)
        {
            int intResult = 0;

            foreach (Word word in wordList[pIndex])
            {
                intResult += word.Points;
            }

            return intResult;
        }

        // I don't understand what this is doing...
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

            // Rebuild the sorted wordList
            wordList = indexedPoints.Select(x => x.Word).ToList();
        }
    }
}
