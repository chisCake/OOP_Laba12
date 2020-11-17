using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace OOP_Laba12 {
	class InvestigatedType {
		public string Assembly { get; set; }
		public bool HasPublicConstructors { get; set; }
		public IEnumerable<string> PublicMethods { get; set; }
		public IEnumerable<string> FieldsAndProperties { get; set; }
		public IEnumerable<string> Interfaces { get; set; }

		public void Info() {
			Console.WriteLine(
				$"Assembly: {Assembly}" +
				$"\nHas public constructors: {HasPublicConstructors}" +
				$"\nPublic methods:"
				);
			foreach (var item in PublicMethods)
				Console.WriteLine(item);
			Console.WriteLine("Field and properties:");
			foreach (var item in FieldsAndProperties)
				Console.WriteLine(item);
			Console.WriteLine("Interfaces:");
			foreach (var item in Interfaces)
				Console.WriteLine(item);
		}
	}

	static class Reflector {
		public static void Research(Type type, bool show = false) {
			var result = new InvestigatedType() {
				Assembly = GetAssembly(type).FullName,
				HasPublicConstructors = HasPublicConstructors(type),
				FieldsAndProperties = GetFieldsAndProperties(type),
				Interfaces = GetInterfaces(type),
				PublicMethods = GetPublicMethods(type)
			};
			if (show)
				result.Info();

			string json = JsonConvert.SerializeObject(result);
			using var sw = new StreamWriter(type.Name + "Type.json");
			sw.Write(json);
		}

		public static Assembly GetAssembly(Type type) => type.Assembly;

		public static bool HasPublicConstructors(Type type) =>
			type.GetConstructors().Length != 0;

		public static IEnumerable<string> GetPublicMethods(Type type) {
			var methods = type.GetMethods();
			var result = new List<string>();
			foreach (var item in methods)
				result.Add(item.Name);
			return result.Distinct();
		}

		public static IEnumerable<string> GetFieldsAndProperties(Type type) {
			var fields = type.GetFields();
			var properties = type.GetProperties();
			var result = new List<string>();
			foreach (var item in fields)
				result.Add(item.Name);
			foreach (var item in properties)
				result.Add(item.Name);
			return result;
		}

		public static IEnumerable<string> GetInterfaces(Type type) {
			var interfaces = type.GetInterfaces();
			var result = new List<string>();
			foreach (var item in interfaces)
				result.Add(item.Name);
			return result;
		}

		public static IEnumerable<string> GetMethods(Type type, string paramType) {
			if (type == null)
				return null;
			var methods = type.GetMethods();
			var result = new List<string>();
			foreach (var item in methods)
				if (item.GetParameters()
					.Any(param => param.ParameterType.Name == paramType))
					result.Add(item.Name);
			return result.Count != 0 ? result : null;
		}

		public static void Invoke(Type type, string methodName, params (Type type, object value)[] paramTuples) {
			var paramsTypes = paramTuples.Select(item => item.type).ToArray();
			var paramsValues = paramTuples.Select(item => item.value).ToArray();
			var method = type.GetMethod(methodName, paramsTypes);
			if (method == null) {
				Console.WriteLine("Метод не найден");
				return;
			}
			method.Invoke(null, paramsValues);
		}

		public static T Create<T>() {
			var type = typeof(T);
			Type[] types = new Type[0];
			var constructor = type.GetConstructor(types);
			if (constructor == null)
				return default;
			else
				return (T)constructor.Invoke(null);
		}

		public static T Create<T>(params (Type type, object value)[] paramTuples) {
			var paramsTypes = paramTuples.Select(item => item.type).ToArray();
			var paramsValues = paramTuples.Select(item => item.value).ToArray();
			var type = typeof(T);
			var constructor = type.GetConstructor(paramsTypes);
			if (constructor == null)
				return default;
			else
				return (T)constructor.Invoke(paramsValues);
		}
	}
}