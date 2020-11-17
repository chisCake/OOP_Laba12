using System;
using System.ComponentModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace OOP_Laba7 {
	enum TechniqueType {
		Centrifuge,
		Computer,
		Microscop,
		Refrigerator,
		Thermometer
	}
	
	class Technique : IComponent {
		[JsonProperty()]
		public TechniqueType Type { get; private set; }
		[JsonProperty()]
		public DateTime ReceiptDate { get; private set; }
		[JsonProperty()]
		public double Price { get; private set; }
		
		public Technique(TechniqueType type, (int year, int month, int day) receiptDate, double price) {
			Type = type;
			ReceiptDate = new DateTime(receiptDate.year, receiptDate.month, receiptDate.day);
			Price = price;
			Site = null;
			Disposed = null;
		}

		[JsonConstructor()]
		public Technique(TechniqueType type, DateTime receiptDate, double price) {
			Debug.Assert(type >= 0 && (int)type <= 4, "Неверный тип объекта");
			Type = type;
			ReceiptDate = receiptDate;
			Price = price;
			Site = null;
			Disposed = null;
		}

		[JsonIgnore()]
		public virtual ISite Site { get; set; }
		public event EventHandler Disposed;

		public virtual void Dispose() => Disposed?.Invoke(this, EventArgs.Empty);

		public new string GetType() {
			return Type switch
			{
				TechniqueType.Centrifuge => "Центрифуга",
				TechniqueType.Computer => "Компьютер",
				TechniqueType.Microscop => "Микроскоп",
				TechniqueType.Refrigerator => "Холодильник",
				TechniqueType.Thermometer => "Термометр",
				_ => "Тип не опознан",
			};
		}

		public static string GetStrType(TechniqueType type) {
			return type switch
			{
				TechniqueType.Centrifuge => "Центрифуга",
				TechniqueType.Computer => "Компьютер",
				TechniqueType.Microscop => "Микроскоп",
				TechniqueType.Refrigerator => "Холодильник",
				TechniqueType.Thermometer => "Термометр",
				_ => "Тип не опознан",
			};
		}

		public static TechniqueType GetEnumType(string type) {
			if (type == "centrifuge")
				return TechniqueType.Centrifuge;
			else if (type == "computer")
				return TechniqueType.Computer;
			else if (type == "microscop")
				return TechniqueType.Microscop;
			else if (type == "refrigerator")
				return TechniqueType.Refrigerator;
			else if (type == "thermometer")
				return TechniqueType.Thermometer;
			else
				throw new Exception("Тип не опознан");
		}
	}
}
