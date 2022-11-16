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

    internal class WorkDictionary
    {
        private List<string> _translation1 = new List<string>() { "Pineapple", "fgas" };
        private Dictionary<string, List<string>> _valuePairs = new();
        private string _way;
        public void CreateDictionary()
        {
            Console.WriteLine($"Введите путь по которому хотите создать файл");
            _way = Console.ReadLine();
            //_way= @"C:\Users\Nikita77227\Desktop\2\D.txt";
            FileInfo finfo = new FileInfo(_way);
            if (!finfo.Exists)
            {
                using (finfo.Create()) ;
            }
            _valuePairs = new Dictionary<string, List<string>>()
            {
                ["Ананас"] = _translation1,
                ["Переводчик"] = new List<string>() { "Translator" },
                ["Зависимость"] = new List<string>() { "Dependence" },
            };
        }
        public async void EntryInitialDictionary()
        {
            using (StreamWriter writer = new StreamWriter(_way, true))
                foreach (var key in _valuePairs.Keys)
                {
                    int i = 1;
                    await writer.WriteAsync($"{key} - ");
                    foreach (var pair in _valuePairs[key])
                    {
                        writer.Write($"{pair}");
                        if (i == _valuePairs[key].Count)
                        {
                            writer.Write($".");
                        }
                        else if (i > 0)
                        {
                            writer.Write($", ");
                        }
                        i++;
                    }
                    writer.WriteLine();
                }
        }
        public void InitialFunction()
        {
            Console.WriteLine($"Введите слово");
            string word = Console.ReadLine();
            bool check = _valuePairs.ContainsKey(word);
            if (check == true)
            {
                Console.WriteLine($"Слово есть в словаре");
            }
            else if (check == false)
            {
                Console.WriteLine($"Такого слова нет введите перевод");
                string translation = Console.ReadLine();
                while (true)
                {
                    Console.WriteLine($"Хотите ли вы записать в словарь это слово и его перевод (введите да или нет)");

                    string enter = Console.ReadLine();
                    if (enter == "да" || enter == "Да")
                    {
                        using (StreamWriter writer = new StreamWriter(_way, true))
                            writer.WriteAsync($"{word} - {translation}.");
                        _valuePairs.Add(translation, new List<string>() { word });
                        break;
                    }
                    else if (enter == "нет" || enter == "Нет")
                    {
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Вы ввели некоректый ответ");
                    }
                }
            }
        }
        public void Add()
        {
            string word = "";
            string translation = "";
            while (true)
            {
                Console.WriteLine($"Введите слово которое хотите добавть");
                word = Console.ReadLine();
                if (word == "" || word == " ")
                {
                    Console.WriteLine($"Строку нельзя оставить пустой");
                }
                else { break; }
            }
            while (true)
            {
                Console.WriteLine($"Введите превод которое хотите добавть");
                translation = Console.ReadLine();
                if (translation == "" || translation == " ")
                {
                    Console.WriteLine($"Строку нельзя оставить пустой");
                }
                else { break; }
            }
            using (StreamWriter writer = new StreamWriter(_way, true))
                writer.WriteAsync($"{word} - {translation}.");
            _valuePairs.Add(word, new List<string>() { translation });
        }
        public void GetDictionary()
        {
            foreach (var pair in _valuePairs)
            {
                int count = 1;
                Console.Write($"Слово - {pair.Key} перевод(ы): ");
                foreach (var i in pair.Value)
                {
                    Console.Write($"{i}");
                    if (count == pair.Value.Count)
                    {
                        Console.Write($".");
                    }
                    else if (count > 0) { Console.Write($", "); }
                    count++;
                }
                Console.WriteLine();
            }
        }
        public void CheckValue()
        {
            Console.WriteLine($"Введите слово перевод которого хотите проверить");
            string checkkey = Console.ReadLine();
            bool verifiedkey = _valuePairs.ContainsKey(checkkey);
            if (verifiedkey == true)
            {
                _valuePairs.TryGetValue(checkkey, out var foundelement);
                Console.WriteLine($"Введите перевод который хотите проверить");
                string value = Console.ReadLine();
                var verifiedvalue = foundelement.Contains(value);
                //bool verifiedvalue = valuePairs.ContainsValue(value);
                if (verifiedvalue == true)
                {
                    Console.WriteLine($"Такой перевод есть в словаре");
                }
                else
                {
                    Console.WriteLine($"Такого перевода нет в словаре");
                }
            }
            else
            {
                Console.WriteLine($"Такого слова нет");
            }
        }
        public void Remove()
        {
            Console.WriteLine($"Введите слово которое хотите удалить");
            string enter = Console.ReadLine();
            bool verifiedvalue = _valuePairs.ContainsKey(enter);
            if (verifiedvalue == true)
            {
                File.WriteAllLines(_way, File.ReadAllLines(_way).Where(v => v.Trim().IndexOf(enter) == -1));
                bool checkremove = _valuePairs.Remove(enter);
                if (checkremove == true)
                {
                    Console.WriteLine($"слово и перевод удалены");
                }
                else
                {
                    Console.WriteLine($"Не удалось удалить слово");
                }
            }
            else
            {
                Console.WriteLine($"Нет такого слова");
            }
        }
        public void Key()
        {
            Console.WriteLine($"Введите слово перевод которого вы хотите заменить");
            string enter = Console.ReadLine();
            _valuePairs.TryGetValue(enter, out var foundelement);
            bool verifiedvalue = _valuePairs.ContainsKey(enter);
            if (verifiedvalue == true)
            {
                Console.WriteLine($"Какой перевод вы хотите заменить");
                string which = Console.ReadLine();
                bool checkvalue = foundelement.Contains(which);
                if (checkvalue == true)
                {
                    Console.WriteLine($"На какой перевод вы хотите заменть");
                    string onwhich = Console.ReadLine();
                    string text = File.ReadAllText(_way);
                    text = text.Replace(which, onwhich);
                    File.WriteAllText(_way, text);
                    foundelement.Remove(which);
                    foundelement.Add(onwhich);
                    Console.WriteLine($"Перевод изменён");
                }
                else
                {
                    Console.WriteLine($"Нет такого перевода");
                }
            }
            else
            {
                Console.WriteLine($"Нет такого слова");
            }
        }
        public void DeleteATranslation()
        {
            Console.WriteLine($"Введите слово перевод которого вы хотите удалить");
            string enter = Console.ReadLine();
            _valuePairs.TryGetValue(enter, out var foundelement);
            bool verifiedvalue = _valuePairs.ContainsKey(enter);
            if (verifiedvalue == true)
            {
                Console.WriteLine($"Введите перевод который хотите удалить");
                string word = Console.ReadLine();
                bool checkvalue = foundelement.Contains(word);
                if (checkvalue == true)
                {
                    if (foundelement.Count > 1)
                    {
                        string text = File.ReadAllText(_way);
                        text = text.Replace(word, "");
                        File.WriteAllText(_way, text);
                        bool a = foundelement.Remove(word);
                        if (a == true)
                        {
                            Console.WriteLine($"Превод удалён");
                        }
                        else
                        {
                            Console.WriteLine($"Перевод не удалён");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Перевод всего один удаление невозможно");
                    }
                }


            }
            else
            {
                Console.WriteLine($"Нет такого слова");
            }
        }
    }
}
