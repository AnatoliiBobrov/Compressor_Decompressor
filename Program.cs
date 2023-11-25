using System;
using System.Linq;
using System.Text;

namespace Task1
{
    public class Program
    {
        
        /// <summary>
        /// Выполняет компрессию строки
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <returns>Строка, подвергнутая компрессии</returns>
        /// <exception cref="ArgumentException">Генерируется в случае неверного ввода символов</exception>
        private static string compressString(string str)
        {
            var result = new StringBuilder();   //нужен для конкатенации символов
            int i = 0;      //счетчик цикла
            var count = 0;  //счетчик подряд идущих символов
            var c = str[0]; //текущий символ

            //Перебор посимвольно
            while (i < str.Length)
            {
                if (('a' <= str[i]) && (str[i]) <= 'z')
                {
                    if (str[i] == c) //если символ совпадает, прибавляем в счетчик
                    {
                        count++;
                    }
                    else //иначе обнуляем счетчик и записываем символ с его количеством
                    {
                        result.Append(c);

                        // если символов подряд идущих 2 и более, после символа записываем количество
                        if (count > 1)
                        {
                            result.Append(count.ToString());
                        }
                        count = 1;
                        c = str[i];
                    }
                }
                // при вводе нелатинской буквы и не прописной буквы генерируется исключение
                else throw new ArgumentException(string.Format("Неожиданный символ '{0}' в позиции {1}", str[i], i + 1));
                i++;
            }

            // Запись последнего символа и его количества
            result.Append(c);

            // если символов подряд идущих 2 и более, после символа записываем количество
            if (count > 1)
            {
                result.Append(count.ToString());
            }

            //вывод результата, ToString() - метод StringBuilder для вывода строки
            return result.ToString();
        }

        /// <summary>
        /// Выполняет декомпрессию строки
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <returns>Строка, подвергнутая декомпрессии</returns>
        /// <exception cref="ArgumentException">Генерируется в случае неверного ввода символов</exception>
        private static string decompressString(string str)
        {
            var result = new StringBuilder();   //нужен для конкатенации символов
            int i = 1;      //счетчик цикла
            var count = ""; //количество символов
            char c = str[0];//текущий символ

            // проверка первого символа
            if (!((('0' <= c) && (c <= '9')) || (('a' <= c) && (c) <= 'z')))
            {
                throw new ArgumentException(string.Format("Неожиданный символ '{0}' в позиции 1", c));
            }


            //Перебор посимвольно
            while (i < str.Length)
            {
                if (('0' <= str[i]) && (str[i] <= '9')) //если это символ, то записываем
                {
                    count += str[i];
                }
                else if (('a' <= str[i]) && (str[i]) <= 'z')//иначе обнуляем количество и записываем символ count раз
                {
                    if (count != "")
                    {
                        result.Append(string.Concat(Enumerable.Repeat<char>(c, int.Parse(count))));
                        count = "";
                    }
                    else // если символ нужно записать однажды
                    {
                        result.Append(c);
                    }
                    c = str[i];
                }
                // при вводе нелатинской буквы и не прописной буквы генерируется исключение
                else throw new ArgumentException(string.Format("Неожиданный символ '{0}' в позиции {1}", str[i], i + 1));
                i++;
            }

            //обработка последнего символа
            if (count != "")
            {
                result.Append(string.Concat(Enumerable.Repeat<char>(c, int.Parse(count))));
            }
            else
            {
                result.Append(c);
            }

            //вывод результата, ToString() - метод StringBuilder для вывода строки
            return result.ToString();
        }


        static void Main(string[] args)
        {
            string input; //строка на обработку
            Console.WriteLine("Для выбора команды введите соответствующую цифру и нажмите клавишу ВВОД. Для выхода введите 3");
            while (true)
            {
                
                Console.Write("Нужно произвести компрессию (1) или декомпрессию (2) строки?:");
                var command = Console.ReadLine();
                if (command == "1") // компрессия строки
                {
                    do
                    {
                        Console.Write("Введите строку, подлежащую компрессии: ");
                        input = Console.ReadLine();
                    }
                    while (input == "");// отсекаем пустые строки
                    try
                    {
                        Console.WriteLine("Строка после компрессии: {0}", compressString(input));
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (command == "2") // декомпрессия строки
                {
                    do
                    {
                        Console.Write("Введите строку, подлежащую декомпрессии: ");
                        input = Console.ReadLine();
                    }
                    while (input == ""); // отсекаем пустые строки
                    try
                    {
                        Console.WriteLine("Строка после компрессии: {0}", decompressString(input));
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (command == "3")  // завершение работы программы
                {
                    return;
                }
                else // предупреждение о неправильно введенной команде
                {
                    Console.WriteLine("Неверно введена команда");
                    continue;
                }
            }
        }
    }
}
