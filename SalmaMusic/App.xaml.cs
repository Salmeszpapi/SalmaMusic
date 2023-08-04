using SalmaMusic.DbHelper;
using SalmaMusic.Model;
using SalmaMusic.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Serialization;

namespace SalmaMusic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainSiteModel mainSiteModel = new MainSiteModel();
            mainSiteModel.GetWindow().Show();

            using (var db = new MyDataProvider())
            {
                db.Database.EnsureCreated();
                var musicContent = db.Music.ToList();

                // Convert data to XML using XmlSerializer
                var serializer = new XmlSerializer(typeof(List<Music>));
                using (var writer = new StreamWriter("A:\\products.xml"))
                {
                    serializer.Serialize(writer, musicContent);
                }
            }

        }
    }
}
