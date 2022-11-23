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
    Console.WriteLine();
}
void UseMenu()
{
    dictionary.SetWay();
    dictionary.InitialFunction();
    while (true)
    {
        Menu();
        string enter = Console.ReadLine();
        bool check = int.TryParse(enter, out int chec);
        Console.WriteLine();
        if (chec == 1)
        {
            dictionary.Add();
            continue;
        }
        else if (chec == 2)
        {
            dictionary.GetDictionary();

        }
        else if (chec == 3)
        {
            dictionary.CheckValue();
            continue;
        }
        else if (chec == 4)
        {
            dictionary.Remove();
            continue;
        }
        else if (chec == 5)
        {
            dictionary.Replacement();
            continue;
        }
        else if (chec == 6)
        {
            dictionary.DeleteATranslation();
            continue;
        }
        else
        {
            Console.WriteLine($"Выберите пункт из меню");
        }
    }
}
UseMenu();