
namespace ScrabbleEngine
{
    public class Dictionary
    {
        private List<string> dictionary;

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
            string filePath = "ScrabbleWords.txt";
            dictionary = new List<string>();

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
    }
}
