using System;
using System.Collections.Generic;
using System.Text;

namespace HotelOpgave3.Model
{
	public class Hotel
	{
		private int _hotel_No;
		private string _name;
		private string _adress;

		public Hotel(int hotelNo, string name, string adress)
		{
			_hotel_No = hotelNo;
			_name = name;
			_adress = adress;
		}

		public int Hotel_No
		{
			get => _hotel_No;
			set => _hotel_No = value;
		}

		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public string Adress
		{
			get => _adress;
			set => _adress = value;
		}

		public Hotel()
		{
		}

		public override string ToString()
		{
			return $"Hotel Number: {Hotel_No}, Name: {Name}, Address: {Adress}";
		}
	}
}
