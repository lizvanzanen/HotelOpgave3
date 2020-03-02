using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using HotelOpgave3.Model;


namespace HotelREST.DBUtil
{
	public class ManageGuest
	{
		private string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=HotelDB1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		string commandStringGetAll = "Select * from Guest";

		public List<Guest> GetAllGuest()
		{
			List<Guest> allGuests = new List<Guest>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(commandStringGetAll, connection))
				{
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						allGuests.Add(ReadNextGuest(reader));
					}
				}
			}
			return allGuests;
		}

		private string commandStringGetGuest = "SELECT * FROM Guest WHERE guest_No = @GuestNr";

		public Guest GetGuestFromId(int guestNr)
		{
			Guest guest = new Guest();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(commandStringGetGuest, connection))
				{
						command.Parameters.AddWithValue("@GuestNr", guestNr);
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						return ReadNextGuest(reader);
					}
				}
			}
			return null;
		}
		
		private string commandStringCreate = "INSERT INTO Guest (guest_No, Name, Address) Values(@GuestNo, '@Name', '@Adress')";
		
		public bool CreateGuest(Guest guest)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				if (GetGuestFromId(guest.GuestNo) == null)
				{
					SqlCommand command = new SqlCommand(commandStringCreate, connection);
					command.Parameters.AddWithValue("@Name", guest.Name);
					command.Parameters.AddWithValue("@Address", guest.Adress);
					command.Parameters.AddWithValue("@GuestNr", guest.GuestNo);
					command.Connection.Open();
					int rows = command.ExecuteNonQuery();
					return true;
				}
			}
			return false;
		}

		string commandStringUpdate = "UPDATE Guest SET Name = '@Name',	Address = '@Address' where guest_No = @GuestNr";

		public bool UpdateGuest(Guest guest, int guestNr)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				if (GetGuestFromId(guest.GuestNo) != null)
				{
					SqlCommand command = new SqlCommand(commandStringUpdate, connection);
					command.Parameters.AddWithValue("@GuestNr", guest.GuestNo);
					command.Parameters.AddWithValue("@Name", guest.Name);
					command.Parameters.AddWithValue("@Address", guest.Adress);
					command.Connection.Open();
					int rows = command.ExecuteNonQuery();
					return true;
				}
			}
			return false;
		}

		public Guest DeleteGuest(int guestNr)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Guest guest = GetGuestFromId(guestNr);
				if (guest != null)
				{
					string commandString = "DELETE Guest WHERE GuestNo = @guestNr";
					SqlCommand command = new SqlCommand(commandString, connection);
					command.Connection.Open();
					command.Parameters.AddWithValue("@GuestNr", guest.GuestNo);

					int rows = command.ExecuteNonQuery();
					return guest;
				}
			}
			return null;
		}

		private Guest ReadNextGuest(SqlDataReader reader)
		{
			Guest guest = new Guest();
			guest.GuestNo = reader.GetInt32(0);
			guest.Name = reader.GetString(1);
			guest.Adress = reader.GetString(2);
			return guest;
		}






	}
}