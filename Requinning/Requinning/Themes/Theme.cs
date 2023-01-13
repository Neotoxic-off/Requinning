using Newtonsoft.Json;
using Requinning.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Requinning.Themes
{
    public class Theme
    {
        private string path = "themes.json";
        private Models.ThemeModel.Root themeModel { get; set; }


        public Theme()
        {
            Load();
        }

        private void Load()
        {
            if (File.Exists(path) == true)
            {
                themeModel = JsonConvert.DeserializeObject<Models.ThemeModel.Root>(File.ReadAllText(path));
                Apply(themeModel.selected);
            }
        }

        public List<Models.ThemeModel.Theme> GetThemes()
        {
            if (themeModel != null)
                return (themeModel.themes);
            return (null);
        }

        private void Save(string theme)
        {
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(themeModel));
        }

        public void Apply(string theme)
        {
            Dictionary<string, string> Brushes = null;

            foreach (ThemeModel.Theme _theme in themeModel.themes)
            {
                if (_theme.name == $"{theme}")
                {
                    themeModel.selected = theme;
                    Brushes = new Dictionary<string, string>()
                    {
                        { "Background", _theme.Background},
                        { "ContentBackground", _theme.ContentBackground},
                        { "Normal", _theme.Normal},
                        { "Title", _theme.Title},
                        { "Action", _theme.Action},
                        { "Hover", _theme.Hover},
                        { "Disabled", _theme.Disabled}

                    };
                    foreach (string key in Brushes.Keys)
                    {
                        Application.Current.Resources[key] = new BrushConverter().ConvertFrom(Brushes[key]);
                    }
                }
            }
            Save(theme);
        }
    }
}
