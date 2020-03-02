using System;
using System.Collections.Generic;
using System.Text;

namespace HotelOpgave3.Model
{
	public class Guest
	{
		private int _guest_No;
		private string _name;
		private string adress;

		public Guest(int guestNo, string name, string adress)
		{
			_guest_No = guestNo;
			_name = name;
			this.adress = adress;
		}

		public int GuestNo
		{
			get => _guest_No;
			set => _guest_No = value;
		}

		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public string Adress
		{
			get => adress;
			set => adress = value;
		}

		public Guest()
		{
		}

	}
}
