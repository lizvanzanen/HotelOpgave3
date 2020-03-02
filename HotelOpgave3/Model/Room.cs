using System;
using System.Collections.Generic;
using System.Text;

namespace HotelOpgave3.Model
{
	public class Room
	{
		private int _room_No;
		private string _roomType;
		private string address;

		public Room(int roomNo, string roomType, string address)
		{
			_room_No = roomNo;
			_roomType = roomType;
			this.address = address;
		}

		public int RoomNo
		{
			get => _room_No;
			set => _room_No = value;
		}

		public string RoomType
		{
			get => _roomType;
			set => _roomType = value;
		}

		public string Address
		{
			get => address;
			set => address = value;
		}

		public Room()
		{
		}
	}
}
