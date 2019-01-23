using AcoreX.Data.Repository;
using AcoreX.Helper;
using Esunco.Data;
using Esunco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Esunco.Models.Enum;
using System.ComponentModel.DataAnnotations;
using FileHelpers;
using Esunco.Logics.Util.FileMappers;
using AcoreX.Utility.Persian;
using AcoreX.Utility;

namespace Esunco.Logics.Contexts
{
    public class SimDisplayFilter
    {
        public SimType? SimType = null;
        public NumberType? NumberType = null;
        public SimPackStatus? Status = null;
        public string query = null;
    }



    public class SimContext : BaseContext
    {
        #region  Sim

        public void Import(string code, long buyPrice, long price, byte[] content, SimType simType)
        {

            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {

                var str = System.Text.Encoding.ASCII.GetString(content);
                if (simType == SimType.PostPaid)
                {
                    FileHelperEngine engine = new FileHelperEngine(typeof(PostpaidFileMapperFormat));
                    var list = engine.ReadString(str) as PostpaidFileMapperFormat[];
                    foreach (var item in list)
                    {
                        var sim = rep.Items.FirstOrDefault(c => c.Number == long.Parse(item.Number));
                        if (sim == null)
                            sim = new SimDataEntity();
                        //
                        sim.Number = long.Parse(item.Number);
                        sim.ActivationCode = item.ActivationCode;
                        sim.TypeID = (byte)SimType.PostPaid;
                        sim.CreateDate = PersianDate.Parse(item.CreateDate);
                        sim.ExpireDate = PersianDate.Parse(item.ExpireDate);
                        sim.ReceiptCode = item.ReceiptCode;
                        sim.PaymentCode = item.PaymentCode;
                        sim.TraceCode = item.TraceCode;
                        sim.RegisterTime = DateTime.Now;
                        sim.Price = price;
                        sim.BuyPrice = buyPrice;
                        sim.Code = code;
                        //
                        rep.Save(sim);
                    }
                }
                else
                {

                    FileHelperEngine engine = new FileHelperEngine(typeof(PrepaidFileMapperFormat));
                    var list = engine.ReadString(str) as PrepaidFileMapperFormat[];
                    foreach (var item in list)
                    {
                        var sim = rep.Items.FirstOrDefault(c => c.Number == long.Parse(item.Number));
                        if (sim == null)
                            sim = new SimDataEntity();
                        //
                        sim.Number = long.Parse(item.Number);
                        sim.ActivationCode = item.ActivationCode;
                        sim.TypeID = (byte)SimType.PrePaid;
                        sim.TraceCode = item.TraceCode;
                        sim.RegisterTime = DateTime.Now;
                        sim.Price = price;
                        sim.BuyPrice = buyPrice;
                        sim.Code = code;
                        //
                        rep.Save(sim);
                    }


                }
                UnitOfWork.SaveChanges();
            }
        }

        public PaginatedList<SimModel> GetSimList(SimDisplayFilter filter)
        {
            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {

                var normal = filter.NumberType == NumberType.Normal;
                var rond = filter.NumberType == NumberType.Rond;
                var all = !filter.NumberType.HasValue;
                var type = filter.SimType.HasValue ? (byte)filter.SimType : 99;

                var status = filter.Status.HasValue ? (byte)filter.Status : 99;

                var query = rep.Items.Where(c =>
                    !c.PackSimDataEntities.Any() &&
                    !c.AuctionSimDataEntities.Any() &&
                    (
                        (normal && !c.RondPrice.HasValue) ||
                        (rond && c.RondPrice.HasValue) ||
                        all
                    ) &&
                    (status == 99 || c.StatusID == status) &&
                    (type == 99 || c.TypeID == type)
                    );

                if (!String.IsNullOrWhiteSpace(filter.query))
                {
                    query = from i in query
                            where
                                i.Number.ToString().Contains(filter.query) ||
                                i.TraceCode.Contains(filter.query) ||
                                i.ActivationCode.Contains(filter.query) ||
                                i.PaymentCode.Contains(filter.query) ||
                                i.ReceiptCode.Contains(filter.query) ||
                                i.Code.Contains(filter.query)
                            select i;
                }


                return new PaginatedList<SimModel>(query.ProjectTo<SimModel>());
            }
        }

        public PaginatedList<SimModel> GetRondSimList()
        {
            using (var rep = new Repository<SimDataEntity>(base.UnitOfWork))
            {
                return new PaginatedList<SimModel>(rep.Items.Where(c => !c.PackSimDataEntities.Any() && c.RondPrice.HasValue).ProjectTo<SimModel>());
            }
        }

        public SimModel NewSim()
        {
            var model = new SimModel();
            return model;
        }

        public SimModel SaveSim(SimModel model)
        {
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            {
                var entity = repSim.Items.FirstOrDefault(c => c.ID == model.ID);
                if (entity == null)
                {
                    entity = new SimDataEntity();
                }
                entity.Code = model.Code;
                entity.Price = model.Price;
                entity.StatusID = (byte)model.Status;
                entity.RondPrice = model.RondPrice;
                entity.Display = model.Display;
                repSim.Save(entity);
                UnitOfWork.SaveChanges();
                return FindSim(entity.ID);
            }
        }

        public SimModel FindSim(long id)
        {
            return base.Find<SimModel, SimDataEntity>(id);
        }

        public void DeleteSim(params long[] list)
        {
            base.Delete<SimDataEntity>(list);
        }


        public void MarkAsRondSims(long price, long[] list)
        {
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            {

                var query = repSim.Items.Where(c => list.Contains(c.ID));
                query.ForEach(c =>
                    {
                        c.RondPrice = price;
                    });

                UnitOfWork.SaveChanges();
            }
        }

        public void SetPrice(long price, long[] list)
        {
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            {

                var query = repSim.Items.Where(c => list.Contains(c.ID));
                query.ForEach(c =>
                {
                    c.Price = price;
                });

                UnitOfWork.SaveChanges();
            }
        }

        public void UndoRondSims(long[] list)
        {
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            {

                var query = repSim.Items.Where(c => list.Contains(c.ID));
                query.ForEach(c =>
                {
                    c.RondPrice = null;
                });

                UnitOfWork.SaveChanges();
            }
        }

        public void MarkAsPublished(long[] list)
        {
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            {

                var query = repSim.Items.Where(c => list.Contains(c.ID));
                query.ForEach(c =>
                {
                    if (c.StatusID == (byte)SimPackStatus.New)
                        c.StatusID = (byte)SimPackStatus.Published;
                });

                UnitOfWork.SaveChanges();
            }
        }

        public void MarkAsUnpublished(long[] list)
        {
            using (var repSim = new Repository<SimDataEntity>(UnitOfWork))
            {

                var query = repSim.Items.Where(c => list.Contains(c.ID));
                query.ForEach(c =>
                {
                    if (c.StatusID == (byte)SimPackStatus.Published)
                        c.StatusID = (byte)SimPackStatus.New;
                });

                UnitOfWork.SaveChanges();
            }
        }







        #endregion


        #region  Pack

        public PaginatedList<PackModel> GetPackList()
        {
            using (var rep = new Repository<PackDataEntity>(base.UnitOfWork))
            {
                //var list = rep.Items.Where(c => !c.OrderItemDataEntities.Any(d => d.OrderDataEntity.Status == (byte)OrderStatus.Paid));
                var list = rep.Items.OrderByDescending(c=>c.CreateTime);
                return new PaginatedList<PackModel>(list.ProjectTo<PackModel>());
            }
        }


        public List<PackModel> GetNewPackList()
        {
            using (var rep = new Repository<PackDataEntity>(base.UnitOfWork))
            {
                var list = rep.Items.Where(c => c.StatusID == (byte)SimPackStatus.New);
                return new List<PackModel>(list.ProjectTo<PackModel>());
            }
        }


        public PackModel AddNewPack(string title, PackType type, string code, long[] list)
        {
            using (var repPack = new Repository<PackDataEntity>(UnitOfWork))
            using (var repPackSim = new Repository<PackSimDataEntity>(UnitOfWork))
            {

                var pack = new PackDataEntity();
                pack.CreateTime = DateTime.Now;
                pack.Title = title;
                pack.TypeID = (byte)type;
                pack.Code = code;
                repPack.Save(pack);
                UnitOfWork.FlushChanges();
                long price = 0;
                foreach (var item in list)
                {
                    price += FindSim(item).Price;
                    var sim = new PackSimDataEntity { PackageID = pack.ID, SimID = item };
                    repPackSim.Save(sim);
                }
                UnitOfWork.FlushChanges();
                pack.Price = price;
                UnitOfWork.SaveChanges();
                return FindPack(pack.ID);
            }
        }

        public PackModel AddToPack(long packId, long[] list)
        {
            using (var repPack = new Repository<PackDataEntity>(UnitOfWork))
            using (var repPackSim = new Repository<PackSimDataEntity>(UnitOfWork))
            {
                var pack = Find<PackDataEntity>(packId);
                long price = pack.Price;
                foreach (var item in list)
                {
                    price += FindSim(item).Price;
                    var sim = new PackSimDataEntity { PackageID = pack.ID, SimID = item };
                    repPackSim.Save(sim);
                }
                UnitOfWork.FlushChanges();
                pack.Price = price;
                UnitOfWork.SaveChanges();
                return FindPack(pack.ID);
            }
        }

        public PackModel SavePack(PackModel model, long[] deleteList)
        {
            using (var repPack = new Repository<PackDataEntity>(UnitOfWork))
            using (var repSim = new Repository<PackSimDataEntity>(UnitOfWork))
            {

                var entity = model.MapTo<PackDataEntity>();
                entity.TypeID = (byte)model.Type;
                entity.Price = model.Price;
                repPack.Save(entity);
                UnitOfWork.FlushChanges();
                foreach (var item in deleteList)
                {
                    repSim.Delete(c => c.SimID == item);
                }
                UnitOfWork.FlushChanges();
                UnitOfWork.SaveChanges();
                return FindPack(entity.ID);
            }
        }

        public PackModel FindPack(long id)
        {
            return base.Find<PackModel, PackDataEntity>(id);
        }

        public PaginatedList<SimModel> GetPackItemsList(long pakId)
        {
            using (var rep = new Repository<PackSimDataEntity>(base.UnitOfWork))
            {
                return new PaginatedList<SimModel>(rep.Items.Where(c => c.PackageID == pakId).Select(c => c.SimDataEntity).ProjectTo<SimModel>());
            }
        }

        public void DeletePack(params long[] list)
        {

            using (var rep = new Repository<PackDataEntity>(this.UnitOfWork))
            {
                foreach (var id in list)
                {
                    var item = Find<PackDataEntity>(id);
                    if (item == null)
                        continue;
                    if (item.OrderItemDataEntities.Any())
                    {
                        throw new AcoreX.Utility.HandledException(String.Format("امکان حذف پک '{0}' وجود ندارد", item.Title));
                    }

                    rep.Delete(item);
                }
                UnitOfWork.SaveChanges();
            }
        }

        public void PublishPack(long id)
        {
            using (var repPack = new Repository<PackDataEntity>(UnitOfWork))
            {
                var aution = repPack.Items.FirstOrDefault(c => c.ID == id);
                if (aution == null)
                    throw new AcoreX.Utility.NotFoundObjectException();
                if (!aution.PackSimDataEntities.Any())
                    throw new AcoreX.Utility.HandledException("پک بدون گزینه ای برای خرید است");

                if (aution.StatusID == (byte)AuctionStatus.Sold)
                    throw new HandledException("این پک فروش رفته است امکان تغییر وجود ندارد");

                aution.StatusID = (byte)AuctionStatus.Published;
                UnitOfWork.SaveChanges();
            }
        }

        #endregion


        #region  Auction



        public AuctionModel AddNewAuction(string title, long price, DateTime startTime, DateTime finishTime, long[] list)
        {
            using (var repAuc = new Repository<AuctionDataEntity>(UnitOfWork))
            using (var repSim = new Repository<AuctionSimDataEntity>(UnitOfWork))
            {

                var auction = new AuctionDataEntity
                {
                    Title = title,
                    BasePrice = price,
                    StartTime = startTime,
                    FinishTime = finishTime,
                };
                repAuc.Save(auction);
                UnitOfWork.FlushChanges();

                foreach (var c in list)
                {
                    repSim.Save(new AuctionSimDataEntity { AuctionID = auction.ID, SimID = c });
                }

                UnitOfWork.SaveChanges();
                return FindAuction(auction.ID);
            }
        }
        public PaginatedList<AuctionModel> GetAuctionList()
        {
            using (var rep = new Repository<AuctionDataEntity>(base.UnitOfWork))
            {
                return new PaginatedList<AuctionModel>(rep.Items.ProjectTo<AuctionModel>());
            }
        }

        public AuctionModel NewAuction()
        {
            var model = new AuctionModel();
            return model;
        }

        public AuctionModel SaveAuction(AuctionModel model)
        {
            using (var repAuction = new Repository<AuctionDataEntity>(UnitOfWork))
            {

                if (model.Status == AuctionStatus.Sold)
                    throw new HandledException("این مزایده فروش رفته است امکان تغییر وجود ندارد");

                var entity = model.MapTo<AuctionDataEntity>();
                repAuction.Save(entity);
                UnitOfWork.SaveChanges();
                return FindAuction(entity.ID);
            }
        }

        public void PublishAuction(long id)
        {
            using (var repAuction = new Repository<AuctionDataEntity>(UnitOfWork))
            {
                var aution = repAuction.Items.FirstOrDefault(c => c.ID == id);
                if (aution == null)
                    throw new AcoreX.Utility.NotFoundObjectException();
                if (!aution.AuctionSimDataEntities.Any())
                    throw new AcoreX.Utility.HandledException("مزایده بدون گزینه ای برای خرید است");

                if (aution.StatusID == (byte)AuctionStatus.Sold)
                    throw new HandledException("این مزایده فروش رفته است امکان تغییر وجود ندارد");

                if (aution.FinishTime < DateTime.Now)
                    throw new AcoreX.Utility.HandledException("تاریخ مزایده صحیح نمی باشد");

                aution.StatusID = (byte)AuctionStatus.Published;
                if (aution.StartTime <= DateTime.Now)
                    aution.StatusID = (byte)AuctionStatus.Started;


                UnitOfWork.SaveChanges();
            }
        }

        //public void CancelAuction(long id)
        //{
        //    using (var repAuction = new Repository<AuctionDataEntity>(UnitOfWork))
        //    {
        //        var aution = repAuction.Items.FirstOrDefault(c => c.ID == id);
        //        if (aution == null)
        //            throw new AcoreX.Utility.NotFoundObjectException();
        //        if (!aution.AuctionSimDataEntities.Any())
        //            throw new AcoreX.Utility.HandledException("مزایده بدون گزینه ای برای خرید است");
        //        aution.StatusID = (byte)AuctionStatus.Published;
        //        UnitOfWork.SaveChanges();
        //    }
        //}

        public AuctionModel FindAuction(long id)
        {
            return base.Find<AuctionModel, AuctionDataEntity>(id);
        }

        public void DeleteAuction(params long[] list)
        {
            using (var rep = new Repository<AuctionDataEntity>(this.UnitOfWork))
            {
                foreach (var id in list)
                {
                    var auction = Find<AuctionDataEntity>(id);
                    if (auction == null)
                        continue;
                    if (auction.AuctionBidDataEntities.Any())
                    {
                        throw new AcoreX.Utility.HandledException(String.Format("امکان حذف مزایده '{0}' وجود ندارد", auction.Title));
                    }

                    rep.Delete(auction);
                }
                UnitOfWork.SaveChanges();
            }
        }


        public PaginatedList<BidModel> GetAuctionBidList(long id)
        {
            using (var rep = new Repository<AuctionBidDataEntity>(base.UnitOfWork))
            {
                return new PaginatedList<BidModel>(rep.Items.Where(c => c.AuctionID == id).ProjectTo<BidModel>());
            }
        }

        #endregion

    }
}
