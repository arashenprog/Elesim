using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Execution;
using AutoMapper.Mappers;
using Esunco.Models;
using Esunco.Data;
using Esunco.Models.Enum;
using AcoreX.Helper;

namespace Esunco.Logics
{
    public class Mapper
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                Config(cfg);
            });
        }

        private static void Config(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<string, int>().ConvertUsing(new NumberTypeConverter());
            cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            //
            cfg.CreateMap<UserDataEntity, UserModel>()
                .ForMember(c => c.Role, opt => opt.MapFrom(d => (Role)d.RoleID))
                .ForMember(c => c.Status, opt => opt.MapFrom(d => d.Enabled ? UserStatus.Enabled : UserStatus.Disabled))
                .ForMember(c => c.Password, opt => opt.Ignore());

            cfg.CreateMap<ClientDataEntity, ClientModel>()
                .ForMember(c => c.BlackList, opt => opt.MapFrom(d => d.Blocked ? YesNo.Yes : YesNo.No))
                .ForMember(c => c.AccountType, opt => opt.MapFrom(d => (AccountType)d.AccountType));

            cfg.CreateMap<ClientModel, ClientDataEntity>()
                .ForMember(c => c.Blocked, opt => opt.MapFrom(d => d.BlackList == YesNo.Yes))
                .ForMember(c => c.AccountType, opt => opt.MapFrom(d => (byte)d.AccountType));

            cfg.CreateMap<SimDataEntity, SimModel>()
                .ForMember(c => c.Status, opt => opt.MapFrom(d => (SimPackStatus)d.StatusID))
                .ForMember(c => c.Type, opt => opt.MapFrom(d => (SimType)d.TypeID));

            cfg.CreateMap<OrderItemDataEntity, OrderItemModel>()
                .ForMember(c => c.ItemID, opt => opt.MapFrom(d => d.AuctionID.HasValue ? d.AuctionID.Value : (d.SimID.HasValue ? d.SimID.Value : d.PackID.Value)))
                .ForMember(c => c.Title, opt => opt.MapFrom(d => d.AuctionID.HasValue ? d.AuctionDataEntity.Title : (d.SimID.HasValue ? d.SimDataEntity.Number.ToString() : d.PackDataEntity.Title)))
                .ForMember(c => c.Type, opt => opt.MapFrom(d => d.AuctionID.HasValue ? OrderItemType.Auction : (d.SimID.HasValue ? OrderItemType.Sim : OrderItemType.Pack)));




            cfg.CreateMap<OrderReportViewDataEntity, OrderReportModel>()
               .ForMember(c => c.OrderStatus, opt => opt.MapFrom(d => (OrderStatus)d.OrderStatus))
               .ForMember(c => c.PaymentStatus, opt => opt.MapFrom(d => (PaymentStatus)d.PaymentStatus));


            cfg.CreateMap<PackDataEntity, PackModel>()
                .ForMember(c => c.Status, opt => opt.MapFrom(d => (SimPackStatus)d.StatusID))
                .ForMember(c => c.Type, opt => opt.MapFrom(d => (PackType)d.TypeID));

            cfg.CreateMap<AuctionDataEntity, AuctionModel>()
                .ForMember(c => c.Numbers, opt => opt.MapFrom(d => String.Join(" - ", d.AuctionSimDataEntities.Select(c => c.SimDataEntity.Number))))
                .ForMember(c => c.MaxPrice, opt => opt.MapFrom(d => d.AuctionBidDataEntities.Any() ? d.AuctionBidDataEntities.Max(c => c.Price) : 0))
                .ForMember(c => c.Status, opt => opt.MapFrom(d => (AuctionStatus)d.StatusID));



            cfg.CreateMap<OrderDataEntity, OrderHistoryModel>()
                 .ForMember(c => c.Price, opt => opt.MapFrom(d => d.TotalPrice))
                 .ForMember(c => c.PaymentType, opt => opt.MapFrom(d => d.IsCredit ? PaymentType.Credit : PaymentType.BankGateway))
                 .ForMember(c => c.Time, opt => opt.MapFrom(d => d.CreateTime));

            cfg.CreateMap<AuctionBidDataEntity, BidModel>()
                .ForMember(c => c.Client, opt => opt.MapFrom(d => AutoMapper.Mapper.Map<ClientModel>(d.ClientDataEntity)));

            cfg.CreateMap<SimDataEntity, SimServiceModel>()
              .ForMember(c => c.Number, opt => opt.MapFrom(d => d.Display != null ? d.Display : String.Format("0{0:### ### ####}", d.Number)))
              .ForMember(c => c.Price, opt => opt.MapFrom(d => d.RondPrice.HasValue ? d.RondPrice.Value : d.Price));

            cfg.CreateMap<PackDataEntity, PackServiceModel>()
                 .ForMember(c => c.Type, opt => opt.MapFrom(d => (PackType)d.TypeID))
                .ForMember(c => c.Numbers, opt => opt.MapFrom(d => d.PackSimDataEntities.Select(c => c.SimDataEntity.Display != null ? c.SimDataEntity.Display : String.Format("0{0:### ### ####}", c.SimDataEntity.Number))));

            cfg.CreateMap<AuctionDataEntity, AuctionServiceModel>()
               .ForMember(c => c.Numbers, opt => opt.MapFrom(d => d.AuctionSimDataEntities.Select(c => c.SimDataEntity.Display != null ? c.SimDataEntity.Display : String.Format("0{0:### ### ####}", c.SimDataEntity.Number))))
               .ForMember(c => c.TotalLeftSeconds, opt => opt.MapFrom(d => (long)(d.FinishTime - DateTime.Now).TotalSeconds));


        }
    }

    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return System.Convert.ToDateTime(source);
        }
    }

    public class NumberTypeConverter : ITypeConverter<string, int>
    {
        public int Convert(string source, int destination, ResolutionContext context)
        {
            return Int32.Parse(source);
        }


    }




}
