using HotelREST.DBUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelOpgave3.Model;

namespace HotelREST.Controllers
{
	[RoutePrefix("api/Hotels")]
	
	public class HotelsController : ApiController
	{
		private static ManageHotel mgr = new ManageHotel();


		// GET: api/Hotels
		[Route("")]
		public IEnumerable<Hotel> Get()
		{
			return mgr.GetAllHotels();
		}

		// GET: api/Hotels/5
		[Route("{id}")]
		public Hotel Get(int id)
		{
			return mgr.GetHotelFromId(id);
		}

		// POST: api/Hotels
		[Route("")]
		public bool Post([FromBody]Hotel value)
		{
			return mgr.CreateHotel(value);
		}

		// PUT: api/Hotels/5
		[Route("{id}")]
		public bool Put(int id, [FromBody]Hotel value)
		{
			return mgr.UpdateHotel(value, id);
		}

		// DELETE: api/Hotels/5
		[Route("{id}")]
		public Hotel Delete(int id)
		{
			return mgr.DeleteHotel(id);
		}
	}
}
