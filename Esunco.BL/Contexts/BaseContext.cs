using AcoreX.Data.Repository;
using AcoreX.Utility;
using Esunco.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Logics.Contexts
{
    public abstract class BaseContext : IDisposable
    {

        bool _isUOFShared = false;

        protected IUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        public BaseContext()
        {
            this.UnitOfWork = new UnitOfWork(new TKDataContext());
        }

        public BaseContext(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            this._isUOFShared = true;
        }

        public void Dispose()
        {
            if (!this._isUOFShared)
            {
                this.UnitOfWork = null;
            }
        }

        protected TModel Find<TModel, TEntity>(long id) where TEntity : class, IDataEntity, new()
        {
            using (var rep = new Repository<TEntity>(this.UnitOfWork))
            {
                return AutoMapper.Mapper.Map<TModel>(rep.Items.FirstOrDefault(c => c.ID == id));
            }
        }

        protected TModel Find<TModel, TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IDataEntity, new()
        {
            using (var rep = new Repository<TEntity>(this.UnitOfWork))
            {
                return AutoMapper.Mapper.Map<TModel>(rep.Items.FirstOrDefault(predicate));
            }
        }

        protected TEntity Find<TEntity>(long id) where TEntity : class, IDataEntity, new()
        {
            using (var rep = new Repository<TEntity>(this.UnitOfWork))
            {
                var item = rep.Items.FirstOrDefault(c => c.ID == id);
                if (item == null)
                    throw new NotFoundObjectException();
                return item;
            }
        }

        internal protected void Delete<TEntity>(params long[] list) where TEntity : class, IDataEntity, new()
        {
            using (var rep = new Repository<TEntity>(this.UnitOfWork))
            {
                rep.Delete(c => list.Contains(c.ID));
                UnitOfWork.SaveChanges();
            }
        }

        internal protected T SaveEntity<T, TEntity>(T model, Action<T, TEntity> action = null, bool saveChanges = true)
           where TEntity : class, new()
        {
            using (var rep = new Repository<TEntity>(this.UnitOfWork))
            {
                var item = AutoMapper.Mapper.Map<TEntity>(model);
                var entity = (dynamic)item;

                if (action != null)
                    action(model, item);

                rep.Save(item);

                UnitOfWork.FlushChanges();
                if (saveChanges)
                {
                    UnitOfWork.SaveChanges();
                }
                return AutoMapper.Mapper.Map(item, model);
            }
        }


        protected void SendSMS(string message, string number)
        {
            TSMSService.tsmsServiceClient soap = new TSMSService.tsmsServiceClient();
            int[] result = soap.sendSms(Settings.SMS_USERNAME, Settings.SMS_PASSWORD, new string[] { Settings.SMS_NUMBER }, new string[] { number }, new string[] { message }, new string[] { }, "");
        }

    }
}
