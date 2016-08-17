using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimpleDictionary
{
    public class DictionaryListViewAdapter : BaseAdapter<TranslateWord>
    {
        public List<TranslateWord> mItems;
        private Context mContext;

        public DictionaryListViewAdapter(Context context, List<TranslateWord> items)
        {
            mItems = items;
            mContext = context;
        }

        public override TranslateWord this[int position]
        {
            get
            {
                return mItems[position];
            }
        }        

        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListView_Row, null, false);
            }
            TextView txtWord = row.FindViewById<TextView>(Resource.Id.txtWord);
            TextView txtTranslatedWord = row.FindViewById<TextView>(Resource.Id.txtTranslatedWord);
            txtWord.Text = mItems[position].Word;
            txtTranslatedWord.Text= mItems[position].TranslatedWord;
            return row;
        }
    }
}