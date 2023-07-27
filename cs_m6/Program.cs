using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_m6 {
    internal class Program {
        static void Main(string[] args) {

            const string dbFile = "db1.txt";
            int programMode = 1;
            ushort employeeCount = 1;

            while (true) {
                Console.WriteLine("Добро пожаловать в программу Справочник сотрудников.\n");
                Console.Write("Для вывода данных на экран введите 1, для добавления новой записи введите 2: ");

                bool inputIsNum = int.TryParse(Console.ReadLine(), out programMode);

                if (inputIsNum && programMode == 1) {
                    if (File.Exists(dbFile)) {
                        PrintData();
                        RefreshProgramWindow("Для продолжения нажмите любую клавишу.");
                        continue;

                    } else {
                        RefreshProgramWindow("Ошибка чтения файла! Нажмите любую клавишу.");
                        continue;
                    }
                } else if (inputIsNum && programMode == 2) {
                    WriteData(GetEmpInfo());
                    RefreshProgramWindow("Данные успешно внесены, для продолжения нажмите любую клавишу");
                    continue;
                } else {
                    RefreshProgramWindow("Введена неверная комманда! Программа будет завершена.");
                    break;
                }
            }

            void RefreshProgramWindow(string str) {
                Console.WriteLine(str);
                Console.ReadKey();
                Console.Clear();
            }

            StringBuilder GetEmpInfo() {
                StringBuilder sb = new StringBuilder($"{employeeCount}#");
                sb.Append(DateTime.Now + "#");
                Console.Write("\nВведите Ф.И.О сотрудника: ");
                sb.Append(Console.ReadLine() + "#");
                Console.Write("Укажите возраст сотрудника: ");
                sb.Append(Console.ReadLine() + "#");
                Console.Write("Укажите рост сотрудника: ");
                sb.Append(Console.ReadLine() + "#");
                Console.Write("Введите дату рождение сотрудника: ");
                sb.Append(Console.ReadLine() + "#");
                Console.Write("Введите место рождения сотрудника: ");
                sb.Append(Console.ReadLine() + "#");
                return sb;
            }

            void WriteData (StringBuilder sb) {
                using (StreamWriter sw = new StreamWriter(dbFile, true)) {
                    sw.WriteLine(sb.ToString());
                }
                employeeCount++;
            }

            void PrintData() {
                using (StreamReader sr = new StreamReader(dbFile)) {
                    while (!sr.EndOfStream) {
                        string[] currentLine = sr.ReadLine().Split('#');
                        foreach (var word in currentLine) {
                            Console.Write(word + " ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
