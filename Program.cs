using System;

using OOP_Laba3;

using OOP_Laba7;

namespace OOP_Laba12 {
	class Private {
		private Private() { }
	}
	class SomeClass { }

	class Program {
		static void Main() {
			Reflector.Research(typeof(Abiturient));
			Reflector.Research(typeof(Laboratory));
			Reflector.Research(typeof(Console));
			Reflector.Research(typeof(string));

			Reflector.Invoke(typeof(Console), "WriteLine", (typeof(string), "Это сообщение выведено через Reflector.Invoke"));
			Console.ReadKey();

			Console.Write("\nВведите тип параметра для поиска метода в Convert: ");
			string userParamType = Console.ReadLine();
			var result = Reflector.GetMethods(typeof(Convert), userParamType);
			if (result == null)
				Console.WriteLine("Ничего не найдено");
			else
				foreach (var item in result)
					Console.WriteLine(item);

			Console.ReadKey();
			SomeClass someClass = Reflector.Create<SomeClass>();
			if (someClass != null)
				Console.WriteLine("\nЭкземпляр создан");
			else
				Console.WriteLine("\nЭкземпляр не создан");

			Abiturient abiturient = Reflector.Create<Abiturient>(
				(typeof(string), "1"),
				(typeof(string), "Фамилия1"),
				(typeof(string), "Имя2"),
				(typeof(string), "Отчество3"),
				(typeof(int), 1));
			if (abiturient != null) {
				Console.WriteLine("\nЭкземпляр абитуриента создан");
				Console.WriteLine($"Фамилия: {abiturient.Surname}, Имя: {abiturient.Name}");
			}
			else
				Console.WriteLine("\nЭкземпляр абитуриента не создан");
			Console.ReadKey();
		}
	}
}
