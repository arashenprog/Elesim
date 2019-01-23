using Esunco.Logics.Contexts;
using Esunco.Models;
using Esunco.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AcoreX.Helper;
using Newtonsoft.Json.Linq;
using Esunco.Models.Filters;

namespace Esunco.Services.Controllers
{
    [CustomExceptionFilter]
    public class ShopController : ApiController
    {
        //[Authorize]
        [HttpPost]
        [Route("Shop/Reqular")]
        public JsonResult<IQueryable<SimServiceModel>> GetReqularSimList([FromBody]JObject model)
        {
            using (var ctx = new ServiceContext())
            {
                long id = 0;
                if (model != null)
                    id = model.Value<long>("LastLoadedID");
                var data = ctx.GetReqularSimList(id);
                return new JsonResult<IQueryable<SimServiceModel>>(data);
            }
        }

        [HttpPost]
        [Route("Shop/Rond")]
        public JsonResult<IQueryable<SimServiceModel>> GetRondSimList([FromBody]JObject model)
        {
            using (var ctx = new ServiceContext())
            {
                long id = 0;
                if (model != null)
                    id = model.Value<long>("LastLoadedID");
                return new JsonResult<IQueryable<SimServiceModel>>(ctx.GetRondSimList(id));
            }
        }


        [HttpPost]
        [Route("Shop/Packs")]
        public JsonResult<IQueryable<PackServiceModel>> GetPacksList([FromBody]JObject model)
        {
            using (var ctx = new ServiceContext())
            {
                long id = 0;
                if (model != null)
                    id = model.Value<long>("LastLoadedID");
                return new JsonResult<IQueryable<PackServiceModel>>(ctx.GetPackList(id));
            }
        }

        [HttpPost]
        [Route("Shop/Auctions")]
        public JsonResult<IQueryable<AuctionServiceModel>> GetAuctionList([FromBody]JObject model)
        {
            using (var ctx = new ServiceContext())
            {
                long id = model.Value<long>("LastLoadedID");
                var token = model.Value<string>("Token");
                return new JsonResult<IQueryable<AuctionServiceModel>>(ctx.GetAuctionList(token, id));
            }
        }

        [HttpPost]
        [Route("Shop/Auctions/Bid")]
        public JsonResult<bool> SetAuctionBid([FromBody]JObject model)
        {
            using (var ctx = new ServiceContext())
            {

                long auctionID = model.Value<long>("AuctionID");
                var token = model.Value<string>("Token");
                long price = model.Value<long>("Price");
                ctx.SetAuctionBid(token, auctionID, price);
                return new JsonResult<bool>(true);
            }
        }


        [HttpPost]
        [Route("Order/History")]
        public JsonResult<IQueryable<OrderHistoryModel>> ShopOrderHistory([FromBody]JObject model)
        {
            using (var ctx = new ServiceContext())
            {
                long id = 0;
                if (model != null)
                    id = model.Value<long>("LastLoadedID");
                return new JsonResult<IQueryable<OrderHistoryModel>>(ctx.GetOrderHistory(model.Value<string>("Token"), id));
            }
        }


        [HttpPost]
        [Route("Shop/Sim/Search")]
        public JsonResult<IQueryable<SimServiceModel>> SearchSim([FromBody]SearchSimFilter filter)
        {
            using (var ctx = new ServiceContext())
            {
                var data = ctx.SearchSims(filter);
                return new JsonResult<IQueryable<SimServiceModel>>(data);
            }
        }

        [HttpGet]
        [Route("Shop/Sim/{id}")]
        public JsonResult<SimServiceModel> GetSim([FromUri]long id)
        {
            using (var ctx = new ServiceContext())
            {
                var data = ctx.GetSim(id);
                return new JsonResult<SimServiceModel>(data);
            }
        }


        [HttpPost]
        [Route("Shop/Pack/Search")]
        public JsonResult<IQueryable<PackServiceModel>> SearchPack([FromBody]SearchPackFilter filter)
        {
            using (var ctx = new ServiceContext())
            {
                var data = ctx.SearchPacks(filter);
                return new JsonResult<IQueryable<PackServiceModel>>(data);
            }
        }

        [HttpGet]
        [Route("Shop/Pack/{id}")]
        public JsonResult<PackServiceModel> GetPack([FromUri]long id)
        {
            using (var ctx = new ServiceContext())
            {
                var data = ctx.GetPack(id);
                return new JsonResult<PackServiceModel>(data);
            }
        }
    }
}
