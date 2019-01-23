using AcoreX.Helper;
using Esunco.Models;
using Esunco.Models.Enum;
using Esunco.Models.Filters;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elesim.Shared
{
    public class Facade
    {


        public Facade()
        {
            Cart = new ShoppingCart();
        }


        public string BaseUrl = "http://api.elesim.ir/";
        //public string BaseUrl = "http://192.168.1.5:9812/";

        private async Task<T> RequestAsync<T>(string url, object json = null, Method method = Method.POST)
        {
            var rest = new RestClient(BaseUrl);
            var request = new RestRequest(url, method);
            if (json != null)
                request.AddJsonBody(json);
            var result = await rest.ExecuteTaskAsync<JsonResult<T>>(request);
            if (result.Data == null)
                throw new Exception("خطا در اتصال به اینترنت");
            if (!result.Data.Succeed)
                throw new Exception(string.Join("\n", result.Data.Error.Message));
            return result.Data.Result;
        }

        private T Request<T>(string url, object json = null, Method method = Method.POST)
        {

            var rest = new RestClient(BaseUrl);
            var request = new RestRequest(url, method);
            if (json != null)
                request.AddJsonBody(json);
            var result = rest.Execute<JsonResult<T>>(request);
            if (result.Data == null)
                throw new Exception("خطا در اتصال به اینترنت");
            if (!result.Data.Succeed)
                throw new Exception(String.Join("\n", result.Data.Error.Message));
            return result.Data.Result;

        }

        #region Account

        public void RequestLoginSMS(string mobile)
        {
            Request<int>("Account/SendSMS", new { Mobile = mobile });
        }

        public string GetLoginToken(string mobile, int code)
        {
            return Request<string>("Account/GetToken", new { Mobile = mobile, Code = code });
        }

        public void SignIn(string token)
        {
            Client = Request<ClientProfileServiceModel>("Account/SignIn", new { Token = token });
        }

        public bool SignOut()
        {
            Client = null;
            return true;
        }

        public bool SaveProfile(ClientProfileServiceModel client)
        {
            Client = RequestAsync<ClientProfileServiceModel>("Account/Save", client).Result;
            return true;
        }

        public void Register(ClientProfileServiceModel model)
        {
            Request<string>("Account/Register", model);
        }

        public PaymentResultModel ChargeAccount(long price)
        {
            return RequestAsync<PaymentResultModel>("Account/Credit/Payment", new PaymentCreditModel { Price = price, Token = Client.Token }).Result;
        }

        #endregion

        public async Task<List<OrderHistoryModel>> GetOrderHistory(long lastLoadedID = 0)
        {
            return await RequestAsync<List<OrderHistoryModel>>("Order/History", new { LastLoadedID = lastLoadedID, Token = Client.Token });
        }

        public async Task<List<SimServiceModel>> GetReqularSims(long lastLoadedID = 0)
        {
            return await RequestAsync<List<SimServiceModel>>("Shop/Reqular", new { LastLoadedID = lastLoadedID });
        }


        public async Task<List<SimServiceModel>> GetRondSims(long lastLoadedID = 0)
        {
            return await RequestAsync<List<SimServiceModel>>("Shop/Rond", new { LastLoadedID = lastLoadedID });
        }

        public async Task<List<PackServiceModel>> GetPacks(long lastLoadedID = 0)
        {
            return await RequestAsync<List<PackServiceModel>>("Shop/Packs", new { LastLoadedID = lastLoadedID });
        }

        public async Task<List<AuctionServiceModel>> GetAuctions(long lastLoadedID = 0)
        {
            return await RequestAsync<List<AuctionServiceModel>>("Shop/Auctions", new { LastLoadedID = lastLoadedID, Token = Client != null ? Client.Token : null });
        }


        public List<SimServiceModel> SearchSim(SearchSimFilter filter)
        {
            return Request<List<SimServiceModel>>("Shop/Sim/Search", filter);
        }

        public List<PackServiceModel> SearchPack(SearchPackFilter filter)
        {
            return Request<List<PackServiceModel>>("Shop/Pack/Search", filter);
        }


        public bool SetAuctionBid(long id, long price)
        {
            var result = Request<bool>("Shop/Auctions/Bid", new { AuctionID = id, Price = price, Token = Client.Token });
            if (result)
            {

            }
            return result;
        }


        #region Order

        public PaymentResultModel CheckCart()
        {
            return Request<PaymentResultModel>("Order/Payment", new { Token = Client.Token, Items = Cart.Items });
        }

        public long CheckCartCredit()
        {
            return Request<long>("Order/Payment/Credit", new { Token = Client.Token, Items = Cart.Items });
        }

        public PaymentStatus GetPaymentStatus(string paymentID)
        {
            return Request<PaymentStatus>("Payment/Status", new { Token = Client.Token, PaymentID = paymentID });
        }


        #endregion

        public void UpdateAppInfo()
        {
            this.Info = Request<AppInfoModel>("App/Info", method : Method.GET);
        }

        public AppInfoModel GetAppInfo()
        {
            return Request<AppInfoModel>("App/Info", method: Method.GET);
        }


        private static ClientProfileServiceModel _client;
        public ClientProfileServiceModel Client
        {
            get
            {
                return _client;
            }
            internal set
            {
                _client = value;
            }

        }

        public ShoppingCart Cart
        {
            get;
            internal set;
        }

        private static AppInfoModel _info;

        public AppInfoModel Info
        {
            get
            {
                return _info;
            }
            internal set
            {
                _info = value;
            }
        }
        public bool IsLogin
        {
            get
            {
                return Client != null;
            }
        }
    }
}
