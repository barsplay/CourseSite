using System;
using System.Collections.Generic;
class BoyerMoore
{
    public static List<List<int>> BoyerMooreAlg(string text, string pattern)
    {
        // Функция для предварительной обработки правила плохого символа
        Dictionary<char, int> PreprocessBadChar(string pat)
        {
            var badChar = new Dictionary<char, int>();
            // Проходим по всем символам шаблона
            for (int i = 0; i < pat.Length; i++)
            {
                // Запоминаем последнее вхождение каждого символа
                badChar[pat[i]] = i;
            }
            return badChar;
        }

        // Функция для предварительной обработки правила хорошего суффикса
        int[] PreprocessGoodSuffix(string pat)
        {
            int m = pat.Length;
            int[] goodSuffix = new int[m + 1]; // Массив для хранения сдвигов по правилу хорошего суффикса
            int[] border = new int[m + 1];     // Массив для хранения границ суффиксов

            // Первый этап: стандартное вычисление
            int i = m;  // Индекс текущего символа в шаблоне (идем с конца)
            int j = m + 1; // Индекс для поиска границы
            border[i] = j; // Инициализация границы для последнего символа

            while (i > 0)
            {
                // Ищем границу для текущего суффикса
                while (j <= m && pat[i - 1] != pat[j - 1])
                {
                    // Если хороший суффикс еще не заполнен, сохраняем сдвиг
                    if (goodSuffix[j] == 0)
                    {
                        goodSuffix[j] = j - i;
                    }
                    j = border[j]; // Переходим к следующей границе
                }
                i--;
                j--;
                border[i] = j; // Сохраняем найденную границу
            }

            // Второй этап: дополнительные вычисления для случаев, когда совпадение префикса
            j = border[0];
            for (i = 0; i <= m; i++)
            {
                // Заполняем оставшиеся нулевые значения
                if (goodSuffix[i] == 0)
                {
                    goodSuffix[i] = j;
                }
                // Если достигли текущей границы, переходим к следующей
                if (i == j)
                {
                    j = border[j];
                }
            }

            return goodSuffix;
        }

        var occurrences = new List<List<int>>(); // Список для хранения найденных вхождений
        int n = text.Length;                    // Длина текста
        int m = pattern.Length;                 // Длина шаблона

        // Если шаблон пустой, возвращаем пустой список
        if (m == 0)
        {
            return occurrences;
        }

        // Предварительная обработка правил
        var badChar = PreprocessBadChar(pattern);     // Таблица плохих символов
        var goodSuffix = PreprocessGoodSuffix(pattern); // Массив хороших суффиксов

        int shift = 0; // Текущая позиция сравнения в тексте
        while (shift <= n - m)
        {
            int j = m - 1; // Индекс для сравнения символов (с конца шаблона)

            // Сравниваем символы шаблона и текста справа налево
            while (j >= 0 && pattern[j] == text[shift + j])
            {
                j--;
            }

            // Если весь шаблон совпал
            if (j < 0)
            {
                // Добавляем найденное вхождение (начальный и конечный индексы)
                occurrences.Add(new List<int> { shift, shift + pattern.Length });
                // Сдвигаем шаблон по правилу хорошего суффикса
                shift += goodSuffix[0];
            }
            else
            {
                // Вычисляем сдвиг по правилу плохого символа
                int bcShift = j - (badChar.TryGetValue(text[shift + j], out int bcValue) ? bcValue : -1);
                // Получаем сдвиг по правилу хорошего суффикса
                int gsShift = goodSuffix[j + 1];
                // Выбираем максимальный сдвиг из двух правил
                shift += Math.Max(bcShift, gsShift);
            }
        }

        return occurrences; // Возвращаем список всех найденных вхождений
    }
}
