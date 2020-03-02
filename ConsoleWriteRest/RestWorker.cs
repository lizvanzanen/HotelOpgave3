using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelOpgave3.Model;
using Newtonsoft.Json;

namespace ConsoleWriteRest
{
	class RestWorker
	{

		public async void Start()
		{
			await PrintAllHotelsAsync();

			await PrintOneHotelAsync(6);

			Hotel newHotel = new Hotel(8, "Slagelse Kro", "4200 Slagelse duh");
			await CreateHotel(newHotel);

			Hotel updatedHotel = new Hotel(8, "Slagelse Kro og Hotel", "Some road, 4200 Slagelse");
			await UpdateHotel(8, updatedHotel);

			await DeleteHotel(8);
		}

		public async Task PrintAllHotelsAsync()
		{
			Console.WriteLine("\nFinding all Hotels...");
			IList<Hotel> allHotels = new List<Hotel>();
			allHotels = await GetAllHotelAsync();
			foreach (Hotel hotel in allHotels)
			{
				Console.WriteLine(hotel.ToString());
			}
		}

		public async Task PrintOneHotelAsync(int hotelNr)
		{
			Console.WriteLine("\nFinding one Hotel...");
			Hotel hotel = new Hotel();
			hotel = await GetOneHotelAsync(hotelNr);
			Console.WriteLine(hotel.ToString());
		}

		public async Task CreateHotel(Hotel hotel)
		{
			Console.WriteLine("\nCreating a new Hotel...");
			if (await CreateHotelAsync(hotel))
			{
				Console.WriteLine($"Hotel: {hotel.ToString()} created.");
			}
			else
			{
				Console.WriteLine($"Hotel: {hotel.ToString()} NOT created.");
			}
		}

		public async Task UpdateHotel(int HotelNr, Hotel hotel)
		{
			Console.WriteLine("\nUpdating a Hotel...");
			if (await UpdateHotelAsync(HotelNr, hotel))
			{
				Console.WriteLine($"Hotel: {hotel.ToString()} updated.");
			}
			else
			{
				Console.WriteLine($"Hotel: {hotel.ToString()} NOT Updated.");
			}
		}

		public async Task DeleteHotel(int hotelNr)
		{
			Console.WriteLine("\nDeleting a Hotel...");
			Hotel deletedHotel = await DeleteHotelAsync(hotelNr);
			if (deletedHotel != null)
			{
				Console.WriteLine($"Hotel: {deletedHotel.ToString()} deleted.");
			}
			else
			{
				Console.WriteLine($"Hotel: {deletedHotel.ToString()} Not deleted.");
			}
		}

		private string URI = "http://localhost:2708/api/Hotels/";

		public async Task<IList<Hotel>> GetAllHotelAsync()
		{
			IList<Hotel> cList = new List<Hotel>();
			using (HttpClient client = new HttpClient())
			{
				string content = await client.GetStringAsync(URI);
				cList = JsonConvert.DeserializeObject<IList<Hotel>>(content);
			}
			return cList;
		}

		public async Task<Hotel> GetOneHotelAsync(int hotelNr)
		{
			using (HttpClient client = new HttpClient())
			{
				string content = await client.GetStringAsync(URI + $"{hotelNr}");
				Hotel hotel = JsonConvert.DeserializeObject<Hotel>(content);
				return hotel;
			}
		}

		public async Task<bool> CreateHotelAsync(Hotel hotel)
		{
			bool res = false;
			using (HttpClient client = new HttpClient())
			{
				String jsonStr = JsonConvert.SerializeObject(hotel);
				StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
				HttpResponseMessage resultMessage = await client.PostAsync(URI, content);
				if (resultMessage.IsSuccessStatusCode)
				{
					res = true;
					//string jsonStr1 = await resultMessage.Content.ReadAsStringAsync();
					//res = JsonConvert.DeserializeObject<bool>(jsonStr1);
					return res;
				}
			}
			return res;
		}

		public async Task<bool> UpdateHotelAsync(int hotelNr, Hotel updatedItem)
		{
			using (HttpClient client = new HttpClient())
			{
				String jsonStr = JsonConvert.SerializeObject(updatedItem);
				StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
				HttpResponseMessage resultMessage = await client.PutAsync(URI + $"{hotelNr}", content);
				if (resultMessage.IsSuccessStatusCode)
				{
					string jsonStr1 = await resultMessage.Content.ReadAsStringAsync();
					bool res = JsonConvert.DeserializeObject<bool>(jsonStr1);
					return res;
				}
			}
			return false;
		}

		public async Task<Hotel> DeleteHotelAsync(int hotelId)
		{
			Hotel res = null;
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage resultMessage = await client.DeleteAsync(URI + $"{hotelId}");
				if (resultMessage.IsSuccessStatusCode)
				{
					//res = true;
					string jsonStr1 = await resultMessage.Content.ReadAsStringAsync();
					res = JsonConvert.DeserializeObject<Hotel>(jsonStr1);
					return res;
				}
			}
			return null;
		}
	}
}
