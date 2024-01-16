using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        const string fileName = @"E:\Oix\Projects\Vent\Students.dat";
        static void Main(string[] args)
        {
            if (File.Exists(fileName))
            {
                var formatter = new BinaryFormatter();

                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    try
                    {
                        var students = (Student[])formatter.Deserialize(fs);

                        MoveToGroups(students);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private static void MoveToGroups(Student[] students)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var pathStudents = Path.Combine(desktopPath, "Students");
            //Если что, CreateDirectory проверяет наличие папки перед созданием и новую не создаст, если она есть и содержимое не заменит
            Directory.CreateDirectory(pathStudents);
            var groups = students.GroupBy(a => a.Group).ToList();
            foreach ( var group in groups)
            {
                
                using (var sw = File.CreateText(Path.Combine(pathStudents, group.Key + ".txt")))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{group.Key}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    var groupStudents = students.Where(a => a.Group.Equals(group.Key)).ToList();
                    foreach (var student in groupStudents)
                    {
                        sw.WriteLine($"{student.Name}, {student.DateOfBirth.ToString("d", CultureInfo.CurrentCulture)}");
                        Console.WriteLine($"{student.Name}, {student.DateOfBirth.ToString("d", CultureInfo.CurrentCulture)}");
                    }
                }

            }

           



            

        }
    }
}
