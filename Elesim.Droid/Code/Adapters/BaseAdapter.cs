using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Support.V7.Widget;
using Elesim.Droid.Code.UI;
using Esunco.Models;
using static Android.Support.V7.Widget.RecyclerView;
using System;
using AcoreX.Xamarin.Droid;

namespace Elesim.Droid.Code.Adapters
{
    public class ItemClickEventArgs<T> : EventArgs
    {
        public T Item { get; set; }
    }

    public delegate void ItemClickEventHandler<T>(object sender, ItemClickEventArgs<T> e);

    public abstract class BaseAdapter : RecyclerView.Adapter
    {
        public abstract void Clear();
    }

    public abstract class BaseAdapter<T, K> : BaseAdapter
        where T : BaseModel
        where K : ViewHolder
    {


        public bool AddItemViewClickEvent { get; set; }

        public event ItemClickEventHandler<T> OnItemClick;

        int layoutId;
        private List<T> Items { get; set; }
        public BaseAdapter(int layoutId, List<T> items)
            : this(layoutId)
        {
            AddItems(items);
        }

        public BaseAdapter(int layoutId)
        {
            this.layoutId = layoutId;
            this.Items = new List<T>();
            this.AddItemViewClickEvent = true;
        }

        public void AddItems(IEnumerable<T> items)
        {
            this.Items.AddRange(items.Where(c => !this.Items.Any(d => d.ID == c.ID)));
        }

        public override void Clear()
        {
            this.Items.Clear();
            NotifyDataSetChanged();
        }
        public override int ItemCount
        {
            get { return this.Items.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            try
            {
                var model = Items[position];
                var view = BindViewHolder(holder as K, model);
                view.Tag = new JavaObjectWrapper<T>() { Value = model };
                if (!view.HasOnClickListeners)
                {
                    view.Click += (sender, e) =>
                    {
                        var selectedModel = view.Tag as JavaObjectWrapper<T>;
                        OnItemClick?.Invoke(this, new ItemClickEventArgs<T> { Item = selectedModel.Value });
                    };
                };
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        protected abstract View BindViewHolder(K holder, T model);

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(this.layoutId, parent, false);
            return (K)Activator.CreateInstance(typeof(K), itemView);
        }
    }
}