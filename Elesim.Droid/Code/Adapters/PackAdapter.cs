using System;
using System.Linq;

using Elesim.Droid.Code.UI;
using AcoreX.DateTimeCalendar;
using Esunco.Models;
using Android.Views;

namespace Elesim.Droid.Code.Adapters
{
    public class PackAdapter : BaseAdapter<PackServiceModel,SimViewHolder>
    {
        public PackAdapter() : base(Resource.Layout.layout_pack_item)
        {
        }
        protected override View BindViewHolder(SimViewHolder holder, PackServiceModel model)
        {

            holder.Number.Text = String.Join("\n", model.Numbers.Take(4));
            //if (model.Numbers.Count > 5)
            //    vh.Number.Text  += "\n...";
            //
            //holder.State.Text = model.Province;
            //
            holder.Title.Text = model.Title;
            //
            holder.Time.Text = model.CreateTime.ToPersian().ToPrettyDate();
            //
            holder.Price.Text = model.Price.ToString("#,###") + " ریال";

            return holder.ItemView;
        }


    }
}