using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    using System.Text.RegularExpressions;
    using System;
    using System.Xml.Linq;
    using System.IO;
    using System.Transactions;

    internal class WorkDictionary
    {
        private string _way;
        public void SetWay()
        {
            bool checkWay = false;
            while (true)
            {
                if (checkWay == false)
                {
                    Console.WriteLine($"Введите путь по которому хотите создать файл");
                    _way = $@"{Console.ReadLine()}\Dictionery.txt";
                    FileInfo finfo = new FileInfo(_way);
                    if (!finfo.Exists)
                    {
                        try
                        {
                            using (finfo.Create()) ;
                            checkWay = true;
                            Console.WriteLine($"Файл с названием Dictionery создан");
                            Console.WriteLine();
                            break;
                        }
                        catch
                        {
                            Console.WriteLine($"Не удалось создать файл по указанному адресу");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Файл Dictionery уже существует по указанному адресу поэтому чтение и запись будет производиться из него");
                        Console.WriteLine();
                        checkWay = true;
                        break;
                    }
                }
            }
        }
        public void InitialFunction()
        {
            bool checkw = false;
            Console.WriteLine($"Введите слово наличие которого хотите проверь в словаре");
            string word = Console.ReadLine();
            IsTheTextEntered(word, "Слово");
            using (StreamReader reader = new(_way))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split('-', ',', ' ');
                    if (split[0] == word)
                    {
                        Console.WriteLine($"Слово есть в словаре");
                        checkw = true;
                        break;
                    }
                }
            }
            if (checkw != true)
            {
                Console.WriteLine($"Такого слова нет в словаре");
                Console.WriteLine();
            }
        }
        public void Add()
        {
            string word;
            string translation;
            Console.WriteLine($"Введите слово которое хотите добавть");
            word = Console.ReadLine();
            IsTheTextEntered(word, "слово");
            Console.WriteLine($"Введите превод который хотите добавть");
            translation = Console.ReadLine();
            IsTheTextEntered(translation, "перевод");
            using (StreamWriter writer = new(_way, true))
            {
                writer.WriteLineAsync($"{word} - {translation} .");
                Console.WriteLine($"В словарь добавленны слово и его перевод");
            }
        }
        public void GetDictionary()
        {
            using (StreamReader reader = new StreamReader(_way))
            {
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
            }
        }
        public void CheckValue()
        {
            Console.WriteLine($"Введите перевод который хотите проверить");
            string value = Console.ReadLine();
            IsTheTextEntered(value,"перевод");
            bool Tverification = false;
            using (StreamReader reader = new(_way))
            {
                string? line;
                while ((line =  reader.ReadLine()) != null)
                {
                    var split = line.Split(',', '-', ' ');
                    for (int i = 1; i < split.Length; i++)
                    {
                        if (split[i] == value)
                        {
                            Console.WriteLine($"Перевод есть в словаре");
                            Tverification = true;
                            break;
                        }
                    }
                }
                if (Tverification == false)
                {
                    Console.WriteLine($"Такого перевода нет");
                }
            }
        }
        public void Remove()
        {
            Console.WriteLine($"Введите слово которое хотите удалить");
            string enter = Console.ReadLine();
            File.WriteAllLines(_way, File.ReadAllLines(_way).Where(v => v.Trim().IndexOf(enter) == -1).ToArray());
            Console.WriteLine($"Слово и его перевод(ы) удалены");
        }
        public void Replacement()
        {
            string onwhich;
            bool Kcheck = false;
            Console.WriteLine($"Слово перевод которого хотите изменить");
            string key = Console.ReadLine();
            Console.WriteLine($"Какой перевод вы хотите заменить");
            string which = Console.ReadLine();                        
            Console.WriteLine($"На какой перевод вы хотите заменть");
            onwhich = Console.ReadLine();
            IsTheTextEntered(onwhich, "перевод на который вы хотите заменить");            
            string str = string.Empty;
            string read = "";
            using (StreamReader reader = new(_way))
            {

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split('-', ',', ' ');
                    if (split[0] == key)
                    {
                        read = line;
                        str = line;
                        Kcheck = true;
                        break;
                    }
                }
            }
            if (Kcheck == true)
            {
                try
                {
                    str = str.Replace(which, onwhich);
                }
                catch
                {
                    Console.WriteLine($"Вы нечего не ввели");
                    Console.WriteLine();
                }
                string[] readText = File.ReadAllLines(_way);
                for (int i = 0; i < readText.Length; i++)
                {
                    if (readText[i] == read)
                    {
                        readText[i] = str;
                        Console.WriteLine($"Перевод успешно заменён");
                        break;
                    }
                }
                File.WriteAllLines(_way, readText);
            }
            else
            {
                Console.WriteLine($"Перевод не найден в библиотеке");
            }
        }
        public void DeleteATranslation()
        {
            string read = "";
            string str = "";
            bool Tcheck = false;
            bool numbert = false;
            Console.WriteLine($"Введите слово перевод которого вы хотите удалить");
            string enter = Console.ReadLine();
            Console.WriteLine($"Введите перевод который хотите удалить");
            string word = Console.ReadLine();
            using (StreamReader reader = new(_way))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split('-', ',', ' ', '.');
                    if (split[0] == enter)
                    {
                        int notempty = 0;
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i] != "")
                            {
                                notempty++;
                            }
                        }
                        if (notempty > 2)
                        {
                            read = line;
                            str = line;
                            Tcheck = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Перевод всего один удаление невозможно");
                            numbert = true;
                            break;
                        }
                    }
                }
            }
            if (Tcheck == true)
            {
                str = str.Replace(word, "");
                string[] readText = File.ReadAllLines(_way);
                for (int i = 0; i < readText.Length; i++)
                {
                    if (readText[i] == read)
                    {
                        readText[i] = str;
                        Console.WriteLine($"Перевод удалён");
                        break;
                    }
                }
                File.WriteAllLines(_way, readText);
            }
            else if (Tcheck == false && numbert == false)
            {
                Console.WriteLine($"Слово или его перевод не найдены в библиотеке");
            }

        }
        private void IsTheTextEntered(string word, string text)
        {
            int number= 1;
            while (true)
            {
                if (number > 1)
                {
                    Console.WriteLine($"Введите {text}");
                }
                if (word == "" || word == " ")
                {
                    Console.WriteLine($"вы нечего не ввели");
                    Console.WriteLine();
                    number++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
