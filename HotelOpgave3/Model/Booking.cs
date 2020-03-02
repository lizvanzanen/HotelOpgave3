using System;
using System.Collections.Generic;
using System.Text;

namespace HotelOpgave3.Model
{
	public class Booking
	{
		private string _room_No;
		private string _roomType;
		private double _price;

		public Booking(string roomNo, string roomType, double price)
		{
			_room_No = roomNo;
			_roomType = roomType;
			_price = price;
		}

		public double Price
		{
			get => _price;
			set => _price = value;
		}

		public string RoomNo
		{
			get => _room_No;
			set => _room_No = value;
		}

		public string RoomType
		{
			get => _roomType;
			set => _roomType = value;
		}

		public Booking() { }
	}
}
