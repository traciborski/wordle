namespace wordle
{
    internal class WordlePlayer
    {
        private readonly string _word;

        public WordlePlayer()
        {
            var answers = File.ReadAllLines("answers.txt").Select(x => x.Trim()).ToList();
            _word = answers[(int)new Random().NextInt64(answers.Count)];
        }

        public char[] Report(string guess)
        {
            var result = new char[5];
            for (var i = 0; i < 5; i++)
                if (guess[i] == _word[i])
                    result[i] = '2';
                else if (_word.Contains(guess[i]))
                    result[i] = '1';
                else
                    result[i] = '0';
            return result;
        }

        public void Play()
        {
            while (true)
            {
                var guess = Console.ReadLine()?.Trim() ?? "";
                if (guess.Length != 5)
                {
                    Console.WriteLine("not nice");
                    Console.WriteLine(_word);
                    break;
                }

                Console.WriteLine(Report(guess));
                if (guess == _word)
                {
                    Console.WriteLine("Nice !!!");
                    break;
                }
            }
        }
    }
}
