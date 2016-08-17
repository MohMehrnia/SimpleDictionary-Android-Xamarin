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
using SQLite.Net.Attributes;

namespace SimpleDictionary
{
    [Table("TranslateWord")]
    public class TranslateWord
    {
        [PrimaryKey]
        public string Word { get; set; }

        [NotNull]
        public string TranslatedWord { get; set; }
    }
}