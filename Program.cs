using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolProvodnik
{
    internal class Program
    {
        static string currentDirectory = Directory.GetCurrentDirectory();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                DisplayCurrentDirectory();
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1.Перейти в подкаталог");
                Console.WriteLine("2.Вернуться в родительский каталог");
                Console.WriteLine("3.Открыть файл");
                Console.WriteLine("4.Создать новый каталог");
                Console.WriteLine("5.Создать новый файл");
                Console.WriteLine("6.Удалить файл или каталог");
                Console.WriteLine("7.Выход");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        EnterSubDirectory();
                            break;
                    case "2":
                        GoToParentDirectory();
                            break;
                    case "3":
                        OpenFile();
                            break;
                    case "4":
                        CreateDirectory();
                            break;
                    case "5":
                        CreateFile();
                            break;
                    case "6":
                        DeleteFileOrDirectory();
                            break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        static void DisplayCurrentDirectory()
        {
            Console.WriteLine($"Текущий каталог: {currentDirectory}");
            Console.WriteLine("Содержимое: ");

            var directories = Directory.GetDirectories(currentDirectory);
            foreach(var dir in directories)
            {
                Console.WriteLine($"[D] {Path.GetFileName(dir)}");
            }

            var files = Directory.GetFiles(currentDirectory);
            foreach( var file in files)
            {
                Console.WriteLine($"[F] {Path.GetFileName(file)}");
            }
        }
        static void EnterSubDirectory()
        {
            Console.Write("Введите имя подкаталога: ");
            var subDir = Console.ReadLine();
            var newPath = Path.Combine(currentDirectory, subDir);

            if (Directory.Exists(newPath))
            {
                currentDirectory = newPath;
            }
            else
            {
                Console.WriteLine("Подкаталог не найден");
            }
            Console.ReadKey();
        }
        static void GoToParentDirectory()
        {
            var parentDir = Directory.GetParent(currentDirectory);
            if (parentDir != null)
            {
                currentDirectory = parentDir.FullName;
            }
            else
            {
                Console.WriteLine("Вы находитесь в корневом каталоге.");
            }
            Console.ReadKey();
        }
        static void OpenFile()
        {
            Console.Write("Введите имя файла для открытия: ");
            var fileName = Console.ReadLine();
            var filePath = Path.Combine (currentDirectory, fileName);

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                Console.WriteLine($"Содержимое файла {fileName}:\n{content}");
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
            Console.ReadKey();
        }
        static void CreateDirectory()
        {
            Console.Write("Введите имя нового каталога: ");
            var dirName = Console.ReadLine();
            var newPath = Path.Combine(currentDirectory, dirName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
                Console.WriteLine("Каталог успешно создан.");
            }
            else
            {
                Console.WriteLine("Каталог с таким именем уже существует.");
            }
            Console.ReadKey();
        }
        static void CreateFile()
        {
            Console.Write("Введите имя нового файла: ");
            var fileName = Console.ReadLine();
            var filepath = Path.Combine(currentDirectory, fileName);

            if (!File.Exists(filepath))
            {
                Console.WriteLine("Введите текст для записи в файл: ");
                var content = Console.ReadLine();
                File.WriteAllText(filepath, content);
                Console.WriteLine("Файл успешно создан.");
            }
            else
            {
                Console.WriteLine("Файл с таким именем уже существует.");
            }
            Console.ReadKey();
        }
        static void DeleteFileOrDirectory()
        {
            Console.Write("Введите имя файла или каталога для удаления: ");
            var name = Console.ReadLine();
            var path = Path.Combine(currentDirectory, name);

            if(!File.Exists(path))
            {
                Console.Write($"Вы уверены, что хотите удалить файл '{name}'? (yes/no): ");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    File.Delete(path);
                    Console.WriteLine("Файл успешно удален");
                }
            }
            else if (Directory.Exists(path))
            {
                Console.Write($"Вы уверены, что хотите удалить каталог '{name}'? (yes/no): ");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    File.Delete(path);
                    Console.WriteLine("Каталог успешно удален");
                }
            }
            else
            {
                Console.WriteLine("Файл или каталог не найден");
            }

            Console.ReadKey();
        }
    }
}
