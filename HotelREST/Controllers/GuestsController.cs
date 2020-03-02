using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelOpgave3.Model;
using HotelREST.DBUtil;

namespace HotelREST.Controllers
{
	public class GuestsController : ApiController
	{
		// GET: api/Guests
		public IEnumerable<Guest> Get()
		{
			ManageGuest mgr = new ManageGuest();
			return mgr.GetAllGuest();
		}

		// GET: api/Guests/5
		public Guest Get(int id)
		{
			ManageGuest mgr = new ManageGuest();
			return mgr.GetGuestFromId(id);
		}

		// POST: api/Guests
		public bool Post([FromBody]Guest value)
		{
			ManageGuest mgr = new ManageGuest();
			return mgr.CreateGuest(value);
		}

		// PUT: api/Guests/5
		public bool Put(int id, [FromBody]Guest value)
		{
			ManageGuest mgr = new ManageGuest();
			return mgr.UpdateGuest(value, id);
		}

		// DELETE: api/Guests/5
		public Guest Delete(int id)
		{
			ManageGuest mgr = new ManageGuest();
			return mgr.DeleteGuest(id);
		}
	}
}
