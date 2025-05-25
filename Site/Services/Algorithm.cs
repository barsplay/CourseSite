class BoyerMoore
{
    public static List<List<int>> BoyerMooreAlg(string text, string pattern)
    {
        // Функция для предварительной обработки правила плохого символа
        Dictionary<char, int> PreprocessBadChar(string pat)
        {
            var badChar = new Dictionary<char, int>();
            for (int i = 0; i < pat.Length; i++)
            {
                badChar[pat[i]] = i;
            }
            return badChar;
        }

        // Функция для предварительной обработки правила хорошего суффикса
        int[] PreprocessGoodSuffix(string pat)
        {
            int m = pat.Length;
            int[] goodSuffix = new int[m + 1];
            int[] border = new int[m + 1];

            // Первый этап: стандартное вычисление
            int i = m;
            int j = m + 1;
            border[i] = j;

            while (i > 0)
            {
                while (j <= m && pat[i - 1] != pat[j - 1])
                {
                    if (goodSuffix[j] == 0)
                    {
                        goodSuffix[j] = j - i;
                    }
                    j = border[j];
                }
                i--;
                j--;
                border[i] = j;
            }

            // Второй этап: дополнительные вычисления
            j = border[0];
            for (i = 0; i <= m; i++)
            {
                if (goodSuffix[i] == 0)
                {
                    goodSuffix[i] = j;
                }
                if (i == j)
                {
                    j = border[j];
                }
            }

            return goodSuffix;
        }

        var occurrences = new List<List<int>>();
        int n = text.Length;
        int m = pattern.Length;

        if (m == 0)
        {
            return occurrences;
        }

        var badChar = PreprocessBadChar(pattern);
        var goodSuffix = PreprocessGoodSuffix(pattern);

        int shift = 0;
        while (shift <= n - m)
        {
            int j = m - 1;
            while (j >= 0 && pattern[j] == text[shift + j])
            {
                j--;
            }

            if (j < 0)
            {
                occurrences.Add(new List<int> { shift, shift + pattern.Length});
                shift += goodSuffix[0];
            }
            else
            {
                int bcShift = j - (badChar.TryGetValue(text[shift + j], out int bcValue) ? bcValue : -1);
                int gsShift = goodSuffix[j + 1];
                shift += Math.Max(bcShift, gsShift);
            }
        }

        return occurrences;
    }
}
