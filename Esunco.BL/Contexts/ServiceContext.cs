using AcoreX.Data.Repository;
using Esunco.Data;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Esunco.Models;
using AcoreX.Helper;
using System;
using Esunco.Models.Enum;
using AcoreX.Security;
using Esunco.Models.Filters;
using AcoreX.DateTimeCalendar;
using System.Text;
using System.IO;
using Aspose.Cells;
using System.Net.Mail;
using AcoreX.Utility.ExceptionHandler;
using AcoreX.Utility;
using AcoreX.Diagnostics;

namespace Esunco.Logics.Contexts
{


    public class ServiceContext : BaseContext
    {
        private readonly int TAKE_COUNT = 8;

        #region Get Products

        public IQueryable<SimServiceModel> GetReqularSimList(long lastLoadedId)
        {
            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {

                var query = from i in rep.Items
                            where
                            !i.RondPrice.HasValue &&
                            !i.PackSimDataEntities.Any() &&
                            !i.AuctionSimDataEntities.Any() &&
                            i.StatusID == (byte)SimPackStatus.Published
                            //(!i.ExpireDate.HasValue || i.ExpireDate > DateTime.Now)
                            orderby i.ID descending
                            select i;

                if (lastLoadedId == 0)
                    return query.Take(TAKE_COUNT).ProjectTo<SimServiceModel>();
                else
                    return query.Where(c => c.ID < lastLoadedId).Take(TAKE_COUNT).ProjectTo<SimServiceModel>();
            }
        }


        public SimServiceModel GetSim(long id)
        {
            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {

                var query = from i in rep.Items
                            where
                            i.ID == id &&
                            !i.PackSimDataEntities.Any() &&
                            !i.AuctionSimDataEntities.Any() &&
                            i.StatusID == (byte)SimPackStatus.Published
                            select i;
                return AutoMapper.Mapper.Map<SimServiceModel>(query.First());
            }
        }

        public IQueryable<SimServiceModel> GetRondSimList(long lastLoadedId)
        {
            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {

                var query = from i in rep.Items
                            where
                            i.RondPrice.HasValue &&
                            !i.PackSimDataEntities.Any() &&
                            !i.AuctionSimDataEntities.Any() &&
                            i.StatusID == (byte)SimPackStatus.Published
                            orderby i.ID descending
                            select i;

                if (lastLoadedId == 0)
                    return query.Take(TAKE_COUNT).ProjectTo<SimServiceModel>();
                else
                    return query.Where(c => c.ID < lastLoadedId).Take(TAKE_COUNT).ProjectTo<SimServiceModel>();
            }
        }

        public IQueryable<PackServiceModel> GetPackList(long lastLoadedId)
        {
            using (var rep = new Repository<PackDataEntity>(base.UnitOfWork))
            {
                var query = from i in rep.Items
                            where i.StatusID == (byte)SimPackStatus.Published
                            orderby i.ID descending
                            select i;

                if (lastLoadedId == 0)
                    return query.Take(100).ProjectTo<PackServiceModel>();
                else
                    return query.Where(c => c.ID < lastLoadedId).Take(100).ProjectTo<PackServiceModel>();
            }
        }


        public PackServiceModel GetPack(long id)
        {
            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {
                var query = from i in rep.Items
                            where i.StatusID == (byte)SimPackStatus.Published &&
                            i.ID == id
                            select i;
                return AutoMapper.Mapper.Map<PackServiceModel>(query.First());
            }
        }

        public IQueryable<SimServiceModel> SearchSims(SearchSimFilter filter)
        {

            if (filter == null)
                filter = new SearchSimFilter();
            if (filter.MaxPrice == 0)
                filter.MaxPrice = int.MaxValue;

            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {
                var query = from i in rep.Items
                            where
                            i.TypeID == (int)filter.SimType &&
                            (string.IsNullOrWhiteSpace(filter.PreCode) || i.Number.ToString().Substring(0, 3) == filter.PreCode) &&
                            !i.PackSimDataEntities.Any() &&
                            !i.AuctionSimDataEntities.Any() &&
                            i.StatusID == (byte)SimPackStatus.Published &&
                            //(!i.ExpireDate.HasValue || i.ExpireDate > DateTime.Now) &&
                            ((i.RondPrice.HasValue && (i.RondPrice >= filter.MinPrice && i.Price <= filter.MaxPrice)) ||
                            (i.Price >= filter.MinPrice && i.Price <= filter.MaxPrice))
                            orderby i.ID descending
                            select new { Item = i, Number = i.Number.ToString() };

                var query2 = from i in query.ToList()
                             where
                              //(String.IsNullOrWhiteSpace(filter.PreCode) || i.Number.Substring(0, 3).Contains(filter.PreCode)) &&
                              (String.IsNullOrWhiteSpace(filter.Num4) || i.Number.Substring(3, 1) == filter.Num4) &&
                              (String.IsNullOrWhiteSpace(filter.Num5) || i.Number.Substring(4, 1) == filter.Num5) &&
                              (String.IsNullOrWhiteSpace(filter.Num6) || i.Number.Substring(5, 1) == filter.Num6) &&
                              (String.IsNullOrWhiteSpace(filter.Num7) || i.Number.Substring(6, 1) == filter.Num7) &&
                              (String.IsNullOrWhiteSpace(filter.Num8) || i.Number.Substring(7, 1) == filter.Num8) &&
                              (String.IsNullOrWhiteSpace(filter.Num9) || i.Number.Substring(8, 1) == filter.Num9) &&
                              (String.IsNullOrWhiteSpace(filter.Num10) || i.Number.Substring(9, 1) == filter.Num10)
                             select i.Item;


                if (filter.LastLoadedId == 0)
                    return query2.AsQueryable().Take(TAKE_COUNT).ProjectTo<SimServiceModel>();
                else
                    return query2.AsQueryable().Where(c => c.ID < filter.LastLoadedId).Take(TAKE_COUNT).ProjectTo<SimServiceModel>();
            }
        }



        public IQueryable<PackServiceModel> SearchPacks(SearchPackFilter filter)
        {
            using (var rep = new Repository<PackDataEntity>(base.UnitOfWork))
            {
                var query = from i in rep.Items
                            where i.TypeID == (int)filter.PackType &&
                            i.StatusID == (byte)SimPackStatus.Published &&
                            (string.IsNullOrWhiteSpace(filter.PreCode) || i.Code.Contains(filter.PreCode)) &&
                            (i.Price >= filter.MinPrice && i.Price <= filter.MaxPrice)
                            orderby i.ID descending
                            select i;

                if (filter.LastLoadedId == 0)
                    return query.Take(TAKE_COUNT).ProjectTo<PackServiceModel>();
                else
                    return query.Where(c => c.ID < filter.LastLoadedId).Take(TAKE_COUNT).ProjectTo<PackServiceModel>();
            }
        }


        #endregion


        #region Auctions

        public IQueryable<AuctionServiceModel> GetAuctionList(string token, long lastLoadedId)
        {
            using (var rep = new Repository<AuctionDataEntity>(base.UnitOfWork))
            using (var repBid = new Repository<AuctionBidDataEntity>(base.UnitOfWork))
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            {
                UpdateAuctionTask();
                //
                var clientId = repClient.Items.Where(c => c.Token == token).Select(c => c.ID).FirstOrDefault().DefaultIfNull(-1);
                var query = from i in rep.Items
                            where (i.StatusID == (byte)AuctionStatus.Started && i.FinishTime >= DateTime.Now) ||
                                  (i.StatusID == (byte)AuctionStatus.Finished && i.AuctionBidDataEntities.Any(c => c.ClientID == clientId && c.IsWinner))
                            orderby i.FinishTime
                            select i;

                var list = new List<AuctionServiceModel>();

                if (lastLoadedId == 0)
                    list.AddRange(query.Take(100).ProjectTo<AuctionServiceModel>());
                else
                    list.AddRange(query.Where(c => c.ID < lastLoadedId).Take(100).ProjectTo<AuctionServiceModel>());

                foreach (var item in list)
                {
                    item.MaxPrice = repBid.Items.Where(d => d.AuctionID == item.ID).Max(d => d.Price);
                    if (clientId != -1)
                    {
                        var q = repBid.Items.Where(d => d.AuctionID == item.ID && d.ClientID == clientId);
                        item.IsWinner = q.Any(d => d.IsWinner);
                        item.YourPrice = q.Max(d => d.Price);
                    }
                }

                return list.AsQueryable();
            }
        }

        public void UpdateAuctionTask()
        {
            try
            {
                using (var rep = new Repository<AuctionDataEntity>(base.UnitOfWork))
                {

                    var startList = from i in rep.Items
                                    where i.StatusID == (byte)AuctionStatus.Published && i.StartTime <= DateTime.Now
                                    select i;

                    foreach (var item in startList)
                    {
                        item.StatusID = (byte)AuctionStatus.Started;
                    }
                    UnitOfWork.FlushChanges();


                    var finishList = from i in rep.Items
                                     where i.StatusID == (byte)AuctionStatus.Started && i.FinishTime < DateTime.Now && i.AuctionBidDataEntities.Any()
                                     select i;

                    foreach (var item in finishList)
                    {
                        item.StatusID = (byte)AuctionStatus.Finished;
                        var maxBid = item.AuctionBidDataEntities.OrderByDescending(c => c.Price).ThenByDescending(c => c.ID).First();
                        maxBid.IsWinner = true;
                        foreach (var j in item.AuctionBidDataEntities)
                        {
                            if (j.ID != maxBid.ID)
                            {
                                var price = j.AuctionDataEntity.BasePrice;
                                j.ClientDataEntity.BlockedCredit -= price;
                                j.ClientDataEntity.Credit += price;
                                UnitOfWork.FlushChanges();
                            }
                        }
                        if (item.NotifyTime == null)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("اِلِسیم");
                            sb.AppendFormat("کاربر گرامی شما برنده مزایده {0} شده اید، لطفا جهت خرید و پرداخت صورت حساب اقدام نمایید.", item.Title);
                            sb.AppendLine();
                            sb.Append(String.Join("-", item.AuctionSimDataEntities.Select(c => c.SimDataEntity.Number.ToString())));
                            SendSMS(sb.ToString(), maxBid.ClientDataEntity.Mobile);
                            item.NotifyTime = DateTime.Now;

                        }
                        UnitOfWork.FlushChanges();
                    }
                    UnitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                UnitOfWork.ClearChanges();
            }
        }

        public void SetAuctionBid(string token, long auctionID, long price)
        {
            using (var rep = new Repository<AuctionBidDataEntity>(base.UnitOfWork))
            using (var repAuction = new Repository<AuctionDataEntity>(base.UnitOfWork))
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            {
                try
                {
                    var client = repClient.Items.FirstOrDefault(c => c.Token == token);
                    var auction = repAuction.Items.FirstOrDefault(c => c.ID == auctionID);
                    //
                    if (client == null || auction == null)
                        throw new InvalidTokenException();
                    if (DateTime.Now > auction.FinishTime)
                        throw new HandledException("مهلت شرکت در مناقصه به پایان رسیده است.");
                    //
                    var maxBid = rep.Items.Where(c => c.AuctionID == auctionID).OrderByDescending(c => c.Price).ThenByDescending(c => c.ID).FirstOrDefault();
                    if (maxBid == null || price > maxBid.Price)
                    {
                        // Apply Credit
                        if (!client.AuctionBidDataEntities.Any(c => c.AuctionID == auctionID))
                        {
                            if (price < auction.BasePrice)
                                throw new HandledException("مبلغ پیشنهادی بایستی برابر یا بزرگتر از قیمت پایه مزایده باشد.");

                            if (client.Credit < auction.BasePrice)
                                throw new HandledException("اعتبار حساب شما از قیمت مزایده کمتر است. لطفا حساب خود را شارژ نمایید.");

                            client.Credit -= auction.BasePrice;
                            client.BlockedCredit += auction.BasePrice;
                            UnitOfWork.FlushChanges();
                        }
                        rep.Save(new AuctionBidDataEntity { Price = price, AuctionID = auctionID, ClientID = client.ID, Time = DateTime.Now });
                        UnitOfWork.FlushChanges();
                        //
                        UnitOfWork.SaveChanges();
                        // Send SMS
                        if (maxBid != null && client.ID != maxBid.ClientDataEntity.ID)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("اِلِسیم");
                            sb.AppendFormat("پیشنهاد بالاتری از پیشنهاد شما برای {0} ثبت گردید.", auction.Title);
                            sb.AppendLine();
                            sb.Append(String.Join("-", auction.AuctionSimDataEntities.Select(c => c.SimDataEntity.Number.ToString())));
                            SendSMS(sb.ToString(), maxBid.ClientDataEntity.Mobile);
                        }
                    }
                    else
                    {
                        throw new HandledException("قیمت شما بایستی از بالاترین پیشنهاد موجود بیشتر باشد.");
                    }
                }
                catch (Exception e)
                {
                    UnitOfWork.ClearChanges();
                    throw e;
                }
            }
        }


        #endregion

        #region Payment

        public long RegisterOrderWithCredit(PaymentOrderModel request)
        {
            using (var repOrder = new Repository<OrderDataEntity>(UnitOfWork))
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            using (var repPack = new Repository<PackDataEntity>(UnitOfWork))
            using (var repAuction = new Repository<AuctionDataEntity>(UnitOfWork))
            {
                try
                {
                    UpdateAuctionTask();
                    //
                    if (request.Items == null || request.Items.Count == 0 || string.IsNullOrWhiteSpace(request.Token))
                        throw new InvalidOperationException();

                    var order = new OrderDataEntity();
                    var client = repClient.Items.FirstOrDefault(c => c.Token == request.Token);

                    //
                    if (client == null)
                        throw new InvalidTokenException();

                    if (client.Blocked)
                        throw new HandledException("امکان خرید این  برای شما وجود ندارد");
                    //
                    order.ClientID = client.ID;
                    order.IsCredit = true;
                    order.CreateTime = DateTime.Now;
                    repOrder.Save(order);
                    UnitOfWork.FlushChanges();
                    long auctionPrice = 0;
                    //
                    foreach (var item in request.Items)
                    {
                        if (item.Type == OrderItemType.Sim)
                        {
                            var sim = repSim.Items.First(c => c.ID == item.ItemID);
                            if (sim.StatusID != (byte)SimPackStatus.Published)
                                throw new HandledException("امکان خرید این  گزینه وجود ندارد");
                            order.OrderItemDataEntities.Add(new OrderItemDataEntity
                            {
                                OrderID = order.ID,
                                SimID = sim.ID,
                                Price = sim.RondPrice.GetValueOrDefault(sim.Price)
                            });
                            sim.StatusID = (byte)SimPackStatus.Sold;
                        }
                        if (item.Type == OrderItemType.Pack)
                        {
                            var pack = repPack.Items.First(c => c.ID == item.ItemID);

                            if (pack.StatusID != (byte)SimPackStatus.Published)
                                throw new HandledException("امکان خرید این  گزینه وجود ندارد");
                            order.OrderItemDataEntities.Add(new OrderItemDataEntity
                            {
                                OrderID = order.ID,
                                PackID = pack.ID,
                                Price = pack.Price
                            });
                            foreach (PackSimDataEntity packSim in pack.PackSimDataEntities)
                            {
                                packSim.SimDataEntity.StatusID = (byte)SimPackStatus.Sold;
                            }
                            pack.StatusID = (byte)SimPackStatus.Sold;
                        }
                        if (item.Type == OrderItemType.Auction)
                        {
                            var auction = repAuction.Items.First(c => c.ID == item.ItemID && c.StatusID == (byte)AuctionStatus.Finished);

                            if (auction.StatusID != (byte)AuctionStatus.Finished)
                                throw new HandledException("امکان خرید این  گزینه وجود ندارد");
                            order.OrderItemDataEntities.Add(new OrderItemDataEntity
                            {
                                OrderID = order.ID,
                                AuctionID = auction.ID,
                                Price = auction.AuctionBidDataEntities.First(c => c.IsWinner).Price
                            });
                            foreach (AuctionSimDataEntity auctSim in auction.AuctionSimDataEntities)
                            {
                                auctSim.SimDataEntity.StatusID = (byte)SimPackStatus.Sold;
                            }
                            auctionPrice = auction.BasePrice;
                            auction.StatusID = (byte)AuctionStatus.Sold;
                        }
                    }
                    order.TotalPrice = order.OrderItemDataEntities.Sum(c => c.Price);
                    UnitOfWork.FlushChanges();
                    //
                    if (auctionPrice > 0 && client.BlockedCredit >= auctionPrice)
                    {
                        client.BlockedCredit -= auctionPrice;
                        client.Credit += auctionPrice;
                        UnitOfWork.FlushChanges();
                    }
                    //if ((client.Credit - client.BlockedCredit) <= order.TotalPrice)
                    if ((client.Credit) < order.TotalPrice)
                        throw new HandledException("ملبغ سفارش از مبلغ اعتبار قابل برداشت شما بیشتر است.");
                    //
                    client.Credit -= order.TotalPrice;
                    order.Status = (byte)OrderStatus.Paid;
                    order.FileID = Guid.NewGuid();
                    order.FilePassword = AcoreX.Security.Token.GenerateKeyPass().ToString();
                    //
                    UnitOfWork.SaveChanges();
                    SendPackDetails(order);
                    return order.ID;
                }
                catch (Exception ex)
                {
                    UnitOfWork.ClearChanges();
                    throw ex;
                }
            }
        }
    
        #region Zarinpal

        public PaymentResultModel RegisterOrderPayment(PaymentOrderModel request)
        {
            using (var repOrder = new Repository<OrderDataEntity>(UnitOfWork))
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            using (var repPack = new Repository<PackDataEntity>(UnitOfWork))
            using (var repAuction = new Repository<AuctionDataEntity>(UnitOfWork))
            {
                try
                {
                    UpdateAuctionTask();
                    //
                    if (request.Items == null || request.Items.Count == 0 || string.IsNullOrWhiteSpace(request.Token))
                        throw new NotFoundObjectException();

                    var order = new OrderDataEntity();
                    var client = repClient.Items.FirstOrDefault(c => c.Token == request.Token);
                    //
                    if (client == null)
                        throw new InvalidTokenException();

                    if (client.Blocked)
                        throw new HandledException("امکان خرید این  برای شما وجود ندارد");

                    //
                    order.ClientID = client.ID;
                    order.Status = (byte)OrderStatus.Unpaid;
                    order.CreateTime = DateTime.Now;
                    //
                    var payment = order.PaymentDataEntity = new PaymentDataEntity();
                    //var rnd = new Random(DateTime.Now.Millisecond);
                    //int requestID = rnd.Next(100000000, 999999999);
                    payment.SaleOrderID = DateTime.Now.Ticks;// requestID;
                    payment.CreateTime = DateTime.Now;
                    payment.Status = (byte)PaymentStatus.Sent;
                    order.IsCredit = false;
                    repOrder.Save(order);
                    UnitOfWork.FlushChanges();
                    long auctionPrice = 0;
                    //
                    foreach (var item in request.Items)
                    {
                        if (item.Type == OrderItemType.Sim)
                        {
                            var sim = repSim.Items.First(c => c.ID == item.ItemID);

                            if (sim.StatusID != (byte)SimPackStatus.Published)
                                throw new HandledException("امکان خرید این  گزینه وجود ندارد");

                            order.OrderItemDataEntities.Add(new OrderItemDataEntity
                            {
                                OrderID = order.ID,
                                SimID = sim.ID,
                                Price = sim.RondPrice.GetValueOrDefault(sim.Price)
                            });
                            sim.StatusID = (byte)SimPackStatus.Waiting;
                        }
                        if (item.Type == OrderItemType.Pack)
                        {
                            var pack = repPack.Items.First(c => c.ID == item.ItemID);
                            if (pack.StatusID != (byte)SimPackStatus.Published)
                                throw new HandledException("امکان خرید این  گزینه وجود ندارد");
                            order.OrderItemDataEntities.Add(new OrderItemDataEntity
                            {
                                OrderID = order.ID,
                                PackID = pack.ID,
                                Price = pack.Price
                            });
                            pack.StatusID = (byte)SimPackStatus.Waiting;
                        }
                        if (item.Type == OrderItemType.Auction)
                        {
                            var auction = repAuction.Items.First(c => c.ID == item.ItemID && c.StatusID == (byte)AuctionStatus.Finished);
                            if (auction.StatusID != (byte)AuctionStatus.Started)
                                throw new HandledException("امکان خرید این  گزینه وجود ندارد");
                            order.OrderItemDataEntities.Add(new OrderItemDataEntity
                            {
                                OrderID = order.ID,
                                AuctionID = auction.ID,
                                Price = auction.AuctionBidDataEntities.First(c => c.IsWinner).Price
                            });
                            auctionPrice = auction.BasePrice;
                            auction.StatusID = (byte)AuctionStatus.Waiting;
                        }
                    }
                    UnitOfWork.FlushChanges();
                    order.TotalPrice =
                    payment.Price = order.OrderItemDataEntities.Sum(c => c.Price) - auctionPrice;
                    UnitOfWork.SaveChanges();
                    //
                    var wsClient = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
                    string payRequest = null;
                    var res = wsClient.PaymentRequest(Settings.PAYMENT_USERNAME, (int)(payment.Price/10), "اِلِ سیم", client.Email, client.Mobile, String.Format(Settings.PAYMENT_ORDER_CALLBACK, payment.ID), out payRequest);
                    payment.ResultCode = String.Format("{0},{1}", res, payRequest);
                    if (res != 100)
                    {
                        payment.Status = (byte)PaymentStatus.Failed;
                        UnitOfWork.SaveChanges();
                        throw new HandledException("خطا در اتصال به درگاه بانک");
                    }
                    else
                    {

                        payment.RefID = payRequest;
                        payment.Status = (byte)PaymentStatus.Verified;
                        UnitOfWork.SaveChanges();
                        return new PaymentResultModel() { PaymentUrl = String.Format(Settings.PAYMENT_BANK_URL, payment.RefID), PaymentID = payment.RefID, OrderID = order.ID };
                    }
                }
                catch (Exception ex)
                {
                    UnitOfWork.ClearChanges();
                    throw ex;
                }
            }
        }


        public void VerifyOrderPayment(PaymentCallbackModel callback)
        {
            using (var repOrder = new Repository<OrderDataEntity>(UnitOfWork))
            using (var repPayment = new Repository<PaymentDataEntity>(UnitOfWork))
            {
                var payment = repPayment.Items.First(c => c.RefID == callback.RefId);
                var order = payment.OrderDataEntities.First();
                if (callback.ResCode != "OK")
                {
                    payment.Status = (byte)PaymentStatus.Failed;
                    payment.ResultCode = callback.ResCode;
                    RollbackOrderStatus(order);
                    UnitOfWork.SaveChanges();
                    throw new HandledException("پرداخت ناموفق");
                }
                payment.SaleOrderID = callback.SaleOrderId;
                UnitOfWork.SaveChanges();
                var wsClient = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
                long refID;
                var res = wsClient.PaymentVerification(Settings.PAYMENT_USERNAME, callback.RefId, (int)(payment.Price / 10), out refID);
                payment.SaleRefID = refID;
                payment.ResultCode = res.ToString();
                if (res == 100)
                {
                    payment.Status = (byte)PaymentStatus.Settled;
                    order.Status = (byte)OrderStatus.Paid;
                    order.FileID = Guid.NewGuid();
                    order.FilePassword = AcoreX.Security.Token.GenerateKeyPass().ToString();
                    foreach (var item in order.OrderItemDataEntities)
                    {
                        if (item.SimID.HasValue)
                            item.SimDataEntity.StatusID = (byte)SimPackStatus.Sold;

                        if (item.PackID.HasValue)
                        {
                            foreach (PackSimDataEntity packSim in item.PackDataEntity.PackSimDataEntities)
                            {
                                packSim.SimDataEntity.StatusID = (byte)SimPackStatus.Sold;
                            }
                        }

                        if (item.AuctionID.HasValue)
                        {
                            item.AuctionDataEntity.StatusID = (byte)AuctionStatus.Sold;
                            foreach (AuctionSimDataEntity auctSim in item.AuctionDataEntity.AuctionSimDataEntities)
                            {
                                auctSim.SimDataEntity.StatusID = (byte)SimPackStatus.Sold;
                            }
                            order.ClientDataEntity.BlockedCredit -= item.AuctionDataEntity.BasePrice;
                        }
                    }
                    UnitOfWork.SaveChanges();
                    SendPackDetails(order);
                }
                else
                {
                    payment.Status = (byte)PaymentStatus.Failed;
                    RollbackOrderStatus(order);
                    UnitOfWork.SaveChanges();
                    throw new HandledException("خطا در تاییدیه پرداخت");
                }
            }
        }

        #endregion

        #region Credit

        public PaymentResultModel RegisterCreditPayment(PaymentCreditModel request)
        {
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            using (var repCredit = new Repository<CreditChargeDataEntity>(UnitOfWork))
            {
                try
                {
                    if (request.Price == 0 || (string.IsNullOrWhiteSpace(request.Token)))
                        throw new NotFoundObjectException();
                    //
                    var credit = new CreditChargeDataEntity();
                    var client = repClient.Items.FirstOrDefault(c => c.Token == request.Token);
                    //
                    if (client == null)
                        throw new InvalidTokenException();

                    //
                    credit.ClientID = client.ID;
                    //
                    var payment = credit.PaymentDataEntity = new PaymentDataEntity();
                    payment.SaleOrderID = DateTime.Now.Ticks;
                    payment.CreateTime = DateTime.Now;
                    payment.Status = (byte)PaymentStatus.Sent;
                    repCredit.Save(credit);
                    UnitOfWork.FlushChanges();
                    payment.Price = request.Price;
                    UnitOfWork.SaveChanges();
                    //
                    var wsClient = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
                    string payRequest = null;
                    var res = wsClient.PaymentRequest(Settings.PAYMENT_USERNAME, (int)(payment.Price / 10), "اِلِ سیم", client.Email, client.Mobile, String.Format(Settings.PAYMENT_ACCOUNT_CALLBACK, payment.ID), out payRequest);

                    if (res != 100)
                    {
                        payment.Status = (byte)PaymentStatus.Failed;
                        payment.ResultCode = String.Format("{0},{1}", res, payRequest);
                        UnitOfWork.SaveChanges();
                        throw new HandledException("خطا در اتصال به درگاه بانک");
                    }
                    else
                    {
                        payment.ResultCode = String.Format("{0},{1}", res, payRequest);
                        payment.RefID = payRequest;
                        payment.Status = (byte)PaymentStatus.Verified;
                        UnitOfWork.SaveChanges();
                        return new PaymentResultModel() { PaymentUrl = String.Format(Settings.PAYMENT_BANK_URL, payment.RefID), PaymentID = payment.RefID, OrderID = credit.ID };
                    }
                }
                catch (Exception ex)
                {
                    UnitOfWork.ClearChanges();
                    throw ex;
                }
            }
        }


        public void VerifyCreditPayment(PaymentCallbackModel callback)
        {
            using (var repPayment = new Repository<PaymentDataEntity>(UnitOfWork))
            {
                Logger.WriteLog("Callback API: {0}-{1}", callback.ResCode, callback.RefId);
                var payment = repPayment.Items.First(c => c.RefID == callback.RefId);
                var credit = payment.CreditChargeDataEntities.First();
                if (callback.ResCode != "OK")
                {
                    payment.Status = (byte)PaymentStatus.Failed;
                    payment.ResultCode = callback.ResCode;
                    UnitOfWork.SaveChanges();
                    throw new HandledException("پرداخت ناموفق");
                }
                payment.SaleOrderID = callback.SaleOrderId;
                UnitOfWork.SaveChanges();
                var wsClient = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
                long refID;
                var res = wsClient.PaymentVerification(Settings.PAYMENT_USERNAME, callback.RefId, (int)(payment.Price / 10), out refID);
                payment.SaleRefID = refID;
                payment.ResultCode = res.ToString();
                if (res == 100)
                {
                    payment.Status = (byte)PaymentStatus.Settled;
                    credit.ClientDataEntity.LastCredit = credit.ClientDataEntity.Credit;
                    credit.ClientDataEntity.Credit += (int)credit.PaymentDataEntity.Price;
                    UnitOfWork.SaveChanges();
                }
                else
                {
                    payment.Status = (byte)PaymentStatus.Failed;
                    UnitOfWork.SaveChanges();
                    throw new HandledException("خطا در تاییدیه پرداخت");
                }
            }
        }

        #endregion

        public PaymentStatus GetPaymentStatus(string token, string paymentId)
        {
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            using (var repPayment = new Repository<PaymentDataEntity>(UnitOfWork))
            {
                var client = repClient.Items.FirstOrDefault(c => c.Token == token);
                //
                if (client == null)
                    throw new InvalidTokenException();
                var payment = repPayment.Items.FirstOrDefault(c => c.RefID == paymentId);
                if (payment == null)
                    throw new NotFoundObjectException();
                return (PaymentStatus)payment.Status;
            }
        }


        public void RollbackOrderStatus(OrderDataEntity order)
        {
            order.Status = (byte)OrderStatus.Canceled;
            foreach (OrderItemDataEntity orderItem in order.OrderItemDataEntities)
            {
                if (orderItem.SimID.HasValue)
                {
                    if (orderItem.SimDataEntity.StatusID == (byte)SimPackStatus.Waiting)
                        orderItem.SimDataEntity.StatusID = (byte)SimPackStatus.Published;
                }
                if (orderItem.PackID.HasValue)
                {
                    if (orderItem.PackDataEntity.StatusID == (byte)SimPackStatus.Waiting)
                        orderItem.PackDataEntity.StatusID = (byte)SimPackStatus.Published;
                    foreach (var sim in orderItem.PackDataEntity.PackSimDataEntities)
                    {
                        if (sim.SimDataEntity.StatusID == (byte)SimPackStatus.Waiting)
                            sim.SimDataEntity.StatusID = (byte)SimPackStatus.Published;
                    }
                }

                if (orderItem.AuctionID.HasValue)
                {
                    if (orderItem.AuctionDataEntity.StatusID == (byte)AuctionStatus.Waiting)
                        orderItem.AuctionDataEntity.StatusID = (byte)AuctionStatus.Published;
                    foreach (var sim in orderItem.AuctionDataEntity.AuctionSimDataEntities)
                    {
                        if (sim.SimDataEntity.StatusID == (byte)SimPackStatus.Waiting)
                            sim.SimDataEntity.StatusID = (byte)SimPackStatus.Published;
                    }
                }
            }
        }
        #endregion


        #region Receipt

        public ReceiptModel GetOrderReceipt(string token, long orderId)
        {
            using (var repClient = new Repository<ClientDataEntity>(UnitOfWork))
            using (var rep = new Repository<OrderDataEntity>(UnitOfWork))
            {
                var client = repClient.Items.FirstOrDefault(c => c.Token == token);
                //
                if (client == null)
                    throw new InvalidTokenException();
                var order = rep.Items.FirstOrDefault(c => c.ClientID == client.ID && c.ID == orderId && c.Status == (byte)OrderStatus.Paid);
                if (order == null)
                    throw new InvalidTokenException();

                var result = new ReceiptModel();
                result.OrderID = order.ID;
                result.ClientName = String.Format("{0} {1}", client.Firstname, client.Lastname);
                result.ClientMobile = client.Mobile;
                result.Price = order.TotalPrice;
                result.Date = order.CreateTime;
                var item = order.OrderItemDataEntities.First();
                if (item.SimID.HasValue)
                {
                    var sim = item.SimDataEntity;
                    result.SimType = (SimType)sim.TypeID;
                    result.Title = String.Format("0{0:### ### ####}", sim.Number);
                    result.Type = OrderItemType.Sim;
                    if (result.SimType == SimType.PrePaid)
                    {
                        result.ActivationCode = sim.ActivationCode;
                    }
                    else
                    {
                        result.ExpireDate = sim.ExpireDate;
                        result.ReceiptCode = sim.ReceiptCode;
                        result.ActivationCode = sim.ActivationCode;
                        result.PaymentCode = sim.PaymentCode;
                    }
                }
                if (item.PackID.HasValue)
                {
                    var pack = item.PackDataEntity;
                    result.Type = OrderItemType.Pack;
                    result.Title = pack.Title;
                    result.DownloadUrl = String.Format(Settings.PAYMENT_OREDR_DOWNLOAD_EXCEL, order.FileID);
                }
                if (item.AuctionID.HasValue)
                {
                    var auction = item.AuctionDataEntity;
                    result.Type = OrderItemType.Auction;
                    result.Title = auction.Title;
                    result.DownloadUrl = String.Format(Settings.PAYMENT_OREDR_DOWNLOAD_EXCEL, order.FileID);
                }

                return result;
            }
        }

        public byte[] GetOrderExcelFile(string id)
        {
            using (var rep = new Repository<OrderDataEntity>(UnitOfWork))
            {
                var order = rep.Items.First(c => c.FileID.ToString() == id);


                Workbook wb = new Workbook();
                Worksheet sheet = wb.Worksheets[0];


                sheet.Cells["A1"].PutValue("شماره");
                sheet.Cells["B1"].PutValue("کد فعال سازی");
                sheet.Cells["C1"].PutValue("شناسه پرداخت");
                sheet.Cells["D1"].PutValue("تاریخ تولید");
                sheet.Cells["E1"].PutValue("تاریخ انقضا");
                sheet.Cells["F1"].PutValue("شناسه قبض");
                sheet.Cells["G1"].PutValue("کد پیگیری");

                var rowIndex = 2;

                var pack = order.OrderItemDataEntities.Where(c => c.PackID.HasValue).Select(c => c.PackDataEntity).FirstOrDefault();
                if (pack != null)
                {
                    foreach (var item in pack.PackSimDataEntities)
                    {
                        var sim = item.SimDataEntity;
                        sheet.Cells["A" + rowIndex].PutValue(sim.Number);
                        sheet.Cells["B" + rowIndex].PutValue(sim.ActivationCode);
                        sheet.Cells["C" + rowIndex].PutValue(sim.PaymentCode);
                        sheet.Cells["D" + rowIndex].PutValue(sim.CreateDate.HasValue ? sim.CreateDate.Value.ToPersian().ToDateString() : "");
                        sheet.Cells["E" + rowIndex].PutValue(sim.ExpireDate.HasValue ? sim.ExpireDate.Value.ToPersian().ToDateString() : "");
                        sheet.Cells["F" + rowIndex].PutValue(sim.ReceiptCode);
                        sheet.Cells["G" + rowIndex].PutValue(sim.TraceCode);
                        rowIndex++;
                    }
                }
                //
                var auction = order.OrderItemDataEntities.Where(c => c.AuctionID.HasValue).Select(c => c.AuctionDataEntity).FirstOrDefault();
                if (auction != null)
                {
                    foreach (var item in auction.AuctionSimDataEntities)
                    {
                        var sim = item.SimDataEntity;
                        sheet.Cells["A" + rowIndex].PutValue(sim.Number);
                        sheet.Cells["B" + rowIndex].PutValue(sim.ActivationCode);
                        sheet.Cells["C" + rowIndex].PutValue(sim.PaymentCode);
                        sheet.Cells["D" + rowIndex].PutValue(sim.CreateDate.HasValue ? sim.CreateDate.Value.ToPersian().ToDateString() : "");
                        sheet.Cells["E" + rowIndex].PutValue(sim.ExpireDate.HasValue ? sim.ExpireDate.Value.ToPersian().ToDateString() : "");
                        sheet.Cells["F" + rowIndex].PutValue(sim.ReceiptCode);
                        sheet.Cells["G" + rowIndex].PutValue(sim.TraceCode);
                        rowIndex++;
                    }
                }
                //
                sheet.AutoFitColumns();
                return wb.SaveToStream().ToArray();
            }
        }

        public byte[] GetOrderProtectedZipFile(string id)
        {
            using (var rep = new Repository<OrderDataEntity>(UnitOfWork))
            {
                var order = rep.Items.First(c => c.FileID.ToString() == id);
                Ionic.Zip.ZipFile z = new Ionic.Zip.ZipFile();
                if (!string.IsNullOrWhiteSpace(order.FilePassword))
                    z.Password = order.FilePassword;
                z.AddEntry("list.xls", GetOrderExcelFile(id));
                using (var ms = new MemoryStream())
                {
                    z.Save(ms);
                    return ms.ToArray();
                }
            }
        }

        public IQueryable<OrderHistoryModel> GetOrderHistory(string token, long lastLoadedId)
        {
            using (var rep = new Repository<OrderDataEntity>(base.UnitOfWork))
            using (var repUser = new Repository<ClientDataEntity>(base.UnitOfWork))
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new InvalidTokenException();
                }
                if (!repUser.Items.Any(c => c.Token == token))
                {
                    throw new InvalidTokenException();
                }


                var query = from i in rep.Items
                            where
                            i.Status == (byte)OrderStatus.Paid &&
                            i.ClientDataEntity.Token == token &&
                            i.OrderItemDataEntities.Any()
                            orderby i.ID descending
                            select new OrderHistoryModel
                            {
                                ID = i.ID,
                                Price = i.TotalPrice,
                                Time = i.CreateTime,
                                PaymentType = i.IsCredit ? PaymentType.Credit : PaymentType.BankGateway,
                                Title = i.OrderItemDataEntities.First().SimID.HasValue ?
                                                i.OrderItemDataEntities.First().SimDataEntity.Number.ToString() :
                                                (i.OrderItemDataEntities.First().PackID.HasValue ?
                                                        i.OrderItemDataEntities.First().PackDataEntity.Title :
                                                        i.OrderItemDataEntities.First().AuctionDataEntity.Title)
                            };

                if (lastLoadedId == 0)
                    return query.Take(100);
                else
                    return query.Where(c => c.ID < lastLoadedId).Take(100);
            }
        }

        private void SendPackDetails(OrderDataEntity order)
        {
            if (order.OrderItemDataEntities.Any(c => c.PackID.HasValue) && order.FileID.HasValue)
            {
                string message = String.Format("پسورد فایل خریداری شده: {0}", order.FilePassword);
                TSMSService.tsmsServiceClient soap = new TSMSService.tsmsServiceClient();
                int[] result = soap.sendSms(Settings.SMS_USERNAME, Settings.SMS_PASSWORD, new string[] { Settings.SMS_NUMBER }, new string[] { order.ClientDataEntity.Mobile }, new string[] { message }, new string[] { }, "");
                var a = result.ToList().First();
                var fileId = order.FileID.Value.ToString();
                using (var ms = new MemoryStream(GetOrderProtectedZipFile(fileId)))
                {
                    AcoreX.Utility.MailProvider.Current.SendWithAttachmentsSync(
                    "جزئیات خرید پک سیم کارت"
                    , "با سلام\n کاربر گرامی جزئیات پک خریداری شده به پیوست خدمتتان ارسال گردید."
                    , new List<Attachment> { new Attachment(ms, "pack.zip") }
                    , order.ClientDataEntity.Email);
                }

            }
        }



        #endregion


        public void UpdateSimStatus()
        {
            using (var repSim = new Repository<OrderDataEntity>(UnitOfWork))
            {
                var query = repSim.Items.Where(c => c.Status == (int)OrderStatus.Unpaid || c.Status == (int)OrderStatus.Suspended).ToList();
                foreach (var order in query)
                {
                    if ((DateTime.Now - order.CreateTime).TotalMinutes > 10)
                        RollbackOrderStatus(order);
                }
                UnitOfWork.SaveChanges();
            }
        }
    }
}
