using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.IO;
using System.Data.SqlClient;
using Xamarin.Essentials;

namespace Пароид.Models
{
    public class Application : ObservableCollection <Application>
    {
        public int AppId { get; set; }

        public string Name { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] File { get; set; } = null!;

        public int Rating { get; set; }

        public int Cost { get; set; }

        public string DateAdded { get
            {
                string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
                string databaseTable = "Wanted";
                string selectQuery = String.Format("SELECT * FROM {0} WHERE Id_User = {1} AND Id_App = {2}", databaseTable, Preferences.Get("currentUserId", "0"), this.AppId.ToString()) ;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteReader();
                    //check if account exists
                    var exists = result.HasRows;
                    while (result.Read())
                    {
                        var wanted = new Wanted();
                        wanted.WantedId = int.Parse(result[0].ToString());
                        wanted.IdUser = int.Parse(result[1].ToString());
                        wanted.IdApp = int.Parse(result[2].ToString());
                        wanted.DateAdded = DateTime.Parse(result[3].ToString());
                        return "Дата добавления " + wanted.DateAdded.Date.ToShortDateString();
                    }
                }
                return null;
            } }

        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();

        public virtual ICollection<Wanted> Wanteds { get; set; } = new List<Wanted>();

        public ImageSource Photo { get
            {
                byte[] imageAsBytes = (byte[])this.Picture;
                var stream = new MemoryStream(imageAsBytes);
                //return ImageSource.FromStream(() => new MemoryStream(Picture));
                return ImageSource.FromStream(() => stream);
            } }
    }
}
