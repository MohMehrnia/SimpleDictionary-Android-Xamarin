using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;

namespace SimpleDictionary
{
    [Activity(Label = "Dictionary", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
    public class MainActivity : Activity
    {
        private List<TranslateWord> items;
        private LinearLayout mContainer;
        private ListView lvMain;
        private EditText txtSearch;
        private DictionaryListViewAdapter adapter;
        private SQLiteConnection dbConn;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);            
            SetContentView(Resource.Layout.Main);

            string dbName = "Dictionary.sqlite";
            string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, dbName);
            
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open("Dictionary")))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }

            using (dbConn = new SQLiteConnection(new SQLitePlatformAndroid(), dbPath))
            {
                items = dbConn.Table<TranslateWord>().ToList<TranslateWord>();
            }

            mContainer = FindViewById<LinearLayout>(Resource.Id.txtMainContainer);            

            lvMain = FindViewById<ListView>(Resource.Id.lvMain);
            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);
            txtSearch.TextChanged += InputSearchOnTextChanged;
        
            adapter = new DictionaryListViewAdapter(this, items);
            lvMain.Adapter = adapter;
            lvMain.FastScrollEnabled = true;
            lvMain.TextFilterEnabled = true;
        }
        
        private void InputSearchOnTextChanged(object sender, Android.Text.TextChangedEventArgs args)
        {
            List<TranslateWord> FilterResult = items.Where(q => q.Word.ToLower().StartsWith(txtSearch.Text.ToLower())).ToList<TranslateWord>();
            adapter = new DictionaryListViewAdapter(this, FilterResult);
            lvMain.Adapter = adapter;
        }
    }
}

