using AcoreX.Data.Repository;
using Aspose.Cells;
using Esunco.Data;
using Esunco.Models;
using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Logics.Contexts
{
    public enum PaymentListFilter
    {
        [Display(Name = "همه")]
        All = 0,
        [Display(Name = "موفق")]
        Succeed = 1,
        [Display(Name = "نا موفق")]
        Failed = 2
    }
    public class ReportContext : BaseContext
    {

        public List<PaymentReportModel> GetPaymentList(DateTime start, DateTime finish, PaymentListFilter filter)
        {
            using (var repOrder = new Repository<OrderDataEntity>(UnitOfWork))
            using (var repCharge = new Repository<CreditChargeDataEntity>(UnitOfWork))
            {
                var succeed = filter == PaymentListFilter.Succeed;
                var failed = filter == PaymentListFilter.Failed;
                var all = filter == PaymentListFilter.All;

                var list = new List<PaymentReportModel>();
                var query1 = from i in repOrder.Items
                             where
                             i.PaymentID.HasValue &&
                             i.PaymentDataEntity.CreateTime.Date >= start &&
                             i.PaymentDataEntity.CreateTime.Date <= finish.Date &&
                             (i.PaymentDataEntity.Status == (byte)PaymentStatus.Settled && succeed ||
                              i.PaymentDataEntity.Status != (byte)PaymentStatus.Settled && failed ||
                              all
                             )
                             select new PaymentReportModel
                             {
                                 ID = i.ID,
                                 ClientName = String.Format("{0} {1}", i.ClientDataEntity.Firstname, i.ClientDataEntity.Lastname),
                                 ClientMobile = i.ClientDataEntity.Mobile,
                                 DateTime = i.PaymentDataEntity.CreateTime,
                                 Price = i.PaymentDataEntity.Price,
                                 RefID = i.PaymentDataEntity.RefID,
                                 SaleOrderID = i.PaymentDataEntity.SaleOrderID,
                                 SaleRefID = i.PaymentDataEntity.SaleRefID,
                                 PaymentStatus = (PaymentStatus)i.PaymentDataEntity.Status,
                                 Type = "خرید محصول"
                             };

                list.AddRange(query1.ToList());

                var query2 = from i in repCharge.Items
                             where
                             i.PaymentDataEntity.CreateTime.Date >= start &&
                             i.PaymentDataEntity.CreateTime.Date <= finish.Date &&
                             (i.PaymentDataEntity.Status == (byte)PaymentStatus.Settled && succeed ||
                              i.PaymentDataEntity.Status != (byte)PaymentStatus.Settled && failed ||
                              all
                             )
                             select new PaymentReportModel
                             {
                                 ID = i.ID,
                                 ClientName = String.Format("{0} {1}", i.ClientDataEntity.Firstname, i.ClientDataEntity.Lastname),
                                 ClientMobile = i.ClientDataEntity.Mobile,
                                 DateTime = i.PaymentDataEntity.CreateTime,
                                 Price = i.PaymentDataEntity.Price,
                                 RefID = i.PaymentDataEntity.RefID,
                                 SaleOrderID = i.PaymentDataEntity.SaleOrderID,
                                 SaleRefID = i.PaymentDataEntity.SaleRefID,
                                 PaymentStatus = (PaymentStatus)i.PaymentDataEntity.Status,
                                 Type = "شارژ حساب"
                             };

                list.AddRange(query2.ToList());

                return list.OrderBy(c => c.DateTime).ToList();

            }
        }

        public List<ClientOrderViewModel> GetClientOrderList(DateTime start, DateTime finish)
        {
            var p1 = new System.Data.SqlClient.SqlParameter("StartDate", start.Date);
            var p2 = new System.Data.SqlClient.SqlParameter("FinishDate", finish.Date);
            return UnitOfWork.Context.GetFunctionResult<ClientOrderViewModel>("[dbo].[ClientOrderView]", new System.Data.Common.DbParameter[] { p1, p2 }).OrderByDescending(c => c.Total).ToList();
        }

        public List<ProfitViewModel> GetProfitList(DateTime start, DateTime finish)
        {
            var p1 = new System.Data.SqlClient.SqlParameter("StartDate", start.Date);
            var p2 = new System.Data.SqlClient.SqlParameter("FinishDate", finish.Date);
            return UnitOfWork.Context.GetFunctionResult<ProfitViewModel>("[dbo].[GetTotalProfit]", new System.Data.Common.DbParameter[] { p1, p2 }).OrderByDescending(c => c.Profit).ToList();
        }

        public List<SimCodeViewModel> GetSimCodeList(DateTime start, DateTime finish)
        {
            var p1 = new System.Data.SqlClient.SqlParameter("StartDate", start.Date);
            var p2 = new System.Data.SqlClient.SqlParameter("FinishDate", finish.Date);
            return UnitOfWork.Context.GetFunctionResult<SimCodeViewModel>("[dbo].[GetSimCodeView]", new System.Data.Common.DbParameter[] { p1, p2 }).OrderBy(c => c.Number).ThenBy(c => c.Code).ToList();
        }

    }
}
