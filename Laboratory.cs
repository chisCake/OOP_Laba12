using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OOP_Laba7 {
	class Laboratory : IContainer {
		public List<IComponent> technics { get; private set; }

		public Laboratory() {
			technics = new List<IComponent>();
		}

		public Laboratory(params Technique[] items) {
			technics = new List<IComponent>();
			foreach (var item in items)
				technics.Add(item);
		}

		public virtual void Add(Technique item) {
			Add(item, item.GetHashCode().ToString());
		}

		public virtual void Add(IComponent item) {
			Add(item, item.GetHashCode().ToString());
		}

		public virtual void Add(Technique item, string ID) {
			technics.Add(item);
		}

		public virtual void Add(IComponent item, string ID) {
			if (technics.Any(el => el.Site.Name == ID))
				return;
			item.Site.Name = ID;
			technics.Add(item);
		}

		public bool Remove(int index) {
			if (index < Components.Count && index > 0) {
				Remove(Components[index]);
				return true;
			}
			return false;
		}

		public virtual void Remove(IComponent item) {
			technics.Remove(item);
		}

		public ComponentCollection Components {
			get {
				var datalist = new IComponent[technics.Count];
				technics.CopyTo(datalist);
				return new ComponentCollection(datalist);
			}
		}

		public virtual void Dispose() {
			foreach (var item in technics)
				item.Dispose();
			technics.Clear();
		}

		public void PrintContent(bool sort = true) {
			if (sort) Sort();
			Console.WriteLine(" №  Тип техники    Дата появления    Цена");
			int counter = 0;
			foreach (var component in Components) {
				var item = (Technique)component;
				Console.WriteLine($"{++counter,2}) {item.GetType(),-13}    {item.ReceiptDate:d}    {item.Price,5}р");
			}
		}

		public void Sort() {
			technics.Sort((item1, item2) => {
				int type = ((Technique)item1).Type.CompareTo(((Technique)item2).Type);
				return type == 0 ? ((Technique)item1).Price.CompareTo(((Technique)item2).Price) : type;
			});
		}
	}
}
