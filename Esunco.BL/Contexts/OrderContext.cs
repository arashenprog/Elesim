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
using AcoreX.Security;

namespace Esunco.Logics.Contexts
{


    public enum OrderDisplayFilter
    {
        [Display(Name = "همه")]
        All = 0,
        [Display(Name = "پرداخت شده")]
        Paid = 1,
        [Display(Name = "پرداخت نشده")]
        Unpaid = 2
    }

    public class OrderContext : BaseContext
    {


        #region  Order

        public PaginatedList<OrderReportModel> GetOrderList(DateTime startDate, DateTime finishDate, OrderDisplayFilter filter)
        {
            using (var rep = new Repository<OrderReportViewDataEntity>(base.UnitOfWork))
            using (var repItems = new Repository<OrderItemDataEntity>(base.UnitOfWork))
            {

                var all = filter == OrderDisplayFilter.All;
                var paid = filter == OrderDisplayFilter.Paid;
                var unpaid = filter == OrderDisplayFilter.Unpaid;
                var data = from c in rep.Items
                           where
                          (all ||
                              (paid && c.OrderStatus == (byte)OrderStatus.Paid) ||
                              (unpaid && c.OrderStatus != (byte)OrderStatus.Paid)
                          ) &&
                          c.OrderTime.Date >= startDate.Date && c.OrderTime.Date <= finishDate.Date
                           orderby c.ID
                           select new OrderReportModel
                           {
                               ID = c.ID,
                               ClientFullname = c.ClientFullname,
                               ClientMobile = c.ClientMobile,
                               ClientNationalCode = c.ClientNationalCode,
                               ClientOfficeCode = c.ClientOfficeCode,
                               OrderStatus = (OrderStatus)c.OrderStatus,
                               Price = c.Price,
                               OrderTime = c.OrderTime,
                               PaymentStatus = c.PaymentStatus.HasValue ? (PaymentStatus)c.PaymentStatus.Value : PaymentStatus.Settled,
                               RefID = c.RefID,
                               ResultCode = c.ResultCode,
                               SaleOrderID = c.SaleOrderID,
                               SaleRefID = c.SaleRefID
                           };

                var d2 = new PaginatedList<OrderReportModel>(data);

                d2.ForEach(c =>
                 {
                     c.Items = repItems.Items.Where(d => d.OrderID == c.ID).ProjectTo<OrderItemModel>().ToList();
                 });


                return d2;
            }
        }

        #endregion




    }
}
