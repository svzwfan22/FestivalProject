﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Net.Http;
using Windows.Data.Json;
using Windows.ApplicationModel;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace Festival.Data
{
    /// <summary>
    /// Base class for <see cref="FestivalDataItem"/> and <see cref="FestivalDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class FestivalDataCommon : Festival.Common.BindableBase
    {
        internal  static Uri _baseUri = new Uri("ms-appx:///");

        public FestivalDataCommon(String uniqueId, String title, String shortTitle, String imagePath)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._shortTitle = shortTitle;
            this._imagePath = imagePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _shortTitle = string.Empty;
        public string ShortTitle
        {
            get { return this._shortTitle; }
            set { this.SetProperty(ref this._shortTitle, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;

        public Uri ImagePath
        {
            get
            {
                return new Uri(FestivalDataCommon._baseUri, this._imagePath); 
            }
        } 

        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(FestivalDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public string GetImageUri()
        {
            return _imagePath;
        }
    }

    public class FestivalDataItem : FestivalDataCommon
    {
        public FestivalDataItem()
            : base(String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }

        public FestivalDataItem(String uniqueId, String title, String shortTitle, String imagePath, String twitter, String facebook, String discription, ObservableCollection<string> genres, FestivalDataGroup group)
            : base(uniqueId, title, shortTitle, imagePath)
        {
            this._twitter = twitter;
            this._facebook = facebook;
            this._discription = discription;
            this._genres = genres;
            this._group = group;
        }

        private string _twitter;
        public string Twitter
        {
            get { return this._twitter; }
            set { this.SetProperty(ref this._twitter, value); }
        }

        private string _facebook;
        public string Facebook
        {
            get { return this._facebook; }
            set { this.SetProperty(ref this._facebook, value); }
        }

        private string _discription = string.Empty;
        public string Discription
        {
            get { return this._discription; }
            set { this.SetProperty(ref this._discription, value); }
        }

        private ObservableCollection<string> _genres;
        public ObservableCollection<string> Genres
        {
            get { return this._genres; }
            set { this.SetProperty(ref this._genres, value); }
        }

        private FestivalDataGroup _group;
        public FestivalDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }

        private ImageSource _tileImage;
        private string _tileImagePath;

        public Uri TileImagePath
        {
            get
            {
                return new Uri(FestivalDataCommon._baseUri, this._tileImagePath);
            }
        }

        public ImageSource TileImage
        {
            get
            {
                if (this._tileImage == null && this._tileImagePath != null)
                {
                    this._tileImage = new BitmapImage(new Uri(FestivalDataCommon._baseUri, this._tileImagePath));
                }
                return this._tileImage;
            }
            set
            {
                this._tileImagePath = null;
                this.SetProperty(ref this._tileImage, value);
            }
        }

        public void SetTileImage(String path)
        {
            this._tileImage = null;
            this._tileImagePath = path;
            this.OnPropertyChanged("TileImage");
        }
    }

    /// <summary>
    /// Recipe group data model.
    /// </summary>
    public class FestivalDataGroup : FestivalDataCommon
    {
        public FestivalDataGroup()
            : base(String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }

        public FestivalDataGroup(String uniqueId, String title, String shortTitle, String imagePath, String description)
            : base(uniqueId, title, shortTitle, imagePath)
        {
        }

        private ObservableCollection<FestivalDataItem> _items = new ObservableCollection<FestivalDataItem>();
        public ObservableCollection<FestivalDataItem> Items
        {
            get { return this._items; }
        }

        public IEnumerable<FestivalDataItem> TopItems
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            get { return this._items.Take(12); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private ImageSource _groupImage;
        private string _groupImagePath;

        public ImageSource GroupImage
        {
            get
            {
                if (this._groupImage == null && this._groupImagePath != null)
                {
                    this._groupImage = new BitmapImage(new Uri(FestivalDataCommon._baseUri, this._groupImagePath));
                }
                return this._groupImage;
            }
            set
            {
                this._groupImagePath = null;
                this.SetProperty(ref this._groupImage, value);
            }
        }

        public int RecipesCount
        {
            get
            {
                return this.Items.Count;
            }
        }

        public void SetGroupImage(String path)
        {
            this._groupImage = null;
            this._groupImagePath = path;
            this.OnPropertyChanged("GroupImage");
        }
    }

    /// <summary>
    /// Creates a collection of groups and items.
    /// </summary>
    public sealed class FestivalDataSource
    {
        //public event EventHandler RecipesLoaded;

        private static FestivalDataSource _festivalDataSource = new FestivalDataSource();

        private ObservableCollection<FestivalDataGroup> _allGroups = new ObservableCollection<FestivalDataGroup>();
        public ObservableCollection<FestivalDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<FestivalDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _festivalDataSource.AllGroups;
        }

        public static FestivalDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _festivalDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static FestivalDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _festivalDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task LoadRemoteDataAsync()
        {
            //// Retrieve recipe data from Azure
            //var client = new HttpClient();
            //client.MaxResponseContentBufferSize = 1024 * 1024; // Read up to 1 MB of data
            //var response = await client.GetAsync(new Uri("http://contosorecipes8.blob.core.windows.net/AzureRecipesRP"));
            //var result = await response.Content.ReadAsStringAsync();

            //// Parse the JSON recipe data
            //var recipes = JsonArray.Parse(result);

            //// Convert the JSON objects into RecipeDataItems and RecipeDataGroups
            //CreateFestivalAndRecipeGroups(recipes);
        }

        public static async Task LoadLocalDataAsync()
        {
            // Retrieve recipe data from Recipes.txt
            var file = await Package.Current.InstalledLocation.GetFileAsync("Data\\Festival.txt");
            var result = await FileIO.ReadTextAsync(file);

            // Parse the JSON recipe data
            var recipes = JsonArray.Parse(result);

            // Convert the JSON objects into RecipeDataItems and RecipeDataGroups
            CreateFestivalAndRecipeGroups(recipes);
        }

        private static void CreateFestivalAndRecipeGroups(JsonArray array)
        {
            foreach (var item in array)
            {
                var obj = item.GetObject();
                FestivalDataItem recipe = new FestivalDataItem();
                FestivalDataGroup group = null;

                foreach (var key in obj.Keys)
                {
                    IJsonValue val;
                    if (!obj.TryGetValue(key, out val))
                        continue;

                    switch (key)
                    {
                        case "key":
                            recipe.UniqueId = val.GetNumber().ToString();
                            break;
                        case "title":
                            recipe.Title = val.GetString();
                            break;
                        case "shortTitle":
                            recipe.ShortTitle = val.GetString();
                            break;
                        case "twitter":
                            recipe.Twitter = val.GetString();
                            break;
                        case "facebook":
                            recipe.Facebook = val.GetString();
                            break;
                        case "discription":
                            recipe.Discription = val.GetString();
                            break;
                        case "genres":
                            var genres = val.GetArray();
                            var list = (from i in genres select i.GetString()).ToList();
                            recipe.Genres = new ObservableCollection<string>(list);
                            break;
                        case "backgroundImage":
                            recipe.SetImage(val.GetString());
                            break;
                        case "tileImage":
                            recipe.SetTileImage(val.GetString());
                            break;
                        case "group":
                            var recipeGroup = val.GetObject();

                            IJsonValue groupKey;
                            if (!recipeGroup.TryGetValue("key", out groupKey))
                                continue;

                            group = _festivalDataSource.AllGroups.FirstOrDefault(c => c.UniqueId.Equals(groupKey.GetString()));

                            if (group == null)
                                group = CreateFestivalGroup(recipeGroup);

                            recipe.Group = group;
                            break;
                    }
                }

                if (group != null)
                    group.Items.Add(recipe);
            }
        }

        private static FestivalDataGroup CreateFestivalGroup(JsonObject obj)
        {
            FestivalDataGroup group = new FestivalDataGroup();

            foreach (var key in obj.Keys)
            {
                IJsonValue val;
                if (!obj.TryGetValue(key, out val))
                    continue;

                switch (key)
                {
                    case "key":
                        group.UniqueId = val.GetString();
                        break;
                    case "title":
                        group.Title = val.GetString();
                        break;
                    case "shortTitle":
                        group.ShortTitle = val.GetString();
                        break;
                    case "description":
                        group.Description = val.GetString();
                        break;
                    case "backgroundImage":
                        group.SetImage(val.GetString());
                        break;
                    case "groupImage":
                        group.SetGroupImage(val.GetString());
                        break;
                }
            }

            _festivalDataSource.AllGroups.Add(group);
            return group;
        }
    }
}