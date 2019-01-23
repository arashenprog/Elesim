
using Elesim.Droid.Code.UI;
using AcoreX.DateTimeCalendar;
using Esunco.Models;
using Android.Views;

namespace Elesim.Droid.Code.Adapters
{
    public class ReqularSimAdapter : BaseAdapter<SimServiceModel, SimViewHolder>
    {
        public ReqularSimAdapter(int layoutId) : base(layoutId)
        {
        }
        public ReqularSimAdapter() : base(Resource.Layout.layout_rond_sim)
        {
        }
        protected override View BindViewHolder(SimViewHolder holder, SimServiceModel model)
        {
            holder.Number.Text = model.Number;
            //
            //holder.State.Text = model.Province;
            //
            holder.Time.Text = model.RegisterTime.ToPersian().ToPrettyDate();
            //
            holder.Price.Text = model.Price.ToString("#,###") + " ریال";

            return holder.ItemView;
        }
    }

    public class RondSimAdapter : ReqularSimAdapter
    {
        public RondSimAdapter() : base(Resource.Layout.layout_rond_sim)
        {
        }
       
    }

    public class SearchSimAdapter : ReqularSimAdapter
    {
        public SearchSimAdapter() : base(Resource.Layout.layout_rond_sim)
        {
        }

    }


}