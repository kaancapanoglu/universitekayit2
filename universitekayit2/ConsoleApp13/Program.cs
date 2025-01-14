using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Sistem başlatma
            DataManager.InitializeFiles();

            // Verileri yükle
            var instructors = DataManager.LoadData<List<Instructor>>("instructors.json") ?? new List<Instructor>();
            var students = DataManager.LoadData<List<Student>>("students.json") ?? new List<Student>();
            var courses = DataManager.LoadData<List<Course>>("courses.json") ?? new List<Course>();

            // Konsol menüsü
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Öğrenci ve Ders Yönetim Sistemi ===");
                Console.WriteLine("1. Öğretim Görevlisi Ekle");
                Console.WriteLine("2. Öğretim Görevlisi ve Dersleri Sil");
                Console.WriteLine("3. Öğrenciyi Derse Kaydet");
                Console.WriteLine("4. Dersler ve Öğrencileri Görüntüle");
                Console.WriteLine("5. Dersteki Öğrenciyi Sil");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.Write("Öğretim Görevlisinin Adı: ");
                    string name = Console.ReadLine();

                    var instructor = new Instructor(name);

                    Console.Write("Kaç ders eklemek istiyorsunuz? ");
                    int courseCount = GetValidIntInput();

                    for (int i = 0; i < courseCount; i++)
                    {
                        Console.Write($"{i + 1}. Dersin Adı: ");
                        string courseName = Console.ReadLine();
                        Console.Write($"{i + 1}. Dersin Kredisi: ");
                        int credit = GetValidIntInput();

                        var course = new Course(courseName, credit, instructor);
                        courses.Add(course);

                        Console.WriteLine($"{courseName} dersi başarıyla {instructor.Name} öğretim görevlisine eklendi.");
                    }

                    instructors.Add(instructor);
                    DataManager.SaveData("instructors.json", instructors);
                    DataManager.SaveData("courses.json", courses);

                    Console.WriteLine("Öğretim görevlisi ve dersleri başarıyla eklendi.");
                    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                    Console.ReadKey();
                    continue;
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Silmek istediğiniz öğretim görevlisini seçin:");
                    for (int i = 0; i < instructors.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {instructors[i].Name}");
                    }

                    int instructorChoice = GetValidInput(instructors.Count) - 1;
                    var instructorToRemove = instructors[instructorChoice];

                    courses.RemoveAll(c => c.Instructor.Name == instructorToRemove.Name);
                    instructors.Remove(instructorToRemove);

                    DataManager.SaveData("instructors.json", instructors);
                    DataManager.SaveData("courses.json", courses);

                    Console.WriteLine("Öğretim görevlisi ve dersleri başarıyla silindi.");
                    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                    Console.ReadKey();
                    continue;
                }
                else if (choice == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Bir ders seçmek için sırasını girin:");
                    for (int i = 0; i < courses.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {courses[i].Name}");
                    }

                    int courseChoice = GetValidInput(courses.Count) - 1;
                    var selectedCourse = courses[courseChoice];

                    Console.Write("Öğrencinin Adı: ");
                    string studentName = Console.ReadLine();
                    Console.Write("Öğrencinin ID'si: ");
                    int studentId = GetValidIntInput();

                    var student = new Student(studentName, studentId);
                    selectedCourse.RegisterStudent(student);
                    students.Add(student);

                    DataManager.SaveData("students.json", students);
                    DataManager.SaveData("courses.json", courses);

                    Console.WriteLine($"{studentName}, {selectedCourse.Name} dersine başarıyla kayıt oldu.");
                    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                    Console.ReadKey();
                    continue;
                }
                else if (choice == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Dersler ve kayıtlı öğrenciler:");
                    foreach (var course in courses)
                    {
                        course.ShowCourseInfo();
                    }
                    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                    Console.ReadKey();
                    continue;
                }
                else if (choice == "5")
                {
                    Console.Clear();
                    Console.WriteLine("Bir ders seçmek için sırasını girin:");
                    for (int i = 0; i < courses.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {courses[i].Name}");
                    }

                    int courseChoice = GetValidInput(courses.Count) - 1;
                    var selectedCourse = courses[courseChoice];

                    Console.WriteLine("Silmek istediğiniz öğrenciyi seçin:");
                    for (int i = 0; i < selectedCourse.RegisteredStudents.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {selectedCourse.RegisteredStudents[i].Name}");
                    }

                    int studentChoice = GetValidInput(selectedCourse.RegisteredStudents.Count) - 1;
                    var studentToRemove = selectedCourse.RegisteredStudents[studentChoice];

                    selectedCourse.RegisteredStudents.Remove(studentToRemove);
                    DataManager.SaveData("courses.json", courses);

                    Console.WriteLine($"{studentToRemove.Name} başarıyla silindi.");
                    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                    Console.ReadKey();
                    continue;
                }
                else if (choice == "6")
                {
                    Console.WriteLine("Çıkış yapılıyor...");
                    break;
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim! Devam etmek için bir tuşa basın...");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        private static int GetValidInput(int maxOption)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > maxOption)
            {
                Console.WriteLine($"Lütfen 1 ile {maxOption} arasında geçerli bir sayı girin:");
            }
            return input;
        }

        private static int GetValidIntInput()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Lütfen geçerli bir sayı girin:");
            }
            return input;
        }
    }

    public interface IPerson
    {
        void ShowInfo();
    }

    public abstract class Person : IPerson
    {
        public string Name { get; set; }

        public Person() { }

        public Person(string name)
        {
            Name = name;
        }

        public abstract void ShowInfo();
    }

    public class Student : Person
    {
        public int ID { get; set; }

        public Student() { }

        public Student(string name, int id) : base(name)
        {
            ID = id;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Öğrenci Adı: {Name}, ID: {ID}");
        }
    }

    public class Instructor : Person
    {
        public Instructor() { }

        public Instructor(string name) : base(name) { }

        public override void ShowInfo()
        {
            Console.WriteLine($"Öğretim Görevlisi Adı: {Name}");
        }
    }

    public class Course
    {
        public string Name { get; set; }
        public int Credit { get; set; }
        public Instructor Instructor { get; set; }
        public List<Student> RegisteredStudents { get; set; }

        public Course() { }

        public Course(string name, int credit, Instructor instructor)
        {
            Name = name;
            Credit = credit;
            Instructor = instructor;
            RegisteredStudents = new List<Student>();
        }

        public void ShowCourseInfo()
        {
            Console.WriteLine($"Ders: {Name} (Kredi: {Credit}), Öğretim Görevlisi: {Instructor.Name}");
            Console.WriteLine("Kayıtlı Öğrenciler:");
            foreach (var student in RegisteredStudents)
            {
                Console.WriteLine($" - {student.Name}");
            }
        }

        public void RegisterStudent(Student student)
        {
            RegisteredStudents.Add(student);
        }
    }

    public static class DataManager
    {
        public static void InitializeFiles()
        {
            if (!File.Exists("students.json")) File.WriteAllText("students.json", "[]");
            if (!File.Exists("instructors.json")) File.WriteAllText("instructors.json", "[]");
            if (!File.Exists("courses.json")) File.WriteAllText("courses.json", "[]");
            if (!File.Exists("students.xml")) File.WriteAllText("students.xml", SerializeToXml(new List<Student>()));
            if (!File.Exists("instructors.xml")) File.WriteAllText("instructors.xml", SerializeToXml(new List<Instructor>()));
            if (!File.Exists("courses.xml")) File.WriteAllText("courses.xml", SerializeToXml(new List<Course>()));
        }

        public static void SaveData<T>(string fileName, T data)
        {
            if (fileName.EndsWith(".json"))
            {
                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
            else if (fileName.EndsWith(".xml"))
            {
                File.WriteAllText(fileName, SerializeToXml(data));
            }
        }

        public static T LoadData<T>(string fileName)
        {
            if (!File.Exists(fileName)) return default;

            if (fileName.EndsWith(".json"))
            {
                var json = File.ReadAllText(fileName);

                // JSON içeriğini doğrula ve geçersiz karakterleri temizle
                json = json.Replace("\r", "\\r").Replace("\n", "\\n");

                try
                {
                    return JsonSerializer.Deserialize<T>(json);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON dosyası ({fileName}) çözümlenirken bir hata oluştu: {ex.Message}");
                    return default;
                }
            }
            else if (fileName.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(fileName);
                return DeserializeFromXml<T>(xml);
            }

            return default;
        }

        private static string SerializeToXml<T>(T data)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringWriter, data);
                return stringWriter.ToString();
            }
        }

        private static T DeserializeFromXml<T>(string xml)
        {
            using (var stringReader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}
