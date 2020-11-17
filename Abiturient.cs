using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Laba3 {
	class Abiturient {
		// Кол-во созданных абитуриентов
		public static int Counter { get; private set; }

		// Данные об абитуриенте
		public string Id { get; private set; }
		public string Surname { get; private set; }
		public string Name { get; private set; }
		public string Patronymic { get; private set; }
		public int Group { get; private set; }
		public Dictionary<string, int?> Marks { get; private set; }

		public void InputMarks(Dictionary<string, int?> marks) {
			if (Marks == null)
				Marks = new Dictionary<string, int?>(marks);
			else
				foreach (var item in marks) {
					if (Marks.ContainsKey(item.Key))
						Marks[item.Key] = item.Value;
					else
						Marks.Add(item.Key, item.Value);
				}
		}

		// Конструктор с параметрами
		public Abiturient(string id, string surname, string name, string patronymic, int group, Dictionary<string, int?> marks) {
			Id = id;
			Surname = surname;
			Name = name;
			Patronymic = patronymic;
			Group = group;
			Marks = new Dictionary<string, int?>(marks);
			Counter++;
		}

		// Конструктор с параметрами по умолчанию
		public Abiturient(string id, string surname, string name, string patronymic = "Не указано", int group = 1) {
			Id = id;
			Surname = surname;
			Name = name;
			Patronymic = patronymic;
			Group = group;
			Marks = new Dictionary<string, int?>();
			Counter++;
		}

		// Поиск абитуриента по id или фамилии
		public static List<Abiturient> GetAbiturient(List<Abiturient> list, string data) {
			var result = new List<Abiturient>();
			foreach (var item in list) {
				if (item.Id == data) {
					result.Add(item);
					break;
				}
				if (item.Surname == data)
					result.Add(item);
			}
			return result;
		}

		public static void PrintList(List<Abiturient> list, PrintType type = 0) {
			int counter = 0;
			switch (type) {
				case PrintType.Short:
					Console.WriteLine("  №      ID           Фамилия             Имя          Группа    Средний балл\n");
					foreach (var item in list) {
						Console.WriteLine($"{++counter,4})  {item.Id,6}      {item.Surname,-13}      {item.Name,-11}         {item.Group}      {item.GetAvg(),8:F2}");
					}
					break;
				case PrintType.Full:
					foreach (var item in list) {
						Console.WriteLine(
						$"\n{++counter,4})    ID: {item.Id}" +
						$"\nФИО: {item.Surname} {item.Name} {item.Patronymic}" +
						$"\nГруппа: {item.Group}" +
						$"\nСредний балл: {item.GetAvg(),3:F2}" +
						$"\nМинимальный/максимальный балл: {item.Marks.Values.Min()}/{item.Marks.Values.Max()}" +
						$"\nСписок оценок:"
						);
						foreach (var mark in item.Marks) {
							Console.WriteLine(
								$"{mark.Key,-24}{mark.Value}");
						}
					}
					break;
				default:
					break;
			}
		}

		// LINQ

		// Средний балл
		public double GetAvg() {
			if (Marks == null)
				return 0;
			var avg = Marks.Values.Average();
			if (avg.HasValue)
				return avg.Value;
			else
				return 0;
		}

		// Список с неудовлетворительными оценками
		public static List<Abiturient> GetUnderperforming(List<Abiturient> list) =>
			list.Where(item => item.Marks.Values.Any(mark => mark < 4)).ToList();

		// Список с суммой оценок выше заданной
		public static List<Abiturient> GetMoreThan(List<Abiturient> list, int userSum) =>
			list.Where(item => item.Marks.Values.Sum() > userSum).ToList();

		// Кол-во с 10 по определённому предмету
		public static int GetAmtOfSmartInSubject(List<Abiturient> list, string subject) =>
			list.Count(item => item.Marks[subject] == 10);

		// Упороядоченный по алфавиту
		public static List<Abiturient> GetOrdered(List<Abiturient> list) =>
			list.OrderBy(item => item.Surname).ThenBy(item => item.Name).ToList();

		// 4 последних с низкой успеваемостью
		public static List<Abiturient> GetWorst(List<Abiturient> list) =>
			list.OrderByDescending(item => item.Marks.Values.Sum()).TakeLast(4).ToList();

		// Абитуриенты с положительными оценками отсортированные и разбитые на группы
		public static Dictionary<int, IGrouping<int, Abiturient>> GetGoodByGroup(List<Abiturient> list) =>
			list.Where(item => item.Marks.Values.All(mark => mark > 3))
			.OrderBy(item => item.Group).ThenByDescending(item => item.Marks.Values.Average()).ThenBy(item => item.Surname)
			.GroupBy(item => item.Group).ToDictionary(item => item.Key);
	}

	public enum PrintType {
		Short,
		Full
	}
}
