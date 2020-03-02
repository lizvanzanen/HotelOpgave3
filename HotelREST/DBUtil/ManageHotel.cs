using HotelOpgave3.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelREST.DBUtil
{
	public class ManageHotel
	{
		//private string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=HotelDB1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		private string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = HotelDB; Integrated Security = True; Connect Timeout = 30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		private const string commandStringGetAll = "Select * from Hotel";

		public List<Hotel> GetAllHotels()
		{

			List<Hotel> allHotels = new List<Hotel>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(commandStringGetAll, connection))
				{
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						allHotels.Add(ReadNextHotel(reader));
					}
				}
			}
			return allHotels;
		}

		string commandStringGetHotel = "SELECT * FROM Hotel WHERE Hotel_No = @HotelNr";

		public Hotel GetHotelFromId(int hotelNr)
		{
			Hotel hotel = new Hotel();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(commandStringGetHotel, connection))
				{
						command.Parameters.AddWithValue("@HotelNr", hotelNr);
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						return ReadNextHotel(reader);
					}
				}
			}
			return null;
		}

		string commandStringCreate = "INSERT INTO Hotel VALUES (@HotelNr, @Name, @Address)";

		public bool CreateHotel(Hotel hotel)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				//if (GetHotelFromId(hotel.Hotel_No) == null)
				//{
					SqlCommand command = new SqlCommand(commandStringCreate, connection);
					command.Parameters.AddWithValue("@HotelNr", hotel.Hotel_No);
					command.Parameters.AddWithValue("@Name", hotel.Name);
					command.Parameters.AddWithValue("@Address", hotel.Adress);
					command.Connection.Open();
					int rows = command.ExecuteNonQuery();
					return true;
				//}
			}
			//return false;
		}

		string commandStringUpdate = "UPDATE Hotel SET Name = @Name, Address = @Address WHERE Hotel_No = @HotelNr";
		public bool UpdateHotel(Hotel hotel, int hotelNr)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				if (GetHotelFromId(hotelNr) != null)
				{
					SqlCommand command = new SqlCommand(commandStringUpdate, connection);
					command.Parameters.AddWithValue("@HotelNr", hotel.Hotel_No);
					command.Parameters.AddWithValue("@Name", hotel.Name);
					command.Parameters.AddWithValue("@Address", hotel.Adress);
					command.Connection.Open();
					int rows = command.ExecuteNonQuery();
					return true;
				}
			}
			return false;
		}

		string commandStringDelete = "DELETE Hotel WHERE Hotel_No = @HotelNr";

		public Hotel DeleteHotel(int hotelNr)
		{
			
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				Hotel hotel = GetHotelFromId(hotelNr);
				if (hotel != null)
				{
					SqlCommand command = new SqlCommand(commandStringDelete, connection);
					command.Parameters.AddWithValue("@HotelNr", hotel.Hotel_No);
					command.Connection.Open();
					int rows = command.ExecuteNonQuery();
					return hotel;
				}
			}
			return null;
		}

		private Hotel ReadNextHotel(SqlDataReader reader)
		{
			Hotel hotel = new Hotel();
			hotel.Hotel_No = reader.GetInt32(0);
			hotel.Name = reader.GetString(1);
			hotel.Adress = reader.GetString(2);
			return hotel;
		}
	}
}