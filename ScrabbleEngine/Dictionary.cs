
namespace ScrabbleEngine
{
    /// <summary>
    /// Utilizes a DAWG (directed acylic word graph for searches if you use the checkword method
    /// </summary>
    public class Dictionary
    {
        private List<string> dictionary;
        private DAWG dictDawg;
        private string filePath = "ScrabbleWords.txt";

        public int Length
        {
            get { return dictionary.Count; }
        }

        public List<string> Dict
        {
            get { return dictionary; }
        }
        public Dictionary()
        {            
            dictionary = new List<string>();
            dictDawg = new DAWG(filePath);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Word list file not found.");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? word;
                while ((word = reader.ReadLine()) != null)
                {
                    dictionary.Add(word.ToLower().Trim());
                }
            }
        }

        public string GetString(int index)
        {
            return this.dictionary[index];
        }

        public Word GetWord(int index)
        {
            return new Word(this.dictionary[index]);
        }

        /// <summary>
        /// Needs testing
        /// </summary>
        /// <param name="pWord"></param>
        /// <returns></returns>
        public bool CheckWordFast(string pWord)
        {
            return dictDawg.Search(pWord);
        }

        public bool CheckWord(string pWord)
        {
            foreach (string word in dictionary)
            {
                if (word == pWord)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Case Sensitive! Checks if pWord is found somewhere within a word in the dictionary
        /// </summary>
        /// <param name="pWord"></param>
        /// <returns></returns>
        public bool CheckWordIn(string pWord)
        {
            foreach (string word in dictionary)
            {
                if (word.Contains(pWord) == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
