namespace wordle
{
    internal class WordleBot
    {
        private readonly List<string> _answers;

        public WordleBot()
        {
            _answers = File.ReadAllLines("answers.txt").Select(x => x.Trim()).ToList();
        }

        private string Erase(string word, int index)
        {
            var word2 = word.ToCharArray();
            word2[index] = ' ';
            return new string(word2);
        }

        public char[] Score(string guess, string word)
        {
            var result = new char[5];
            for (var i = 0; i < 5; i++)
            {
                if (guess[i] == word[i])
                {
                    result[i] = '2';
                    word = Erase(word, i);
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (word[i] == ' ') continue;

                if (word.Contains(guess[i]))
                {
                    result[i] = '1';
                    guess = Erase(guess, i);
                }
                else
                {
                    result[i] = '0';
                }
            }
            return result;
        }

        public void Play()
        {
            var candidates = _answers;
            var initial = "baste";
            var guess = initial;

            while (true)
            {
                Console.WriteLine(guess);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Clear();
                    candidates = _answers;
                    guess = initial;
                    continue; // once again
                }
                var result = input.ToCharArray();
                candidates = candidates.Where(w => Score(guess, w).SequenceEqual(result)).ToList();
                guess = BestGuess(candidates);
            }
        }

        private string BestGuess(List<string> candidates)
        {
            return candidates.First();
        }
    }
}
