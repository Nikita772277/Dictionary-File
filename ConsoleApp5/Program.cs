using ConsoleApp5;

WorkDictionary dictionary = new WorkDictionary();
void Menu()
{
    Console.WriteLine();
    Console.WriteLine($"1) Добавить слово и его перевод");
    Console.WriteLine($"2) Получить все данные словаря");
    Console.WriteLine($"3) Проверка наличия перевода слова");
    Console.WriteLine($"4) Удалить слово и его перевод(ы)");
    Console.WriteLine($"5) Заменить перевод конкретного слова");
    Console.WriteLine($"6) Удалить конкретный перевод");
    Console.WriteLine($"7) Проверка наличия слова");
    Console.WriteLine();
}
void UseMenu()
{
    dictionary.SetWay();
    while (true)
    {
        Menu();
        string enter = Console.ReadLine();
        bool check = int.TryParse(enter, out int chec);
        Console.WriteLine();
        if (chec == 1)
        {
            dictionary.Add();
            return; 
        }
        else if (chec == 2)
        {
            dictionary.GetDictionary();
            return; 
        }
        else if (chec == 3)
        {
            dictionary.CheckValue();
            return;
        }
        else if (chec == 4)
        {
            dictionary.Remove();
            return;
        }
        else if (chec == 5)
        {
            dictionary.Replacement();
            return;
        }
        else if (chec == 6)
        {
            dictionary.DeleteATranslation();
            return;
        }
        else if (chec == 7)
        {
            dictionary.InitialFunction();
            return;
        }
        else
        {
            Console.WriteLine($"Выберите пункт из меню");
        }
    }
}
UseMenu();